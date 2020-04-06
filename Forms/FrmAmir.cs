using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;


namespace KrmKantar2013
{
    public partial class FrmAmir : DevExpress.XtraEditors.XtraForm
    {

        //-------------------------------------------------------------------------------------------------------------//

        public FrmAmir()
        {
            // Initalize
            InitializeComponent();

            // Set UserName
            txtUserName.Text = "AMIR";

            // Disable UserName
            alfaCtrl.ControlDisable(txtUserName, Color.LightGray);
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmUser_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC Close
            if (e.KeyCode == Keys.Escape) this.Close();

            // Control + ENTER
            else if ((e.Control) && (e.KeyCode == Keys.Enter))
            {
                if (btnOnay.Enabled)
                {
                    // btnOnay
                    this.btnOnay_Click(null, null);
                }
            }
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmArac_Shown(object sender, EventArgs e)
        {
            // Focus
            txtPassword.Focus();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnOnay_Click(object sender, EventArgs e)
        {
            if (alfaEntity.CheckUser(txtUserName.Text, txtPassword.Text))
            {
                // Result
                this.DialogResult = DialogResult.OK;

                // Close
                this.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void txtALL_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPassword.Text.Length > 0)
            {
                // Disable btnChange
                alfaCtrl.ButtonEnable(btnOnay);

                // BtnOnay Click
                if (e.KeyCode == Keys.Enter) this.btnOnay_Click(null, null);
            }
            else
            {
                // Disable btnChange
                alfaCtrl.ButtonDisable(btnOnay);
            }
        }

        //-------------------------------------------------------------------------------------------------------------//
    }
}