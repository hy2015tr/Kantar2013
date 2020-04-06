using System.Collections.Generic;
namespace KrmKantar2013
{
    partial class RepKargoPlani
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepKargoPlani));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.txtLocation = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.txtTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPageInfo = new DevExpress.XtraReports.UI.XRPageInfo();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.txtOperator = new DevExpress.XtraReports.UI.XRLabel();
            this.txtDateTime = new DevExpress.XtraReports.UI.XRLabel();
            this.lbApplication = new DevExpress.XtraReports.UI.XRLabel();
            this.txtVersion = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Detail.HeightF = 27.16669F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.StylePriority.UseFont = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // txtLocation
            // 
            this.txtLocation.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtLocation.LocationFloat = new DevExpress.Utils.PointFloat(112.6689F, 0F);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtLocation.SizeF = new System.Drawing.SizeF(139.8962F, 35.99991F);
            this.txtLocation.StylePriority.UseFont = false;
            this.txtLocation.Text = "[ Liman ]";
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 20F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 20F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox1.Image")));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(112.6689F, 35.99991F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // txtTitle
            // 
            this.txtTitle.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTitle.LocationFloat = new DevExpress.Utils.PointFloat(462.5F, 0F);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtTitle.SizeF = new System.Drawing.SizeF(214.3339F, 35.99991F);
            this.txtTitle.StylePriority.UseFont = false;
            this.txtTitle.Text = "KARGO PLANI";
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo,
            this.xrPictureBox1,
            this.txtLocation,
            this.txtTitle});
            this.PageHeader.HeightF = 59.22429F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrPageInfo
            // 
            this.xrPageInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.xrPageInfo.LocationFloat = new DevExpress.Utils.PointFloat(1050.726F, 10.00001F);
            this.xrPageInfo.Name = "xrPageInfo";
            this.xrPageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo.SizeF = new System.Drawing.SizeF(68.27429F, 23F);
            this.xrPageInfo.StylePriority.UseFont = false;
            this.xrPageInfo.StylePriority.UseTextAlignment = false;
            this.xrPageInfo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.txtOperator,
            this.txtDateTime,
            this.lbApplication,
            this.txtVersion});
            this.ReportFooter.HeightF = 108.9876F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // txtOperator
            // 
            this.txtOperator.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtOperator.LocationFloat = new DevExpress.Utils.PointFloat(931.6182F, 77.88747F);
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtOperator.SizeF = new System.Drawing.SizeF(187.3818F, 17.99991F);
            this.txtOperator.StylePriority.UseFont = false;
            this.txtOperator.StylePriority.UseTextAlignment = false;
            this.txtOperator.Text = "Operator";
            this.txtOperator.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txtDateTime
            // 
            this.txtDateTime.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtDateTime.LocationFloat = new DevExpress.Utils.PointFloat(931.6182F, 58.88755F);
            this.txtDateTime.Name = "txtDateTime";
            this.txtDateTime.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtDateTime.SizeF = new System.Drawing.SizeF(187.3818F, 17.99991F);
            this.txtDateTime.StylePriority.UseFont = false;
            this.txtDateTime.StylePriority.UseTextAlignment = false;
            this.txtDateTime.Text = "09/10/2012 18:00:00";
            this.txtDateTime.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // lbApplication
            // 
            this.lbApplication.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbApplication.LocationFloat = new DevExpress.Utils.PointFloat(931.6182F, 40.88758F);
            this.lbApplication.Name = "lbApplication";
            this.lbApplication.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbApplication.SizeF = new System.Drawing.SizeF(119.1073F, 17.99997F);
            this.lbApplication.StylePriority.UseFont = false;
            this.lbApplication.StylePriority.UseTextAlignment = false;
            this.lbApplication.Text = "Kantar2013";
            this.lbApplication.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // txtVersion
            // 
            this.txtVersion.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtVersion.LocationFloat = new DevExpress.Utils.PointFloat(1051.725F, 40.88764F);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.txtVersion.SizeF = new System.Drawing.SizeF(67.27441F, 17.99994F);
            this.txtVersion.StylePriority.UseFont = false;
            this.txtVersion.StylePriority.UseTextAlignment = false;
            this.txtVersion.Text = "v1.0.0.0";
            this.txtVersion.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // RepKargoPlani
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.ReportFooter});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20);
            this.PageHeight = 827;
            this.PageWidth = 1169;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ShowPrintMarginsWarning = false;
            this.ShowPrintStatusDialog = false;
            this.Version = "12.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel txtLocation;
        private DevExpress.XtraReports.UI.XRLabel txtTitle;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel lbApplication;
        private DevExpress.XtraReports.UI.XRLabel txtVersion;
        private DevExpress.XtraReports.UI.XRLabel txtDateTime;
        private DevExpress.XtraReports.UI.XRLabel txtOperator;
    }
}
