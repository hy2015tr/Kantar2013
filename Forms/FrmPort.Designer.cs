namespace KrmKantar2013
{
    partial class FrmPort
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPort));
            this.grpPort = new DevExpress.XtraEditors.GroupControl();
            this.txtKantarSeriNo = new DevExpress.XtraEditors.TextEdit();
            this.lbKantarSeriNo = new DevExpress.XtraEditors.LabelControl();
            this.checkPort = new DevExpress.XtraEditors.CheckEdit();
            this.lbKantar2 = new DevExpress.XtraEditors.LabelControl();
            this.lbKantar1 = new DevExpress.XtraEditors.LabelControl();
            this.txtResultP2 = new DevExpress.XtraEditors.LabelControl();
            this.txtResultP1 = new DevExpress.XtraEditors.LabelControl();
            this.radioKantar = new DevExpress.XtraEditors.RadioGroup();
            this.lbKantarTipi = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.cbKantarTipi = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lbKantarNo = new DevExpress.XtraEditors.LabelControl();
            this.btnTest = new DevExpress.XtraEditors.SimpleButton();
            this.propertyPort = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.lbKantarPortu = new DevExpress.XtraEditors.LabelControl();
            this.cbKantarPorts = new DevExpress.XtraEditors.ComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.grpPort)).BeginInit();
            this.grpPort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKantarSeriNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioKantar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbKantarTipi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbKantarPorts.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPort
            // 
            this.grpPort.Controls.Add(this.txtKantarSeriNo);
            this.grpPort.Controls.Add(this.lbKantarSeriNo);
            this.grpPort.Controls.Add(this.checkPort);
            this.grpPort.Controls.Add(this.lbKantar2);
            this.grpPort.Controls.Add(this.lbKantar1);
            this.grpPort.Controls.Add(this.txtResultP2);
            this.grpPort.Controls.Add(this.txtResultP1);
            this.grpPort.Controls.Add(this.radioKantar);
            this.grpPort.Controls.Add(this.lbKantarTipi);
            this.grpPort.Controls.Add(this.btnClose);
            this.grpPort.Controls.Add(this.cbKantarTipi);
            this.grpPort.Controls.Add(this.lbKantarNo);
            this.grpPort.Controls.Add(this.btnTest);
            this.grpPort.Controls.Add(this.propertyPort);
            this.grpPort.Controls.Add(this.lbKantarPortu);
            this.grpPort.Controls.Add(this.cbKantarPorts);
            this.grpPort.Location = new System.Drawing.Point(12, 12);
            this.grpPort.Name = "grpPort";
            this.grpPort.Size = new System.Drawing.Size(470, 578);
            this.grpPort.TabIndex = 0;
            this.grpPort.Text = "Port Ayarları";
            // 
            // txtKantarSeriNo
            // 
            this.txtKantarSeriNo.EditValue = "12345678901";
            this.txtKantarSeriNo.Location = new System.Drawing.Point(119, 40);
            this.txtKantarSeriNo.Name = "txtKantarSeriNo";
            this.txtKantarSeriNo.Properties.Appearance.BackColor = System.Drawing.Color.Black;
            this.txtKantarSeriNo.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtKantarSeriNo.Properties.Appearance.ForeColor = System.Drawing.Color.Aqua;
            this.txtKantarSeriNo.Properties.Appearance.Options.UseBackColor = true;
            this.txtKantarSeriNo.Properties.Appearance.Options.UseFont = true;
            this.txtKantarSeriNo.Properties.Appearance.Options.UseForeColor = true;
            this.txtKantarSeriNo.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtKantarSeriNo.Properties.MaxLength = 20;
            this.txtKantarSeriNo.Size = new System.Drawing.Size(228, 24);
            this.txtKantarSeriNo.TabIndex = 73;
            this.txtKantarSeriNo.EditValueChanged += new System.EventHandler(this.txtKantarSeriNo_EditValueChanged);
            // 
            // lbKantarSeriNo
            // 
            this.lbKantarSeriNo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbKantarSeriNo.Location = new System.Drawing.Point(18, 46);
            this.lbKantarSeriNo.Name = "lbKantarSeriNo";
            this.lbKantarSeriNo.Size = new System.Drawing.Size(92, 14);
            this.lbKantarSeriNo.TabIndex = 72;
            this.lbKantarSeriNo.Text = "Kantar SeriNo :";
            // 
            // checkPort
            // 
            this.checkPort.EditValue = true;
            this.checkPort.Location = new System.Drawing.Point(228, 229);
            this.checkPort.Name = "checkPort";
            this.checkPort.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkPort.Properties.Appearance.Options.UseFont = true;
            this.checkPort.Properties.Caption = "Aktiflik Durumu";
            this.checkPort.Size = new System.Drawing.Size(123, 19);
            this.checkPort.TabIndex = 66;
            this.checkPort.EditValueChanged += new System.EventHandler(this.checkPort_EditValueChanged);
            // 
            // lbKantar2
            // 
            this.lbKantar2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbKantar2.Location = new System.Drawing.Point(29, 301);
            this.lbKantar2.Name = "lbKantar2";
            this.lbKantar2.Size = new System.Drawing.Size(81, 14);
            this.lbKantar2.TabIndex = 65;
            this.lbKantar2.Text = "Kantar ( II ) :";
            // 
            // lbKantar1
            // 
            this.lbKantar1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbKantar1.Location = new System.Drawing.Point(34, 268);
            this.lbKantar1.Name = "lbKantar1";
            this.lbKantar1.Size = new System.Drawing.Size(76, 14);
            this.lbKantar1.TabIndex = 64;
            this.lbKantar1.Text = "Kantar ( I ) :";
            // 
            // txtResultP2
            // 
            this.txtResultP2.Appearance.BackColor = System.Drawing.Color.Green;
            this.txtResultP2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtResultP2.Appearance.ForeColor = System.Drawing.Color.White;
            this.txtResultP2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.txtResultP2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtResultP2.Location = new System.Drawing.Point(119, 298);
            this.txtResultP2.Name = "txtResultP2";
            this.txtResultP2.Size = new System.Drawing.Size(330, 20);
            this.txtResultP2.TabIndex = 63;
            this.txtResultP2.Text = "RESULT";
            // 
            // txtResultP1
            // 
            this.txtResultP1.Appearance.BackColor = System.Drawing.Color.Green;
            this.txtResultP1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtResultP1.Appearance.ForeColor = System.Drawing.Color.White;
            this.txtResultP1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.txtResultP1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtResultP1.Location = new System.Drawing.Point(119, 265);
            this.txtResultP1.Name = "txtResultP1";
            this.txtResultP1.Size = new System.Drawing.Size(330, 20);
            this.txtResultP1.TabIndex = 62;
            this.txtResultP1.Text = "RESULT";
            // 
            // radioKantar
            // 
            this.radioKantar.Location = new System.Drawing.Point(119, 82);
            this.radioKantar.Name = "radioKantar";
            this.radioKantar.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.radioKantar.Properties.Appearance.Options.UseFont = true;
            this.radioKantar.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, " [1]  KANTAR ( I )"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, " [2]  KANTAR ( II )")});
            this.radioKantar.Size = new System.Drawing.Size(228, 90);
            this.radioKantar.TabIndex = 32;
            this.radioKantar.SelectedIndexChanged += new System.EventHandler(this.radioKantar_SelectedIndexChanged);
            // 
            // lbKantarTipi
            // 
            this.lbKantarTipi.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbKantarTipi.Location = new System.Drawing.Point(36, 190);
            this.lbKantarTipi.Name = "lbKantarTipi";
            this.lbKantarTipi.Size = new System.Drawing.Size(74, 14);
            this.lbKantarTipi.TabIndex = 30;
            this.lbKantarTipi.Text = "Kantar Tipi :";
            // 
            // btnClose
            // 
            this.btnClose.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(359, 178);
            this.btnClose.LookAndFeel.SkinName = "Dark Side";
            this.btnClose.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 70);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Kapat";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbKantarTipi
            // 
            this.cbKantarTipi.Location = new System.Drawing.Point(119, 187);
            this.cbKantarTipi.Name = "cbKantarTipi";
            this.cbKantarTipi.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbKantarTipi.Properties.Appearance.Options.UseFont = true;
            this.cbKantarTipi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbKantarTipi.Properties.Items.AddRange(new object[] {
            "TUNAYLAR",
            "BAYKON",
            "ESIT"});
            this.cbKantarTipi.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbKantarTipi.Size = new System.Drawing.Size(228, 20);
            this.cbKantarTipi.TabIndex = 29;
            this.cbKantarTipi.SelectedIndexChanged += new System.EventHandler(this.cbKantarTipi_SelectedIndexChanged);
            // 
            // lbKantarNo
            // 
            this.lbKantarNo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbKantarNo.Location = new System.Drawing.Point(41, 127);
            this.lbKantarNo.Name = "lbKantarNo";
            this.lbKantarNo.Size = new System.Drawing.Size(69, 14);
            this.lbKantarNo.TabIndex = 28;
            this.lbKantarNo.Text = "Kantar No :";
            // 
            // btnTest
            // 
            this.btnTest.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTest.Appearance.ForeColor = System.Drawing.Color.Black;
            this.btnTest.Appearance.Options.UseFont = true;
            this.btnTest.Appearance.Options.UseForeColor = true;
            this.btnTest.Location = new System.Drawing.Point(359, 40);
            this.btnTest.LookAndFeel.SkinName = "Glass Oceans";
            this.btnTest.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(90, 132);
            this.btnTest.TabIndex = 26;
            this.btnTest.Text = "TEST\r\n\r\n( Ports )";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // propertyPort
            // 
            this.propertyPort.Appearance.Category.BackColor = System.Drawing.Color.DimGray;
            this.propertyPort.Appearance.Category.BorderColor = System.Drawing.Color.Black;
            this.propertyPort.Appearance.Category.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.propertyPort.Appearance.Category.ForeColor = System.Drawing.Color.White;
            this.propertyPort.Appearance.Category.Options.UseBackColor = true;
            this.propertyPort.Appearance.Category.Options.UseBorderColor = true;
            this.propertyPort.Appearance.Category.Options.UseFont = true;
            this.propertyPort.Appearance.Category.Options.UseForeColor = true;
            this.propertyPort.Appearance.CategoryExpandButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.propertyPort.Appearance.CategoryExpandButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.propertyPort.Appearance.CategoryExpandButton.ForeColor = System.Drawing.Color.Black;
            this.propertyPort.Appearance.CategoryExpandButton.Options.UseBackColor = true;
            this.propertyPort.Appearance.CategoryExpandButton.Options.UseBorderColor = true;
            this.propertyPort.Appearance.CategoryExpandButton.Options.UseForeColor = true;
            this.propertyPort.Appearance.Empty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.propertyPort.Appearance.Empty.Options.UseBackColor = true;
            this.propertyPort.Appearance.ExpandButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.propertyPort.Appearance.ExpandButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.propertyPort.Appearance.ExpandButton.ForeColor = System.Drawing.Color.Black;
            this.propertyPort.Appearance.ExpandButton.Options.UseBackColor = true;
            this.propertyPort.Appearance.ExpandButton.Options.UseBorderColor = true;
            this.propertyPort.Appearance.ExpandButton.Options.UseForeColor = true;
            this.propertyPort.Appearance.FocusedCell.BackColor = System.Drawing.Color.Yellow;
            this.propertyPort.Appearance.FocusedCell.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.propertyPort.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.propertyPort.Appearance.FocusedCell.Options.UseBackColor = true;
            this.propertyPort.Appearance.FocusedCell.Options.UseFont = true;
            this.propertyPort.Appearance.FocusedCell.Options.UseForeColor = true;
            this.propertyPort.Appearance.FocusedRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.propertyPort.Appearance.FocusedRecord.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.propertyPort.Appearance.FocusedRecord.Options.UseBackColor = true;
            this.propertyPort.Appearance.FocusedRecord.Options.UseFont = true;
            this.propertyPort.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.propertyPort.Appearance.FocusedRow.BackColor2 = System.Drawing.Color.Black;
            this.propertyPort.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.propertyPort.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.propertyPort.Appearance.FocusedRow.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.propertyPort.Appearance.FocusedRow.Options.UseBackColor = true;
            this.propertyPort.Appearance.FocusedRow.Options.UseFont = true;
            this.propertyPort.Appearance.FocusedRow.Options.UseForeColor = true;
            this.propertyPort.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.Gray;
            this.propertyPort.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.propertyPort.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.propertyPort.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.propertyPort.Appearance.HorzLine.BackColor = System.Drawing.Color.Black;
            this.propertyPort.Appearance.HorzLine.Options.UseBackColor = true;
            this.propertyPort.Appearance.ModifiedRecordValue.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.propertyPort.Appearance.ModifiedRecordValue.Options.UseFont = true;
            this.propertyPort.Appearance.RecordValue.BackColor = System.Drawing.Color.White;
            this.propertyPort.Appearance.RecordValue.ForeColor = System.Drawing.Color.Black;
            this.propertyPort.Appearance.RecordValue.Options.UseBackColor = true;
            this.propertyPort.Appearance.RecordValue.Options.UseForeColor = true;
            this.propertyPort.Appearance.RowHeaderPanel.BackColor = System.Drawing.Color.Gray;
            this.propertyPort.Appearance.RowHeaderPanel.ForeColor = System.Drawing.Color.White;
            this.propertyPort.Appearance.RowHeaderPanel.Options.UseBackColor = true;
            this.propertyPort.Appearance.RowHeaderPanel.Options.UseForeColor = true;
            this.propertyPort.Appearance.VertLine.BackColor = System.Drawing.Color.Black;
            this.propertyPort.Appearance.VertLine.Options.UseBackColor = true;
            this.propertyPort.Location = new System.Drawing.Point(14, 336);
            this.propertyPort.Name = "propertyPort";
            this.propertyPort.OptionsBehavior.ResizeHeaderPanel = false;
            this.propertyPort.OptionsBehavior.ResizeRowHeaders = false;
            this.propertyPort.OptionsBehavior.ResizeRowValues = false;
            this.propertyPort.OptionsBehavior.UseEnterAsTab = true;
            this.propertyPort.OptionsView.ShowRootCategories = false;
            this.propertyPort.Size = new System.Drawing.Size(435, 224);
            this.propertyPort.TabIndex = 25;
            this.propertyPort.CellValueChanging += new DevExpress.XtraVerticalGrid.Events.CellValueChangedEventHandler(this.propertyPort_CellValueChanging);
            // 
            // lbKantarPortu
            // 
            this.lbKantarPortu.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbKantarPortu.Location = new System.Drawing.Point(19, 231);
            this.lbKantarPortu.Name = "lbKantarPortu";
            this.lbKantarPortu.Size = new System.Drawing.Size(91, 14);
            this.lbKantarPortu.TabIndex = 24;
            this.lbKantarPortu.Text = "İletişim Portu :";
            // 
            // cbKantarPorts
            // 
            this.cbKantarPorts.Location = new System.Drawing.Point(119, 228);
            this.cbKantarPorts.Name = "cbKantarPorts";
            this.cbKantarPorts.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cbKantarPorts.Properties.Appearance.Options.UseFont = true;
            this.cbKantarPorts.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbKantarPorts.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbKantarPorts.Size = new System.Drawing.Size(99, 20);
            this.cbKantarPorts.TabIndex = 0;
            this.cbKantarPorts.SelectedIndexChanged += new System.EventHandler(this.cbKantarPorts_SelectedIndexChanged);
            // 
            // FrmPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 602);
            this.Controls.Add(this.grpPort);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPort";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPort_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.grpPort)).EndInit();
            this.grpPort.ResumeLayout(false);
            this.grpPort.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKantarSeriNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioKantar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbKantarTipi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.propertyPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbKantarPorts.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpPort;
        private DevExpress.XtraEditors.ComboBoxEdit cbKantarPorts;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.LabelControl lbKantarPortu;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyPort;
        private DevExpress.XtraEditors.SimpleButton btnTest;
        private DevExpress.XtraEditors.LabelControl lbKantarTipi;
        private DevExpress.XtraEditors.ComboBoxEdit cbKantarTipi;
        private DevExpress.XtraEditors.LabelControl lbKantarNo;
        private DevExpress.XtraEditors.RadioGroup radioKantar;
        private DevExpress.XtraEditors.LabelControl txtResultP1;
        private DevExpress.XtraEditors.LabelControl txtResultP2;
        private DevExpress.XtraEditors.LabelControl lbKantar2;
        private DevExpress.XtraEditors.LabelControl lbKantar1;
        private DevExpress.XtraEditors.CheckEdit checkPort;
        private DevExpress.XtraEditors.LabelControl lbKantarSeriNo;
        private DevExpress.XtraEditors.TextEdit txtKantarSeriNo;
    }
}