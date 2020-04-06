namespace KrmKantar2013
{
	partial class FrmSplash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSplash));
            this.pictureSplash = new System.Windows.Forms.PictureBox();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbApplication = new System.Windows.Forms.Label();
            this.lbCopyright = new System.Windows.Forms.Label();
            this.lbSupervisors = new System.Windows.Forms.Label();
            this.lbDevelopers = new System.Windows.Forms.Label();
            this.lbTestQuality = new System.Windows.Forms.Label();
            this.lbBeta = new System.Windows.Forms.Label();
            this.pnSplash = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnAppVersion = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSplash)).BeginInit();
            this.pnSplash.SuspendLayout();
            this.pnAppVersion.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureSplash
            // 
            this.pictureSplash.Image = global::KrmKantar2013.Properties.Resources.YBSplash;
            this.pictureSplash.Location = new System.Drawing.Point(0, 0);
            this.pictureSplash.Name = "pictureSplash";
            this.pictureSplash.Size = new System.Drawing.Size(596, 231);
            this.pictureSplash.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureSplash.TabIndex = 0;
            this.pictureSplash.TabStop = false;
            this.pictureSplash.Click += new System.EventHandler(this.pictureSplash_Click);
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.BackColor = System.Drawing.Color.White;
            this.lbVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbVersion.Location = new System.Drawing.Point(8, 50);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(60, 16);
            this.lbVersion.TabIndex = 3;
            this.lbVersion.Text = "v1.0.0.0";
            this.lbVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbVersion.Click += new System.EventHandler(this.pictureSplash_Click);
            // 
            // lbApplication
            // 
            this.lbApplication.AutoSize = true;
            this.lbApplication.BackColor = System.Drawing.Color.White;
            this.lbApplication.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbApplication.ForeColor = System.Drawing.Color.Black;
            this.lbApplication.Location = new System.Drawing.Point(3, 8);
            this.lbApplication.Name = "lbApplication";
            this.lbApplication.Size = new System.Drawing.Size(224, 42);
            this.lbApplication.TabIndex = 30;
            this.lbApplication.Text = "Kantar2013";
            this.lbApplication.Click += new System.EventHandler(this.pictureSplash_Click);
            // 
            // lbCopyright
            // 
            this.lbCopyright.AutoSize = true;
            this.lbCopyright.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbCopyright.Location = new System.Drawing.Point(7, 156);
            this.lbCopyright.Name = "lbCopyright";
            this.lbCopyright.Size = new System.Drawing.Size(250, 13);
            this.lbCopyright.TabIndex = 32;
            this.lbCopyright.Text = "Copyright 2013 © YBG. All Rights Reserved.";
            this.lbCopyright.Click += new System.EventHandler(this.pictureSplash_Click);
            // 
            // lbSupervisors
            // 
            this.lbSupervisors.AutoSize = true;
            this.lbSupervisors.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbSupervisors.Location = new System.Drawing.Point(8, 96);
            this.lbSupervisors.Name = "lbSupervisors";
            this.lbSupervisors.Size = new System.Drawing.Size(148, 13);
            this.lbSupervisors.TabIndex = 33;
            this.lbSupervisors.Text = "Management :  Hasan Anbarlý";
            this.lbSupervisors.Click += new System.EventHandler(this.pictureSplash_Click);
            // 
            // lbDevelopers
            // 
            this.lbDevelopers.AutoSize = true;
            this.lbDevelopers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbDevelopers.Location = new System.Drawing.Point(8, 111);
            this.lbDevelopers.Name = "lbDevelopers";
            this.lbDevelopers.Size = new System.Drawing.Size(208, 13);
            this.lbDevelopers.TabIndex = 35;
            this.lbDevelopers.Text = "Developers :  Hasan Yýldýrým / Veli Adýgüzel";
            this.lbDevelopers.Click += new System.EventHandler(this.pictureSplash_Click);
            // 
            // lbTestQuality
            // 
            this.lbTestQuality.AutoSize = true;
            this.lbTestQuality.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbTestQuality.Location = new System.Drawing.Point(8, 126);
            this.lbTestQuality.Name = "lbTestQuality";
            this.lbTestQuality.Size = new System.Drawing.Size(239, 13);
            this.lbTestQuality.TabIndex = 36;
            this.lbTestQuality.Text = "Analys && Design :  Emru Çalýþkan / Ferhat Sevinç";
            // 
            // lbBeta
            // 
            this.lbBeta.AutoSize = true;
            this.lbBeta.Font = new System.Drawing.Font("Tahoma", 6.75F);
            this.lbBeta.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbBeta.Location = new System.Drawing.Point(69, 53);
            this.lbBeta.Name = "lbBeta";
            this.lbBeta.Size = new System.Drawing.Size(127, 11);
            this.lbBeta.TabIndex = 44;
            this.lbBeta.Text = "[ Tunaylar + Baykon + Esit ]";
            // 
            // pnSplash
            // 
            this.pnSplash.BackColor = System.Drawing.Color.White;
            this.pnSplash.Controls.Add(this.label1);
            this.pnSplash.Controls.Add(this.pnAppVersion);
            this.pnSplash.Controls.Add(this.lbTestQuality);
            this.pnSplash.Controls.Add(this.lbCopyright);
            this.pnSplash.Controls.Add(this.lbDevelopers);
            this.pnSplash.Controls.Add(this.lbSupervisors);
            this.pnSplash.Location = new System.Drawing.Point(308, 22);
            this.pnSplash.Name = "pnSplash";
            this.pnSplash.Size = new System.Drawing.Size(282, 182);
            this.pnSplash.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(8, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Director :  Özgür Saðlam";
            // 
            // pnAppVersion
            // 
            this.pnAppVersion.Controls.Add(this.lbApplication);
            this.pnAppVersion.Controls.Add(this.lbVersion);
            this.pnAppVersion.Controls.Add(this.lbBeta);
            this.pnAppVersion.Location = new System.Drawing.Point(0, 0);
            this.pnAppVersion.Name = "pnAppVersion";
            this.pnAppVersion.Size = new System.Drawing.Size(279, 74);
            this.pnAppVersion.TabIndex = 45;
            // 
            // FrmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(637, 273);
            this.Controls.Add(this.pnSplash);
            this.Controls.Add(this.pictureSplash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSplash";
            this.Text = "frmSplash";
            ((System.ComponentModel.ISupportInitialize)(this.pictureSplash)).EndInit();
            this.pnSplash.ResumeLayout(false);
            this.pnSplash.PerformLayout();
            this.pnAppVersion.ResumeLayout(false);
            this.pnAppVersion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureSplash;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label lbApplication;
        private System.Windows.Forms.Label lbCopyright;
        private System.Windows.Forms.Label lbSupervisors;
        private System.Windows.Forms.Label lbDevelopers;
        private System.Windows.Forms.Label lbTestQuality;
        private System.Windows.Forms.Label lbBeta;
        private System.Windows.Forms.Panel pnSplash;
        private System.Windows.Forms.Panel pnAppVersion;
        private System.Windows.Forms.Label label1;
	}
}