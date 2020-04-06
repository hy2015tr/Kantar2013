namespace KrmKantar2013
{
    partial class FrmKantarRapor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKantarRapor));
            this.printControl = new DevExpress.XtraPrinting.Control.PrintControl();
            this.lbTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnAyarlar = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnBottom)).BeginInit();
            this.pnBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // printControl
            // 
            this.printControl.AutoSize = true;
            this.printControl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.printControl.AutoZoom = true;
            this.printControl.BackColor = System.Drawing.Color.Empty;
            this.printControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.printControl.ForeColor = System.Drawing.Color.Empty;
            this.printControl.IsMetric = true;
            this.printControl.Location = new System.Drawing.Point(0, 40);
            this.printControl.Name = "printControl";
            this.printControl.Size = new System.Drawing.Size(984, 662);
            this.printControl.TabIndex = 4;
            this.printControl.TooltipFont = new System.Drawing.Font("Tahoma", 8.25F);
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
            this.lbTitle.Size = new System.Drawing.Size(984, 40);
            this.lbTitle.TabIndex = 5;
            this.lbTitle.Text = "KANTAR RAPORU";
            // 
            // pnBottom
            // 
            this.pnBottom.Controls.Add(this.btnAyarlar);
            this.pnBottom.Controls.Add(this.btnPrint);
            this.pnBottom.Controls.Add(this.btnClose);
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 702);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(984, 60);
            this.pnBottom.TabIndex = 6;
            // 
            // btnAyarlar
            // 
            this.btnAyarlar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAyarlar.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAyarlar.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnAyarlar.Appearance.Options.UseFont = true;
            this.btnAyarlar.Appearance.Options.UseForeColor = true;
            this.btnAyarlar.Location = new System.Drawing.Point(735, 15);
            this.btnAyarlar.LookAndFeel.SkinName = "Glass Oceans";
            this.btnAyarlar.Name = "btnAyarlar";
            this.btnAyarlar.Size = new System.Drawing.Size(100, 30);
            this.btnAyarlar.TabIndex = 7;
            this.btnAyarlar.Text = "Ayarlar";
            this.btnAyarlar.Click += new System.EventHandler(this.btnAyarlar_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnPrint.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnPrint.Appearance.Options.UseFont = true;
            this.btnPrint.Appearance.Options.UseForeColor = true;
            this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnPrint.Location = new System.Drawing.Point(608, 15);
            this.btnPrint.LookAndFeel.SkinName = "Glass Oceans";
            this.btnPrint.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(100, 30);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Yazdır";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(861, 15);
            this.btnClose.LookAndFeel.SkinName = "DevExpress Dark Style";
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Kapat";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmKantarRapor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 762);
            this.Controls.Add(this.printControl);
            this.Controls.Add(this.pnBottom);
            this.Controls.Add(this.lbTitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1000, 800);
            this.Name = "FrmKantarRapor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yazdırma Formu";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmReport_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pnBottom)).EndInit();
            this.pnBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraPrinting.Control.PrintControl printControl;
        private DevExpress.XtraEditors.LabelControl lbTitle;
        private DevExpress.XtraEditors.PanelControl pnBottom;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnAyarlar;
    }
}