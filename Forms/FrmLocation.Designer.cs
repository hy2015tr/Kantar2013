namespace KrmKantar2013
{
    partial class FrmLocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLocation));
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.radioLocation = new DevExpress.XtraEditors.RadioGroup();
            ((System.ComponentModel.ISupportInitialize)(this.radioLocation.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Appearance.Options.UseFont = true;
            this.btnCancel.Appearance.Options.UseForeColor = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(350, 90);
            this.btnCancel.LookAndFeel.SkinName = "Stardust";
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "İptal";
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Appearance.Options.UseForeColor = true;
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(350, 25);
            this.btnSave.LookAndFeel.SkinName = "Glass Oceans";
            this.btnSave.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 59);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Kaydet";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // radioLocation
            // 
            this.radioLocation.Location = new System.Drawing.Point(25, 25);
            this.radioLocation.Name = "radioLocation";
            this.radioLocation.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.radioLocation.Properties.Appearance.Options.UseFont = true;
            this.radioLocation.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
            this.radioLocation.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, " 1-)  MERKEZ ( Fabrika / Darıca )"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, " 2-)  LİMAN ( Liman Tesisleri / Gebze )")});
            this.radioLocation.Size = new System.Drawing.Size(305, 95);
            this.radioLocation.TabIndex = 29;
            // 
            // FrmLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 144);
            this.Controls.Add(this.radioLocation);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLocation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lokasyon Formu";
            ((System.ComponentModel.ISupportInitialize)(this.radioLocation.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.RadioGroup radioLocation;

    }
}