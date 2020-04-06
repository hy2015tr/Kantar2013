namespace KrmKantar2013
{
    partial class FrmUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUser));
            this.grpLogin = new DevExpress.XtraEditors.GroupControl();
            this.txtPassNew = new DevExpress.XtraEditors.TextEdit();
            this.lbPassNew = new DevExpress.XtraEditors.LabelControl();
            this.btnChange = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lbPassOld = new DevExpress.XtraEditors.LabelControl();
            this.txtPassOld = new DevExpress.XtraEditors.TextEdit();
            this.lbUser = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grpLogin)).BeginInit();
            this.grpLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassNew.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassOld.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpLogin
            // 
            this.grpLogin.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grpLogin.AppearanceCaption.Options.UseFont = true;
            this.grpLogin.Controls.Add(this.txtPassNew);
            this.grpLogin.Controls.Add(this.lbPassNew);
            this.grpLogin.Controls.Add(this.btnChange);
            this.grpLogin.Controls.Add(this.btnClear);
            this.grpLogin.Controls.Add(this.btnClose);
            this.grpLogin.Controls.Add(this.lbPassOld);
            this.grpLogin.Controls.Add(this.txtPassOld);
            this.grpLogin.Controls.Add(this.lbUser);
            this.grpLogin.Controls.Add(this.txtUserName);
            this.grpLogin.Location = new System.Drawing.Point(21, 22);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(440, 193);
            this.grpLogin.TabIndex = 4;
            this.grpLogin.Text = "Kulanıcı Bilgisi";
            // 
            // txtPassNew
            // 
            this.txtPassNew.EditValue = "";
            this.txtPassNew.Location = new System.Drawing.Point(135, 126);
            this.txtPassNew.Name = "txtPassNew";
            this.txtPassNew.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPassNew.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtPassNew.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtPassNew.Properties.Appearance.Options.UseBackColor = true;
            this.txtPassNew.Properties.Appearance.Options.UseFont = true;
            this.txtPassNew.Properties.Appearance.Options.UseForeColor = true;
            this.txtPassNew.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.txtPassNew.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtPassNew.Properties.PasswordChar = '*';
            this.txtPassNew.Size = new System.Drawing.Size(150, 22);
            this.txtPassNew.TabIndex = 2;
            this.txtPassNew.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtALL_KeyUp);
            // 
            // lbPassNew
            // 
            this.lbPassNew.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbPassNew.Location = new System.Drawing.Point(31, 129);
            this.lbPassNew.Name = "lbPassNew";
            this.lbPassNew.Size = new System.Drawing.Size(82, 16);
            this.lbPassNew.TabIndex = 22;
            this.lbPassNew.Text = "Yeni Parola :";
            // 
            // btnChange
            // 
            this.btnChange.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnChange.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnChange.Appearance.Options.UseFont = true;
            this.btnChange.Appearance.Options.UseForeColor = true;
            this.btnChange.Location = new System.Drawing.Point(312, 41);
            this.btnChange.LookAndFeel.SkinName = "Glass Oceans";
            this.btnChange.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(100, 32);
            this.btnChange.TabIndex = 3;
            this.btnChange.Text = "Değiştir";
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnClear
            // 
            this.btnClear.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClear.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.Appearance.Options.UseForeColor = true;
            this.btnClear.Location = new System.Drawing.Point(312, 91);
            this.btnClear.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 28);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Temizle";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.Location = new System.Drawing.Point(312, 141);
            this.btnClose.LookAndFeel.SkinName = "Stardust";
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Kapat";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lbPassOld
            // 
            this.lbPassOld.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbPassOld.Location = new System.Drawing.Point(31, 94);
            this.lbPassOld.Name = "lbPassOld";
            this.lbPassOld.Size = new System.Drawing.Size(78, 16);
            this.lbPassOld.TabIndex = 19;
            this.lbPassOld.Text = "Eski Parola :";
            // 
            // txtPassOld
            // 
            this.txtPassOld.EditValue = "";
            this.txtPassOld.Location = new System.Drawing.Point(135, 91);
            this.txtPassOld.Name = "txtPassOld";
            this.txtPassOld.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPassOld.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtPassOld.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtPassOld.Properties.Appearance.Options.UseBackColor = true;
            this.txtPassOld.Properties.Appearance.Options.UseFont = true;
            this.txtPassOld.Properties.Appearance.Options.UseForeColor = true;
            this.txtPassOld.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.txtPassOld.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtPassOld.Properties.PasswordChar = '*';
            this.txtPassOld.Size = new System.Drawing.Size(150, 22);
            this.txtPassOld.TabIndex = 1;
            this.txtPassOld.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtALL_KeyUp);
            // 
            // lbUser
            // 
            this.lbUser.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbUser.Location = new System.Drawing.Point(31, 57);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(60, 16);
            this.lbUser.TabIndex = 21;
            this.lbUser.Text = "Kullanıcı :";
            // 
            // txtUserName
            // 
            this.txtUserName.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", global::KrmKantar2013.Properties.Settings.Default, "UserName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtUserName.EditValue = global::KrmKantar2013.Properties.Settings.Default.UserName;
            this.txtUserName.EnterMoveNextControl = true;
            this.txtUserName.Location = new System.Drawing.Point(135, 54);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtUserName.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtUserName.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtUserName.Properties.Appearance.Options.UseBackColor = true;
            this.txtUserName.Properties.Appearance.Options.UseFont = true;
            this.txtUserName.Properties.Appearance.Options.UseForeColor = true;
            this.txtUserName.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.txtUserName.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtUserName.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUserName.Size = new System.Drawing.Size(150, 22);
            this.txtUserName.TabIndex = 0;
            // 
            // FrmUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 236);
            this.Controls.Add(this.grpLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUser";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kullanıcı Formu";
            this.Shown += new System.EventHandler(this.FrmArac_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmUser_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grpLogin)).EndInit();
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassNew.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassOld.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpLogin;
        private DevExpress.XtraEditors.TextEdit txtPassNew;
        private DevExpress.XtraEditors.LabelControl lbPassNew;
        private DevExpress.XtraEditors.SimpleButton btnChange;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl lbPassOld;
        private DevExpress.XtraEditors.TextEdit txtPassOld;
        private DevExpress.XtraEditors.LabelControl lbUser;
        private DevExpress.XtraEditors.TextEdit txtUserName;

    }
}