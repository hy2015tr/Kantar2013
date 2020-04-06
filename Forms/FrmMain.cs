
#region //-----------Using List----------//

using System;
using System.IO;
using System.Xml;
using UsbLibrary;
using System.Text;
using System.Data;
using System.Linq;
using System.Drawing;
using DevExpress.Data;
using System.IO.Ports;
using System.Threading;
using KrmKantar2013.WR;
using DevExpress.Utils;
using DevExpress.XtraTab;
using System.Diagnostics;
using System.Data.Objects;
using DevExpress.XtraBars;
using System.Globalization;
using System.Configuration;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Collections.Generic;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraGrid.Views.Base;
using System.Text.RegularExpressions;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.Objects.DataClasses;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGauges.Core.Model;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGauges.Core.Drawing;
using DevExpress.XtraGauges.Win.Gauges.State;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGauges.Win.Gauges.Digital;


#endregion    


namespace KrmKantar2013
{
    public partial class FrmMain : XtraForm
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        #region //---Member Fields---//

        // ESIT Start Bytes
        private byte[] m_EsitBytes = new byte[] { 0x40, 0x41, 0x42, 0x43 };

        // Splash Form
        private static FrmSplash m_frmSplash = new FrmSplash();

        // Session Info
        private alfaSession m_Session = new alfaSession();

        // Active Grid
        private GridView m_grdViewActive = null;

        // Kantar
        private alfaKntr KANTAR1 = null;
        private alfaKntr KANTAR2 = null;
        private alfaKntr SENSOR1 = null;

        // TabKey
        bool m_IsTabKeyPressed = false;
 
        #endregion

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public FrmMain()
        {
            // Show Splash
            m_frmSplash.Show();
            m_frmSplash.Update();

            // Initializing
            this.InitializeComponent();

            // Set LookAndFeel
            defaultLookAndFeel.LookAndFeel.SkinName = alfaCfg.Load_LookAndFeel();

            #if DEBUG
                // TEST
                radioSAP.SelectedIndex = 1;
                btnTest.Visible = true;
            #else
                // PROD
                radioSAP.SelectedIndex = 0;
                btnTest.Visible = false;
            #endif

            // Systems
            radioSAP.Properties.Items[0].Enabled = (radioSAP.SelectedIndex == 0);
            radioSAP.Properties.Items[1].Enabled = (radioSAP.SelectedIndex == 1);
            radioSAP.Properties.Items[2].Enabled = false;

            // Set Version
            this.lbVersion.Text = this.m_Session.AppVer;

            // Set Merkez / Liman
            this.SetAppLocationText();

            // Sensors OFF
            this.DisableSensors();

            // Brin Login
            pnMain.Hide();
            pnLogin.Show();

            // Set Mask
            this.Set_Mask_TextInputs();

            // Clear LoginPage
            this.btnClearLogin_Click(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void SetAppLocationText()
        {
            // Set Liman
            if (alfaCfg.CheckLiman())
            {
                // Liman True
                alfaSession.Liman = true;

                // Color
                lbLocation.ForeColor = Color.Maroon;

                // Location    
                lbLocation.Text = string.Format("({0})", "LIMAN");

                // Gumruk Modu
                alfaSession.Gumruk = (alfaSession.Lokasyon.ToUpper() == "GUMRUK");
            }
            else
            {
                // Liman False
                alfaSession.Liman = false;

                // Color
                lbLocation.ForeColor = Color.RoyalBlue;

                // Location    
                lbLocation.Text = string.Format("({0})", alfaSession.Lokasyon);
            }

#if DEBUG

                // Color
                lbLocation.ForeColor = Color.Red;

                // Location    
                lbLocation.Text += " TEST";

#endif

        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Load_UserName_from_AppSettings()
        {
            // Get UserName
            object p_Username = KrmKantar2013.Properties.Settings.Default.UserName;

            if (p_Username != null)
            {
                // Set UserName
                txtUserName.Text = p_Username.ToString();

                // Focus Password
                txtPassword.Focus();
            }
            // Focus UserName
            else txtUserName.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Set_Status_Fields()
        {
            try
            {
                // Satus Left Items
                statusSQL.Caption = String.Format("SQL : {0}", this.m_Session.DB);
                statusName.Caption = String.Format("PC : {0}", this.m_Session.PC);
                statusIP.Caption = String.Format("IP : {0}", this.m_Session.LocIP);
                statusNet.Caption = String.Format("NET : {0}", this.m_Session.NetVer);

                // Set Ports
                statusPortK1.Caption = String.Format("{0} ({1})", this.KANTAR1.m_PortKntr, this.KANTAR1.m_Port.GetPortName);
                statusPortK2.Caption = String.Format("{0} ({1})", this.KANTAR2.m_PortKntr, this.KANTAR2.m_Port.GetPortName);
                statusPortS1.Caption = String.Format("SENSOR {0} ({1})", this.SENSOR1.m_PortKntr, this.SENSOR1.m_Port.GetPortName);
                
                // Set User
                statusUser.Caption = String.Format("{0}", this.m_Session.User);

                // Get Selected Client
                string p_Client = radioSAP.Properties.Items[radioSAP.SelectedIndex].ToString();

                // Set Client Status
                statusMode.Caption = this.m_Session.GetSAP(p_Client);

                // Admin Profile
                if (alfaSession.Admin) statusUser.Caption += " (Admin)";

                // Set App Version
                this.lbVersion.Text = this.m_Session.AppVer;

            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Set_DesenLookup(bool p_Status)
        {
            // Clear 
            txtDesen.EditValue = null;

            // Desen
            txtDesen.Properties.DataSource = null;
            txtDesen.Properties.DataSource = alfaEntity.Desen_GetList().Select(tt => new { tt.Desen }).ToList();
            txtDesen.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            txtDesen.Properties.SearchMode = SearchMode.AutoComplete;
            txtDesen.Properties.DisplayMember = "Desen";
            txtDesen.Properties.ValueMember = "Desen";
            txtDesen.Properties.SortColumnIndex = 0;

            // Set Status
            if (p_Status) 
                 alfaCtrl.ControlEnable(txtDesen, Color.LightCyan);
            else alfaCtrl.ControlDisable(txtDesen, Color.LightGray);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void SetDefaultLook(DevExpress.XtraBars.BarButtonItem p_MenuItem)
        {
            // Add Check to ViewMenuItems
            foreach (Object Item in barMenu.Manager.Items)
            {
                if (Item.GetType().ToString() == "DevExpress.XtraBars.BarButtonItem")
                {
                    // Get Item
                    BarButtonItem obj = (BarButtonItem)Item;

                    if (obj.Name.Contains("Skin"))
                    {
                        // Set Properties
                        obj.ButtonStyle = BarButtonStyle.Check;
                        obj.AllowAllUp = true;
                        obj.GroupIndex = 1;
                    }
                }
            }

            // Set Default LookAndFeel
            defaultLookAndFeel.LookAndFeel.SkinName = p_MenuItem.Caption;
            p_MenuItem.Down = true;

            // Save to Config
            alfaCfg.Save_LookAndFeel(p_MenuItem.Caption);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menuExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menuAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Show Splash
            m_frmSplash = new FrmSplash();
            m_frmSplash.ShowDialog();

            //Hide Splash
            m_frmSplash.Close();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            // Update
            this.Update();

            // Close Splash
            m_frmSplash.Close();

            // Load UserName
            Load_UserName_from_AppSettings();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //TODO
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void ViewItemALL_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Set Default
            this.SetDefaultLook((BarButtonItem)e.Item);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (alfaEntity.Login(txtUserName.Text, txtPassword.Text, ref this.m_Session)) // Check Login
            {
                // Gumruk Modu
                if ("KOLCU-MUHAFAZA".Contains(alfaSession.UserName)) alfaSession.Gumruk = true;

                // Hide
                tabControlMain.Hide();

                // Load Variants
                this.GridView_Variant(true);

                // Refresh ClientInfo
                this.m_Session.User = txtUserName.Text;
                this.m_Session.RefreshLoginDateTime();

                // Save UserName
                KrmKantar2013.Properties.Settings.Default.UserName = txtUserName.Text;
                KrmKantar2013.Properties.Settings.Default.Save();

                // SAP Connection
                alfaCfg.Load_SAP_Settings(radioSAP.SelectedIndex);

                // Create Kantars
                this.KANTAR1 = new alfaKntr(new alfaPort(PortK1), "K1", statusDataK1, LOGK1, LEDK1_Live, LEDK1_Test, gaugeLED1, gaugeK1S1, gaugeK1S2, gaugeK1S3, btnKantarK1, pnKantar1);
                this.KANTAR2 = new alfaKntr(new alfaPort(PortK2), "K2", statusDataK2, LOGK2, LEDK2_Live, LEDK2_Test, gaugeLED2, gaugeK2S1, gaugeK2S2, gaugeK2S3, btnKantarK2, pnKantar2);

                // Create Sensor
                this.SENSOR1 = new alfaKntr(new alfaPort(PortS1), "S1", statusDataS1, LOGK2, null, null, null, null, null, null, null, null);

                // Disable ESIT Sensor
                this.SENSOR1.m_Aktif = false;

                // Admin Tables
                this.Admin_Tables_Items();
                this.Admin_Tables_Refresh();
                listAdminTables.SelectedIndex = 0;

                // Page Visibility
                pageGemi01.PageVisible = alfaSession.Liman;
                pageGemi02.PageVisible = alfaSession.Liman;
                pageYedek.PageVisible = alfaSession.Admin;
                pageTest.PageVisible = alfaSession.Admin;
                pageSAP.PageVisible = alfaSession.Admin;

                // Gumruk Modu
                if (alfaSession.Gumruk)
                {
                    // Pages
                    pageKantar.PageVisible = false;
                    pageRapor.PageVisible = false;
                    pageYedek.PageVisible = false;
                    pageGemi01.PageVisible = true;
                    pageGemi02.PageVisible = false;
                    pageSAP.PageVisible = false;
                    pageTest.PageVisible = false;
                    pageAdmin.PageVisible = false;

                    // Status
                    statusPortK1.Visibility = BarItemVisibility.Never;
                    statusPortK2.Visibility = BarItemVisibility.Never;
                    statusDataK1.Visibility = BarItemVisibility.Never;
                    statusDataK2.Visibility = BarItemVisibility.Never;
                    statusPortS1.Visibility = BarItemVisibility.Never;
                    statusDataS1.Visibility = BarItemVisibility.Never;
                    statusUSBText.Visibility = BarItemVisibility.Never;
                    statusUSBLabel.Visibility = BarItemVisibility.Never;
                }

                // Set Status
                this.Set_Status_Fields();

                // Clear Inputs
                this.btnGemi01Clear_Click(null, null);
                this.btnGemi02Clear_Click(null, null);
                this.btnClearArac_Click(null, null);
                this.btnClearRapor_Click(null, null);
                this.btnYedekClear_Click(null, null);
                this.btnTransferClear_Click(null, null);

                // Timer Start-Stop
                this.timerMain.Enabled = this.SENSOR1.m_Aktif;

                // Main
                pnMain.Show();
                pnLogin.Hide();

                // Show
                tabControlMain.Show();
                tabControlMain.SelectedTabPage = pageKantar;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnClearLogin_Click(object sender, EventArgs e)
        {
            // Disable Login
            alfaCtrl.ButtonDisable(btnLogin);

            // Clear Inputs
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;

            // Focus
            txtUserName.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Close
            this.Close();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Set_Mask_TextInputs()
        {
            // SapFisNo - Numeric(11)
            txtSapFisNo.Properties.Mask.EditMask = "[0-9]{0,11}";
            txtSapFisNo.Properties.Mask.UseMaskAsDisplayFormat = true;
            txtSapFisNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;

            // PlakaNo - AlfaNumeric(11)
            txtPlakaNo.Properties.Mask.EditMask = "[A-Z0-9.]{0,11}";
            txtPlakaNo.Properties.Mask.UseMaskAsDisplayFormat = true;
            txtPlakaNo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;

            // txtTestK1 - Numeric(5)
            txtTestK1.Properties.Mask.EditMask = "[0-9]{0,5}";
            txtTestK1.Properties.Mask.UseMaskAsDisplayFormat = true;
            txtTestK1.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;

            // txtTestK2 - Numeric(5)
            txtTestK2.Properties.Mask.EditMask = "[0-9]{0,5}";
            txtTestK2.Properties.Mask.UseMaskAsDisplayFormat = true;
            txtTestK2.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnClearArac_Click(object sender, EventArgs e)
        {
            // Get AracAktif List
            grdAracAktif.DataSource = alfaEntity.AracAktif_GetList();

            // Clear Inputs
            txtSapFisNo.Text = null;
            txtPlakaNo.Text = null;
            txtTestK1.Text = null;
            txtTestK2.Text = null;
            txtDesen.Text = null;

            // Reset Desen
            this.Set_DesenLookup(false);

            // Disable Buttons
            alfaCtrl.ButtonDisable(btnFindArac);

            // Enable SapFisNo / PlakaNo
            alfaCtrl.ControlEnable(txtPlakaNo, Color.Black);
            alfaCtrl.ControlEnable(txtSapFisNo, Color.Black);

            // Reset PropGrid
            propGrid.Rows.Clear();
            propGrid.SelectedObject = null;
            propGrid.RepositoryItems.Clear();

            // Reset Message
            alfaCtrl.SetResult(txtResult, string.Empty, alfaResult.None);

            // Focus
            if (sender != null) txtSapFisNo.Focus();

            // Disable Kantar Buttons
            alfaCtrl.ButtonDisable(btnKantarK1);
            alfaCtrl.ButtonDisable(btnKantarK2);

            // Reset Gaugaes
            LEDK1_Live.Text = "0";
            LEDK1_Test.Text = "0";
            LEDK2_Live.Text = "0";
            LEDK2_Test.Text = "0";
        }
        
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void PortS1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (this.SENSOR1.m_PortLock) //=========================================== PORT-S =================//
                {
                    while (PortS1.BytesToRead > 0 && PortS1.IsOpen)
                    {
                        // Get Byte
                        byte bb = (byte)PortS1.ReadByte();

                        // Start
                        if (bb == 0x3A) this.SENSOR1.m_PaketStart = true;

                        // Add to Paket
                        if (this.SENSOR1.m_PaketStart) this.SENSOR1.m_PaketBuffer.Add(bb);

                        // Finish
                        if (bb == 0x0A)
                        {
                            // Process
                            this.Process_Paket_S1(this.SENSOR1);

                            // Clear Buffer
                            this.SENSOR1.m_PaketBuffer.Clear();

                            // Set Flag
                            this.SENSOR1.m_PaketStart = false;
                        }
                    }

                } //========================================================================== PORT-S ===============//

            }
            catch
            {
                // Error Flag
                this.SENSOR1.m_Error = true;

                // Reset Flag
                this.SENSOR1.m_PaketStart = false;

                // Clear Buffer
                this.SENSOR1.m_PaketBuffer.Clear();

                // Error Status
                if (alfaSession.Admin) this.Process_Status(this.SENSOR1, "HATA");
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Process_Paket_S1(alfaKntr p_Kantar) // PROCESS_S1
        { 
            //                                               S4     S4     S    4xS                               //
            //  1     2     3     4      5     6      7      8      9      10     11     12     13     14     15  //
            // 0x3A, 0x30, 0x31, 0x30 , 0x33, 0x30 , 0x32 , 0x30 , 0x30 , 0x30 , 0x30 , 0x46 , 0x37 , 0x0D , 0x0A //

            // Get Paket
            string hexPaket = alfaStr.ByteArrayToStringV1(p_Kantar.m_PaketBuffer.ToArray());
            string strPaket = System.Text.Encoding.UTF8.GetString(p_Kantar.m_PaketBuffer.ToArray());

            // Add To Log
            this.Add_Log_Message(p_Kantar.m_LOG, "YOK", hexPaket);

            // Get Sensors
            string strSensors = strPaket.Substring(10, 1);

            // Check Number
            p_Kantar.m_Value = Convert.ToInt32(strSensors);

            // Binary Format
            string p_Sensors = Convert.ToString(p_Kantar.m_Value, 2).PadLeft(4, '0');

            // Process Sensors
            this.Process_Sensors(p_Sensors);

            // Status
            p_Kantar.SetStatus("OK");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Process_Sensors(string p_Sensors)
        {
            if (this.InvokeRequired)
            {
                // Thread Safe
                this.BeginInvoke((MethodInvoker)delegate { this.Set_Sensor_Values(p_Sensors); });
            }
            else
            {
                // Main Thread
                this.Set_Sensor_Values(p_Sensors); 
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Set_Sensor_Values(string p_Sensors)
        {
            // Set Sensor Flags            
            if (p_Sensors[3] == '0') SensorK1S1.StateIndex = alfaSensor.PASS; else SensorK1S1.StateIndex = alfaSensor.FAIL;  // K1-S1
            if (p_Sensors[2] == '0') SensorK1S2.StateIndex = alfaSensor.PASS; else SensorK1S2.StateIndex = alfaSensor.FAIL;  // K1-S2
            if (p_Sensors[1] == '0') SensorK2S1.StateIndex = alfaSensor.PASS; else SensorK2S1.StateIndex = alfaSensor.FAIL;  // K2-S1
            if (p_Sensors[0] == '0') SensorK2S2.StateIndex = alfaSensor.PASS; else SensorK2S2.StateIndex = alfaSensor.FAIL;  // K2-S2

            // Disable 3.Sensors
            SensorK1S3.StateIndex = alfaSensor.OFF;
            SensorK2S3.StateIndex = alfaSensor.OFF;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void PortK1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (this.KANTAR1.m_PortLock) //=========================================== PORT-1 =================//
                {
                    while (PortK1.BytesToRead > 0 && PortK1.IsOpen)
                    {
                        // Get Byte
                        byte bb = (byte)PortK1.ReadByte();

                        // Start
                        if (this.KANTAR1.m_PortKntr == "ESIT" && this.m_EsitBytes.Contains(bb)) this.KANTAR1.m_PaketStart = true;

                        // Start
                        else if (bb == 0x02) this.KANTAR1.m_PaketStart = true;

                        // Add to Paket
                        if (this.KANTAR1.m_PaketStart) this.KANTAR1.m_PaketBuffer.Add(bb);

                        // Finish
                        if (bb == 0x0D)
                        {
                            // Process
                            this.Process_Paket_K1_K2(this.KANTAR1);

                            // Clear Buffer
                            this.KANTAR1.m_PaketBuffer.Clear();

                            // Set Flag
                            this.KANTAR1.m_PaketStart = false;
                        }
                    }

                } //========================================================================== PORT-1 ===============//

            }
            catch
            {
                // Error Flag
                this.KANTAR1.m_Error = true;

                // Reset Flag
                this.KANTAR1.m_PaketStart = false;

                // Clear Buffer
                this.KANTAR1.m_PaketBuffer.Clear();

                // Error Status
                if (alfaSession.Admin) this.Process_Status(this.KANTAR1, "HATA");
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void PortK2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (this.KANTAR2.m_PortLock) //=========================================== PORT-2 =================//
                {
                    while (PortK2.BytesToRead > 0 && PortK2.IsOpen)
                    {
                        // Get Byte
                        byte bb = (byte)PortK2.ReadByte();

                        // Start
                        if (this.KANTAR2.m_PortKntr == "ESIT" && this.m_EsitBytes.Contains(bb)) this.KANTAR2.m_PaketStart = true;

                        // Start
                        else if (bb == 0x02) this.KANTAR2.m_PaketStart = true;

                        // Add to Paket
                        if (this.KANTAR2.m_PaketStart) this.KANTAR2.m_PaketBuffer.Add(bb);

                        // Finish
                        if (bb == 0x0D)
                        {
                            // Process
                            this.Process_Paket_K1_K2(this.KANTAR2);

                            // Clear Buffer
                            this.KANTAR2.m_PaketBuffer.Clear();

                            // Set Flag
                            this.KANTAR2.m_PaketStart = false;
                        }
                    }

                } //======================================================================== PORT-2 =================//

            }
            catch
            {
                // Error Flag
                this.KANTAR2.m_Error = true;

                // Reset Flag
                this.KANTAR2.m_PaketStart = false;

                // Clear Buffer
                this.KANTAR2.m_PaketBuffer.Clear();

                // Error Status
                if (alfaSession.Admin) this.Process_Status(this.KANTAR2, "HATA");
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Process_Paket_K1_K2(alfaKntr p_Kantar) // PROCESS_K1_K2
        {
            //                                 W      E      I      G      H      T                                                                         //
            //  1     2     3     4      5     1      2      3      4      5      6      1      2      3      4      5      6      7      8      9      10  //
            // 0x02, 0x35, 0x35, 0x35 , 0x20, 0x30 , 0x30 , 0x30 , 0x31 , 0x32 , 0x33 , 0x20 , 0x35 , 0x35 , 0x35 , 0x35 , 0x35 , 0x35 , 0x35 , 0x35 , 0x0D //

            // Get Paket
            string hexPaket = alfaStr.ByteArrayToStringV1(p_Kantar.m_PaketBuffer.ToArray());
            string strPaket = System.Text.Encoding.UTF8.GetString(p_Kantar.m_PaketBuffer.ToArray());

            // Add To Log
            this.Add_Log_Message(p_Kantar.m_LOG, strPaket, hexPaket);

            // Get Weigth
            string strWeight = "0";

            switch (p_Kantar.m_PortKntr)
            {
                case "TUNAYLAR" : strWeight = strPaket.Substring(5, 6); break;
                case "BAYKON"   : strWeight = strPaket.Substring(4, 6); break;
                case "ESIT"     : strWeight = strPaket.Substring(1, 5); break;
            }

            // Check Number
            p_Kantar.m_Value = Convert.ToInt32(strWeight);

            // Weight
            this.Process_Weight(p_Kantar);

            // Status
            p_Kantar.SetStatus("OK");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Process_Weight(alfaKntr p_Kantar)
        {
            if (this.InvokeRequired)
            {
                // Thread Safe
                this.BeginInvoke((MethodInvoker)delegate { this.Set_LED_Value(p_Kantar); });
            }
            else
            {
                // Main Thread
                this.Set_LED_Value(p_Kantar);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Set_LED_Value(alfaKntr p_Kantar)
        {
            // Sensors_FAIL
            bool p_Sensors_FAIL = false;

            if (p_Kantar.m_KNO == "K1")
            {
                // Set Sensors
                p_Sensors_FAIL = (SensorK1S1.StateIndex == alfaSensor.FAIL || SensorK1S2.StateIndex == alfaSensor.FAIL || SensorK1S3.StateIndex == alfaSensor.FAIL);
            }
            else   if (p_Kantar.m_KNO == "K2")
            {
                // Set Sensors
                p_Sensors_FAIL = (SensorK2S1.StateIndex == alfaSensor.FAIL || SensorK2S2.StateIndex == alfaSensor.FAIL || SensorK2S3.StateIndex == alfaSensor.FAIL);
            }

            // Set Button
            alfaCtrl.SetButton(p_Kantar.m_Button, propGrid.SelectedObject != null && p_Kantar.m_Value > 0 && !p_Sensors_FAIL);

            if (p_Kantar.m_Error==false)
            {
                // LED Text
                p_Kantar.m_DIG_Live.Text = p_Kantar.m_Value.ToString();
                p_Kantar.m_DIG_Test.Text = p_Kantar.m_Value.ToString();

                // LED Color
                p_Kantar.m_DIG_Live.AppearanceOn.ContentBrush = new SolidBrushObject(Color.FromArgb(p_Kantar.m_Color));
                p_Kantar.m_DIG_Test.AppearanceOn.ContentBrush = new SolidBrushObject(Color.FromArgb(p_Kantar.m_Color));
            }

            else if (p_Kantar.m_Error && alfaSession.Admin)
            {
                // LED Color
                p_Kantar.m_DIG_Live.AppearanceOn.ContentBrush = new SolidBrushObject(Color.Red);
                p_Kantar.m_DIG_Test.AppearanceOn.ContentBrush = new SolidBrushObject(Color.Red);
            }

            // Reset
            p_Kantar.m_Error = false;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Process_Status(alfaKntr p_Kantar, string p_Status)
        {
            if (this.InvokeRequired)
            {
                // Thread Safe
                this.BeginInvoke((MethodInvoker)delegate { p_Kantar.SetStatus(p_Status); });
            }
            else
            {
                // Main Thread
                p_Kantar.SetStatus(p_Status);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Add_Log_Message(MemoEdit txtLog, string p_strPaket, string p_hexPaket)
        {
            if (btnLogStart.Text == "Start") return; // Log Stopped

            // Get Log Message
            string p_Message = String.Format(" {0} ______ {1} ______ {2}", DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"), p_strPaket, p_hexPaket);

            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    // Add Log
                    txtLog.Text += p_Message + Environment.NewLine;

                    // Select
                    txtLog.Select(txtLog.Text.Length - 1, 0);
                    txtLog.ScrollToCaret();
                });
            }
            else
            {
                // Add Log       
                txtLog.Text += p_Message + Environment.NewLine;

                // Select
                txtLog.Select(txtLog.Text.Length - 1, 0);
                txtLog.ScrollToCaret();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private bool SAP_ZKNT_F_KANTAR_V2(string p_SapFisNo, TableAracAktif p_Aktif)
        {
            // Reset
            alfaEntity.SAPResponse01 = null;

            // Check - I
            if (string.IsNullOrEmpty(p_SapFisNo)) return true;

            // Get Arac
            var ListArac = alfaEntity.AracAktif_GetList_V2(p_SapFisNo);

            // Check - II
            if (ListArac.Count == 1 && ListArac[0].PlakaNo.Contains("."))
            {
                // Set Plaka
                txtPlakaNo.Text = ListArac[0].PlakaNo;

                // Refresh SAP
                alfaEntity.SAP_ZKNT_F_KANTAR_V2(p_SapFisNo);

                // Return
                return true;
            }

            try
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Create SAP
                alfaSAP p_SAP = new alfaSAP();

                // Create Parameters
                WR.ZKNT_F_KANTAR_V2 prms = new WR.ZKNT_F_KANTAR_V2();

                // Set Parameter
                prms.VA_FISNO = p_SapFisNo;
                prms.TB_ACIKLAMA = new WR.ZMM_T_ACIKLAMA[0];
                prms.TB_AMBAR = new WR.ZSD_T_SEFERAMBAR[0];
                prms.TB_BEYAN = new WR.ZSD_T_SEFERB[0];
                prms.TB_DEPO = new WR.ZMM_S_T001L[0];
                prms.TB_GUMRUK = new WR.ZSD_T_GUMRUK[0];
                prms.TB_NAKLIYE = new WR.ZMM_T_NAKLIYECI[0];
                prms.TB_RENK = new WR.ZSD_T_RENK[0];
                prms.TB_RET = new WR.BAPIRET2[0];
                prms.TB_SEFER = new WR.ZSD_S_SEFER1[0];
                prms.TB_TESL = new WR.ZMM_S_KANTAR_TESL[0];
                prms.TB_YRDDET = new WR.ZMM_S_KANTAR_D[0];
                prms.VA_LOKASYON = alfaSession.Lokasyon;

                // Return
                prms.TB_RET = new WR.BAPIRET2[0];
                prms.TB_YRDDET = new WR.ZMM_S_KANTAR_D[0];

                // Call Service
                alfaEntity.SAPResponse01 = p_SAP.ZKNT_F_KANTAR_V2(prms);

                // CursorDefult
                alfaMsg.CursorDefult();

                if (alfaEntity.SAPResponse01.TB_RET.Count() == 0)
                {
                    // Set Plaka
                    txtPlakaNo.Text = alfaEntity.SAPResponse01.LN_DRM.PLAKA;

                    if (string.IsNullOrEmpty(txtPlakaNo.Text.Trim()))
                    {
                        // Set Message
                        alfaCtrl.SetResult(txtResult, "PlakaNo Boþ Geçilemez ... !", alfaResult.Fail); return false;
                    }

                    else
                    {
                        if (p_Aktif != null)
                        {
                            // Set Values
                            p_Aktif.Adet = alfaEntity.SAPResponse01.LN_DRM.ADET;
                            if (!string.IsNullOrEmpty(alfaEntity.SAPResponse01.LN_DRM.ARKTX)) p_Aktif.Malzeme = alfaEntity.SAPResponse01.LN_DRM.ARKTX;
                            if (!string.IsNullOrEmpty(alfaEntity.SAPResponse01.LN_DRM.SEVKYERI)) p_Aktif.SevkYeri = alfaEntity.SAPResponse01.LN_DRM.SEVKYERI;
                            if (!string.IsNullOrEmpty(alfaEntity.SAPResponse01.LN_DRM.FIRMA)) p_Aktif.FirmaAdi = alfaEntity.SAPResponse01.LN_DRM.FIRMA;
                            if (!string.IsNullOrEmpty(alfaEntity.SAPResponse01.LN_DRM.RENK)) p_Aktif.Renk = alfaEntity.SAPResponse01.LN_DRM.RENK;
                            if (!string.IsNullOrEmpty(alfaEntity.SAPResponse01.LN_DRM.NAKLIYE)) p_Aktif.Nakliye = alfaEntity.SAPResponse01.LN_DRM.NAKLIYE;
                            if (!string.IsNullOrEmpty(alfaEntity.SAPResponse01.LN_DRM.LGORT)) p_Aktif.SapSevkNo = alfaEntity.SAPResponse01.LN_DRM.LGORT;
                            if (!string.IsNullOrEmpty(alfaEntity.SAPResponse01.LN_DRM.VBELN)) p_Aktif.SapTeslimat = alfaEntity.SAPResponse01.LN_DRM.VBELN + "-" + alfaEntity.SAPResponse01.LN_DRM.POSNR;

                            // Clear Zero
                            if (p_Aktif.Adet == 0) p_Aktif.Adet = null;
                        }

                        // Return
                        return true;
                    }
                }
                else
                {
                    // Reset Object
                    propGrid.Rows.Clear();
                    propGrid.SelectedObject = null;

                    // Set Message
                    string p_SapMesaj = String.Format("({0}) {1}", alfaEntity.SAPResponse01.TB_RET[0].TYPE, alfaEntity.SAPResponse01.TB_RET[0].MESSAGE);

                    // Error Message
                    alfaMsg.Error(p_SapMesaj);

                    // Return
                    return false;
                }
            }

            catch (Exception ex)
            {
                // Reset Object
                propGrid.Rows.Clear();
                propGrid.SelectedObject = null;

                // Error Message
                alfaMsg.Error(ex);

                // Return
                return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private bool SAP_ZKNT_F_KANTAR_OKU_V2(string p_SapFisNo, ref TableAracAktif p_Aktif)
        {
            // Check - I
            if (string.IsNullOrEmpty(p_SapFisNo)) return true;

            // Check - II
            if (p_Aktif.PlakaNo.Contains("."))
            {
                // Save SapMesaj
                p_Aktif.SapMesaj = "(S) Dolu tartým yapýldý."; 
                
                // Return
                return true;
            }

            try
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Create SAP
                alfaSAP p_SAP = new alfaSAP();

                // Create Parameters
                WR.ZKNT_F_KANTAR_OKU_V2 prms = new WR.ZKNT_F_KANTAR_OKU_V2();

                // Set Parameter
                prms.ACKLM = Convert.ToString(p_Aktif.Aciklama1);
                prms.AMBAR = Convert.ToString(p_Aktif.AmbarNo);
                prms.BAGADT = Convert.ToString(p_Aktif.Adet);
                prms.BEYAN = Convert.ToString(p_Aktif.Beyanname);
                prms.FIRMAR = Convert.ToString(p_Aktif.FirmaAdi);
                prms.FISNO = Convert.ToString(p_Aktif.SapFisNo);
                prms.FISRF = string.Empty;
                prms.IHRAC = string.Empty;
                prms.KANTAR = Convert.ToString(p_Aktif.KNo);
                prms.LGORT = Convert.ToString(p_Aktif.SapSevkNo);
                prms.LMILK = Convert.ToString(p_Aktif.YLTartim1);
                prms.LMSON = Convert.ToString(p_Aktif.YLTartim2);
                prms.MCINSI = Convert.ToString(p_Aktif.Malzeme);
                prms.MEMUR = Convert.ToString(p_Aktif.Gumruk);
                prms.NAKLIYECI = string.Empty;
                prms.OPERATOR = string.Empty;
                prms.RENK = Convert.ToString(p_Aktif.Renk);
                prms.SEFER = Convert.ToString(p_Aktif.SapSeferNo);
                prms.SEVKYERI = Convert.ToString(p_Aktif.SevkYeri);
                prms.YFISN = string.Empty;

                // Set Tartim
                if (p_Aktif.Tartim2 > 0)
                    prms.TARTIM = Convert.ToDecimal(p_Aktif.Tartim2);
                else prms.TARTIM = Convert.ToDecimal(p_Aktif.Tartim1);

                // Return
                prms.TB_RET = new WR.BAPIRET2[0];
                prms.TB_YRDDET = new WR.ZMM_S_KANTAR_D[0];

                // NakilGemi
                if (!String.IsNullOrEmpty(p_Aktif.NakilGemi))
                {
                    prms.NAKIL = "X";
                    prms.GEMIA = Convert.ToString(p_Aktif.NakilGemi);
                    prms.NAKILSEFERNO = Convert.ToString(p_Aktif.NakilSeferNo);
                }
                else
                {
                    prms.NAKIL = " ";
                    prms.GEMIA = string.Empty;
                    prms.NAKILSEFERNO = string.Empty;
                }

                // Sabit Darali
                if (p_Aktif.TartimTipi == "SDARALI") prms.SABIT = "1"; else prms.SABIT = "0";

                try
                {
                    // Teslimat 1234567890 (VBELN) - 123456 (POSNT)
                    int p_Pos = p_Aktif.SapTeslimat.LastIndexOf("-");

                    // Set Values
                    prms.VBELN = p_Aktif.SapTeslimat.Substring(0, p_Pos);
                    prms.POSNR = p_Aktif.SapTeslimat.Substring(p_Pos + 1, 6);

                    // VBELN - POSNR
                    p_Aktif.VBELN = prms.VBELN;
                    p_Aktif.POSNR = prms.POSNR;
                }
                catch 
                { 
                    //NULL
                }

                // Call Service
                WR.ZKNT_F_KANTAR_OKU_V2Response p_resp = p_SAP.ZKNT_F_KANTAR_OKU_V2(prms);

                // CursorDefult
                alfaMsg.CursorDefult();

                if (p_resp.TB_RET.Count() > 0)
                {
                    // Set Message
                    p_Aktif.SapMesaj = String.Format("({0}) {1}", p_resp.TB_RET[0].TYPE, p_resp.TB_RET[0].MESSAGE);

                    // Save SapMesaj
                    alfaEntity.Update_SapMesaj(p_Aktif.PlakaNo, p_Aktif.SapMesaj);

                    // Check Result
                    if (p_resp.TB_RET.Where(tt => tt.TYPE == "E").Count() > 0)
                    {
                        // Get Message
                        var obj = p_resp.TB_RET.Where(tt => tt.TYPE == "E").First();

                        // Show Message
                        return alfaMsg.Show(obj.TYPE, obj.MESSAGE);
                    }

                    // Success
                    else return true;
                }

                // Success
                else return true;
            }

            catch (Exception ex)
            {
                // Error Message
                alfaMsg.Error(ex);

                // Return
                return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnFindArac_Click(object sender, EventArgs e)
        {
            //Focus
            this.btnFindArac.Focus();
            
            // Check PlakaNo
            if (!this.Is_PlakaNo_Valid(txtPlakaNo.Text)) return;

            // Get Data from SAP
            if (!SAP_ZKNT_F_KANTAR_V2(txtSapFisNo.Text, null)) return;

            // Refresh
            this.Set_DesenLookup(true);

            // Find Item
            var ListArac = alfaEntity.AracAktif_GetList_V1(txtPlakaNo.Text);

            if (ListArac.Count == 1) // TARTIM_02
            {
                // Set Message
                alfaCtrl.SetResult(txtResult, alfaStr.Tartim02, alfaResult.Pass);

                // Get RowHandle
                int p_RowHandle = grdViewAracAktif.LocateByValue("PlakaNo", txtPlakaNo.Text, null);

                // Select RowHandle
                alfaGrid.SelectRow(grdViewAracAktif, p_RowHandle);

                // Check SapFisNo Empty (Merkez)
                if (!alfaSession.Liman && string.IsNullOrEmpty(ListArac[0].SapFisNo) && !string.IsNullOrEmpty(txtSapFisNo.Text))
                {
                    // Set Message
                    alfaCtrl.SetResult(txtResult, "DÝKKAT: <TARTIM-I> PlakaNo ile Yapýlmýþ ... !", alfaResult.Fail); return;
                }

                // Assign SAP FisNo
                if (string.IsNullOrEmpty(txtSapFisNo.Text)) txtSapFisNo.Text = ListArac[0].SapFisNo; else ListArac[0].SapFisNo = txtSapFisNo.Text;

                // Set AmbarNo
                if (alfaSession.Liman && ListArac[0].AmbarNo == null) ListArac[0].AmbarNo = 0;

                // Set Gumruk
                if (alfaSession.Liman && string.IsNullOrEmpty(ListArac[0].Gumruk)) ListArac[0].Gumruk = alfaEntity.GetDefaultGumrukName();

                // Get Data from SAP
                if (!SAP_ZKNT_F_KANTAR_V2(txtSapFisNo.Text, ListArac[0])) return;

                // Set Grid
                alfaVGrid.SetPropertyGridV1(propGrid, ListArac[0], "T2");
            }

            else  // TARTIM_01
            {
                // Arac
                var p_Arac = new TableAracAktif();

                // Set SapFisNo
                p_Arac.SapFisNo = txtSapFisNo.Text;

                // Set Message
                alfaCtrl.SetResult(txtResult, alfaStr.Tartim01, alfaResult.Pass);

                // Get Data from SAP
                if (!SAP_ZKNT_F_KANTAR_V2(txtSapFisNo.Text, p_Arac)) return;

                // Set Grid
                alfaVGrid.SetPropertyGridV1(propGrid, p_Arac, "T1");
            }

            // Focus
            propGrid.Focus();
            propGrid.FocusFirst();
                
            // Disable Find
            alfaCtrl.ButtonDisable(btnFindArac);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private bool Is_PlakaNo_Valid(string p_PlakaNo)
        {
            try
            {
                // Get Value
                decimal val = Decimal.Parse(p_PlakaNo);

                // Set Message
                alfaCtrl.SetResult(txtResult, "( SAP FiþNo ) PlakaNo Yerine Girilemez ... !", alfaResult.Fail);

                // Return
                return false;
            }
            catch
            {
                // Error
                return true;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnKantarK1_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Count
                int p_Count = alfaEntity.AracAktif_GetList_V1(txtPlakaNo.Text).Count;

                // Tartim
                if (p_Count == 0) 
                     this.Process_Tartim_01(this.KANTAR1);
                else this.Process_Tartim_02(this.KANTAR1);

                // Focus
                if (propGrid.SelectedObject == null) txtSapFisNo.Focus();
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnKantarK2_Click(object sender, EventArgs e)
        {
            try
            {
                // Get Count
                int p_Count = alfaEntity.AracAktif_GetList_V1(txtPlakaNo.Text).Count;

                // Tartim
                if (p_Count == 0) 
                     this.Process_Tartim_01(this.KANTAR2);
                else this.Process_Tartim_02(this.KANTAR2);

                // Focus
                if (propGrid.SelectedObject == null) txtSapFisNo.Focus();
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Process_Tartim_01(alfaKntr p_Kantar)
        {
            // Tartiom No
            p_Kantar.m_TartimNo = alfaStr.Tartim01;

            // Get Object
            var p_Aktif = (TableAracAktif)propGrid.SelectedObject;

            //=========================== FIELDS ==================================//

            // Assign Values
            p_Aktif.Lokasyon = alfaSession.Lokasyon;
            p_Aktif.SN1 = p_Kantar.m_KantarSeriNo;
            p_Aktif.PlakaNo = txtPlakaNo.Text;
            p_Aktif.SapFisNo = txtSapFisNo.Text;
            p_Aktif.Operator = alfaSession.FullName;
            p_Aktif.OP1 = alfaSession.FullName;
            p_Aktif.KantarPC = this.m_Session.PC;
            p_Aktif.KantarDB = this.m_Session.DB;
            p_Aktif.KNo = p_Kantar.m_KNO;
            p_Aktif.SAP = false;
            p_Aktif.YL = false;
            p_Aktif.CiktiNo = 0;

            // Tartim1
            p_Aktif.Tartim1 = p_Kantar.m_Value;
            p_Aktif.Zaman1 = DateTime.Now;

            // Tartim2
            p_Aktif.Zaman2 = null;
            p_Aktif.Tartim2 = 0;

            //  Tutarlar
            p_Aktif.NetTutar = 0;
            p_Aktif.NetGiris = 0;
            p_Aktif.NetCikis = 0;

            // GemiNo
            p_Aktif.GemiNo = alfaEntity.Gemi_GetID(p_Aktif.GemiAdi);

            //=========================== FIELDS ==================================//

            // Yan Liman Tartim
            if (!alfaSession.Liman && alfaEntity.SAPResponse01 != null && p_Aktif.SapSeferNo != null && alfaEntity.SAPResponse01.TB_SEFER.Where(tt => tt.SEFER == p_Aktif.SapSeferNo).First().YANYIMANMI == "X")
            {
                // Create YanLiman
                FrmYanLiman frmYL = new FrmYanLiman(this.m_Session, p_Aktif);

                // Show YanLiman
                if (frmYL.ShowDialog() != DialogResult.OK) return;
            }

            // Sensors
            if (p_Kantar.m_KNO == "K1")
            {
                if (SensorK1S1.StateIndex == alfaSensor.PASS && SensorK1S3.StateIndex == alfaSensor.PASS) p_Aktif.Sensor = "VAR"; else p_Aktif.Sensor = "YOK";
            }
            else if (p_Kantar.m_KNO == "K2")
	        {
                if (SensorK2S1.StateIndex == alfaSensor.PASS && SensorK2S3.StateIndex == alfaSensor.PASS) p_Aktif.Sensor = "VAR"; else p_Aktif.Sensor = "YOK";
	        }

            // Create Tartim
            FrmTartim frmTartim = new FrmTartim(p_Kantar.Title, p_Aktif, "T1", txtTestK1.Visible || txtTestK2.Visible);

            // Show Tartim
            if (frmTartim.ShowDialog() == DialogResult.OK)
            {
                // Call SAP Service
                if (!SAP_ZKNT_F_KANTAR_OKU_V2(txtSapFisNo.Text, ref p_Aktif)) return;

                // Add Record
                if (!alfaEntity.AracAktif_Add(p_Aktif)) return;

                // Clear
                this.btnClearArac_Click(null, null);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Process_Tartim_02(alfaKntr p_Kantar)
        {
            // Tartiom No
            p_Kantar.m_TartimNo = alfaStr.Tartim02;

            // Get Object
            var p_Aktif = (TableAracAktif)propGrid.SelectedObject;

            //=========================== FIELDS ==================================//

            // Assign Values
            p_Aktif.Lokasyon = alfaSession.Lokasyon;
            p_Aktif.SN2 = p_Kantar.m_KantarSeriNo;
            p_Aktif.Operator = alfaSession.FullName;
            p_Aktif.OP2 = alfaSession.FullName;
            p_Aktif.KantarPC = this.m_Session.PC;
            p_Aktif.KantarDB = this.m_Session.DB;
            p_Aktif.KNo = p_Kantar.m_KNO;
            p_Aktif.SAP = false;

            // Tartim2
            p_Aktif.Tartim2 = p_Kantar.m_Value;
            p_Aktif.Zaman2 = DateTime.Now;

            // Set Values
            p_Aktif.NetTutar = Math.Abs((int)p_Aktif.Tartim2 - (int)p_Aktif.Tartim1);
            p_Aktif.NetCikis = alfaCtrl.SetNetCikis((int)p_Aktif.Tartim1, (int)p_Aktif.Tartim2);
            p_Aktif.NetGiris = alfaCtrl.SetNetGiris((int)p_Aktif.Tartim1, (int)p_Aktif.Tartim2);

            if (alfaSession.Liman)
            {
                // GemiNo
                p_Aktif.GemiNo = alfaEntity.Gemi_GetID(p_Aktif.GemiAdi);

                // Check GemiNo
                if (p_Aktif.GemiNo == 0)
                {
                    // Focus
                    propGrid.Focus();
                    propGrid.FocusedRow = propGrid.Rows[0].ChildRows.GetRowByFieldName("GemiAdi");

                    // Message
                    throw new Exception("HATA :  GemiAdi Aktif Gemi Listesinde Bulunamadi ...!");
                }

                // Check AmbarNo
                if (p_Aktif.AmbarNo == null)
                {
                    // Focus
                    propGrid.Focus();
                    propGrid.FocusedRow = propGrid.Rows[0].ChildRows.GetRowByFieldName("AmbarNo");
                 
                    // Message
                    throw new Exception("HATA :  AmbarNo Girilmesi Zorunludur ...!");
                }

                // Get AracNo
                p_Aktif.AracNo = alfaEntity.GetNextAracNo(Convert.ToInt32(p_Aktif.GemiNo));

                // Nakil SeferNo
                p_Aktif.NakilSeferNo = alfaEntity.Get_NakilSerferNo(Convert.ToInt32(p_Aktif.NakilGemiNo));

                // Nakil GemiAdi
                p_Aktif.NakilGemi = alfaEntity.Get_NakilGemiAdi(Convert.ToInt32(p_Aktif.NakilGemiNo));

                try
                {
                    // VBELN - POSNR
                    p_Aktif.VBELN = Convert.ToString(p_Aktif.SapTeslimat.Split('-')[0]);
                    p_Aktif.POSNR = Convert.ToString(p_Aktif.SapTeslimat.Split('-')[1]);
                }
                catch 
                { 
                    //NULL
                }
            }

            //=========================== FIELDS ==================================//

            // Sensors
            if (p_Kantar.m_KNO == "K1")
            {
                if (SensorK1S1.StateIndex == alfaSensor.PASS && SensorK1S3.StateIndex == alfaSensor.PASS) p_Aktif.Sensor = "VAR"; else p_Aktif.Sensor = "YOK";
            }
            else if (p_Kantar.m_KNO == "K2")
	        {
                if (SensorK2S1.StateIndex == alfaSensor.PASS && SensorK2S3.StateIndex == alfaSensor.PASS) p_Aktif.Sensor = "VAR"; else p_Aktif.Sensor = "YOK";
	        }

            // Create Tartim
            FrmTartim frmTartim = new FrmTartim(p_Kantar.Title, p_Aktif, "T2", txtTestK1.Visible || txtTestK2.Visible);

            // Show Form
            if (frmTartim.ShowDialog() == DialogResult.OK)
            {
                // --------------------------- Normal Tartim Kapanis ----------------------------- //

                // (1) Send Data to SAP
                if (!SAP_ZKNT_F_KANTAR_OKU_V2(txtSapFisNo.Text, ref p_Aktif)) return;

                // (2) Aktif --> Arsiv
                if (!alfaEntity.AracAktif_To_AracArsiv(p_Aktif)) return;

                // (3) SAP Data Transfer
                alfaEntity.SAP_Transfer_ARAC();

                // (4) Kantar Fisi(Tikesi)
                if (frmTartim.radioTartimTipi.SelectedIndex != 3)
                {
                    // Create Report
                    FrmKantarFisi rep = new FrmKantarFisi(p_Aktif, p_Kantar.Title);

                    // Show Report
                    if (rep.ShowDialog() == DialogResult.OK) 
                    {
                        // CiktiNo
                        p_Aktif.CiktiNo += 1;

                        // Update
                        alfaEntity.AracArsiv_Update(p_Aktif);
                    }
                }

                // Liman - Noktalý Tartim
                if (alfaSession.Liman && p_Aktif.PlakaNo.Contains("."))
                {
                    // Update SahaTartim
                    var p_Arac = alfaEntity.AracArsiv_SahaTartim(p_Aktif);

                    if (p_Arac != null)
                    {
                        // Create Report
                        FrmKantarFisi rep = new FrmKantarFisi(p_Arac, p_Kantar.Title);

                        // Show Report
                        if (rep.ShowDialog() == DialogResult.OK)
                        {
                            // CiktiNo
                            p_Arac.CiktiNo += 1;

                            // Update
                            alfaEntity.AracArsiv_Update(p_Arac);
                        }
                    }
                }

                // Delete AracAktif (Ilave Tartim Yok / Full Kapanis)
                if (frmTartim.radioTartimTipi.SelectedIndex == 0) //-------------------- 1-) NORMAL
                {
                    if (!alfaEntity.AracAktif_Del(p_Aktif.PlakaNo)) return;
                }

                else
                {
                    // Set Common Fields
                    p_Aktif.Lokasyon = alfaSession.Lokasyon;
                    p_Aktif.Guid = System.Guid.NewGuid();
                    p_Aktif.Zaman2 = null;
                    p_Aktif.NetCikis = 0;
                    p_Aktif.NetGiris = 0;
                    p_Aktif.NetTutar = 0;
                    p_Aktif.CiktiNo = 0;
                    p_Aktif.KayitEden = null;
                    p_Aktif.KayitZamani = null;
                    p_Aktif.KayitDurumu = null;
                    p_Aktif.KayitAciklama = null;

                    if (frmTartim.radioTartimTipi.SelectedIndex == 1) //-------------------- 2-) SDARALI
                    {
                        // Reset Values
                        p_Aktif.SapFisNo = null;
                        p_Aktif.Zaman1 = DateTime.Now;
                        p_Aktif.FisNo = (int)alfaEntity.GetNextFisNo();
                        p_Aktif.TartimTipi = "SDARALI";
                        p_Aktif.Tartim1 = p_Aktif.Tartim1;
                        p_Aktif.Tartim2 = 0;
                        p_Aktif.T1 = p_Aktif.T1;
                        p_Aktif.T2 = null;

                        // Update Object
                        if (!alfaEntity.AracAktif_Update(p_Aktif)) return;
                    }

                    else if (frmTartim.radioTartimTipi.SelectedIndex == 2) //--------------- 3-) ARATARTIM
                    {
                        // Reset Values
                        p_Aktif.SapFisNo = null;
                        p_Aktif.Zaman1 = DateTime.Now;
                        p_Aktif.FisNo = (int)alfaEntity.GetNextFisNo();
                        p_Aktif.TartimTipi = "ARATARTIM";
                        p_Aktif.Tartim1 = p_Aktif.Tartim2;
                        p_Aktif.Tartim2 = 0;
                        p_Aktif.T1 = p_Aktif.T2;
                        p_Aktif.T2 = null;

                        // Update Object
                        if (!alfaEntity.AracAktif_Update(p_Aktif)) return;
                    }

                    else if (frmTartim.radioTartimTipi.SelectedIndex == 3) //--------------- 4-) SAHATARTIM
                    {
                        // Create AktifArac (Noktali Plaka)
                        TableAracAktif p_Aktif_CT = new TableAracAktif();

                        // Assign Values
                        p_Aktif_CT.Guid = p_Aktif.Guid;
                        p_Aktif_CT.FisNo = p_Aktif.FisNo;
                        p_Aktif_CT.TartimTipi = "SAHATARTIM";
                        p_Aktif_CT.SapFisNo = p_Aktif.SapFisNo;
                        p_Aktif_CT.PlakaNo = p_Aktif.PlakaNo + ".";
                        p_Aktif_CT.Operator = p_Aktif.Operator;
                        p_Aktif_CT.OP1 = p_Aktif.OP1;
                        p_Aktif_CT.SN1 = p_Aktif.SN1;
                        p_Aktif_CT.Lokasyon = p_Aktif.Lokasyon;
                        p_Aktif_CT.KantarPC = this.m_Session.PC;
                        p_Aktif_CT.KantarDB = this.m_Session.DB;
                        p_Aktif_CT.KNo = p_Kantar.m_KNO;
                        p_Aktif_CT.Sensor = "YOK";
                        p_Aktif_CT.CiktiNo = 0;
                        p_Aktif_CT.SAP = false;
                        p_Aktif_CT.YL = false;
                        p_Aktif_CT.T1 = p_Aktif.T1;
                        p_Aktif_CT.T2 = null;
                        p_Aktif_CT.VBELN = p_Aktif.VBELN;
                        p_Aktif_CT.POSNR = p_Aktif.POSNR;

                        // Tartim1
                        p_Aktif_CT.Tartim1 = p_Aktif.Tartim1;
                        p_Aktif_CT.Zaman1 = p_Aktif.Zaman1;

                        // Tartim2
                        p_Aktif_CT.Zaman2 = null;
                        p_Aktif_CT.Tartim2 = 0;

                        //  Tutarlar
                        p_Aktif_CT.NetTutar = 0;
                        p_Aktif_CT.NetGiris = 0;
                        p_Aktif_CT.NetCikis = 0;

                        // Add Object
                        if (!alfaEntity.AracAktif_Add(p_Aktif_CT)) return;

                        // Delete Object
                        alfaEntity.AracAktif_Del(p_Aktif.PlakaNo);
                    }
                }

                // Clear
                btnClearArac_Click(null, null);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdViewALL_DataSourceChanged(object sender, EventArgs e)
        {
            // Set GridView
            alfaGrid.SetView((GridView)sender);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnListRapor_Click(object sender, EventArgs e)
        {
            // Get List
            var listArac = alfaEntity.AracArsiv_GetList(txtArsivDate01.DateTime, 
                                                        txtArsivDate02.DateTime, 
                                                        txtArsivPlaka.Text, 
                                                        txtArsivSapFisNo.Text,
                                                        txtGemiAdiRapor.Text,
                                                        txtFirmaAdiRapor.Text,
                                                        txtMalzemeRapor.Text,
                                                        txtNakliyeRapor.Text,
                                                        txtSevkYeriRapor.Text,
                                                        txtArsivOperator.Text,
                                                        txtRaporT1.Text,
                                                        txtRaporT2.Text);

            // Assign to Grid
            grdAracArsiv.DataSource = listArac;

            // Set Values
            txtToplamAracSayisi.Text = listArac.Count().ToString();
            txtGirisAracSayisi.Text = listArac.Where(tt => tt.NetGiris > 0).Count().ToString();
            txtCikisAracSayisi.Text = listArac.Where(tt => tt.NetCikis > 0).Count().ToString();

            // Enable Button
            alfaCtrl.SetButton(btnPrint, listArac.Count > 0);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnClearRapor_Click(object sender, EventArgs e)
        {
            // DateTime
            DateTime dtNow = DateTime.Now;
            dtNow = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);

            // Reset DateTime
            txtArsivDate01.DateTime = dtNow;
            txtArsivDate02.DateTime = dtNow.AddDays(1);

            if (alfaSession.Liman)
            {
                // Get DateTime
                dtNow = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 7, 0, 0);

                // Reset DateTime
                txtArsivDate01.DateTime = dtNow.AddDays(-1);
                txtArsivDate02.DateTime = dtNow;
            }

            // Reset Text Values
            txtGirisAracSayisi.Text = "0";
            txtCikisAracSayisi.Text = "0";
            txtToplamAracSayisi.Text = "0";
            txtRaporT1.Text = string.Empty;
            txtRaporT2.Text = string.Empty;
            txtArsivPlaka.Text = string.Empty;
            txtNakliyeRapor.Text = string.Empty;
            txtMalzemeRapor.Text = string.Empty;
            txtGemiAdiRapor.Text = string.Empty;
            txtSevkYeriRapor.Text = string.Empty;
            txtFirmaAdiRapor.Text = string.Empty;
            txtArsivSapFisNo.Text = string.Empty;
            txtArsivOperator.Text = string.Empty;

            // Clear Grid
            grdAracArsiv.DataSource = null;

            // Disable Button
            alfaCtrl.ButtonDisable(btnPrint);

            // Focus
            grpArsivSelection.Focus();
            txtArsivSapFisNo.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnYedekList_Click(object sender, EventArgs e)
        {
            // DateTimes
            DateTime? p_DT1 = txtYedekDate01.DateTime;
            DateTime? p_DT2 = txtYedekDate02.DateTime;

            // Set DateTimes
            if (!txtYedekDate01.Enabled) p_DT1 = null;
            if (!txtYedekDate02.Enabled) p_DT2 = null;

            // Get List
            var listArac = alfaEntity.AracYedek_GetList(p_DT1, p_DT2, txtYedekPlaka.Text, txtYedekGuid.Text, txtYedekT1.Text, txtYedekT2.Text);

            // Set Page
            tabYedek.SelectedTabPageIndex = 0;

            // Assign to Grid
            grdYedek01View.Columns.Clear();
            grdYedek01View.GridControl.DataSource = listArac;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnYedekClear_Click(object sender, EventArgs e)
        {
            // DateTime
            DateTime p_Now = DateTime.Now;
            p_Now = new DateTime(p_Now.Year, p_Now.Month, p_Now.Day, 0, 0, 0);

            // Reset Values
            txtYedekT1.Text = string.Empty;
            txtYedekT2.Text = string.Empty;
            txtYedekGuid.Text = string.Empty;
            txtYedekPlaka.Text = string.Empty;
            txtYedekDate01.DateTime = p_Now;
            txtYedekDate02.DateTime = p_Now.AddDays(1);

            // Enable DateTimes
            txtYedekDate01.Enabled = true;
            txtYedekDate02.Enabled = true;

            // Set Color
            txtYedekDate01.ForeColor = Color.Black;
            txtYedekDate02.ForeColor = Color.Black;

            // Clear Grids
            grdYedek01View.Columns.Clear();
            grdYedek02View.Columns.Clear();
            grdYedek01View.GridControl.DataSource = null;
            grdYedek02View.GridControl.DataSource = null;

            // Set Page
            tabYedek.SelectedTabPageIndex = 0;

            // Focus
            grpYedekSelection.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtDateALL_EditValueChanged(object sender, EventArgs e)
        {
            // Dates must be Selected
            if (txtArsivDate01.DateTime == DateTime.MinValue || txtArsivDate02.DateTime == DateTime.MinValue)
            {
                alfaCtrl.ButtonDisable(btnListRapor);
            }
            else
            {
                alfaCtrl.ButtonEnable(btnListRapor);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void tabControlMain_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            if (tabControlMain.SelectedTabPage == pageKantar)
            {
                // Focus
                txtSapFisNo.Focus();
            }
            else if (tabControlMain.SelectedTabPage == pageRapor)
            {
                // Focus
                txtArsivPlaka.Focus();
            }
            else if (tabControlMain.SelectedTabPage == pageAdmin)
            {
                // Set GridView
                alfaGrid.SetView(grdAdminView);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdViewALL_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            // Skip Selected Row
            if (e.RowHandle == (sender as GridView).FocusedRowHandle)
            {
                // Set Font
                e.Appearance.Font = new Font("Tahoma", 8, FontStyle.Bold);
            }

            else if ("PlakaNo-SapFisNo-GemiNo-GemiAdi-AmbarNo-SAP-Adet".Contains(e.Column.FieldName))
            {
                // Custom View
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.BackColor = Color.Lavender;
                e.Appearance.Font = new Font("Tahoma", 8, FontStyle.Bold);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtSapFisNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPlakaNo.Text == string.Empty && txtSapFisNo.Text == string.Empty)
            {
                // Reset
                this.btnClearArac_Click(null, null);
            }
            else
            {
                // Clear PlakaNo
                txtPlakaNo.Text = string.Empty;

                // Disable txtPlakaNo01
                alfaCtrl.ControlDisable(txtPlakaNo, Color.Gray);

                // Enable btnFindArac
                alfaCtrl.ButtonEnable(btnFindArac);

                // Reset PropGrid
                propGrid.Rows.Clear();
                propGrid.SelectedObject = null;
                propGrid.RepositoryItems.Clear();

                // Call Find
                if (e.KeyCode == Keys.Enter) this.btnFindArac_Click(null, null);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtPlakaNo_KeyUp(object sender, KeyEventArgs e)
        {
            // Check
            if (txtPlakaNo.Text == string.Empty && txtSapFisNo.Text == string.Empty)
            {
                // Reset
                this.btnClearArac_Click(null, null);
            }
            
            else
            {
                // Clear SapFisNo
                txtSapFisNo.Text = string.Empty;

                // Disable txtSapFisNo
                alfaCtrl.ControlDisable(txtSapFisNo, Color.Gray);

                // Enable btnFindArac
                alfaCtrl.ButtonEnable(btnFindArac);

                // Reset PropGrid
                propGrid.Rows.Clear();
                propGrid.SelectedObject = null;
                propGrid.RepositoryItems.Clear();

                // Call Find
                if (e.KeyCode == Keys.Enter) this.btnFindArac_Click(null, null);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtArsivALL_KeyUp(object sender, KeyEventArgs e)
        {
            // Set Colors
            if (txtArsivDate01.Enabled) txtArsivDate01.ForeColor = Color.Black; else txtArsivDate01.ForeColor = Color.Silver;
            if (txtArsivDate02.Enabled) txtArsivDate02.ForeColor = Color.Black; else txtArsivDate02.ForeColor = Color.Silver;

            // Get Lisy
            if (e.KeyData == Keys.Enter && btnListRapor.Enabled == true) this.btnListRapor_Click(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if ((txtUserName.Text != string.Empty) && (txtPassword.Text != string.Empty))
            {
                // Enable Login
                alfaCtrl.ButtonEnable(btnLogin);
            }
            else
            {
                // Disable Login
                alfaCtrl.ButtonDisable(btnLogin);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && btnLogin.Enabled == true)
            {
                // Enter
                this.btnLogin_Click(null, null);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnTransferClear_Click(object sender, EventArgs e)
        {
            // DateTime
            DateTime dtNow = DateTime.Now;
            dtNow = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);

            // Reset Values
            txtSAPDate01.DateTime = dtNow;
            txtSAPDate02.DateTime = dtNow.AddDays(1);

            // Disable Run
            alfaCtrl.ButtonDisable(btnTransferRun);

            // Clear Grid
            grdViewTransferDat.Columns.Clear();
            grdViewTransferLog.Columns.Clear();
            grdTransferDat.DataSource = null;
            grdTransferLog.DataSource = null;

            // Reset Selection
            radioTransferAracGemi.SelectedIndex = 0;
            radioTransferSQLSAP.SelectedIndex = 0;

            // Set Page
            tabControlTransfer.SelectedTabPageIndex = 0;

            // Focus
            grpSapSelection.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnTransferList_Click(object sender, EventArgs e)
        {
            // Set Page
            tabControlTransfer.SelectedTabPageIndex = 0;

            if (radioTransferSQLSAP.SelectedIndex == 0) // =============================> SQL to SAP 
            {
                if (radioTransferAracGemi.SelectedIndex == 0)
                {
                    // ARAC
                    var p_ListArac = alfaEntity.AracArsiv_GetList_FromSQL(txtSAPDate01.DateTime, txtSAPDate02.DateTime, null, null, null);

                    // Grid
                    grdViewTransferDat.Columns.Clear();
                    grdTransferDat.DataSource = p_ListArac;
                }

                else if (radioTransferAracGemi.SelectedIndex == 1) 
                {
                    // GEMI
                    var p_ListGemi = alfaEntity.GemiHareket_GetList_FromSQL(txtSAPDate01.DateTime, txtSAPDate02.DateTime, null, null, null, false);

                    // Grid
                    grdViewTransferDat.Columns.Clear();
                    grdTransferDat.DataSource = p_ListGemi;
                }
            }

            else if (radioTransferSQLSAP.SelectedIndex == 1) // =======================> SAP to SQL
            {
                // Get Date Parameters
                string p_Zaman1 = alfaDate.GetDateV2(txtSAPDate01.DateTime);
                string p_Zaman2 = alfaDate.GetDateV2(txtSAPDate02.DateTime);

                if (radioTransferAracGemi.SelectedIndex == 0)
                {
                    // ARAC
                    alfaEntity.SAP_ZSD_F_KANTAR_01_V2(p_Zaman1, p_Zaman2, "X", " ");

                    // Grid
                    grdViewTransferDat.Columns.Clear();
                    grdTransferDat.DataSource = alfaEntity.SAPResponse02.P_ARAC;
                }

                else if (radioTransferAracGemi.SelectedIndex == 1) 
                {
                    // GEMI
                    alfaEntity.SAP_ZSD_F_KANTAR_01_V2(p_Zaman1, p_Zaman2, " ", "X");

                    // Grid
                    grdViewTransferDat.Columns.Clear();
                    grdTransferDat.DataSource = alfaEntity.SAPResponse02.P_GEMI;
                }
            }

            // Set Button
            alfaCtrl.SetButton(btnTransferRun, grdViewTransferDat.RowCount > 0);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnTransferRun_Click(object sender, EventArgs e)
        {
            // Confirmation
            if (alfaMsg.Quest("Listedeki Kayýtlarýn Transferi için Emin misiniz ... ?") == DialogResult.No) return;

            // Set PageLOG
            tabControlTransfer.SelectedTabPageIndex = 1;

            // Create Log
            var p_ListLog = new List<DataLOG>();

            if (radioTransferSQLSAP.SelectedIndex == 0) // SQL to SAP
            {
                if (radioTransferAracGemi.SelectedIndex == 0)
                {
                    // ARAC
                    var p_ListArac = (List<TableAracArsiv>)grdTransferDat.DataSource;

                    // SQL -> SAP
                    alfaEntity.SQL_to_SAP_Arac(p_ListArac, p_ListLog);
                }

                else
                {
                    // GEMI
                    var p_ListGemi = (List<TableGemiHareket>)grdTransferDat.DataSource;

                    // SQL -> SAP
                    alfaEntity.SQL_to_SAP_Gemi(p_ListGemi, p_ListLog);
                }
            }

            else if (radioTransferSQLSAP.SelectedIndex == 1) // SAP to SQL
            {
                if (radioTransferAracGemi.SelectedIndex == 0)
                {
                    // ARAC
                    var p_ListArac = (ZSD_S_ARAC[])grdTransferDat.DataSource;

                    // SAP -> SQL
                    alfaEntity.SAP_to_SQL_Arac(p_ListArac, p_ListLog);
                }

                else
                {
                    // GEMI
                    var p_ListGemi = (ZSD_S_GEMI[])grdTransferDat.DataSource;

                    // SAP -> SQL
                    alfaEntity.SAP_to_SQL_Gemi(p_ListGemi, p_ListLog);
                }
            }

            // Grid
            grdViewTransferLog.Columns.Clear();
            grdTransferLog.DataSource = p_ListLog;

            // Info Message
            alfaMsg.Info("Transfer Ýþlemi Tamamlanmýþtýr ... !");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menuExportALL_ItemClick(object sender, ItemClickEventArgs e)
        {
            // SaveDialog
            SaveFileDialog sf = new SaveFileDialog();

            // Set Properties
            sf.Filter = "XLS Files (*.xls)|*.xls |TXT Files (*.txt)|*.txt |PDF Files (*.pdf)|*.pdf  |HTML Files (*.htm)|*.htm";
            sf.FilterIndex = Int32.Parse(e.Item.Tag.ToString());

            // Check for Cancel
            if (sf.ShowDialog() != DialogResult.OK) return;

            // Update UI
            this.Update();

            // Export to Target
            switch (e.Item.Name)
            {
                case "menuExportXLS": m_grdViewActive.ExportToXls(sf.FileName); break;
                case "menuExportTXT": m_grdViewActive.ExportToText(sf.FileName); break;
                case "menuExportPDF": m_grdViewActive.ExportToPdf(sf.FileName); break;
                case "menuExportHTM": m_grdViewActive.ExportToHtml(sf.FileName); break;
            }

            // Result Message
            alfaMsg.Info("Export Ýþlemi Baþarýlý Bir Þekilde Tamamlanmýþtýr ...!");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menuKayitYAZ_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (m_grdViewActive == grdViewAracAktif)  // AKTIF
                {
                    // Get Aktif
                    var p_Aktif = (TableAracAktif)grdViewAracAktif.GetFocusedRow();

                    // Set Title
                    string p_Title = String.Format("({0})  {1}", p_Aktif.KNo, alfaStr.Tartim01);

                    // Create FrmReport
                    FrmKantarFisi rep = new FrmKantarFisi(p_Aktif, p_Title);

                    // Show Report
                    if (rep.ShowDialog() == DialogResult.OK)
                    {
                        // Print Count
                        p_Aktif.CiktiNo += 1;

                        // Update
                        alfaEntity.AracAktif_Update(p_Aktif);

                        // Refresh
                        grdViewAracAktif.RefreshRow(grdViewAracAktif.FocusedRowHandle);
                    }
                }

                else if (m_grdViewActive == grdViewAracArsiv || m_grdViewActive == grdGemiArsivView)  // ARSIV
                {
                    // AracArsiv
                    TableAracArsiv p_Arsiv = null;
                    
                    // Get Arsiv
                    if (m_grdViewActive == grdViewAracArsiv) 
                          p_Arsiv = (TableAracArsiv)grdViewAracArsiv.GetFocusedRow();
                    else  p_Arsiv = (TableAracArsiv)grdGemiArsivView.GetFocusedRow();
                    
                    // Set Title
                    string p_Title = String.Format("({0})  {1}", p_Arsiv.KNo, alfaStr.Tartim02);

                    // Create FrmReport
                    FrmKantarFisi rep = new FrmKantarFisi(p_Arsiv, p_Title);

                    // Show Report
                    if (rep.ShowDialog() == DialogResult.OK)
                    {
                        // Print Count
                        p_Arsiv.CiktiNo += 1;

                        // Update
                        alfaEntity.AracArsiv_Update(p_Arsiv);

                        // Refresh
                        grdViewAracAktif.RefreshRow(grdViewAracAktif.FocusedRowHandle);
                    }
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menuKayitIPTAL_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (m_grdViewActive == grdViewAracAktif)  // AKTIF
                {
                    // Get Aktif
                    var p_Aktif = (TableAracAktif)grdViewAracAktif.GetFocusedRow();

                    // Confirmation
                    if (alfaMsg.Quest("( " + p_Aktif.PlakaNo + " )  Kaydý Ýptal Etmek için Emin misiniz ?") == DialogResult.No) return;

                    // Create Yedek
                    var p_Yedek = new TableAracYedek();

                    // Aktif --> Yedek
                    alfaEntity.Copy_V1(p_Aktif, p_Yedek);

                    // Save Yedek
                    alfaEntity.AracYedek_Save(p_Aktif, p_Yedek, alfaStr.Delete);

                    // Delete Aktif
                    alfaEntity.AracAktif_Del(p_Aktif.PlakaNo);

                    // Refresh
                    this.btnClearArac_Click(null, null);

                    // Message
                    alfaMsg.Info("Kayýt Baþarýlý Bir Þekilde Ýptal Edilmiþtir.");
                }

                else if (m_grdViewActive == grdViewAracArsiv || m_grdViewActive == grdGemiArsivView)  // ARSIV
                {
                    // AracArsiv
                    TableAracArsiv p_Arsiv = null;
                    
                    // Get Arsiv
                    if (m_grdViewActive == grdViewAracArsiv) 
                          p_Arsiv = (TableAracArsiv)grdViewAracArsiv.GetFocusedRow();
                    else  p_Arsiv = (TableAracArsiv)grdGemiArsivView.GetFocusedRow();

                    // Check for Canceling
                    if (!alfaSession.Admin && p_Arsiv.SapFisNo.Length > 0 && p_Arsiv.SapFisNo.Substring(0, 1) == "1")
                    {
                        alfaMsg.Error("ÝÇ PÝYASA SATIÞ Kaydýný Ýptal Etme Yetkiniz Bulunmamaktadýr ... !"); return;
                    }

                    // Confirmation
                    if (alfaMsg.Quest("( " + p_Arsiv.PlakaNo + " )  Kaydý Ýptal Etmek için Emin misiniz ?") == DialogResult.No) return;

                    // Create Aktif
                    TableAracAktif p_Aktif = new TableAracAktif();

                    // Create Yedek
                    TableAracYedek p_Yedek = new TableAracYedek();

                    // Arsiv --> Aktif
                    alfaEntity.Copy_V1(p_Arsiv, p_Aktif);

                    // Arsiv --> Yedek
                    alfaEntity.Copy_V1(p_Arsiv, p_Yedek);

                    // Delete Arac from SAP
                    alfaEntity.SAP_Delete_ARAC(p_Arsiv);

                    // Assign Values
                    p_Aktif.Zaman2 = null;
                    p_Aktif.Tartim2 = 0;
                    p_Aktif.NetTutar = 0;
                    p_Aktif.NetGiris = 0;
                    p_Aktif.NetCikis = 0;
                    p_Aktif.KayitEden = this.m_Session.User;
                    p_Aktif.KayitDurumu = alfaStr.Cancel;
                    p_Aktif.KayitZamani = DateTime.Now;

                    // Add Aktif
                    if (alfaEntity.AracAktif_Add(p_Aktif))
                    {
                        // Delete Arsiv
                        alfaEntity.AracArsiv_Del(p_Arsiv.Guid);

                        // Save Yedek
                        alfaEntity.AracYedek_Save(p_Aktif, p_Yedek, alfaStr.Cancel);

                        // Refresh
                        this.btnListRapor_Click(null, null);
                        this.btnClearArac_Click(null, null);

                        // Message
                        alfaMsg.Info("Kayýt Baþarýlý Bir Þekilde Ýptal Edilmiþtir.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menuKayitGUNCELLE_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Check
            if (propGrid.SelectedObject !=null)
            {
                alfaMsg.Error("Tartým Yapýlýrken Kayýt Güncelleme Yapýlamaz ...!"); return;
            }

            try
            {
                if (m_grdViewActive == grdViewAracArsiv || m_grdViewActive == grdGemiArsivView) // ---> ARSIV
                {
                    // AracArsiv
                    TableAracArsiv p_Arsiv = null;
                    
                    // Get Arsiv
                    if (m_grdViewActive == grdViewAracArsiv) 
                          p_Arsiv = (TableAracArsiv)grdViewAracArsiv.GetFocusedRow();
                    else  p_Arsiv = (TableAracArsiv)grdGemiArsivView.GetFocusedRow();

                    // Create Aktif
                    var p_Aktif = new TableAracAktif();

                    // Copy Entity
                    alfaEntity.Copy_V1(p_Arsiv, p_Aktif);

                    // Form Edit
                    var frmEdit = new FrmEdit(" ARAÇ ARÞÝV ( Kayýt Güncelleme )", p_Aktif, "T2");

                    // Show Form
                    if (frmEdit.ShowDialog() != DialogResult.OK) return;

                    // Copy Back
                    alfaEntity.Copy_V1(p_Aktif, p_Arsiv);

                    // Update
                    alfaEntity.AracArsiv_Update(p_Arsiv);

                    // SAP Data Transfer
                    alfaEntity.SAP_Transfer_ARAC();

                    // Print Record
                    this.menuKayitYAZ_ItemClick(null, null);
                }

                else if (m_grdViewActive == grdViewAracAktif) // ---> AKTIF
                {
                    // Get Aktif
                    var p_Aktif_v1 = (TableAracAktif)grdViewAracAktif.GetFocusedRow();

                    // Create Aktif
                    var p_Aktif_v2 = new TableAracAktif();

                    // Copy Entity
                    alfaEntity.Copy_V1(p_Aktif_v1, p_Aktif_v2);

                    // Form Edit
                    var frmEdit = new FrmEdit(" ARAÇ AKTÝF ( Kayýt Güncelleme )", p_Aktif_v2, "T1");

                    // Show Form
                    if (frmEdit.ShowDialog() != DialogResult.OK) return;

                    // Copy Back
                    alfaEntity.Copy_V1(p_Aktif_v2, p_Aktif_v1);

                    // Update
                    alfaEntity.AracAktif_Update(p_Aktif_v1);

                    // Print Record
                    this.menuKayitYAZ_ItemClick(null, null);
                }

            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdALL_Export_MouseUp(object sender, MouseEventArgs e)
        {
            // Gumruk Modu
            if (alfaSession.Gumruk) return;

            if (e.Button == MouseButtons.Right)
            {
                // Check for Empty Records
                if ((sender as GridView).DataRowCount == 0) return;

                // Set Activator
                this.m_grdViewActive = sender as GridView;

                // Get HitInfo
                GridHitInfo hitInfo = this.m_grdViewActive.CalcHitInfo(e.Location);

                if (hitInfo.InRow)
                {
                    // Popup Menu
                    popupMenu.ShowPopup(Control.MousePosition);
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void statusMode_DoubleClick(object sender, ItemClickEventArgs e)
        {
            // SAP Connection
            alfaCfg.Load_SAP_Settings(radioSAP.SelectedIndex);

            // Get Selected Client
            string p_Client = radioSAP.Properties.Items[radioSAP.SelectedIndex].ToString();

            // Set Status
            statusMode.Caption = this.m_Session.GetSAP(p_Client);

            // Message
            alfaMsg.Info("SAP Server : " + statusMode.Caption);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void statusUser_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            // Check
            if (alfaSession.Gumruk) return;

            // Check UserName
            if (string.IsNullOrEmpty(txtUserName.Text)) return;

            // Set UserName
            this.m_Session.User = txtUserName.Text;

            // Create Form
            FrmUser frm = new FrmUser(this.m_Session);

            // Call Form
            if (frm.ShowDialog() == DialogResult.OK)
            {
                statusUser.Caption = String.Format("{0}", this.m_Session.User);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void statusSQL_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            // Check
            if (alfaSession.Gumruk) return;

            // Create Form
            FrmSqlServer frm = new FrmSqlServer(m_Session);

            // Call Form
            if (frm.ShowDialog() == DialogResult.OK)
            {
                statusSQL.Caption = String.Format("SQL : {0}", m_Session.DB);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void statusPortK1K2_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            // Check
            if (this.KANTAR1 == null || this.KANTAR2 == null || this.SENSOR1 == null) return;

            // Hide Tests
            txtTestK1.Hide();
            txtTestK2.Hide();

            // Close Ports
            this.KANTAR1.m_Port.Close();
            this.KANTAR2.m_Port.Close();
            this.SENSOR1.m_Port.Close();

            // Port Setup
            FrmPort frmPort = new FrmPort(this.KANTAR1, this.KANTAR2, this.SENSOR1);

            // Show Form
            frmPort.ShowDialog();

            // Update
            this.Set_Status_Fields();

            // Open Ports
            this.KANTAR1.SetStatus(this.KANTAR1.m_Port.Open(this.KANTAR1.m_Aktif));
            this.KANTAR2.SetStatus(this.KANTAR2.m_Port.Open(this.KANTAR2.m_Aktif));
            this.SENSOR1.SetStatus(this.SENSOR1.m_Port.Open(this.SENSOR1.m_Aktif));
                
            // Timer Start-Stop
            this.timerMain.Enabled = this.SENSOR1.m_Aktif;

            if (!this.SENSOR1.m_Aktif)
            {
                // Disable Sensor Leds
                SensorK1S1.StateIndex = alfaSensor.OFF;
                SensorK1S2.StateIndex = alfaSensor.OFF;
                SensorK1S3.StateIndex = alfaSensor.OFF;
                SensorK2S1.StateIndex = alfaSensor.OFF;
                SensorK2S2.StateIndex = alfaSensor.OFF;
                SensorK2S3.StateIndex = alfaSensor.OFF;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtDesen_EditValueChanged(object sender, EventArgs e)
        {
            // Get Desen
            var listDesen = alfaEntity.Desen_GetList().Where(tt => tt.Desen == txtDesen.Text).ToList();

            if (listDesen.Count > 0)
            {
                // Get Object
                TableDesen p_Desen = listDesen[0];

                // Get Aktif
                TableAracAktif p_Aktif = (TableAracAktif)propGrid.SelectedObject;

                // Copy Desen
                alfaEntity.Copy_V2(p_Desen, p_Aktif);
                 
                // Set Grid
                alfaVGrid.SetPropertyGridV1(propGrid, p_Aktif, "T1-T2");

                // Focus
                propGrid.Focus();
                propGrid.FocusFirst();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnAdminUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Update Record
                this.Admin_Tables_Update();
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnAdminInsert_Click(object sender, EventArgs e)
        {
            try
            {
                // Add Record
                this.Admin_Tables_Insert();
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnAdminDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Delete Record
                this.Admin_Tables_Delete();
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Admin_Tables_Items()
        {
            // Clear List
            listAdminTables.Items.Clear();

            // Add Items
            listAdminTables.Items.Add(new alfaItem(" ( 01 )  -  DESENLER    " , "TableDesen"));
            listAdminTables.Items.Add(new alfaItem(" ( 02 )  -  FIRMA       " , "TableFirma"));
            listAdminTables.Items.Add(new alfaItem(" ( 03 )  -  GEMI        " , "TableGemi"));
            listAdminTables.Items.Add(new alfaItem(" ( 04 )  -  MALZEME     " , "TableMalzeme"));
            listAdminTables.Items.Add(new alfaItem(" ( 05 )  -  NAKLIYE     " , "TableNakliye"));
            listAdminTables.Items.Add(new alfaItem(" ( 06 )  -  BEYANNAME   " , "TableBeyanname"));
            listAdminTables.Items.Add(new alfaItem(" ( 07 )  -  SEVKYERÝ    " , "TableLokasyon"));
            listAdminTables.Items.Add(new alfaItem(" ( 08 )  -  AÇIKLAMA    " , "TableAciklama"));
            listAdminTables.Items.Add(new alfaItem(" ( 09 )  -  GUMRUK      " , "TableGumruk"));
            listAdminTables.Items.Add(new alfaItem(" ( 10 )  -  RENK        " , "TableRenk"));

            if (alfaSession.Admin)
            {
                // Add Users
                listAdminTables.Items.Add(new alfaItem(" ( 11 )  -  KULLANICI (Admin)", "TableKullanici"));
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Admin_Tables_Refresh()
        {
            using (alfaDS DS = new alfaDS())
            {
                // Get Item
                var p_Item = (alfaItem)listAdminTables.SelectedItem;
                
                // Clear
                grdAdminView.Columns.Clear();

                // Set DataSource
                switch (p_Item.Name)
                {
                    case "TableAciklama"  : grdAdmin.DataSource = DS.Context.TableAciklama.OrderBy(tt => tt.ID).ToList(); break;
                    case "TableBeyanname" : grdAdmin.DataSource = DS.Context.TableBeyanname.OrderBy(tt => tt.ID).ToList(); break;
                    case "TableDesen"     : grdAdmin.DataSource = DS.Context.TableDesen.OrderBy(tt => tt.ID).ToList(); break;
                    case "TableFirma"     : grdAdmin.DataSource = DS.Context.TableFirma.OrderBy(tt => tt.ID).ToList(); break;
                    case "TableGemi"      : grdAdmin.DataSource = DS.Context.TableGemi.OrderBy(tt => tt.ID).ToList(); break;
                    case "TableGumruk"    : grdAdmin.DataSource = DS.Context.TableGumruk.OrderBy(tt => tt.ID).ToList(); break;
                    case "TableKullanici" : grdAdmin.DataSource = DS.Context.TableKullanici.OrderBy(tt => tt.ID).ToList(); break;
                    case "TableMalzeme"   : grdAdmin.DataSource = DS.Context.TableMalzeme.OrderBy(tt => tt.ID).ToList(); break;
                    case "TableNakliye"   : grdAdmin.DataSource = DS.Context.TableNakliye.OrderBy(tt => tt.ID).ToList(); break;
                    case "TableRenk"      : grdAdmin.DataSource = DS.Context.TableRenk.OrderBy(tt => tt.ID).ToList(); break;
                    case "TableLokasyon"  : grdAdmin.DataSource = DS.Context.TableLokasyon.Select(tt => new { tt.ID, SevkYeri = tt.Lokasyon }).OrderBy(tt => tt.ID).ToList(); break;
                }

                // Set GridView
                alfaGrid.SetView(grdAdminView);

                // Set Buttons
                alfaCtrl.SetButton(btnAdminUpdate, (grdAdminView.RowCount > 0));
                alfaCtrl.SetButton(btnAdminDelete, (grdAdminView.RowCount > 0));
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Admin_Tables_Update()
        {
            using (alfaDS DS = new alfaDS())
            {
                // Create Object
                Object objEntity = null;

                // Check
                if (grdAdminView.FocusedRowHandle < 0) return;

                // Get ID
                int p_ID = (int)grdAdminView.GetRowCellValue(grdAdminView.FocusedRowHandle, "ID");

                // Get Item
                alfaItem p_Item = (alfaItem)listAdminTables.SelectedItem;

                // Assign Object
                switch (p_Item.Name)
                {
                    case "TableAciklama"  : objEntity = DS.Context.TableAciklama.First(tt => tt.ID == p_ID); break;
                    case "TableBeyanname" : objEntity = DS.Context.TableBeyanname.First(tt => tt.ID == p_ID); break;
                    case "TableDesen"     : objEntity = DS.Context.TableDesen.First(tt => tt.ID == p_ID); break;
                    case "TableFirma"     : objEntity = DS.Context.TableFirma.First(tt => tt.ID == p_ID); break;
                    case "TableKullanici" : objEntity = DS.Context.TableKullanici.First(tt => tt.ID == p_ID); break;
                    case "TableGemi"      : objEntity = DS.Context.TableGemi.First(tt => tt.ID == p_ID); break;
                    case "TableGumruk"    : objEntity = DS.Context.TableGumruk.First(tt => tt.ID == p_ID); break;
                    case "TableLokasyon"  : objEntity = DS.Context.TableLokasyon.First(tt => tt.ID == p_ID); break;
                    case "TableMalzeme"   : objEntity = DS.Context.TableMalzeme.First(tt => tt.ID == p_ID); break;
                    case "TableNakliye"   : objEntity = DS.Context.TableNakliye.First(tt => tt.ID == p_ID); break;
                    case "TableRenk"      : objEntity = DS.Context.TableRenk.First(tt => tt.ID == p_ID); break;
                }

                // Create Form
                FrmRecord frm = new FrmRecord(p_Item.Text, objEntity, null, null);

                // Confirmation
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // SaveChanges
                    DS.Context.SaveChanges();

                    // DataSource
                    this.Admin_Tables_Refresh();

                    // Get Row
                    int p_RowHandle = grdAdminView.LocateByValue("ID", p_ID, null);

                    // Set GridView
                    alfaGrid.SelectRow(grdAdminView, p_RowHandle);
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Admin_Tables_Insert()
        {
            using (alfaDS DS = new alfaDS())
            {
                // Create Object
                Object objEntity = null;

                // Get Item
                alfaItem p_Item = (alfaItem)listAdminTables.SelectedItem;

                // Assign Object
                switch (p_Item.Name)
                {
                    case "TableAciklama"  : objEntity = new TableAciklama(); break;
                    case "TableBeyanname" : objEntity = new TableBeyanname(); break;
                    case "TableDesen"     : objEntity = new TableDesen(); break;
                    case "TableFirma"     : objEntity = new TableFirma(); break;
                    case "TableGemi"      : objEntity = new TableGemi(); break;
                    case "TableGumruk"    : objEntity = new TableGumruk(); break;
                    case "TableKullanici" : objEntity = new TableKullanici(); break;
                    case "TableLokasyon"  : objEntity = new TableLokasyon(); break;
                    case "TableMalzeme"   : objEntity = new TableMalzeme(); break;
                    case "TableNakliye"   : objEntity = new TableNakliye(); break;
                    case "TableRenk"      : objEntity = new TableRenk(); break;
                }

                // Create Form
                FrmRecord frm = new FrmRecord(p_Item.Text, objEntity, null, null);

                // Confirmation
                if (frm.ShowDialog() != DialogResult.OK) return;

                // Add Record
                switch (p_Item.Name)
                {
                    case "TableAciklama"   : DS.Context.TableAciklama.AddObject(objEntity as TableAciklama); break;
                    case "TableBeyanname"  : DS.Context.TableBeyanname.AddObject(objEntity as TableBeyanname); break;
                    case "TableDesen"      : DS.Context.TableDesen.AddObject(objEntity as TableDesen); break;
                    case "TableFirma"      : DS.Context.TableFirma.AddObject(objEntity as TableFirma); break;
                    case "TableGemi"       : DS.Context.TableGemi.AddObject(objEntity as TableGemi); break;
                    case "TableGumruk"     : DS.Context.TableGumruk.AddObject(objEntity as TableGumruk); break;
                    case "TableKullanici"  : DS.Context.TableKullanici.AddObject(objEntity as TableKullanici); break;
                    case "TableLokasyon"   : DS.Context.TableLokasyon.AddObject(objEntity as TableLokasyon); break;
                    case "TableMalzeme"    : DS.Context.TableMalzeme.AddObject(objEntity as TableMalzeme); break;
                    case "TableNakliye"    : DS.Context.TableNakliye.AddObject(objEntity as TableNakliye); break;
                    case "TableRenk"       : DS.Context.TableRenk.AddObject(objEntity as TableRenk); break;
                }

                // SaveChanges
                DS.Context.SaveChanges();
            }

            // DataSource
            this.Admin_Tables_Refresh();

            // Select Row
            alfaGrid.SelectRow(grdAdminView, grdAdminView.RowCount - 1);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Admin_Tables_Delete()
        {
            // Confirmation
            if (alfaMsg.Quest("Seçilen Kaydý Silmek için Emin misiniz ?") == DialogResult.No) return;

            using (alfaDS DS = new alfaDS())
            {
                // Get Item
                alfaItem p_Item = (alfaItem)listAdminTables.SelectedItem;

                // Get ID
                int p_ID = (int)grdAdminView.GetRowCellValue(grdAdminView.FocusedRowHandle, "ID");

                // Delete Object
                switch (p_Item.Name)
                {
                    case "TableAciklama"  : DS.Context.TableAciklama.DeleteObject(DS.Context.TableAciklama.First(tt => tt.ID == p_ID)); break;
                    case "TableBeyanname" : DS.Context.TableBeyanname.DeleteObject(DS.Context.TableBeyanname.First(tt => tt.ID == p_ID)); break;
                    case "TableDesen"     : DS.Context.TableDesen.DeleteObject(DS.Context.TableDesen.First(tt => tt.ID == p_ID)); break;
                    case "TableFirma"     : DS.Context.TableFirma.DeleteObject(DS.Context.TableFirma.First(tt => tt.ID == p_ID)); break;
                    case "TableGemi"      : DS.Context.TableGemi.DeleteObject(DS.Context.TableGemi.First(tt => tt.ID == p_ID)); break;
                    case "TableGumruk"    : DS.Context.TableGumruk.DeleteObject(DS.Context.TableGumruk.First(tt => tt.ID == p_ID)); break;
                    case "TableKullanici" : DS.Context.TableKullanici.DeleteObject(DS.Context.TableKullanici.First(tt => tt.ID == p_ID)); break;
                    case "TableLokasyon"  : DS.Context.TableLokasyon.DeleteObject(DS.Context.TableLokasyon.First(tt => tt.ID == p_ID)); break;
                    case "TableMalzeme"   : DS.Context.TableMalzeme.DeleteObject(DS.Context.TableMalzeme.First(tt => tt.ID == p_ID)); break;
                    case "TableNakliye"   : DS.Context.TableNakliye.DeleteObject(DS.Context.TableNakliye.First(tt => tt.ID == p_ID)); break;
                    case "TableRenk"      : DS.Context.TableRenk.DeleteObject(DS.Context.TableRenk.First(tt => tt.ID == p_ID)); break;
                }

                // SaveChanges
                DS.Context.SaveChanges();
            }

            // DataSource
            this.Admin_Tables_Refresh();

            //Focus
            grdAdminView.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void listAdminTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get Tables
            if (listAdminTables.Items.Count>0) this.Admin_Tables_Refresh();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdAdminView_DoubleClick(object sender, EventArgs e)
        {
            // Update
            this.btnAdminUpdate_Click(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdAdminView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                // Update 
                if (e.KeyCode == Keys.Enter && btnAdminUpdate.Enabled) this.btnAdminUpdate_Click(null, null);

                // Delete
                if (e.KeyCode == Keys.Delete && btnAdminDelete.Enabled) this.btnAdminDelete_Click(null, null);

                // Insert
                if (e.KeyCode == Keys.Insert && btnAdminInsert.Enabled) this.btnAdminInsert_Click(null, null);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdViewAracArsiv_DoubleClick(object sender, EventArgs e)
        {
            // Check
            if (grdViewAracArsiv.RowCount == 0) return;

            // Get Arsiv
            var p_Arsiv = (TableAracArsiv)grdViewAracArsiv.GetFocusedRow();

            // Check
            if (p_Arsiv == null) return;

            // Guid Clicked
            if (grdViewAracArsiv.FocusedColumn.FieldName == "Guid" && pageYedek.PageVisible == true) this.GetKayitTarihcesi(p_Arsiv.Guid);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdViewAracAktif_DoubleClick(object sender, EventArgs e)
        {
            // Check
            if (grdViewAracAktif.RowCount == 0) return;

            // Get Aktif
            var p_Aktif = (TableAracAktif)grdViewAracAktif.GetFocusedRow();

            // Check
            if (p_Aktif == null) return;

            if ("PlakaNo-SapFisNo".Contains(grdViewAracAktif.FocusedColumn.FieldName))  // PLAKA_NO - SAP_FISNO
            {
                // Clear
                this.btnClearArac_Click(null, null);

                // Set Values
                txtPlakaNo.Text = p_Aktif.PlakaNo;
                txtSapFisNo.Text = p_Aktif.SapFisNo;

                // Set Controls
                if (string.IsNullOrEmpty(p_Aktif.SapFisNo))
                     this.txtPlakaNo_KeyUp(null, new KeyEventArgs(Keys.Enter));
                else this.txtSapFisNo_KeyUp(null, new KeyEventArgs(Keys.Enter));
            }

            else if (grdViewAracAktif.FocusedColumn.FieldName == "Guid" && pageYedek.PageVisible == true) // GUID
            {
                // Guid Clicked
                this.GetKayitTarihcesi(p_Aktif.Guid);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdYedekView_DoubleClick(object sender, EventArgs e)
        {
            // Check
            if (grdYedek01View.RowCount == 0) return;

            // Get Yedek
            var p_Yedek = (TableAracYedek)grdYedek01View.GetFocusedRow();

            // Guid Clicked
            if (grdYedek01View.FocusedColumn.FieldName == "Guid") this.GetKayitTarihcesi(p_Yedek.Guid);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void GetKayitTarihcesi(Guid p_Guid)
        {
            // Get Yedek List
            var listYedek = alfaEntity.AracYedek_GetList(p_Guid);

            // Check
            if (listYedek.Count ==0)
            {
                alfaMsg.Error("UYARI :  Kaydýn Yedek Tabloda Herhangi bir Hareketi Yoktur ... !"); return;
            }

            // Get Arsiv List
            var listAktif = alfaEntity.AracAktif_GetList_V3(p_Guid);

            // Get Arsiv List
            var listArsiv = alfaEntity.AracArsiv_GetList(p_Guid);

            // Add Aktif Item
            if (listAktif.Count == 1)
            {
                // Create Yedek
                TableAracYedek p_YedekNew = new TableAracYedek();

                // Copy
                alfaEntity.Copy_V1(listAktif[0], p_YedekNew);

                // Add
                listYedek.Add(p_YedekNew);
            }

            // Add Arsiv Item
            if (listArsiv.Count == 1)
            {
                // Create Yedek
                TableAracYedek p_YedekNew = new TableAracYedek();

                // Copy
                alfaEntity.Copy_V1(listArsiv[0], p_YedekNew);

                // Add
                listYedek.Add(p_YedekNew);
            }

            // Assign Grid Yedek
            grdYedek02View.Columns.Clear();
            grdYedek02View.GridControl.DataSource = listYedek;

            // Show Page
            tabControlMain.SelectedTabPage = pageYedek;
            tabYedek.SelectedTabPage = pageYedek02;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnLogClear_Click(object sender, EventArgs e)
        {
            // Clear Log
            LOGK1.Text = string.Empty;
            LOGK2.Text = string.Empty;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnLogStart_Click(object sender, EventArgs e)
        {
            // Focus
            grpLogSelection.Focus();

            if (btnLogStart.Text == "Start")
            {
                // Stop
                btnLogStart.Text = "Stop";
                btnLogStart.Appearance.BackColor = Color.Red;
            }
            else
            {
                // Start
                btnLogStart.Text = "Start";
                btnLogStart.Appearance.BackColor = Color.Green;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop Timer
            this.timerMain.Stop();

            // Close Ports
            if (this.KANTAR1 != null) this.KANTAR1.m_Port.Close();
            if (this.KANTAR2 != null) this.KANTAR2.m_Port.Close();

            // Save Variants
            this.GridView_Variant(false);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void splitContainerControl_Resize(object sender, EventArgs e)
        {
            // Panel Widths
            splitContainerTest.SplitterPosition = (int)(splitContainerTest.Width / 2);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtYedekALL_KeyUp(object sender, KeyEventArgs e)
        {
            // Set DateTimes
            txtYedekDate01.Enabled = string.IsNullOrEmpty(txtYedekGuid.Text);
            txtYedekDate02.Enabled = string.IsNullOrEmpty(txtYedekGuid.Text);

            // Set Colors
            if (txtYedekDate01.Enabled) txtYedekDate01.ForeColor = Color.Black; else txtYedekDate01.ForeColor = Color.Silver;
            if (txtYedekDate02.Enabled) txtYedekDate02.ForeColor = Color.Black; else txtYedekDate02.ForeColor = Color.Silver;
            
            // List
            if (e.KeyData == Keys.Enter && btnYedekList.Enabled == true) this.btnYedekList_Click(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnGemi01List_Click(object sender, EventArgs e)
        {
            // Get List
            var listGemiHareket = alfaEntity.GemiHareket_GetList_FromSQL(txtGemi01DateStart.DateTime, txtGemi01DateFinish.DateTime, null, (bool)radioGemi01.EditValue, false, true);

            // Format List
            var p_ListV1 = listGemiHareket.Select( tt=> new ListGemiV1{ ID = tt.ID, 
                                                                        GemiAdi = tt.GemiAdi, 
                                                                        Malzeme = tt.Malzeme, 
                                                                        Acenta = tt.Acenta, 
                                                                        Tonaj = (int)tt.Tonaj, 
                                                                        NetTonaj = (int)tt.NetTonaj,
                                                                        Kalan = tt.Kalan,
                                                                        Yuzde = tt.Yuzde } ).ToList();

            // Assign to Grid
            grdGemi01View.Columns.Clear();
            grdGemi01View.GridControl.DataSource = p_ListV1;
            
            // Column Yuzde
            if (grdGemi01View.Columns["Yuzde"] != null)
            {
                grdGemi01View.Columns["Yuzde"].ColumnEdit = repProgressBar;
                grdGemi01View.Columns["Yuzde"].Width = 120;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnGemi01Clear_Click(object sender, EventArgs e)
        {
            // DateTime
            DateTime dtNow = DateTime.Now;
            dtNow = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);

            // Reset GemiDurum
            radioGemi01.SelectedIndex = 0;

            // DateTime Values
            txtGemi01DateStart.DateTime = dtNow.AddDays(-7);
            txtGemi01DateFinish.DateTime = dtNow.AddDays(3);

            // Clear GemiHareket
            grdGemi01View.Columns.Clear();
            grdGemi01View.GridControl.DataSource = null;

            // Clear GemiAmbar
            grdAmbar01View.Columns.Clear();
            grdAmbar01View.GridControl.DataSource = null;

            // Clear GemiArsiv
            grdGemiArsivView.GridControl.DataSource = null;

            // Focus
            grpGemi01.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdGemi01View_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            // Get ID
            int p_ID = Convert.ToInt32(grdGemi01View.GetRowCellValue(grdGemi01View.FocusedRowHandle, "ID"));

            // Get List
            var ListAmbar = alfaEntity.GemiAmbar_GetList(p_ID);

            // Set GridView
            grdAmbar01View.Columns.Clear();
            grdAmbar01View.GridControl.DataSource = ListAmbar.Select(tt => new ListAmbarV1 { No = tt.AmbarNo, Tonaj = (int)tt.Tonaj, NetTonaj = (int)tt.NetTonaj, Kalan = tt.Kalan, Yuzde = tt.Yuzde });

            // Column Hide
            alfaGrid.ColumnHide(grdAmbar01View, "ID-GemiNo");

            // Column Yuzde
            if (grdAmbar01View.Columns["Yuzde"] != null)
            {
                // Column Yuzde
                grdAmbar01View.Columns["Yuzde"].ColumnEdit = repProgressBar;
                grdAmbar01View.Columns["Yuzde"].Width = 120;
            }

            // Refresh
            grdAmbar01View.FocusedRowHandle = 10;
            grdAmbar01View.FocusedRowHandle = 0;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdAmbar01View_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            // GemiID
            int p_GemiID = Convert.ToInt32(grdGemi01View.GetRowCellValue(grdGemi01View.FocusedRowHandle, "ID"));

            // AmbarNo
            int p_Ambar = Convert.ToInt32(grdAmbar01View.GetRowCellValue(grdAmbar01View.FocusedRowHandle, "No"));

            // Set GridView
            grdGemiArsivView.GridControl.DataSource = alfaEntity.AracArsiv_GetList(txtGemi01DateStart.DateTime, txtGemi01DateFinish.DateTime, p_GemiID, p_Ambar);
            alfaGrid.SetView(grdGemiArsivView);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnGemi02Clear_Click(object sender, EventArgs e)
        {
            // DateTime
            DateTime dtNow = DateTime.Now;
            dtNow = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0);

            // DateTime Values
            txtGemi02DateStart.DateTime = dtNow.AddDays(-7);
            txtGemi02DateFinish.DateTime = dtNow.AddDays(1);

            // Reset GemiDurum
            radioGemi02.SelectedIndex = 0;

            // GemiAdi
            txtGemi02Adi.Text = string.Empty;

            // Clear Gemi
            grdGemi02View.Columns.Clear();
            grdGemi02View.GridControl.DataSource = null;

            // Clear Ambar
            grdAmbar02View.Columns.Clear();
            grdAmbar02View.GridControl.DataSource = null;

            // Gemi Close / Gemi Open
            alfaCtrl.ButtonDisable(btnGemiClose, Color.Gray);
            alfaCtrl.ButtonDisable(btnGemiOpen, Color.Gray);

            // Set Gemi Buttons
            alfaCtrl.ButtonDisable(btnGemiInsert);
            alfaCtrl.ButtonDisable(btnGemiUpdate);
            alfaCtrl.ButtonDisable(btnGemiDelete);
            alfaCtrl.ButtonDisable(btnGemiKargo);

            // Set Ambar Buttons
            alfaCtrl.ButtonDisable(btnAmbarInsert);
            alfaCtrl.ButtonDisable(btnAmbarUpdate);
            alfaCtrl.ButtonDisable(btnAmbarDelete);

            // Focus
            grpGemi02.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnGemi02List_Click(object sender, EventArgs e)
        {
            // Get Value
            bool? p_Value = Convert.ToBoolean(radioGemi02.EditValue);

            // Check Null
            if (radioGemi02.EditValue == null) p_Value = null;

            // Get List
            var listGemiHareket = alfaEntity.GemiHareket_GetList_FromSQL(txtGemi02DateStart.DateTime, txtGemi02DateFinish.DateTime, txtGemi02Adi.Text, p_Value, null, true);

            //  Format List
            var p_ListV2 = listGemiHareket.Select( tt=> new ListGemiV2{ ID = tt.ID, 
                                                                        GemiAdi = tt.GemiAdi, 
                                                                        Malzeme = tt.Malzeme, 
                                                                        Acenta = tt.Acenta, 
                                                                        SapSeferNo = tt.SapSeferNo,
                                                                        TripNo = tt.TripNo,
                                                                        AmbarSayisi = Convert.ToInt32(tt.AmbarSayisi), 
                                                                        Tonaj =  Convert.ToInt32(tt.Tonaj),
                                                                        NetTonaj = Convert.ToInt32(tt.NetTonaj),
                                                                        Kalan = Convert.ToInt32(tt.Kalan),
                                                                        Zaman1 = tt.Zaman1, 
                                                                        Zaman2 = tt.Zaman2, 
                                                                        NakilGemi = Convert.ToBoolean(tt.NakilGemi),
                                                                        Durum = Convert.ToBoolean(tt.Durum),
                                                                        Listeleme = Convert.ToBoolean(tt.Listeleme),
                                                                        GemiLinkNo = Convert.ToInt32(tt.GemiLinkNo)
                                                                        } ).ToList(); 

            // Assign to Grid
            grdGemi02View.Columns.Clear();
            grdGemi02View.GridControl.DataSource = p_ListV2.OrderBy(tt => tt.ID);

            // Refresh
            this.grdGemi02View_FocusedRowChanged(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdGemi02View_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            // Gemi
            var p_Gemi = (ListGemiV2)grdGemi02View.GetFocusedRow();

            // GemiID
            int p_GemiID = -1;

            // Set GemiID
            if (p_Gemi != null) p_GemiID = p_Gemi.ID;

            // Get List
            var ListAmbar = alfaEntity.GemiAmbar_GetList(p_GemiID);

            // Grid Ambar
            grdAmbar02View.Columns.Clear();
            grdAmbar02View.GridControl.DataSource = ListAmbar.Select(tt => new ListAmbarV1 { No = tt.AmbarNo, Tonaj = (int)tt.Tonaj, NetTonaj = (int)tt.NetTonaj, Kalan = tt.Kalan, Yuzde = tt.Yuzde });

            // Column Hide
            alfaGrid.ColumnHide(grdAmbar02View, "ID-GemiNo");

            // Column Yuzde
            if (grdAmbar02View.Columns["Yuzde"] != null)
            {
                grdAmbar02View.Columns["Yuzde"].ColumnEdit = repProgressBar;
                grdAmbar02View.Columns["Yuzde"].Width = 120;
            }

            // Set Gemi Buttons
            alfaCtrl.SetButton(btnGemiInsert, true);
            alfaCtrl.SetButton(btnGemiKargo, true);
            alfaCtrl.SetButton(btnGemiDelete, p_Gemi!=null && !p_Gemi.Durum && grdGemi02View.RowCount > 0);
            alfaCtrl.SetButton(btnGemiUpdate, p_Gemi!=null && !p_Gemi.Durum && grdGemi02View.RowCount > 0);

            // Set Ambar Buttons
            alfaCtrl.SetButton(btnAmbarInsert, p_Gemi!=null && !p_Gemi.Durum && grdGemi02View.RowCount > 0);
            alfaCtrl.SetButton(btnAmbarDelete, p_Gemi!=null && !p_Gemi.Durum && grdAmbar02View.RowCount > 0);
            alfaCtrl.SetButton(btnAmbarUpdate, p_Gemi!=null && !p_Gemi.Durum && grdAmbar02View.RowCount > 0);

            // Set GemiClose
            alfaCtrl.SetButtonV2(btnGemiClose, p_Gemi!=null && !p_Gemi.Durum && grdGemi02View.RowCount > 0);
            
            //  / GemiOpen
            alfaCtrl.SetButtonV2(btnGemiOpen, p_Gemi!=null &&  p_Gemi.Durum && grdGemi02View.RowCount > 0);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnGemiClose_Click(object sender, EventArgs e)
        {
            // GemiV2
            var p_GemiV2 = (ListGemiV2)grdGemi02View.GetFocusedRow();

            // Gemi
            var p_Gemi = alfaEntity.Gemi_Get(p_GemiV2.ID);

            // Check
            if (p_Gemi == null) return;

            // Title
            string p_Title = string.Format("({0}) {1} Gemisini Ýþleme Kapatmak için Emin Misiniz ?", p_Gemi.ID, p_Gemi.GemiAdi);

            // Confirmation
            if (alfaMsg.Quest(p_Title) == DialogResult.Yes)
            {
                // Get DateTime
                DateTime p_DateTime = DateTime.Now;

                // Close
                p_Gemi.Durum = true;
                p_GemiV2.Durum = true;
                p_Gemi.Zaman2 = p_DateTime;
                p_GemiV2.Zaman2 = p_DateTime;

                // Transfer SAP
                alfaEntity.SAP_Transfer_GEMI(p_Gemi, string.Empty);

                // Update 
                alfaEntity.TableGemiHareket_Update(p_Gemi);

                // Refresh
                grdGemi02View.RefreshRow(grdGemi02View.FocusedRowHandle);

                // Refresh
                this.grdGemi02View_FocusedRowChanged(null, null);

                // Focus
                grdGemi02View.Focus();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnGemiOpen_Click(object sender, EventArgs e)
        {
            // GemiV2
            var p_GemiV2 = (ListGemiV2)grdGemi02View.GetFocusedRow();

            // Gemi
            var p_Gemi = alfaEntity.Gemi_Get(p_GemiV2.ID);

            // Check
            if (p_Gemi == null) return;

            // Title
            string p_Title = string.Format("({0}) {1} Gemisini Ýþleme Açmak için Emin Misiniz ?", p_Gemi.ID, p_Gemi.GemiAdi);

            // Confirmation
            if (alfaMsg.Quest(p_Title) == DialogResult.Yes)
            {
                // Close
                p_Gemi.Durum = false;
                p_GemiV2.Durum = false;
                p_Gemi.Zaman2 = null;
                p_GemiV2.Zaman2 = null;

                // Transfer SAP
                alfaEntity.SAP_Transfer_GEMI(p_Gemi, string.Empty);

                // Update 
                alfaEntity.TableGemiHareket_Update(p_Gemi);

                // Refresh
                grdGemi02View.RefreshRow(grdGemi02View.FocusedRowHandle);

                // Refresh
                this.grdGemi02View_FocusedRowChanged(null, null);

                // Focus
                grdGemi02View.Focus();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnGemiInsert_Click(object sender, EventArgs e)
        {
            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // Create Gemi
                    TableGemiHareket p_Gemi = new TableGemiHareket();

                    // Zaman1
                    p_Gemi.Zaman1 = DateTime.Now;

                    // Durum
                    p_Gemi.Durum = false;

                    // NakilGemi
                    p_Gemi.NakilGemi = false;

                    // Create Form
                    FrmRecord frm = new FrmRecord("YENÝ GEMÝ", p_Gemi, null, "ID-Aktarma");

                    // Confirmation
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        // Add Record
                        DS.Context.TableGemiHareket.AddObject(p_Gemi);

                        // Save Changes
                        DS.Context.SaveChanges();

                        // Refresh
                        this.btnGemi02List_Click(null, null);

                        // Get Row
                        int p_RowHandle = grdGemi02View.LocateByValue("ID", p_Gemi.ID, null);

                        // Set GridView
                        alfaGrid.SelectRow(grdGemi02View, p_RowHandle);

                        // Transfer SAP
                        alfaEntity.SAP_Transfer_GEMI(p_Gemi, string.Empty);
                    }
                }
            }

            catch (Exception ex)
            {
                // Show Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnAmbarInsert_Click(object sender, EventArgs e)
        {
            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // Get GemiID
                    int p_GemiID = Convert.ToInt32(grdGemi02View.GetRowCellValue(grdGemi02View.FocusedRowHandle, "ID"));

                    // Check
                    if (p_GemiID == 0) return;

                    // Create Ambar
                    TableGemiAmbar p_Ambar = new TableGemiAmbar();

                    // Set GemiNo
                    p_Ambar.GemiNo = p_GemiID;

                    // Create Form
                    FrmRecord frm = new FrmRecord("YENÝ AMBAR", p_Ambar, null, "ID-Yuzde");

                    // Confirmation
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        // Add Record
                        DS.Context.TableGemiAmbar.AddObject(p_Ambar);

                        // Save Changes
                        DS.Context.SaveChanges();

                        // Refresh
                        this.grdGemi02View_FocusedRowChanged(null, null);

                        // Get Row
                        int p_RowHandle = grdAmbar02View.LocateByValue("No", p_Ambar.AmbarNo, null);

                        // Set GridView
                        alfaGrid.SelectRow(grdAmbar02View, p_RowHandle);
                    }
                }
            }
            catch (Exception ex)
            {
                // Show Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtGemi02Adi_KeyUp(object sender, KeyEventArgs e)
        {
            // Enter -> List
            if (!string.IsNullOrEmpty(txtGemi02Adi.Text) && e.KeyCode == Keys.Enter) this.btnGemi02List_Click(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtPlakaNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // BackSpace
            if (e.KeyChar == (char)Keys.Back) return;

            // Check for DOT
            if (txtPlakaNo.Text.Contains('.')) e.Handled = true;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnGemiUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Check
                if (grdGemi02View.FocusedRowHandle < 0) return;

                // GemiV2
                var p_GemiV2 = (ListGemiV2)grdGemi02View.GetFocusedRow();
                 
                // Gemi
                var p_Gemi = alfaEntity.Gemi_Get(p_GemiV2.ID);
                 
                // Title
                string p_Title = string.Format("({0})  {1}", p_Gemi.ID, p_Gemi.GemiAdi);

                // Create Form
                FrmRecord frm = new FrmRecord(p_Title, p_Gemi, null, null);

                // Confirmation
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Update
                    alfaEntity.TableGemiHareket_Update(p_Gemi);

                    // Copy
                    alfaEntity.Copy_V1(p_Gemi, p_GemiV2);

                    // Refresh
                    grdGemi02View.RefreshRow(grdGemi02View.FocusedRowHandle);

                    // Transfer SAP
                    alfaEntity.SAP_Transfer_GEMI(p_Gemi, string.Empty);
                }
            }

            catch (Exception ex)
            {
                // Show Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnAmbarUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Check
                if (!btnAmbarUpdate.Enabled) return;

                // Check
                if (grdAmbar02View.FocusedRowHandle < 0) return;

                // Get Ambar
                var p_AmbarV1 = (ListAmbarV1)grdAmbar02View.GetFocusedRow();

                // Get GemiID
                int p_GemiID = Convert.ToInt32(grdGemi02View.GetRowCellValue(grdGemi02View.FocusedRowHandle, "ID"));

                // Get Ambar
                var p_Ambar = alfaEntity.GemiAmbar_Get(p_GemiID, p_AmbarV1.No);

                // Create Form
                FrmRecord frm = new FrmRecord("AMBAR GÜNCELLEME", p_Ambar, null, "ID-Yuzde");

                // Confirmation
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Update
                    alfaEntity.TableGemiAmbar_Update(p_Ambar, p_AmbarV1.No);

                    // Copy Back
                    p_AmbarV1.No = p_Ambar.AmbarNo;
                    p_AmbarV1.Tonaj = (int)p_Ambar.Tonaj;

                    // Refresh
                    grdAmbar02View.RefreshRow(grdAmbar02View.FocusedRowHandle);
                }
            }
            catch (Exception ex)
            {
                // Show Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdGemi02View_DoubleClick(object sender, EventArgs e)
        {
            // Update
            this.btnGemiUpdate_Click(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdAmbar02View_DoubleClick(object sender, EventArgs e)
        {
            // Update
            this.btnAmbarUpdate_Click(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnGemiDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Check
                if (grdGemi02View.FocusedRowHandle < 0) return;

                // GemiV2
                var p_GemiV2 = (ListGemiV2)grdGemi02View.GetFocusedRow();
                 
                // Gemi
                var p_Gemi = alfaEntity.Gemi_Get(p_GemiV2.ID);

                // Check Arsiv
                if (alfaEntity.Gemi_Check(p_Gemi.ID))
                {
                    alfaMsg.Error("Bu Gemi Üzerinden Ýþlem Yapýldýðý Ýçin Silme Yapýlamaz ... !"); return;
                }

                // Title
                string p_Title = string.Format("({0}) {1} Gemisini Silmek Ýçin Emin misiniz ?", p_Gemi.ID, p_Gemi.GemiAdi);

                // Confirmation
                if (alfaMsg.Quest(p_Title) == DialogResult.Yes)
                {
                    // Delete Ambar
                    alfaEntity.TableGemiAmbar_Delete(p_Gemi.ID, null);

                    // Delete Gemi
                    alfaEntity.TableGemiHareket_Delete(p_Gemi.ID);

                    // Delete Gemi from SAP
                    alfaEntity.SAP_Transfer_GEMI(p_Gemi, "X");

                    // Refresh
                    this.btnGemi02List_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                // Show Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnAmbarDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Check
                if (grdAmbar02View.FocusedRowHandle < 0) return;

                // Get Ambar
                var p_AmbarV1 = (ListAmbarV1)grdAmbar02View.GetFocusedRow();

                // Get GemiID
                int p_GemiID = Convert.ToInt32(grdGemi02View.GetRowCellValue(grdGemi02View.FocusedRowHandle, "ID"));

                // Title
                string p_Title = string.Format("({0}) Nolu Ambarý Silmek Ýçin Emin misiniz ?", p_AmbarV1.No);

                // Confirmation
                if (alfaMsg.Quest(p_Title) == DialogResult.Yes)
                {
                    // Delete Ambar
                    alfaEntity.TableGemiAmbar_Delete(p_GemiID, p_AmbarV1.No);

                    // Refresh
                    this.grdGemi02View_FocusedRowChanged(null, null);
                }
            }
            catch (Exception ex)
            {
                // Show Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void grdGemi02View_RowStyle(object sender, RowStyleEventArgs e)
        {
            // GridView
            GridView p_GridView = sender as GridView;

            // Check
            if (e.RowHandle < 0) return;

            // Durum
            bool p_Durum = Convert.ToBoolean(p_GridView.GetRowCellValue(e.RowHandle, "Durum"));

            if (p_Durum)
            {
                // Closed Items
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.DarkGray;
                e.Appearance.Font = new Font("Tahoma", 8, FontStyle.Bold);
            }
            else
	        {
                // Open Items
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.MediumSeaGreen;
                e.Appearance.Font = new Font("Tahoma", 8, FontStyle.Bold);
	        }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            // Control + Shift + L
            if (e.Control == true && e.Shift == true && e.KeyCode == Keys.L)
            {
                // SAP Systems
                radioSAP.Properties.Items[0].Enabled = true;
                radioSAP.Properties.Items[1].Enabled = true;
                radioSAP.Properties.Items[2].Enabled = true;

                // Check
                if (!pnMain.Visible) return;

                // Liman Prod OR Test Everybody
                if ((radioSAP.SelectedIndex == 0 && alfaSession.Liman) || radioSAP.SelectedIndex != 0)
                {
                    // Amir Onayi icin Sifre Sor
                    if (alfaSession.Liman && !txtTestK1.Visible)
                    {
                        // Create Form
                        FrmAmir frmAmir = new FrmAmir();

                        // Show Form
                        if (frmAmir.ShowDialog() != DialogResult.OK) return;
                    }

                    // Manual Tartim
                    if (this.KANTAR1.m_Aktif) txtTestK1.Visible = !txtTestK1.Visible;
                    if (this.KANTAR2.m_Aktif) txtTestK2.Visible = !txtTestK2.Visible;

                    // Set Port1
                    if (txtTestK1.Visible) this.KANTAR1.SetStatus(this.KANTAR1.m_Port.Open(false));
                    else this.KANTAR1.SetStatus(this.KANTAR1.m_Port.Open(this.KANTAR1.m_Aktif));

                    // Set Port2
                    if (txtTestK2.Visible) this.KANTAR2.SetStatus(this.KANTAR2.m_Port.Open(false));
                    else this.KANTAR2.SetStatus(this.KANTAR2.m_Port.Open(this.KANTAR2.m_Aktif));

                    // Focus
                    if (txtTestK1.Visible) txtTestK1.Focus();
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtTestK1_KeyUp(object sender, KeyEventArgs e)
        {
            // Check
            if (string.IsNullOrEmpty(txtTestK1.Text) || (e.KeyCode != Keys.Enter)) return;

            try
            {
                // Set Value
                this.KANTAR1.m_Value = Convert.ToInt32(txtTestK1.Text);

                // Set Weigth
                this.Process_Weight(this.KANTAR1);
            }
            catch (Exception ex)
            {
                // Message
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtTestK2_KeyUp(object sender, KeyEventArgs e)
        {
            // Check
            if (string.IsNullOrEmpty(txtTestK2.Text) || (e.KeyCode != Keys.Enter)) return;

            try
            {
                // Set Value
                this.KANTAR2.m_Value = Convert.ToInt32(txtTestK2.Text);

                // Set Weigth
                this.Process_Weight(this.KANTAR2);
            }

            catch (Exception ex)
            {
                // Message
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void lbLocation_DoubleClick(object sender, EventArgs e)
        {
            if (radioSAP.SelectedIndex != 0)
            {
                // Create Form
                FrmLocation p_Form = new FrmLocation();

                // Show Form
                p_Form.ShowDialog();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            // Restart
            pnMain.Hide();
            pnLogin.Show();

            // Reset Ports
            this.KANTAR1.m_Port.Close();
            this.KANTAR2.m_Port.Close();
            this.SENSOR1.m_Port.Close();

            // Reset User
            statusUser.Caption = "User : ?";

            // Clear
            this.btnClearLogin_Click(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // Row Handle
            int currentRowHandle = grdViewAracArsiv.GetVisibleRowHandle(0);

            // Arac List
            var p_ListArac = new List<TableAracArsiv>();

            while (currentRowHandle != DevExpress.XtraGrid.GridControl.InvalidRowHandle)
            {
                // Get Arac
                TableAracArsiv p_Arac = (TableAracArsiv)grdViewAracArsiv.GetRow(currentRowHandle);

                // Add Arac
                p_ListArac.Add(p_Arac);

                // Get RowHandle
                currentRowHandle = grdViewAracArsiv.GetNextVisibleRow(currentRowHandle);
            }
            
            // Create FrmReport
            FrmKantarRapor rep = new FrmKantarRapor(p_ListArac.OrderBy(tt=>tt.Zaman2).ToList());

            // Show Report
            rep.ShowDialog();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void GridView_Variant(bool p_Load)
        {
            try
            {
                // Get Path
                string p_Path = Application.StartupPath + "\\Variants";

                // Create Directory
                if (!Directory.Exists(p_Path)) Directory.CreateDirectory(p_Path);

                // Get FileNames
                string p_FileAktif = string.Format("{0}\\Variants\\grdViewAracAktif_{1}.xml", Application.StartupPath, alfaSession.UserName);
                string p_FileArsiv = string.Format("{0}\\Variants\\grdViewAracArsiv_{1}.xml", Application.StartupPath, alfaSession.UserName);
                string p_FileGemix = string.Format("{0}\\Variants\\grdGemiArsivView_{1}.xml", Application.StartupPath, alfaSession.UserName);

                if (p_Load)
                {
                    // ForceInitialize
                    grdAracAktif.ForceInitialize();
                    grdAracArsiv.ForceInitialize();
                    grdGemiArsiv.ForceInitialize();

                    // Load Files
                    if (File.Exists(p_FileAktif)) grdViewAracAktif.RestoreLayoutFromXml(p_FileAktif);
                    if (File.Exists(p_FileArsiv)) grdViewAracArsiv.RestoreLayoutFromXml(p_FileArsiv);
                    if (File.Exists(p_FileGemix)) grdGemiArsivView.RestoreLayoutFromXml(p_FileGemix);
                }
                else
                {
                    // Save Files
                    grdViewAracAktif.SaveLayoutToXml(p_FileAktif);
                    grdViewAracArsiv.SaveLayoutToXml(p_FileArsiv);
                    grdGemiArsivView.SaveLayoutToXml(p_FileGemix);
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void menuResetGRID_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.m_grdViewActive == grdViewAracAktif)
            {
                // Clear
                this.grdViewAracAktif.Columns.Clear();

                // Get List
                this.btnClearArac_Click(null, null);
            }
            
            else if (this.m_grdViewActive == grdViewAracArsiv)
            {
                // Clear
                this.grdViewAracArsiv.Columns.Clear();

                // Get List
                this.btnListRapor_Click(null, null);
            }

            else if (this.m_grdViewActive == grdGemiArsivView)
            {
                // Clear
                this.grdGemiArsivView.Columns.Clear();

                // Get List
                this.btnGemi01List_Click(null, null);

                // Reset View
                this.grdGemi01View_FocusedRowChanged(null, null);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        protected override void OnHandleCreated(EventArgs e)
        {
            // Register USB
            base.OnHandleCreated(e);
            this.USB_4LED.RegisterHandle(Handle);
            this.USB_8LED.RegisterHandle(Handle);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        protected override void WndProc(ref Message m)
        {
            // Parse Message
            this.USB_4LED.ParseMessages(ref m);
            this.USB_8LED.ParseMessages(ref m);
            base.WndProc(ref m);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnTest_Click(object sender, EventArgs e)
        {
            txtTestK1.Visible = !txtTestK1.Visible;
            txtTestK2.Visible = !txtTestK2.Visible;
            txtTestK1.Text = "12500";
            txtTestK2.Text = "42500";
            txtTestK1_KeyUp(null, new KeyEventArgs(Keys.Enter));
            txtTestK2_KeyUp(null, new KeyEventArgs(Keys.Enter));
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnGemiKargo_Click(object sender, EventArgs e)
        {
            try
            {
                // Cursor
                alfaMsg.CursorWait();
            
                // Check
                if (grdGemi02View.FocusedRowHandle < 0) return;

                // GemiV2
                var p_GemiV2 = (ListGemiV2)grdGemi02View.GetFocusedRow();

                // Get Result
                DataTable p_TableResult = alfaEntity.KargoPlani(p_GemiV2.ID);

                // Title
                string p_Title = string.Format("({0})  {1}", p_GemiV2.ID, p_GemiV2.GemiAdi);

                // Create Report
                FrmKargoPlani rep = new FrmKargoPlani(p_TableResult, p_Title);

                // Show Report
                rep.ShowDialog();

                // Cursor
                alfaMsg.CursorDefult();

            }
            catch (Exception ex)
            {
                // Message
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void timerMain_Tick(object sender, EventArgs e)
        {
            // Send Command for Sensors
            if (PortS1.IsOpen) PortS1.Write(":010300030001F8");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void propGrid_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            if (propGrid.FocusedRow == propGrid.GetFirstVisible())
            {
                // Focus Kantar Buttons after PropGrid
                     if (this.btnKantarK1.Visible && this.btnKantarK1.Enabled) this.btnKantarK1.Focus(); 
                else if (this.btnKantarK2.Visible && this.btnKantarK2.Enabled) this.btnKantarK2.Focus();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnKantarK1_Enter(object sender, EventArgs e)
        {
            // Enable Focus
            btnKantarK1.Appearance.ForeColor = Color.White;
            btnKantarK1.Text = "(K1)";
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnKantarK1_Leave(object sender, EventArgs e)
        {
            // Disable Focus
            btnKantarK1.Appearance.ForeColor = Color.Black;
            btnKantarK1.Text = "K1";
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnKantarK2_Enter(object sender, EventArgs e)
        {
            // Enable Focus
            btnKantarK2.Appearance.ForeColor = Color.White;
            btnKantarK2.Text = "(K2)";
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnKantarK2_Leave(object sender, EventArgs e)
        {
            // Disable Focus
            btnKantarK2.Appearance.ForeColor = Color.Black;
            btnKantarK2.Text = "K2";
        }

       //-----------------------------------------------------------------------------------------------------------------------------------------//

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Set Flag
            this.m_IsTabKeyPressed = (keyData == Keys.Tab);

            // Call Base
            return base.ProcessCmdKey(ref msg, keyData);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnKantarK1_Validating(object sender, CancelEventArgs e)
        {
            if (this.m_IsTabKeyPressed)
            {
                // Clear Filag
                this.m_IsTabKeyPressed = false;

                // Focus 
                if (this.btnKantarK2.CanFocus) this.btnKantarK2.Focus(); else this.btnKantarK1.Focus();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void btnKantarK2_Validating(object sender, CancelEventArgs e)
        {
            if (this.m_IsTabKeyPressed)
            {
                // Clear Filag
                this.m_IsTabKeyPressed = false;

                // Focus 
                if (this.btnKantarK1.CanFocus) this.btnKantarK1.Focus(); else this.btnKantarK2.Focus();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtGemiAdiRapor_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            // Create Form
            FrmSearch p_Form = new FrmSearch("GemiAdi", txtGemiAdiRapor.Text);

            // Show Form
            if (p_Form.ShowDialog() == DialogResult.OK) txtGemiAdiRapor.Text = p_Form.ResultValue;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtFirmaAdiRapor_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            // Create Form
            FrmSearch p_Form = new FrmSearch("FirmaAdi", txtFirmaAdiRapor.Text);

            // Show Form
            if (p_Form.ShowDialog() == DialogResult.OK) txtFirmaAdiRapor.Text = p_Form.ResultValue;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtNakliyeRapor_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            // Create Form
            FrmSearch p_Form = new FrmSearch("Nakliye", txtNakliyeRapor.Text);

            // Show Form
            if (p_Form.ShowDialog() == DialogResult.OK) txtNakliyeRapor.Text = p_Form.ResultValue;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtSevkYeriRapor_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            // Create Form
            FrmSearch p_Form = new FrmSearch("SevkYeri", txtSevkYeriRapor.Text);

            // Show Form
            if (p_Form.ShowDialog() == DialogResult.OK) txtSevkYeriRapor.Text = p_Form.ResultValue;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtMalzemeRapor_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            // Create Form
            FrmSearch p_Form = new FrmSearch("Malzeme", txtMalzemeRapor.Text);

            // Show Form
            if (p_Form.ShowDialog() == DialogResult.OK) txtMalzemeRapor.Text = p_Form.ResultValue;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtGemiAdiRapor_KeyDown(object sender, KeyEventArgs e)
        {
            // F4 Call
            if (e.KeyCode == Keys.F4 && !e.Alt) this.txtGemiAdiRapor_ButtonClick(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtFirmaAdiRapor_KeyDown(object sender, KeyEventArgs e)
        {
            // F4 Call
            if (e.KeyCode == Keys.F4 && !e.Alt) this.txtFirmaAdiRapor_ButtonClick(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtMalzemeRapor_KeyDown(object sender, KeyEventArgs e)
        {
            // F4 Call
            if (e.KeyCode == Keys.F4 && !e.Alt) this.txtMalzemeRapor_ButtonClick(null, null);
        }

       //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtNakliyeRapor_KeyDown(object sender, KeyEventArgs e)
        {
            // F4 Call
            if (e.KeyCode == Keys.F4 && !e.Alt) this.txtNakliyeRapor_ButtonClick(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtSevkYeriRapor_KeyDown(object sender, KeyEventArgs e)
        {
            // F4 Call
            if (e.KeyCode == Keys.F4 && !e.Alt) this.txtSevkYeriRapor_ButtonClick(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtArsivOperator_KeyDown(object sender, KeyEventArgs e)
        {
            // F4 Call
            if (e.KeyCode == Keys.F4 && !e.Alt) this.txtArsivOperator_ButtonClick(null, null);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void txtArsivOperator_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            // Create Form
            FrmSearch p_Form = new FrmSearch("Operator", txtArsivOperator.Text);

            // Show Form
            if (p_Form.ShowDialog() == DialogResult.OK) txtArsivOperator.Text = p_Form.ResultValue;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void DisableSensors()
        {
            // Disable Sensor Leds
            SensorK1S1.StateIndex = alfaSensor.OFF;
            SensorK1S2.StateIndex = alfaSensor.OFF;
            SensorK1S3.StateIndex = alfaSensor.OFF;
            SensorK2S1.StateIndex = alfaSensor.OFF;
            SensorK2S2.StateIndex = alfaSensor.OFF;
            SensorK2S3.StateIndex = alfaSensor.OFF;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void gaugeLED1_DoubleClick(object sender, EventArgs e)
        {
            // Color Dialog
            var p_Color = new ColorDialog();

            // Set Properties
            p_Color.AllowFullOpen = false;
            p_Color.AnyColor = true;
            p_Color.SolidColorOnly = true;
            p_Color.Color = Color.Aqua;

            // Show and Select
            if (p_Color.ShowDialog() == DialogResult.OK)
            {
                // SetColor
                this.KANTAR1.m_DIG_Live.AppearanceOn.ContentBrush = new SolidBrushObject(p_Color.Color);

                // SetColor
                this.KANTAR1.m_Color = p_Color.Color.ToArgb();

                // Save
                alfaCfg.Save_PORT_Settings(this.KANTAR1);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void gaugeLED2_DoubleClick(object sender, EventArgs e)
        {
            // Color Dialog
            var p_Color = new ColorDialog();

            // Set Properties
            p_Color.AllowFullOpen = false;
            p_Color.AnyColor = true;
            p_Color.SolidColorOnly = true;
            p_Color.Color = Color.Yellow;

            // Show and Select
            if (p_Color.ShowDialog() == DialogResult.OK)
            {
                // SetColor
                this.KANTAR2.m_DIG_Live.AppearanceOn.ContentBrush = new SolidBrushObject(p_Color.Color);

                // SetColor
                this.KANTAR2.m_Color = p_Color.Color.ToArgb();

                // Save
                alfaCfg.Save_PORT_Settings(this.KANTAR2);
            }
        }

        //============================================================================================ USB - 4LED =================================//

        private void USB_4LED_OnDeviceArrived(object sender, EventArgs e)
        {
            // NULL
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void USB_4LED_OnDeviceRemoved(object sender, EventArgs e)
        {
            // NULL
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void USB_4LED_OnSpecifiedDeviceArrived(object sender, EventArgs e)
        {
            //if (this.InvokeRequired)
            //{
            //    // Thread Safe
            //    this.Invoke(new EventHandler(USB_4LED_OnSpecifiedDeviceArrived), new object[] { sender, e });
            //}
            //else
            //{
            //    // Message
            //    alfaCtrl.SetStatusText(statusUSBText, "Sensor : OK", Color.Green);
            //}
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void USB_4LED_OnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
            //if (this.InvokeRequired)
            //{
            //    // Thread Safe
            //    this.Invoke(new EventHandler(USB_4LED_OnSpecifiedDeviceRemoved), new object[] { sender, e });
            //}
            //else
            //{
            //    // Message
            //    alfaCtrl.SetStatusText(statusUSBText, "Sensor Cihazý Çýkartýldý ...", Color.Red);

            //    // Sensors OFF
            //    this.DisableSensors();
            //}
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void USB_4LED_OnDataRecieved(object sender, UsbLibrary.DataRecievedEventArgs args)
        {
            //if (this.InvokeRequired)
            //{
            //    try
            //    {
            //        // Thread Safe
            //        Invoke(new DataRecievedEventHandler(USB_4LED_OnDataRecieved), new object[] { sender, args });
            //    }
            //    catch 
            //    {
            //        // NULL
            //    }
            //}

            //else if (args.data.Count() == 9)
            //{
            //    // Kantar1 USB Sensor1
            //    if (args.data[3] == 1) SensorK1S1.StateIndex = alfaSensor.PASS; else SensorK1S1.StateIndex = alfaSensor.FAIL;

            //    // Kantar1 USB Sensor2
            //    if (args.data[4] == 1) SensorK1S2.StateIndex = alfaSensor.PASS; else SensorK1S2.StateIndex = alfaSensor.FAIL;
            //}
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void USB_4LED_OnDataSend(object sender, EventArgs e)
        {
            // NULL
        }

        //============================================================================================ USB - 8LED =================================//

        private void USB_8LED_OnDeviceArrived(object sender, EventArgs e)
        {
            // NULL
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void USB_8LED_OnDeviceRemoved(object sender, EventArgs e)
        {
            // NULL
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void USB_8LED_OnSpecifiedDeviceArrived(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                // Thread Safe
                this.Invoke(new EventHandler(USB_8LED_OnSpecifiedDeviceArrived), new object[] { sender, e });
            }
            else
            {
                // Message
                alfaCtrl.SetStatusText(statusUSBText, "Sensor : OK", Color.Green);

                // Set alfaUSB
                alfaUSB.Status = true;
                alfaUSB.ChipID = USB_8LED.VendorId;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void USB_8LED_OnSpecifiedDeviceRemoved(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                // Thread Safe
                this.Invoke(new EventHandler(USB_8LED_OnSpecifiedDeviceRemoved), new object[] { sender, e });
            }
            else
            {
                // Message
                alfaCtrl.SetStatusText(statusUSBText, "Sensor Cihazý Çýkartýldý ...", Color.Red);

                // Sensors OFF
                this.DisableSensors();

                // Set alfaUSB
                alfaUSB.Status = false;
                alfaUSB.ChipID = 0;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void USB_8LED_OnDataRecieved(object sender, DataRecievedEventArgs args)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    // Thread Safe
                    Invoke(new DataRecievedEventHandler(USB_8LED_OnDataRecieved), new object[] { sender, args });
                }
                catch {}
            }

            else if (args.data.Count() == 9)
            {
                if (alfaSensor.GetIndex("K1-S1") == null) SensorK1S1.StateIndex = alfaSensor.OFF;
                                                     else SensorK1S1.StateIndex = args.data[(int)alfaSensor.GetIndex("K1-S1")];
                
                if (alfaSensor.GetIndex("K1-S2") == null) SensorK1S2.StateIndex = alfaSensor.OFF;
                                                     else SensorK1S2.StateIndex = args.data[(int)alfaSensor.GetIndex("K1-S2")];

                if (alfaSensor.GetIndex("K1-S3") == null) SensorK1S3.StateIndex = alfaSensor.OFF;
                                                     else SensorK1S3.StateIndex = args.data[(int)alfaSensor.GetIndex("K1-S3")];

                if (alfaSensor.GetIndex("K2-S1") == null) SensorK2S1.StateIndex = alfaSensor.OFF;
                                                     else SensorK2S1.StateIndex = args.data[(int)alfaSensor.GetIndex("K2-S1")];
                
                if (alfaSensor.GetIndex("K2-S2") == null) SensorK2S2.StateIndex = alfaSensor.OFF;
                                                     else SensorK2S2.StateIndex = args.data[(int)alfaSensor.GetIndex("K2-S2")];

                if (alfaSensor.GetIndex("K2-S3") == null) SensorK2S3.StateIndex = alfaSensor.OFF;
                                                     else SensorK2S3.StateIndex = args.data[(int)alfaSensor.GetIndex("K2-S3")];

                // Set alfaUSB
                alfaUSB.ChipID = USB_8LED.VendorId;
                alfaUSB.Data = args.data;
                alfaUSB.Status = true;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void USB_8LED_OnDataSend(object sender, EventArgs e)
        {
            // NULL
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void statusUSB_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            // Check
            if (alfaUSB.ChipID != USB_8LED.VendorId) return;

            // USB Form
            var frmUSB = new FrmUSB();

            // Show Form
            frmUSB.ShowDialog();
        }

        //============================================================================================ USB - 8LED =================================//

    }
}