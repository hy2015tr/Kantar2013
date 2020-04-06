using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;


namespace KrmKantar2013
{
    public partial class FrmPort : DevExpress.XtraEditors.XtraForm
    {
        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private alfaKntr m_K1 = null;
        private alfaKntr m_K2 = null;
        private alfaKntr m_S1 = null;


        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        public FrmPort(alfaKntr p_K1, alfaKntr p_K2, alfaKntr p_S1)
        {
            // Initalize
            InitializeComponent();

            // Set Buttons
            alfaCtrl.ButtonEnable(btnTest);
            alfaCtrl.ButtonDisable(btnClose);

            // Reset Message
            alfaCtrl.SetResult(txtResultP1, string.Empty, alfaResult.None);
            alfaCtrl.SetResult(txtResultP2, string.Empty, alfaResult.None);

            // Assign Values
            this.m_K1 = p_K1;
            this.m_K2 = p_K2;
            this.m_S1 = p_S1;

            // Add Kantar Objects
            radioKantar.Properties.Items[0].Value = p_K1;
            radioKantar.Properties.Items[1].Value = p_K2;

            // Add Ports
            this.Add_Ports_to_Combobox();

            // Set First
            radioKantar.SelectedIndex = 0;

            // Rows ReadOnly
            alfaVGrid.RowReadOnly(propertyPort, "PortName");
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void Add_Ports_to_Combobox()
        {
            // Clear Items
            cbKantarPorts.Properties.Items.Clear();

            // Add Kantar Ports    
            cbKantarPorts.Properties.Items.Add(this.m_K1.m_Port);
            cbKantarPorts.Properties.Items.Add(this.m_K2.m_Port);
            cbKantarPorts.Properties.Items.Add(this.m_S1.m_Port);

            // Port Names
            string strPorts = this.m_K1.m_Port.GetPortName + "-" + this.m_K2.m_Port.GetPortName + "-" + this.m_S1.m_Port.GetPortName;

            // Add Other Ports
            foreach (string strPortName in SerialPort.GetPortNames())
            {
                if (!strPorts.Contains(strPortName))
                {
                    // Create Port
                    var portTemp = new alfaPort(new SerialPort(strPortName));

                    // Add Combobox
                    cbKantarPorts.Properties.Items.Add(portTemp);
                }
            }
        }
        
        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void FrmPort_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC Close
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void btnTest_Click(object sender, EventArgs e)
        {
            // Cursor
            alfaMsg.CursorWait();

            try
            {
                // Test Ports
                string str1 = this.m_K1.m_Port.Open(this.m_K1.m_Aktif);
                string str2 = this.m_K2.m_Port.Open(this.m_K2.m_Aktif);
                string str3 = this.m_S1.m_Port.Open(this.m_S1.m_Aktif);

                // Set Status
                this.m_K1.SetStatus(str1);
                this.m_K2.SetStatus(str2);
                this.m_S1.SetStatus(str3);

                // Check Result                
                if (str1 == "OK")
                     alfaCtrl.SetResult(txtResultP1, String.Format("{0} : OK", this.m_K1.m_Port.GetPortName), alfaResult.Pass);
                else alfaCtrl.SetResult(txtResultP1, str1, alfaResult.Fail);

                // Check Result                
                if (str2 == "OK")
                     alfaCtrl.SetResult(txtResultP2, String.Format("{0} : OK", this.m_K2.m_Port.GetPortName), alfaResult.Pass);
                else alfaCtrl.SetResult(txtResultP2, str2, alfaResult.Fail);

            }

            finally
            {
                // Focus
                grpPort.Focus();

                // Cursor
                alfaMsg.CursorDefult();

                // Close Ports
                this.m_K1.m_Port.Close();
                this.m_K2.m_Port.Close();
                this.m_S1.m_Port.Close();

                // Set Buttons
                alfaCtrl.ButtonEnable(btnClose, "Dark Side");
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Save Ports
                alfaCfg.Save_PORT_Settings(this.m_K1);
                alfaCfg.Save_PORT_Settings(this.m_K2);
                alfaCfg.Save_PORT_Settings(this.m_S1);

                // CursorDefault
                alfaMsg.CursorDefult();

            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void propertyPort_CellValueChanging(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            // Disable btnSave
            alfaCtrl.ButtonDisable(btnClose);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void radioKantar_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Disable btnClose
            alfaCtrl.ButtonDisable(btnClose);

            // Get Kantar
            var p_Kantar = (alfaKntr)radioKantar.Properties.Items[radioKantar.SelectedIndex].Value;

            // Set Kantar Tipi
            cbKantarTipi.Text = p_Kantar.m_PortKntr;

            // Set ComPort
            cbKantarPorts.SelectedIndex = cbKantarPorts.Properties.Items.IndexOf(p_Kantar.m_Port);

            // Set propertyPort
            propertyPort.SelectedObject = p_Kantar.m_Port.GetPort;

            // Aktif
            checkPort.Checked = p_Kantar.m_Aktif;

            // Set SeriNo
            txtKantarSeriNo.Text = p_Kantar.m_KantarSeriNo;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void cbKantarAdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Disable btnClose
            alfaCtrl.ButtonDisable(btnClose);

            // Get Kantar Adi
            var p_Kantar = (alfaKntr)radioKantar.Properties.Items[radioKantar.SelectedIndex].Value;

            // Set Kantar Tipi
            cbKantarTipi.Text = p_Kantar.m_PortKntr;

            // Set ComPort
            cbKantarPorts.SelectedIndex = cbKantarPorts.Properties.Items.IndexOf(p_Kantar.m_Port);

            // Set propertyPort
            propertyPort.SelectedObject = p_Kantar.m_Port.GetPort;

            // Set Aktif
            checkPort.Checked = p_Kantar.m_Aktif;

            // Set Kantar SeriNo
            txtKantarSeriNo.Text = p_Kantar.m_KantarSeriNo;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void cbKantarTipi_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Disable btnClose
            alfaCtrl.ButtonDisable(btnClose);

            // Get Kantar
            var p_Kantar = (alfaKntr)radioKantar.Properties.Items[radioKantar.SelectedIndex].Value;
        
            // Set Kantar Tipi
            p_Kantar.m_PortKntr = cbKantarTipi.Text;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void cbKantarPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Disable btnClose
            alfaCtrl.ButtonDisable(btnClose);

            // Get Kantar Adi
            var p_Kantar = (alfaKntr)radioKantar.Properties.Items[radioKantar.SelectedIndex].Value;

            // Set Kantar Port
            p_Kantar.m_Port = (alfaPort)cbKantarPorts.SelectedItem;

            // Set propertyPort
            propertyPort.SelectedObject = p_Kantar.m_Port.GetPort;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void checkPort_EditValueChanged(object sender, EventArgs e)
        {
            // Get Kantar
            var p_Kantar = (alfaKntr)radioKantar.Properties.Items[radioKantar.SelectedIndex].Value;

            // Set Aktif
            p_Kantar.m_Aktif = checkPort.Checked;

            // Kantar-I && Kantar-II
            if (radioKantar.SelectedIndex == 0 || radioKantar.SelectedIndex == 1)
            {
                // Set Button Visibility
                p_Kantar.m_Button.Visible = p_Kantar.m_Aktif;

                // Set LED Visibility
                p_Kantar.m_Gauge_DIG_Live.Visible = p_Kantar.m_Aktif;

                // Set SEN Visibility
                p_Kantar.m_Gauge_S1.Visible = p_Kantar.m_Aktif;
                p_Kantar.m_Gauge_S2.Visible = p_Kantar.m_Aktif;
                p_Kantar.m_Gauge_S3.Visible = p_Kantar.m_Aktif;

                // Set Kantar Panel
                if (checkPort.Checked)
                     p_Kantar.m_Panel.BorderStyle = BorderStyles.NoBorder;
                else p_Kantar.m_Panel.BorderStyle = BorderStyles.Flat;
            }

            // Close Port
            if (!p_Kantar.m_Aktif) p_Kantar.m_Port.Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void txtKantarSeriNo_EditValueChanged(object sender, EventArgs e)
        {
            // Get Kantar
            var p_Kantar = (alfaKntr)radioKantar.Properties.Items[radioKantar.SelectedIndex].Value;

            // Set Kantar SeriNo
            p_Kantar.m_KantarSeriNo = txtKantarSeriNo.Text;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//
    }
}