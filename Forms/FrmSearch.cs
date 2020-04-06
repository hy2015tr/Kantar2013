using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;


namespace KrmKantar2013
{
    public partial class FrmSearch : DevExpress.XtraEditors.XtraForm
    {
        //-------------------------------------------------------------------------------------------------------------//

        // FieldName
        private string m_FieldName = null;
        
        // Result
        public string ResultValue { get; set; }

        //-------------------------------------------------------------------------------------------------------------//

        public FrmSearch(string p_FieldName, string p_SearchValue)
        {
            // Initalize
            InitializeComponent();

            // Set FieldName
            this.m_FieldName = p_FieldName;

            // Set SearchValue
            this.txtSecim.Text = p_SearchValue;

            // Set Name
            this.Text = this.m_FieldName;

            // Clear
            this.btnClear_Click(null, null);

            // List
            this.btnList_Click(null, null);
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear
            if (sender != null) txtSecim.Text = string.Empty;

            // DateTime
            DateTime p_Now = DateTime.Now;
            p_Now = new DateTime(p_Now.Year, p_Now.Month, p_Now.Day, 0, 0, 0);

            // Reset DateTime
            txtDate01.DateTime = p_Now.AddMonths(-1);
            txtDate02.DateTime = p_Now.AddDays(1);

            // Reset GridView
            grdListView.Columns.Clear();

            // Reset Datasource
            grdList.DataSource = null;

            // Focus
            txtSecim.Focus();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close
            this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC Close
            if (e.KeyCode == Keys.Escape) this.Close();

            // Control + ENTER
            else if ((e.Control) && (e.KeyCode == Keys.Enter)) this.btnList_Click(null, null);
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnList_Click(object sender, EventArgs e)
        {
            try
            {
                // Cursor
                alfaMsg.CursorWait();

                // Get Result
                grdList.DataSource = alfaEntity.SearchHelp(txtSecim.Text, this.m_FieldName, txtDate01.DateTime, txtDate02.DateTime);

                // Set View
                alfaGrid.SetView(grdListView);

                // Cursor
                alfaMsg.CursorDefult();

            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
       }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmSearch_Shown(object sender, EventArgs e)
        {
            // Focus
            txtSecim.Focus();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void txtSecim_KeyUp(object sender, KeyEventArgs e)
        {
            // Enter
            if (e.KeyCode == Keys.Enter) this.btnList_Click(null, null);
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void grdListView_DoubleClick(object sender, EventArgs e)
        {

            // Check
            if (grdListView.DataSource == null || grdListView.RowCount == 0) return;

            // Check
            if (grdListView.FocusedRowHandle < 0) return;

            // Get ID
            this.ResultValue = (string)grdListView.GetRowCellValue(grdListView.FocusedRowHandle, this.m_FieldName);

            // Set Result
            this.DialogResult = DialogResult.OK;
        }

        //-------------------------------------------------------------------------------------------------------------//
    }
}