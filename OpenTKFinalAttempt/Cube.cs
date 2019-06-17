using OpenTK;

namespace OpenTKFinalAttempt
{
    public enum cubeside
    {
        leftside=0,
        backside=4,
        rightside=8,
        topside=12,
        frontside=16,
        bottomside=20,
        unknownside=33
    }
    /// <summary>
    /// A simple, colorful cube
    /// </summary>
    class Cube : Volume
    {
        public static cubeside convertIndexToCubeside (int in_number) {
            switch (in_number)
            {
                case 0: return cubeside.leftside;
                case 1: return cubeside.backside;
                case 2: return cubeside.rightside;
                case 3: return cubeside.topside;
                case 4: return cubeside.frontside;
                case 5: return cubeside.bottomside;

                default:
                    return cubeside.unknownside;
            }
        }

        public Cube()
        {
            VertCount = 8;
            IndiceCount = 36;
            ColorDataCount = 8;
        }
        private Vector3[] ColorsOfCube = new Vector3[]  {
                //left
                new Vector3(1f,0f,0f),
                new Vector3(1f,0f,0f),
                new Vector3(1f,0f,0f),
                new Vector3(1f,0f,0f),
                //back
                new Vector3(0f,1f,0f),
                new Vector3(0f,1f,0f),
                new Vector3(0f,1f,0f),
                new Vector3(0f,1f,0f),
                //right
                new Vector3(0f,0f,1f),
                new Vector3(0f,0f,1f),
                new Vector3(0f,0f,1f),
                new Vector3(0f,0f,1f),
                //top
                new Vector3(1f,0f,0f),
                new Vector3(1f,0f,0f),
                new Vector3(1f,0f,0f),
                new Vector3(1f,0f,0f),
                //front
                new Vector3(0f,1f,1f),
                new Vector3(0f,1f,1f),
                new Vector3(0f,1f,1f),
                new Vector3(0f,1f,1f),
                //bottom
                new Vector3(1f,0f,1f),
                new Vector3(1f,0f,1f),
                new Vector3(1f,0f,1f),
                new Vector3(1f,0f,1f)
            };
        /// <summary>
        /// set color of cube side
        /// </summary>
        /// <param name="in_color">a color to set up, Vector3 should have 3 components, between 0 and 1</param>
        /// <param name="in_targetSide">side of cube to be colored</param>
        public void setColorOfCube(Vector3 in_color,cubeside in_targetSide)
        {
            ColorsOfCube[(int)in_targetSide] = in_color;
            ColorsOfCube[(int)in_targetSide+1] = in_color;
            ColorsOfCube[(int)in_targetSide+2] = in_color;
            ColorsOfCube[(int)in_targetSide+3] = in_color;
        }
        public override Vector3[] GetVerts()
        {
            /*
            return new Vector3[] {
                new Vector3(-0.5f, -0.5f,  -0.5f),
                new Vector3(0.5f, -0.5f,  -0.5f),
                new Vector3(0.5f, 0.5f,  -0.5f),
                new Vector3(-0.5f, 0.5f,  -0.5f),
                new Vector3(-0.5f, -0.5f,  0.5f),
                new Vector3(0.5f, -0.5f,  0.5f),
                new Vector3(0.5f, 0.5f,  0.5f),
                new Vector3(-0.5f, 0.5f,  0.5f),
            };
            */
            return new Vector3[] {
                //left side
                new Vector3(-0.5f, -0.5f,  -0.5f), //0 :: 0
                new Vector3(0.5f, -0.5f,  -0.5f),  //1 :: 1
                new Vector3(0.5f, 0.5f,  -0.5f),   //2 :: 2
                new Vector3(-0.5f, 0.5f,  -0.5f),  //3 :: 3
                //back side
                new Vector3(0.5f, -0.5f,  -0.5f),  //1 :: 4
                new Vector3(0.5f, 0.5f,  -0.5f),   //2 :: 5
                new Vector3(0.5f, -0.5f,  0.5f),   //5 :: 6
                new Vector3(0.5f, 0.5f,  0.5f),    //6 :: 7
                //right side
                new Vector3(-0.5f, -0.5f,  0.5f),  //4 :: 8
                new Vector3(0.5f, -0.5f,  0.5f),   //5 :: 9
                new Vector3(0.5f, 0.5f,  0.5f),    //6 :: 10
                new Vector3(-0.5f, 0.5f,  0.5f),   //7 :: 11
                // top side
                new Vector3(0.5f, 0.5f,  -0.5f),   //2 :: 12
                new Vector3(-0.5f, 0.5f,  -0.5f),  //3 :: 13
                new Vector3(0.5f, 0.5f,  0.5f),    //6 :: 14
                new Vector3(-0.5f, 0.5f,  0.5f),   //7 :: 15
                //front side
                new Vector3(-0.5f, -0.5f,  -0.5f), //0 :: 16
                new Vector3(-0.5f, 0.5f,  0.5f),   //7 :: 17
                new Vector3(-0.5f, 0.5f,  -0.5f),  //3 :: 18
                new Vector3(-0.5f, -0.5f,  0.5f),  //4 :: 19
                //bottom side
                new Vector3(-0.5f, -0.5f,  -0.5f), //0 :: 20
                new Vector3(0.5f, -0.5f,  -0.5f),  //1 :: 21
                new Vector3(-0.5f, -0.5f,  0.5f),  //4 :: 22
                new Vector3(0.5f, -0.5f,  0.5f),   //5 :: 23
            };

        }

        public override int[] GetIndices(int offset = 0)
        {
            /*
            int[] inds = new int[] {
                //left
                0, 2, 1,
                0, 3, 2,
                //back
                1, 2, 6,
                6, 5, 1,
                //right
                4, 5, 6,
                6, 7, 4,
                //top
                2, 3, 6,
                6, 3, 7,
                //front
                0, 7, 3,
                0, 4, 7,
                //bottom
                0, 1, 5,
                0, 5, 4
            };
            */
            int[] inds = new int[] {
                //left
                0, 2, 1,
                0, 3, 2,
                //back
                4, 5, 7,
                7, 6, 4,
                //right
                8, 9, 10,
                10, 11, 8,
                //top
                12, 13, 14,
                14, 13, 15,
                //front
                16, 17, 18,
                16, 19, 17,
                //bottom
                20, 21, 23,
                20, 23, 22
            };

            if (offset != 0)
            {
                for (int i = 0; i < inds.Length; i++)
                {
                    inds[i] += offset;
                }
            }

            return inds;
        }

        public override Vector3[] GetColorData()
        {
            /*
            return new Vector3[] {
                new Vector3(1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f, 1f, 0f),
                new Vector3( 1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f, 1f, 0f),
                new Vector3( 1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f)
            };
            */
            return ColorsOfCube;

        }

        public override void CalculateModelMatrix()
        {
            ModelMatrix = Matrix4.CreateScale(Scale) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z) * Matrix4.CreateTranslation(Position);
        }
    }
}
