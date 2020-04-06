using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Data.Objects.DataClasses;


namespace KrmKantar2013
{
    public partial class FrmYanLiman : DevExpress.XtraEditors.XtraForm
    {
        //-------------------------------------------------------------------------------------------------------------//

        private TableAracAktif m_Aktif = null;

        //-------------------------------------------------------------------------------------------------------------//

        public FrmYanLiman(alfaSession p_Session, TableAracAktif p_Aktif)
        {
            // Initalize
            InitializeComponent();

            // Set Entity
            this.m_Aktif = p_Aktif;

            // Enable Editing
            this.SetTartimStatus(txtT01, true);
            this.SetTartimStatus(txtT02, true);

            // Set Text
            txtPlakaNo.Text = p_Aktif.PlakaNo;
            txtMalzeme.Text = p_Aktif.Malzeme;
            txtSapFisNo.Text = p_Aktif.SapFisNo;
            txtT01.Text = Convert.ToString(p_Aktif.YLTartim1);
            txtT02.Text = Convert.ToString(p_Aktif.YLTartim2);
        } 

        //-------------------------------------------------------------------------------------------------------------//

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close
            this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmTartim_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC Close
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            // Control + ENTER
            else if (e.Control == true && e.KeyCode == Keys.Enter)
            {
                // btnSave
                this.btnSave_Click(null, null);
            }
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Set Flag
            this.m_Aktif.YL = true;

            // Tartimlar
            this.m_Aktif.YLTartim1 = Convert.ToInt32(txtT01.EditValue);
            this.m_Aktif.YLTartim2 = Convert.ToInt32(txtT02.EditValue);
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmTartim_Shown(object sender, EventArgs e)
        {
            // Focus
            lbTitle.Focus();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void SetTartimStatus(SpinEdit p_Edit, bool p_Status)
        {
            if (p_Status)
            {
                // Enable Manual Tartim
                p_Edit.ForeColor = Color.Red;
                p_Edit.Properties.ReadOnly = false;
            }
            else
            {
                // Disable Manual Tartim
                p_Edit.ForeColor = Color.Aqua;
                p_Edit.Properties.ReadOnly = true;
            }
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void txtALL_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                // Get Values
                int T01 = Convert.ToInt32(txtT01.EditValue);
                int T02 = Convert.ToInt32(txtT02.EditValue);
                
                // Calculate Net Value
                int NET = Math.Abs(T01 - T02);

                // Set Net Value
                txtNet.EditValue = NET;
            }
            catch 
            {  
                // EMPTY
            }
        }

        //-------------------------------------------------------------------------------------------------------------//

    }
}