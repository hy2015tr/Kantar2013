using System;
using UsbLibrary;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;

using System.Linq;


namespace KrmKantar2013
{
    public partial class FrmUSB : XtraForm
    {
        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        public FrmUSB()
        {
            // Initalize
            InitializeComponent();

            // Quick Run
            timerUSB_Tick(null, null);

            // Start
            timerUSB.Start();

            // Load USB Settings
            alfaCfg.Load_USB_Settings();

            // Load Sensors
            this.Load_Sensors();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void Load_Sensors()
        {
            // Set Led Values
            cbSensor11.Text = alfaSensor.LED11;
            cbSensor22.Text = alfaSensor.LED22;
            cbSensor33.Text = alfaSensor.LED33;
            cbSensor44.Text = alfaSensor.LED44;
            cbSensor55.Text = alfaSensor.LED55;
            cbSensor66.Text = alfaSensor.LED66;
            cbSensor77.Text = alfaSensor.LED77;
            cbSensor88.Text = alfaSensor.LED88;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void Save_Sensors()
        {
            // Set Led Values
            alfaSensor.LED11 = cbSensor11.Text;
            alfaSensor.LED22 = cbSensor22.Text;
            alfaSensor.LED33 = cbSensor33.Text;
            alfaSensor.LED44 = cbSensor44.Text;
            alfaSensor.LED55 = cbSensor55.Text;
            alfaSensor.LED66 = cbSensor66.Text;
            alfaSensor.LED77 = cbSensor77.Text;
            alfaSensor.LED88 = cbSensor88.Text;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void FrmUSB_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC Close
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Save Sensors
                this.Save_Sensors();

                // Save Ports
                alfaCfg.Save_USB_Settings();

                // CursorDefault
                alfaMsg.CursorDefult();

                // Close
                this.Close();

            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex.Message);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close
            this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void timerUSB_Tick(object sender, EventArgs e)
        {
            // Set USB Status
            picUSB_ON.Visible = alfaUSB.Status;
            picUSB_OFF.Visible = !alfaUSB.Status;

            // Set Form Status
            pnMain.Enabled = alfaUSB.Status;

            // Set Button Status
            alfaCtrl.SetButton(btnSave, alfaUSB.Status);

            if (alfaUSB.Status && alfaUSB.Data != null)
            {
                // Set Sensor Leds
                stateIndicatorComponent11.StateIndex = alfaUSB.Data[1];
                stateIndicatorComponent22.StateIndex = alfaUSB.Data[2];
                stateIndicatorComponent33.StateIndex = alfaUSB.Data[3];
                stateIndicatorComponent44.StateIndex = alfaUSB.Data[4];
                stateIndicatorComponent55.StateIndex = alfaUSB.Data[5];
                stateIndicatorComponent66.StateIndex = alfaUSB.Data[6];
                stateIndicatorComponent77.StateIndex = alfaUSB.Data[7];
                stateIndicatorComponent88.StateIndex = alfaUSB.Data[8];

                // Set Color
                txtData11.ForeColor = Color.Lime;
                txtData22.ForeColor = Color.Lime;
                txtData33.ForeColor = Color.Lime;
                txtData44.ForeColor = Color.Lime;
                txtData55.ForeColor = Color.Lime;
                txtData66.ForeColor = Color.Lime;
                txtData77.ForeColor = Color.Lime;
                txtData88.ForeColor = Color.Lime;
            }

            else

            {
                // Set Sensor Leds
                stateIndicatorComponent11.StateIndex = alfaSensor.OFF;
                stateIndicatorComponent22.StateIndex = alfaSensor.OFF;
                stateIndicatorComponent33.StateIndex = alfaSensor.OFF;
                stateIndicatorComponent44.StateIndex = alfaSensor.OFF;
                stateIndicatorComponent55.StateIndex = alfaSensor.OFF;
                stateIndicatorComponent66.StateIndex = alfaSensor.OFF;
                stateIndicatorComponent77.StateIndex = alfaSensor.OFF;
                stateIndicatorComponent88.StateIndex = alfaSensor.OFF;

                // Set Color
                txtData11.ForeColor = Color.Silver;
                txtData22.ForeColor = Color.Silver;
                txtData33.ForeColor = Color.Silver;
                txtData44.ForeColor = Color.Silver;
                txtData55.ForeColor = Color.Silver;
                txtData66.ForeColor = Color.Silver;
                txtData77.ForeColor = Color.Silver;
                txtData88.ForeColor = Color.Silver;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//



    }
}