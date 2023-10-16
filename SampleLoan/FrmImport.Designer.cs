namespace SampleLoan
{
    partial class FrmImport
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
            this.btClose = new System.Windows.Forms.Button();
            this.btOpenPN = new System.Windows.Forms.Button();
            this.btOpenCIF = new System.Windows.Forms.Button();
            this.btOpenSched = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btClose
            // 
            this.btClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btClose.Location = new System.Drawing.Point(47, 119);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(177, 23);
            this.btClose.TabIndex = 7;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = true;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btOpenPN
            // 
            this.btOpenPN.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOpenPN.Location = new System.Drawing.Point(47, 61);
            this.btOpenPN.Name = "btOpenPN";
            this.btOpenPN.Size = new System.Drawing.Size(177, 23);
            this.btOpenPN.TabIndex = 6;
            this.btOpenPN.Text = "Open File (PN Info)";
            this.btOpenPN.UseVisualStyleBackColor = true;
            this.btOpenPN.Click += new System.EventHandler(this.btOpen_Click);
            // 
            // btOpenCIF
            // 
            this.btOpenCIF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOpenCIF.Location = new System.Drawing.Point(47, 32);
            this.btOpenCIF.Name = "btOpenCIF";
            this.btOpenCIF.Size = new System.Drawing.Size(177, 23);
            this.btOpenCIF.TabIndex = 8;
            this.btOpenCIF.Text = "Open File (CIF Info)";
            this.btOpenCIF.UseVisualStyleBackColor = true;
            this.btOpenCIF.Click += new System.EventHandler(this.btOpenCIF_Click);
            // 
            // btOpenSched
            // 
            this.btOpenSched.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOpenSched.Location = new System.Drawing.Point(47, 90);
            this.btOpenSched.Name = "btOpenSched";
            this.btOpenSched.Size = new System.Drawing.Size(177, 23);
            this.btOpenSched.TabIndex = 9;
            this.btOpenSched.Text = "Open File (Sched Info)";
            this.btOpenSched.UseVisualStyleBackColor = true;
            this.btOpenSched.Click += new System.EventHandler(this.btOpenSched_Click);
            // 
            // FrmImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 180);
            this.Controls.Add(this.btOpenSched);
            this.Controls.Add(this.btOpenCIF);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btOpenPN);
            this.Name = "FrmImport";
            this.Text = "Import";
            this.Load += new System.EventHandler(this.FrmImport_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btOpenPN;
        private System.Windows.Forms.Button btOpenCIF;
        private System.Windows.Forms.Button btOpenSched;
    }
}