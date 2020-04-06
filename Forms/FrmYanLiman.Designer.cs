namespace KrmKantar2013
{
    partial class FrmYanLiman
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmYanLiman));
            this.grpArac = new DevExpress.XtraEditors.GroupControl();
            this.txtSapFisNo = new DevExpress.XtraEditors.TextEdit();
            this.txtT02 = new DevExpress.XtraEditors.SpinEdit();
            this.txtT01 = new DevExpress.XtraEditors.SpinEdit();
            this.lbNetTutar = new DevExpress.XtraEditors.LabelControl();
            this.txtNet = new DevExpress.XtraEditors.TextEdit();
            this.lbMalzeme = new DevExpress.XtraEditors.LabelControl();
            this.txtMalzeme = new DevExpress.XtraEditors.TextEdit();
            this.lbPlakaSap = new DevExpress.XtraEditors.LabelControl();
            this.txtPlakaNo = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.lbTitle = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grpArac)).BeginInit();
            this.grpArac.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSapFisNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtT02.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtT01.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMalzeme.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlakaNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpArac
            // 
            this.grpArac.Controls.Add(this.txtSapFisNo);
            this.grpArac.Controls.Add(this.txtT02);
            this.grpArac.Controls.Add(this.txtT01);
            this.grpArac.Controls.Add(this.lbNetTutar);
            this.grpArac.Controls.Add(this.txtNet);
            this.grpArac.Controls.Add(this.lbMalzeme);
            this.grpArac.Controls.Add(this.txtMalzeme);
            this.grpArac.Controls.Add(this.lbPlakaSap);
            this.grpArac.Controls.Add(this.txtPlakaNo);
            this.grpArac.Location = new System.Drawing.Point(20, 56);
            this.grpArac.Name = "grpArac";
            this.grpArac.ShowCaption = false;
            this.grpArac.Size = new System.Drawing.Size(510, 170);
            this.grpArac.TabIndex = 0;
            // 
            // txtSapFisNo
            // 
            this.txtSapFisNo.EditValue = "";
            this.txtSapFisNo.EnterMoveNextControl = true;
            this.txtSapFisNo.Location = new System.Drawing.Point(304, 24);
            this.txtSapFisNo.Name = "txtSapFisNo";
            this.txtSapFisNo.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.txtSapFisNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtSapFisNo.Properties.Appearance.ForeColor = System.Drawing.Color.Aqua;
            this.txtSapFisNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtSapFisNo.Properties.Appearance.Options.UseFont = true;
            this.txtSapFisNo.Properties.Appearance.Options.UseForeColor = true;
            this.txtSapFisNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtSapFisNo.Properties.MaxLength = 10;
            this.txtSapFisNo.Properties.ReadOnly = true;
            this.txtSapFisNo.Size = new System.Drawing.Size(177, 26);
            this.txtSapFisNo.TabIndex = 1;
            // 
            // txtT02
            // 
            this.txtT02.EditValue = new decimal(new int[] {
            116500,
            0,
            0,
            0});
            this.txtT02.EnterMoveNextControl = true;
            this.txtT02.Location = new System.Drawing.Point(244, 118);
            this.txtT02.Name = "txtT02";
            this.txtT02.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.txtT02.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtT02.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.txtT02.Properties.Appearance.Options.UseBackColor = true;
            this.txtT02.Properties.Appearance.Options.UseFont = true;
            this.txtT02.Properties.Appearance.Options.UseForeColor = true;
            this.txtT02.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtT02.Properties.DisplayFormat.FormatString = "0,0";
            this.txtT02.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtT02.Properties.EditFormat.FormatString = "0,0";
            this.txtT02.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtT02.Properties.IsFloatValue = false;
            this.txtT02.Properties.Mask.EditMask = "N00";
            this.txtT02.Properties.MaxValue = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtT02.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtT02.Size = new System.Drawing.Size(114, 26);
            this.txtT02.TabIndex = 4;
            this.txtT02.EditValueChanged += new System.EventHandler(this.txtALL_EditValueChanged);
            // 
            // txtT01
            // 
            this.txtT01.EditValue = new decimal(new int[] {
            122500,
            0,
            0,
            0});
            this.txtT01.EnterMoveNextControl = true;
            this.txtT01.Location = new System.Drawing.Point(121, 118);
            this.txtT01.Name = "txtT01";
            this.txtT01.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.txtT01.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtT01.Properties.Appearance.ForeColor = System.Drawing.Color.White;
            this.txtT01.Properties.Appearance.Options.UseBackColor = true;
            this.txtT01.Properties.Appearance.Options.UseFont = true;
            this.txtT01.Properties.Appearance.Options.UseForeColor = true;
            this.txtT01.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtT01.Properties.DisplayFormat.FormatString = "0,0";
            this.txtT01.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtT01.Properties.EditFormat.FormatString = "0,0";
            this.txtT01.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtT01.Properties.IsFloatValue = false;
            this.txtT01.Properties.Mask.EditMask = "N00";
            this.txtT01.Properties.MaxValue = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtT01.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtT01.Size = new System.Drawing.Size(114, 26);
            this.txtT01.TabIndex = 3;
            this.txtT01.EditValueChanged += new System.EventHandler(this.txtALL_EditValueChanged);
            // 
            // lbNetTutar
            // 
            this.lbNetTutar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbNetTutar.Location = new System.Drawing.Point(23, 125);
            this.lbNetTutar.Name = "lbNetTutar";
            this.lbNetTutar.Size = new System.Drawing.Size(90, 14);
            this.lbNetTutar.TabIndex = 31;
            this.lbNetTutar.Text = "T1 / T2 / NET :";
            // 
            // txtNet
            // 
            this.txtNet.EditValue = 65500;
            this.txtNet.EnterMoveNextControl = true;
            this.txtNet.Location = new System.Drawing.Point(367, 118);
            this.txtNet.Name = "txtNet";
            this.txtNet.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.txtNet.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtNet.Properties.Appearance.ForeColor = System.Drawing.Color.Aqua;
            this.txtNet.Properties.Appearance.Options.UseBackColor = true;
            this.txtNet.Properties.Appearance.Options.UseFont = true;
            this.txtNet.Properties.Appearance.Options.UseForeColor = true;
            this.txtNet.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNet.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtNet.Properties.DisplayFormat.FormatString = "0,0 Kg";
            this.txtNet.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNet.Properties.EditFormat.FormatString = "0,0 Kg";
            this.txtNet.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtNet.Properties.MaxLength = 10;
            this.txtNet.Properties.ReadOnly = true;
            this.txtNet.Size = new System.Drawing.Size(114, 26);
            this.txtNet.TabIndex = 5;
            // 
            // lbMalzeme
            // 
            this.lbMalzeme.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbMalzeme.Location = new System.Drawing.Point(29, 63);
            this.lbMalzeme.Name = "lbMalzeme";
            this.lbMalzeme.Size = new System.Drawing.Size(84, 14);
            this.lbMalzeme.TabIndex = 29;
            this.lbMalzeme.Text = "Malzeme Adı :";
            // 
            // txtMalzeme
            // 
            this.txtMalzeme.EditValue = "";
            this.txtMalzeme.EnterMoveNextControl = true;
            this.txtMalzeme.Location = new System.Drawing.Point(121, 56);
            this.txtMalzeme.Name = "txtMalzeme";
            this.txtMalzeme.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.txtMalzeme.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtMalzeme.Properties.Appearance.ForeColor = System.Drawing.Color.Aqua;
            this.txtMalzeme.Properties.Appearance.Options.UseBackColor = true;
            this.txtMalzeme.Properties.Appearance.Options.UseFont = true;
            this.txtMalzeme.Properties.Appearance.Options.UseForeColor = true;
            this.txtMalzeme.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMalzeme.Properties.MaxLength = 10;
            this.txtMalzeme.Properties.ReadOnly = true;
            this.txtMalzeme.Size = new System.Drawing.Size(360, 26);
            this.txtMalzeme.TabIndex = 2;
            // 
            // lbPlakaSap
            // 
            this.lbPlakaSap.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbPlakaSap.Location = new System.Drawing.Point(33, 31);
            this.lbPlakaSap.Name = "lbPlakaSap";
            this.lbPlakaSap.Size = new System.Drawing.Size(80, 14);
            this.lbPlakaSap.TabIndex = 27;
            this.lbPlakaSap.Text = "Plaka / SAP :";
            // 
            // txtPlakaNo
            // 
            this.txtPlakaNo.EditValue = "";
            this.txtPlakaNo.EnterMoveNextControl = true;
            this.txtPlakaNo.Location = new System.Drawing.Point(121, 24);
            this.txtPlakaNo.Name = "txtPlakaNo";
            this.txtPlakaNo.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.txtPlakaNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtPlakaNo.Properties.Appearance.ForeColor = System.Drawing.Color.Aqua;
            this.txtPlakaNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtPlakaNo.Properties.Appearance.Options.UseFont = true;
            this.txtPlakaNo.Properties.Appearance.Options.UseForeColor = true;
            this.txtPlakaNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPlakaNo.Properties.MaxLength = 10;
            this.txtPlakaNo.Properties.ReadOnly = true;
            this.txtPlakaNo.Size = new System.Drawing.Size(179, 26);
            this.txtPlakaNo.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(334, 242);
            this.btnSave.LookAndFeel.SkinName = "Glass Oceans";
            this.btnSave.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Kaydet";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(450, 242);
            this.btnCancel.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "İptal";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbTitle
            // 
            this.lbTitle.Appearance.BackColor = System.Drawing.Color.Navy;
            this.lbTitle.Appearance.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lbTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lbTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lbTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.lbTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbTitle.Location = new System.Drawing.Point(0, 0);
            this.lbTitle.LookAndFeel.UseDefaultLookAndFeel = false;
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(554, 40);
            this.lbTitle.TabIndex = 3;
            this.lbTitle.Text = "YAN LİMAN TARTIMLARI";
            // 
            // FrmYanLiman
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 284);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpArac);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmYanLiman";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tartım Formu";
            this.Shown += new System.EventHandler(this.FrmTartim_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmTartim_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grpArac)).EndInit();
            this.grpArac.ResumeLayout(false);
            this.grpArac.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSapFisNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtT02.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtT01.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMalzeme.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPlakaNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpArac;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl lbNetTutar;
        private DevExpress.XtraEditors.TextEdit txtNet;
        private DevExpress.XtraEditors.LabelControl lbMalzeme;
        private DevExpress.XtraEditors.TextEdit txtMalzeme;
        private DevExpress.XtraEditors.LabelControl lbPlakaSap;
        private DevExpress.XtraEditors.TextEdit txtPlakaNo;
        private DevExpress.XtraEditors.LabelControl lbTitle;
        private DevExpress.XtraEditors.SpinEdit txtT01;
        private DevExpress.XtraEditors.SpinEdit txtT02;
        private DevExpress.XtraEditors.TextEdit txtSapFisNo;
    }
}