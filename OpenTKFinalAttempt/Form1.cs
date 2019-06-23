using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace OpenTKFinalAttempt
{
    public partial class Form1 : Form
    {
        bool loaded = false;

        /// <summary>
        /// ID of our program on the graphics card
        /// </summary>
        int pgmID;
        /// <summary>
        /// Address of the vertex shader
        /// </summary>
        int vsID;
        /// <summary>
        /// Address of the fragment shader
        /// </summary>
        int fsID;
        /// <summary>
        /// Address of the color parameter
        /// </summary>
        int attribute_vcol;
        /// <summary>
        /// Address of the position parameter
        /// </summary>
        int attribute_vpos;
        /// <summary>
        /// Address of the modelview matrix uniform
        /// </summary>
        int uniform_mview;

        /// <summary>
        /// Address of the Vertex Buffer Object for our position parameter
        /// </summary>
        int vbo_position;

        /// <summary>
        /// Address of the Vertex Buffer Object for our color parameter
        /// </summary>
        int vbo_color;

        /// <summary>
        /// Address of the Vertex Buffer Object for our modelview matrix
        /// </summary>
        int vbo_mview;
        /// <summary>
        /// Index Buffer Object
        /// </summary>
        int ibo_elements;
        /// <summary>
        /// Array of our vertex positions
        /// </summary>
        Vector3[] vertdata;
        /// <summary>
        /// Array of our vertex colors
        /// </summary>
        Vector3[] coldata;
        /// <summary>
        /// Array of our indices
        /// </summary>
        int[] indicedata;
        /// <summary>
        /// Current time, for animation
        /// </summary>
        float time = 0.0f;
        /// <summary>
        /// List of all the Volumes to be drawn
        /// </summary>
        List<Volume> objects = new List<Volume>();
        /// <summary>
        /// The camera for the scene
        /// </summary>
        Camera cam = new Camera();
        /// <summary>
        /// The last position of the mouse
        /// </summary>
        Vector2 lastMousePos = new Vector2(0,0);

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {

            colorControlForSelect1.insertAllColors(new List<Color>(new Color[] {
                Color.FromArgb(255,0,0) ,
                Color.FromArgb(0,255,0),
                Color.FromArgb(0,0,255),
                Color.FromArgb(255,0,0),
                Color.FromArgb(0,255,255),
                Color.FromArgb(255,0,255) })
                );
            colorControlForSelect1.eventForColorChangedHandler += ColorControlForSelect1_eventForColorChangedHandler;
            
            GL.ClearColor(Color.SkyBlue);
            initProgram();
            loaded = true;
        }

        private void ColorControlForSelect1_eventForColorChangedHandler(Color in_Color, int index)
        {
            (objects[0] as Cube).setColorOfCube(new Vector3(in_Color.R / 255.0f, in_Color.G / 255.0f, in_Color.B / 255.0f), Cube.convertIndexToCubeside(index));
            this.glControl1.Invalidate();
        }

        void initProgram()
        {
            lastMousePos = new Vector2(0, 0);
            cam.MouseSensitivity = 0.0025f;

            objects.Add(new Cube());
            objects.Add(new Cube());

            /** In this function, we'll start with a call to the GL.CreateProgram() function,
             * which returns the ID for a new program object, which we'll store in pgmID. */
            pgmID = GL.CreateProgram();

            loadShader("vs.glsl", ShaderType.VertexShader, pgmID, out vsID);
            loadShader("fs.glsl", ShaderType.FragmentShader, pgmID, out fsID);

            /** Now that the shaders are added, the program needs to be linked.
             * Like C code, the code is first compiled, then linked, so that it goes
             * from human-readable code to the machine language needed. */
            GL.LinkProgram(pgmID);
            Console.WriteLine(GL.GetProgramInfoLog(pgmID));

            /** We have multiple inputs on our vertex shader, so we need to get
            * their addresses to give the shader position and color information for our vertices.
            * 
            * To get the addresses for each variable, we use the 
            * GL.GetAttribLocation and GL.GetUniformLocation functions.
            * Each takes the program's ID and the name of the variable in the shader. */
            attribute_vpos = GL.GetAttribLocation(pgmID, "vPosition");
            attribute_vcol = GL.GetAttribLocation(pgmID, "vColor");
            uniform_mview = GL.GetUniformLocation(pgmID, "modelview");

            /** Now our shaders and program are set up, but we need to give them something to draw.
             * To do this, we'll be using a Vertex Buffer Object (VBO).
             * When you use a VBO, first you need to have the graphics card create
             * one, then bind to it and send your information. 
             * Then, when the DrawArrays function is called, the information in
             * the buffers will be sent to the shaders and drawn to the screen. */
            GL.GenBuffers(1, out vbo_position);
            GL.GenBuffers(1, out vbo_color);
            GL.GenBuffers(1, out vbo_mview);

            /** We'll need to get another buffer object to put our indice data into.  */
            GL.GenBuffers(1, out ibo_elements);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (loaded)
            {
                UpdateFrame();
                RenderFrame();

            }
        }

        private void UpdateFrame()
        {
            List<Vector3> verts = new List<Vector3>();
            List<int> inds = new List<int>();
            List<Vector3> colors = new List<Vector3>();

            int vertcount = 0;
            foreach (Volume v in objects)
            {
                verts.AddRange(v.GetVerts().ToList());
                inds.AddRange(v.GetIndices(vertcount).ToList());
                colors.AddRange(v.GetColorData().ToList());
                vertcount += v.VertCount;
            }

            vertdata = verts.ToArray();
            indicedata = inds.ToArray();
            coldata = colors.ToArray();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_color);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(coldata.Length * Vector3.SizeInBytes), coldata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vcol, 3, VertexAttribPointerType.Float, true, 0, 0);

            objects[0].Position = new Vector3(0.3f, -0.5f + (float)Math.Sin(time), -3.0f);
//            objects[0].Rotation = new Vector3(0.55f * time, 0.25f * time, 0);
            objects[0].Scale = new Vector3(0.5f, 0.5f, 0.5f);

            objects[1].Position = new Vector3(-1f, 0.5f + (float)Math.Cos(time), -2.0f);
            objects[1].Rotation = new Vector3(-0.25f * time, -0.35f * time, 0);
            objects[1].Scale = new Vector3(0.7f, 0.7f, 0.7f);

            foreach (Volume v in objects)
            {
                v.CalculateModelMatrix();
                v.ViewProjectionMatrix = cam.GetViewMatrix() * Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, ClientSize.Width / (float)ClientSize.Height, 1.0f, 40.0f);
                v.ModelViewProjectionMatrix = v.ModelMatrix * v.ViewProjectionMatrix;
            }

            GL.UseProgram(pgmID);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo_elements);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indicedata.Length * sizeof(int)), indicedata, BufferUsageHint.StaticDraw);
        }


        private void RenderFrame()
        {
            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);

            GL.EnableVertexAttribArray(attribute_vpos);
            GL.EnableVertexAttribArray(attribute_vcol);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vbo_color);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vbo_mview);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, vbo_position);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo_elements);

            int indiceat = 0;

            foreach (Volume v in objects)
            {
                GL.UniformMatrix4(uniform_mview, false, ref v.ModelViewProjectionMatrix);
                GL.DrawElements(BeginMode.Triangles, v.IndiceCount, DrawElementsType.UnsignedInt, indiceat * sizeof(uint));
                indiceat += v.IndiceCount;
            }

            GL.DisableVertexAttribArray(attribute_vpos);
            GL.DisableVertexAttribArray(attribute_vcol);
            GL.Flush();
            glControl1.SwapBuffers();
        }

        /// <summary>
        /// This creates a new shader (using a value from the ShaderType enum), loads code for it, compiles it, and adds it to our program.
        /// It also prints any errors it found to the console, which is really nice for when you make a mistake in a shader (it will also yell at you if you use deprecated code).
        /// </summary>
        /// <param name="filename">File to load the shader from</param>
        /// <param name="type">Type of shader to load</param>
        /// <param name="program">ID of the program to use the shader with</param>
        /// <param name="address">Address of the compiled shader</param>
        void loadShader(String filename, ShaderType type, int program, out int address)
        {

            address = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            Console.WriteLine(GL.GetShaderInfoLog(address));
        }

        private void glControl1_MouseDown(object sender, MouseEventArgs e)
        {
            lastMousePos = new Vector2(e.X, e.Y);
        }

        private void glControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                Vector2 delta = lastMousePos - new Vector2(e.X, e.Y);
                lastMousePos += delta;

                cam.AddRotation(delta.X, delta.Y);
                lastMousePos = new Vector2(e.X, e.Y);
                glControl1.Invalidate();
            }
        }

        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: {    cam.Move(0f, 0.1f, 0f); glControl1.Invalidate(); break;      }
                case Keys.S: {    cam.Move(0f, -0.1f, 0f); glControl1.Invalidate(); break;      }
                case Keys.A: {    cam.Move(-0.1f, 0f, 0f); glControl1.Invalidate(); break; }
                case Keys.D: { cam.Move(0.1f, 0f, 0f); glControl1.Invalidate(); break; }
                case Keys.Q: { cam.Move(0f, 0f, 0.1f); glControl1.Invalidate(); break; }
                case Keys.E: { cam.Move(0f, 0f, -0.1f); glControl1.Invalidate(); break; }
                default: break;
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (sender == btnLeft)
                objects[0].Rotation = new Vector3(0.0f, objects[0].Rotation.Y - MathHelper.DegreesToRadians(10), 0);
            if (sender==btnRight)
                objects[0].Rotation = new Vector3(0.0f, objects[0].Rotation.Y + MathHelper.DegreesToRadians(10), 0);
            this.glControl1.Invalidate();
        }

        //
        // some part of useless code
    }
}
