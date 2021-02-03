namespace CourseAche2
{
    partial class FormShowInfo
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
            this.DGShow = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DGShow)).BeginInit();
            this.SuspendLayout();
            // 
            // DGShow
            // 
            this.DGShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGShow.Location = new System.Drawing.Point(0, 0);
            this.DGShow.Name = "DGShow";
            this.DGShow.Size = new System.Drawing.Size(633, 208);
            this.DGShow.TabIndex = 0;
            // 
            // FormShowInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 208);
            this.Controls.Add(this.DGShow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormShowInfo";
            this.Text = "Информация";
            this.Load += new System.EventHandler(this.FormShowInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGShow;
    }
}