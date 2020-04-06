using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using System.Data.Objects.DataClasses;

namespace KrmKantar2013
{
    public partial class FrmKantarFisi : XtraForm
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        // Report Object
        private RepKantarFisiV1 m_Report_V1 = null;
        private RepKantarFisiV2 m_Report_V2 = null;


        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public FrmKantarFisi(EntityObject p_EntityObject, string p_Title)
        {
            // Initialize
            InitializeComponent();

            // Title
            this.lbTitle.Text = p_Title;

            // Create Report
            this.m_Report_V1 = new RepKantarFisiV1(p_EntityObject, p_Title);
            this.m_Report_V2 = new RepKantarFisiV2(p_EntityObject, p_Title);

            // CreateDocument
            this.CreateDocument();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void CreateDocument()
        {
            // Bind to Report Control
            if (this.checkFastMode.Checked)
            {
                // DOS Mode (Fast)
                this.printControl.PrintingSystem = this.m_Report_V2.PrintingSystem;

                // Generate 
                this.m_Report_V2.CreateDocument();

                // Zoom To PageWidth
                this.m_Report_V2.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToWholePage);
            }
            else
            {
                // WIN Mode (Slow)
                this.printControl.PrintingSystem = this.m_Report_V1.PrintingSystem;

                // Generate 
                this.m_Report_V1.CreateDocument();

                // Zoom To PageWidth
                this.m_Report_V1.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToWholePage);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.checkFastMode.Checked)
            {
                // FAST
                this.m_Report_V2.PrintReport();
            }
            else
            {
                // SLOW
                this.m_Report_V1.PrintReport();
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

        private void checkFastMode_CheckedChanged(object sender, EventArgs e)
        {
            // CreateDocument
            this.CreateDocument();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            // Print Dialog
            if (this.checkFastMode.Checked)
            {
                // FAST
                this.m_Report_V2.ShowPrintDialog();
            }
            else
            {
                // SLOW
                this.m_Report_V1.ShowPrintDialog();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
    }
}