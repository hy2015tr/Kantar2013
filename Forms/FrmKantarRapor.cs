using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;

namespace KrmKantar2013
{
    public partial class FrmKantarRapor : XtraForm
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        // Report Object
        private RepKantarLiman m_KantarRaporLiman = null;
        private RepKantarMerkez m_KantarRaporMerkez = null;

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public FrmKantarRapor(List<TableAracArsiv> p_ListArac)
        {
            // Initialize
            InitializeComponent();

            if (alfaSession.Liman)
            {
                // Create Report
                this.m_KantarRaporLiman = new RepKantarLiman(p_ListArac);

                // Set DataSource
                this.m_KantarRaporLiman.DataSource = p_ListArac;

                // Printing System
                this.printControl.PrintingSystem = this.m_KantarRaporLiman.PrintingSystem;

                // Generate 
                this.m_KantarRaporLiman.CreateDocument();

                // Zoom To PageWidth
                this.m_KantarRaporLiman.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToWholePage);
            }
            else
            {
                // Create Report
                this.m_KantarRaporMerkez = new RepKantarMerkez(p_ListArac);
            
                // Set DataSource
                this.m_KantarRaporMerkez.DataSource = p_ListArac;

                // Printing System
                this.printControl.PrintingSystem = this.m_KantarRaporMerkez.PrintingSystem;

                // Generate 
                this.m_KantarRaporMerkez.CreateDocument();

                // Zoom To PageWidth
                this.m_KantarRaporMerkez.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToWholePage);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (alfaSession.Liman)
            {
                // Print Report
                this.m_KantarRaporLiman.PrintReport();
            }
            else
            {
                // Print Report
                this.m_KantarRaporMerkez.PrintReport();
            }

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
            if (alfaSession.Liman)
            {
                // Print Report
                this.m_KantarRaporLiman.ShowPrintDialog();
            }
            else
            {
                // Print Report
                this.m_KantarRaporMerkez.ShowPrintDialog();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
    }
}