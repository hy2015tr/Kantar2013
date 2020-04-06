using System;
using System.Linq;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.EntityClient;


namespace KrmKantar2013
{
    public partial class FrmLocation : DevExpress.XtraEditors.XtraForm
    {
        //-------------------------------------------------------------------------------------------------------------//

        public FrmLocation()
        {
            // Initalize
            InitializeComponent();

            // SelectedItem
            if (alfaSession.Liman) radioLocation.SelectedIndex = 1; else radioLocation.SelectedIndex = 0;
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save
            alfaCfg.Save_LOC_Settings(radioLocation.SelectedIndex == 1);

            // Restart
            Application.Restart();
        }

        //-------------------------------------------------------------------------------------------------------------//
    }
}