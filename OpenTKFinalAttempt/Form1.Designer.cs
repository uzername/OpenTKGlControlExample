namespace OpenTKFinalAttempt
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.glControl1 = new OpenTK.GLControl();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ColorGroupBox = new System.Windows.Forms.GroupBox();
            this.colorControlForSelect1 = new OpenTKFinalAttempt.ColorControlForSelect();
            this.tableLayoutPanel1.SuspendLayout();
            this.ColorGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(3, 3);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(480, 357);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.14745F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.85255F));
            this.tableLayoutPanel1.Controls.Add(this.glControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.ColorGroupBox, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85.44601F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.55399F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(746, 426);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // ColorGroupBox
            // 
            this.ColorGroupBox.Controls.Add(this.colorControlForSelect1);
            this.ColorGroupBox.Location = new System.Drawing.Point(489, 3);
            this.ColorGroupBox.Name = "ColorGroupBox";
            this.ColorGroupBox.Size = new System.Drawing.Size(254, 177);
            this.ColorGroupBox.TabIndex = 1;
            this.ColorGroupBox.TabStop = false;
            this.ColorGroupBox.Text = "Color";
            // 
            // colorControlForSelect1
            // 
            this.colorControlForSelect1.Location = new System.Drawing.Point(6, 19);
            this.colorControlForSelect1.Name = "colorControlForSelect1";
            this.colorControlForSelect1.Size = new System.Drawing.Size(242, 152);
            this.colorControlForSelect1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ColorGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox ColorGroupBox;
        private ColorControlForSelect colorControlForSelect1;
    }
}

