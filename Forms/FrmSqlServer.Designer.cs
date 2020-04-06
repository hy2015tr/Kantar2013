namespace KrmKantar2013
{
    partial class FrmSqlServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSqlServer));
            this.grpDatabase = new DevExpress.XtraEditors.GroupControl();
            this.txtResult = new DevExpress.XtraEditors.LabelControl();
            this.txtSQLServer = new DevExpress.XtraEditors.TextEdit();
            this.btnTest = new DevExpress.XtraEditors.SimpleButton();
            this.lbUser = new DevExpress.XtraEditors.LabelControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grpDatabase)).BeginInit();
            this.grpDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLServer.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDatabase
            // 
            this.grpDatabase.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grpDatabase.AppearanceCaption.Options.UseFont = true;
            this.grpDatabase.Controls.Add(this.txtResult);
            this.grpDatabase.Controls.Add(this.txtSQLServer);
            this.grpDatabase.Controls.Add(this.btnTest);
            this.grpDatabase.Controls.Add(this.lbUser);
            this.grpDatabase.Location = new System.Drawing.Point(21, 22);
            this.grpDatabase.Name = "grpDatabase";
            this.grpDatabase.Size = new System.Drawing.Size(480, 164);
            this.grpDatabase.TabIndex = 4;
            this.grpDatabase.Text = "Database Bilgisi";
            // 
            // txtResult
            // 
            this.txtResult.Appearance.BackColor = System.Drawing.Color.Green;
            this.txtResult.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtResult.Appearance.ForeColor = System.Drawing.Color.White;
            this.txtResult.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.txtResult.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtResult.Location = new System.Drawing.Point(34, 110);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(416, 20);
            this.txtResult.TabIndex = 63;
            this.txtResult.Text = "RESULT";
            // 
            // txtSQLServer
            // 
            this.txtSQLServer.Location = new System.Drawing.Point(130, 59);
            this.txtSQLServer.Name = "txtSQLServer";
            this.txtSQLServer.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSQLServer.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtSQLServer.Properties.Appearance.Options.UseFont = true;
            this.txtSQLServer.Properties.Appearance.Options.UseForeColor = true;
            this.txtSQLServer.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.txtSQLServer.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtSQLServer.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSQLServer.Size = new System.Drawing.Size(200, 22);
            this.txtSQLServer.TabIndex = 0;
            this.txtSQLServer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSQLServer_KeyUp);
            // 
            // btnTest
            // 
            this.btnTest.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnTest.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnTest.Appearance.Options.UseFont = true;
            this.btnTest.Appearance.Options.UseForeColor = true;
            this.btnTest.Location = new System.Drawing.Point(350, 54);
            this.btnTest.LookAndFeel.SkinName = "Glass Oceans";
            this.btnTest.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(100, 32);
            this.btnTest.TabIndex = 1;
            this.btnTest.Text = "Test";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lbUser
            // 
            this.lbUser.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbUser.Location = new System.Drawing.Point(34, 62);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(81, 16);
            this.lbUser.TabIndex = 21;
            this.lbUser.Text = "SQL Server :";
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.Location = new System.Drawing.Point(401, 205);
            this.btnCancel.LookAndFeel.SkinName = "Stardust";
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "İptal";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.Location = new System.Drawing.Point(274, 203);
            this.btnSave.LookAndFeel.SkinName = "Glass Oceans";
            this.btnSave.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 32);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Kaydet";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmSqlServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 250);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpDatabase);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSqlServer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Microsoft SQL Server Formu";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSqlServer_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grpDatabase)).EndInit();
            this.grpDatabase.ResumeLayout(false);
            this.grpDatabase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSQLServer.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpDatabase;
        private DevExpress.XtraEditors.SimpleButton btnTest;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lbUser;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtSQLServer;
        private DevExpress.XtraEditors.LabelControl txtResult;

    }
}