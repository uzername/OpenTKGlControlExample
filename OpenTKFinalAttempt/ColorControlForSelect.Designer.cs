namespace OpenTKFinalAttempt
{
    partial class ColorControlForSelect
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanelContainer = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // tableLayoutPanelContainer
            // 
            this.tableLayoutPanelContainer.AutoScroll = true;
            this.tableLayoutPanelContainer.ColumnCount = 1;
            this.tableLayoutPanelContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelContainer.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelContainer.Name = "tableLayoutPanelContainer";
            this.tableLayoutPanelContainer.RowCount = 1;
            this.tableLayoutPanelContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelContainer.Size = new System.Drawing.Size(280, 209);
            this.tableLayoutPanelContainer.TabIndex = 0;
            // 
            // ColorControlForSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelContainer);
            this.Name = "ColorControlForSelect";
            this.Size = new System.Drawing.Size(280, 209);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelContainer;
    }
}
