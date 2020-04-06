using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;


namespace KrmKantar2013
{
    public partial class FrmKargoPlani : XtraForm
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        // Report Object
        private RepKargoPlani m_RepKargoPlani = null;

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public FrmKargoPlani(DataTable p_TableResult, string p_Title)
        {
            // Initialize
            InitializeComponent();

            // Title
            this.lbTitle.Text = p_Title;

            // Create Report
            this.m_RepKargoPlani = new RepKargoPlani(p_TableResult);

            // Create DataSet
            DataSet ds = new DataSet();
            ds.Tables.Add(p_TableResult);

            // Set DataSource
            this.m_RepKargoPlani.DataSource = ds;

            // Set DataMember
            this.m_RepKargoPlani.DataMember = p_TableResult.TableName;

            // Printing System
            this.printControl.PrintingSystem = this.m_RepKargoPlani.PrintingSystem;

            // Generate 
            this.m_RepKargoPlani.CreateDocument();

            // Zoom To PageWidth
            this.m_RepKargoPlani.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToWholePage);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // Print Report
            this.m_RepKargoPlani.PrintReport();

            // Close
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void FrmReport_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC Close
            if (e.KeyCode == Keys.Escape) this.Close();

            // Control + ENTER
            else if ((e.Control) && (e.KeyCode == Keys.Enter))
            {
                // btnSave
                this.btnPrint_Click(null, null);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            // Print Dialog
            this.m_RepKargoPlani.ShowPrintDialog();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
    }
}