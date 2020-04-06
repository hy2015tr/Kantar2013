using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;


namespace KrmKantar2013 
{
    public partial class RepKargoPlani : XtraReport
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public RepKargoPlani(DataTable p_TableResult)
        {
            // Initialize
            InitializeComponent();

            // DateTime
            txtDateTime.Text = DateTime.Now.ToString(alfaDate.DTFormat);

            // Version
            txtVersion.Text = alfaVer.GetAppVersion();

            // Operator
            txtOperator.Text = alfaSession.FullName;

            // Create rowHeader
            DataRow rowHeader = p_TableResult.NewRow();

            foreach (DataColumn col in p_TableResult.Columns)
            {
                // Assign Text
                rowHeader[col.ColumnName] = col.ColumnName;
            }

            // Add to Table
            p_TableResult.Rows.InsertAt(rowHeader, 0);

            //========================================== Create XRTable ========================================//

            int p_Padding = 10;
            int p_TableWidth = this.PageWidth - this.Margins.Left - this.Margins.Right - p_Padding * 2;
            int p_ColWidth = p_TableWidth / p_TableResult.Columns.Count;

            // Create Dynamic XRTable
            XRTable repTable = XRTable.CreateTable(new Rectangle(p_Padding, 2, p_TableWidth, 40), 1, 0);

            repTable.BorderWidth = 2;
            repTable.Width = p_TableWidth;
            repTable.Rows.FirstRow.Width = p_TableWidth;
            repTable.Font = new Font("Tahoma", 10, FontStyle.Bold);
            repTable.Borders = DevExpress.XtraPrinting.BorderSide.All;

            // Begin
            repTable.BeginInit();

            foreach (DataColumn col in p_TableResult.Columns)
            {
                // Create Cell
                XRTableCell cell = new XRTableCell();

                // Binding
                XRBinding binding = new XRBinding("Text", this.DataSource, col.ColumnName);

                // Cell Properties
                cell.TextAlignment = TextAlignment.MiddleCenter;
                cell.DataBindings.Add(binding);
                cell.Text = col.ColumnName;
                cell.Width = p_ColWidth;
                cell.Multiline = true;
                cell.CanShrink = false;
                cell.CanGrow = false;

                // Add Cell
                repTable.Rows.FirstRow.Cells.Add(cell);
            }

            // Add to Detail
            Detail.Controls.Add(repTable);

            repTable.BeforePrint += new System.Drawing.Printing.PrintEventHandler(repTable_BeforePrint);

            // Adjust
            repTable.AdjustSize();

            // Begin
            repTable.EndInit();



            //========================================== Create XRTable ========================================//
        }
        
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        void repTable_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //throw new NotImplementedException();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public void PrintReport()
        {
            // Print
            this.Print();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public void ShowPrintDialog()
        {
            // Print
            this.PrintDialog();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

    }
}
