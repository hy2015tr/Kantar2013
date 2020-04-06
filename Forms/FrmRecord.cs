using System;
using System.IO.Ports;
using System.Data.Objects;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections.Generic;


namespace KrmKantar2013
{
    public partial class FrmRecord : DevExpress.XtraEditors.XtraForm
    {

        //-------------------------------------------------------------------------------------------------------------//

        public FrmRecord(string p_Title, Object p_Object, string p_ReadOnlyFields, string p_HiddenFields)
        {
            // Initalize
            InitializeComponent();

            // Set Title
            txtTabloAdi.Text = p_Title;

            // Set Object
            alfaVGrid.SetPropertyGridV2(propGrid, p_Object);

            // ReadOnly Fields
            if (p_ReadOnlyFields != null) alfaVGrid.RowReadOnly(propGrid, p_ReadOnlyFields);

            // InVisible Fields
            if (p_HiddenFields != null) alfaVGrid.RowHide(propGrid, p_HiddenFields);
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmPort_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC Close
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Set Result
            this.DialogResult = DialogResult.OK;
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmArac_Shown(object sender, EventArgs e)
        {
            // Focus
            propGrid.Focus();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void propGrid_KeyUp(object sender, KeyEventArgs e)
        {
            // Control + ENTER
            if (e.Control==true && e.KeyCode == Keys.Enter)
            {
                this.btnSave_Click(null, null);
            }
        }

        //-------------------------------------------------------------------------------------------------------------//
    }
}