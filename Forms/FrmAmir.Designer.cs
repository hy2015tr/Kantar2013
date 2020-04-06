namespace KrmKantar2013
{
    partial class FrmAmir
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAmir));
            this.grpAmir = new DevExpress.XtraEditors.GroupControl();
            this.txtPassword = new DevExpress.XtraEditors.TextEdit();
            this.lbPass = new DevExpress.XtraEditors.LabelControl();
            this.btnOnay = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lbUser = new DevExpress.XtraEditors.LabelControl();
            this.txtUserName = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grpAmir)).BeginInit();
            this.grpAmir.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpAmir
            // 
            this.grpAmir.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.grpAmir.AppearanceCaption.Options.UseFont = true;
            this.grpAmir.Controls.Add(this.txtPassword);
            this.grpAmir.Controls.Add(this.lbPass);
            this.grpAmir.Controls.Add(this.btnOnay);
            this.grpAmir.Controls.Add(this.btnClose);
            this.grpAmir.Controls.Add(this.lbUser);
            this.grpAmir.Controls.Add(this.txtUserName);
            this.grpAmir.Location = new System.Drawing.Point(21, 22);
            this.grpAmir.Name = "grpAmir";
            this.grpAmir.Size = new System.Drawing.Size(424, 193);
            this.grpAmir.TabIndex = 4;
            this.grpAmir.Text = "Amir Teyit Yetkisi";
            // 
            // txtPassword
            // 
            this.txtPassword.EditValue = "";
            this.txtPassword.Location = new System.Drawing.Point(110, 126);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.txtPassword.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
            this.txtPassword.Properties.Appearance.Options.UseBackColor = true;
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.Appearance.Options.UseForeColor = true;
            this.txtPassword.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.txtPassword.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(150, 22);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtALL_KeyUp);
            // 
            // lbPass
            // 
            this.lbPass.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbPass.Location = new System.Drawing.Point(51, 129);
            this.lbPass.Name = "lbPass";
            this.lbPass.Size = new System.Drawing.Size(50, 16);
            this.lbPass.TabIndex = 22;
            this.lbPass.Text = "Parola :";
            // 
            // btnOnay
            // 
            this.btnOnay.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnOnay.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnOnay.Appearance.Options.UseFont = true;
            this.btnOnay.Appearance.Options.UseForeColor = true;
            this.btnOnay.Location = new System.Drawing.Point(284, 48);
            this.btnOnay.LookAndFeel.SkinName = "Glass Oceans";
            this.btnOnay.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnOnay.Name = "btnOnay";
            this.btnOnay.Size = new System.Drawing.Size(100, 60);
            this.btnOnay.TabIndex = 3;
            this.btnOnay.Text = "Onayla";
            this.btnOnay.Click += new System.EventHandler(this.btnOnay_Click);
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.Location = new System.Drawing.Point(284, 123);
            this.btnClose.LookAndFeel.SkinName = "Stardust";
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 28);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "İptal";
            this.btnClose.Click += new System.EventHandler(this.btnIptal_Click);
            // 
            // lbUser
            // 
            this.lbUser.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbUser.Location = new System.Drawing.Point(41, 57);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(60, 16);
            this.lbUser.TabIndex = 21;
            this.lbUser.Text = "Kullanıcı :";
            // 
            // txtUserName
            // 
            this.txtUserName.EnterMoveNextControl = true;
            this.txtUserName.Location = new System.Drawing.Point(110, 54);
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
            this.txtUserName.Properties.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(150, 22);
            this.txtUserName.TabIndex = 0;
            // 
            // FrmAmir
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 232);
            this.Controls.Add(this.grpAmir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAmir";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Onay Formu";
            this.Shown += new System.EventHandler(this.FrmArac_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmUser_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grpAmir)).EndInit();
            this.grpAmir.ResumeLayout(false);
            this.grpAmir.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUserName.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpAmir;
        private DevExpress.XtraEditors.TextEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl lbPass;
        private DevExpress.XtraEditors.SimpleButton btnOnay;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl lbUser;
        private DevExpress.XtraEditors.TextEdit txtUserName;

    }
}