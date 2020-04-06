using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Xml.Xsl;
using System.IO.Ports;
using DevExpress.Utils;
using System.Reflection;
using System.Diagnostics;
using System.Collections;
using DevExpress.XtraGrid;
using DevExpress.XtraBars;
using System.Globalization;
using System.Windows.Forms;
using System.Configuration;
using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.XtraGauges.Win;
using System.Collections.Generic;
using DevExpress.XtraVerticalGrid;
using DevExpress.XtraGrid.Columns;
using System.Security.Cryptography;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraVerticalGrid.Rows;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGauges.Win.Gauges.State;
using DevExpress.XtraGauges.Win.Gauges.Digital;


namespace KrmKantar2013
{

    #region //-----------alfaSAP------------//

    public class alfaSAP : WR.ZWS_KANTAR_FUNCTIONS_V2
    {

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public alfaSAP()
        {
            // Set Credentials
            this.Credentials = new System.Net.NetworkCredential("webuser", "123456");
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
    }

    #endregion


    #region //-----------alfaItem-----------//

    public class alfaItem
    {
        // Fields
        private string m_EntityName;
        private string m_EntityText;

        // Properties
        public string Name { get { return this.m_EntityName;  }  }
        public string Text { get { return this.m_EntityText;  }  }

        // Constructor
        public alfaItem( string p_EntityText, string p_EntityName )
        {
            this.m_EntityName = p_EntityName;
            this.m_EntityText = p_EntityText;
        }

        // Override Method
        public override string ToString() { return this.m_EntityText; }
    }

    #endregion    
 

	#region //-----------alfaEnum-----------//

	public static class alfaResult
	{
		public static int None = 0;
		public static int Pass = 1;
        public static int Fail = 2;
	}

	# endregion


	#region //-----------alfaInputXML-------//

	public class alfaInputXML
	{
		// Fields
		private XmlNode m_XmlNode     = null;
		private string  m_ServiceName = null;
		private int     m_ServiceID   = 0;

		// Constructor
		public alfaInputXML( XmlNode p_XmlNode )
		{
			// Set Members
			m_XmlNode     = p_XmlNode;
			m_ServiceID   = Convert.ToInt32( p_XmlNode.Attributes["ID"].Value );
			m_ServiceName = Convert.ToString( p_XmlNode.Attributes["Name"].Value );
		}

		// Property
		public string AsHtml 
		{ 
			get {
					// Create XmlDoc
					XmlDocument doc = new XmlDocument();
					doc.LoadXml( m_XmlNode.OuterXml );

					// Get Html Format
					return alfaStr.GetHtmlFormat(doc);
				} 
		}

		// Property
		public int ServiceID { get {return m_ServiceID;} }

		// Property
		public XmlNode XmlNode { get {return m_XmlNode;} set {m_XmlNode=value;} }

		// Override Method
		public override string ToString()
		{
			return m_XmlNode.Name + "-->" + m_ServiceName;
		}
	}

	#endregion    


	#region //-----------alfaConvertXML-----//

	public class alfaConvertXML
	{
		// Fields
		private string      m_FormatHTML  = null;
		private string      m_FormatTEXT  = null;
		private DataTable   m_FormatTABLE = new DataTable();

		// Constructor
		public alfaConvertXML( string p_XML )
		{
			try 
			{
				// Save XML to Stream
				MemoryStream stream = new MemoryStream();
				XmlDocument doc = new XmlDocument();
				doc.LoadXml( p_XML );
				doc.Save( stream );

				// FormatHTML
				m_FormatHTML = alfaStr.GetHtmlFormat( doc );

				// FormatTEXT
				StreamReader sr = new StreamReader( stream, Encoding.UTF8 );
				stream.Position = 0;
				m_FormatTEXT = sr.ReadToEnd();
				
				// FormatTABLE
				DataSet ds = new DataSet();
				stream.Position = 0;
				ds.ReadXml( stream );
				m_FormatTABLE = ds.Tables[0];
			}
			catch(Exception ex)
			{
				// Set Nulls
				Console.WriteLine(ex.Message);
				m_FormatHTML  = "";
				m_FormatTEXT  = ""; 
				m_FormatTABLE = null;
			}
		}

		// Property HTML
		public string asHtml { get { return m_FormatHTML; } }

		// Property TEXT
		public string asText { get { return m_FormatTEXT; } }

		// Property TABLE
		public DataTable asTable { get { return m_FormatTABLE; } }

	}

	#endregion    
	

	#region //-----------alfaSec------------//

	public static class alfaSec
	{
		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static string Crypt(string s_Data, string s_Password, bool b_Encrypt) // Encryption & Decryption
		{
			byte[] u8_Salt = new byte[] { 0x26, 0x19, 0x81, 0x4E, 0xA0, 0x6D, 0x95, 0x34, 0x26, 0x75, 0x64, 0x05, 0xF6 };

			PasswordDeriveBytes i_Pass = new PasswordDeriveBytes(s_Password, u8_Salt);

			Rijndael i_Alg = Rijndael.Create();
			i_Alg.Key = i_Pass.GetBytes(32);
			i_Alg.IV = i_Pass.GetBytes(16);

			ICryptoTransform i_Trans = (b_Encrypt) ? i_Alg.CreateEncryptor() : i_Alg.CreateDecryptor();

			MemoryStream i_Mem = new MemoryStream();
			CryptoStream i_Crypt = new CryptoStream(i_Mem, i_Trans, CryptoStreamMode.Write);

			byte[] u8_Data;

			try
			{
				if (b_Encrypt) u8_Data = Encoding.Unicode.GetBytes(s_Data);
				else u8_Data = Convert.FromBase64String(s_Data);

				i_Crypt.Write(u8_Data, 0, u8_Data.Length);
				i_Crypt.Close();
			}
			catch { return null; }

			if (b_Encrypt) return Convert.ToBase64String(i_Mem.ToArray());
			else return Encoding.Unicode.GetString(i_Mem.ToArray());

		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static string EnCrypt(string  p_Message)
		{
			return alfaSec.Crypt(p_Message, "YUCELBORU", true);
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static string DeCrypt(string p_Message)
		{
			return alfaSec.Crypt(p_Message, "YUCELBORU", false);
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//
	}

	#endregion


	#region //-----------alfaMsg------------//

	public class alfaMsg
	{
        

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void CursorWait()
		{
			// Wait Cursor
			Cursor.Current = Cursors.WaitCursor;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void CursorDefult()
		{
			// Wait Cursor
			Cursor.Current = Cursors.Default;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static DialogResult Quest(string strMessage)
		{
			// Cursor
			Cursor.Current = Cursors.Default;

			// Show Message
			return MessageBox.Show(strMessage, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static DialogResult Error(string strMessage)
		{
			// Cursor
			Cursor.Current = Cursors.Default;

			// Show Message
			return MessageBox.Show(strMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static DialogResult Error(Exception p_Exception)
		{
			// Cursor
			Cursor.Current = Cursors.Default;

			// Message
			string strMessage = null;

			// Get Exception Message
			if (p_Exception.InnerException == null)
				strMessage = p_Exception.Message;
			else strMessage = p_Exception.InnerException.Message;
				
			// Show Message
			return MessageBox.Show(strMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static string GetError(Exception p_Exception)
		{
			// Message
			string strMessage = null;

			// Get Exception Message
			if (p_Exception.InnerException == null)
				strMessage = p_Exception.Message;
			else strMessage = p_Exception.InnerException.Message;
				
			// Get Message
            return strMessage;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool Show(string p_Type, string p_Message)
        {
            if (p_Type == "E")
            {
                // Fail
                alfaMsg.Error(p_Message); return false;
            }

            else
            {
                // Pass
                alfaMsg.Info(p_Message); return true;
            }
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static DialogResult Info(string strMessage)
		{
			// Cursor
			Cursor.Current = Cursors.Default;

			// Show Message
			return MessageBox.Show(strMessage, "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//
	}

	#endregion


	#region //-----------alfaStr------------//

	public class alfaStr
	{
		//-----------------------------------------------------------------------------------------------------------------------------------------//

		#region //---Member Fields---//

		// Client Info
		public static string Tartim01 = "TARTIM - I";
		public static string Tartim02 = "TARTIM - II";
		public static string Manuel = "MANUEL";
		public static string Update = "GUNCELLEME";
		public static string Current = "(AKTIF)";
		public static string Delete = "SILME";
		public static string Cancel = "IPTAL";
		public static string HEPSI = "HEPSI";
		public static string TAMAM = "TAMAM";
		public static string YUKLEME = "YUKLEME";
		public static string BOSALTMA = "BOSALTMA";

		#endregion

		//-----------------------------------------------------------------------------------------------------------------------------------------//
		
		public static void SetColumnsUpper( GridColumnCollection p_GridColumnCollection )
		{
			// Set UpperCase for Columns
			foreach( GridColumn col in p_GridColumnCollection ) 
			{                
				col.FieldName = col.FieldName.ToUpper( new CultureInfo("en-US") );
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static string GetAppVersion(bool FullVersion)
		{
			// Get FileVersionInfo
			FileVersionInfo fi = FileVersionInfo.GetVersionInfo(Application.ExecutablePath);

			// Get App Version
			if (FullVersion) return "v" + fi.FileVersion;
			else return "v" + fi.FileMajorPart + "." + fi.FileMinorPart;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static string GetHtmlFormat( XmlDocument p_XmlDoc )
		{
			// Get DefaultTSS File
			string str = KrmKantar2013.Properties.Resources.DefaulTSS;
			Stream s = new MemoryStream(ASCIIEncoding.Default.GetBytes(str));

			// Create Transform
			XmlReader xr = XmlReader.Create(s);
			XslCompiledTransform xct = new XslCompiledTransform();
			xct.Load(xr);

			// Convert to HTML
			StringBuilder sb = new StringBuilder();
			XmlWriter xw = XmlWriter.Create(sb);
			xct.Transform( p_XmlDoc, xw );

			// Return
			return sb.ToString();
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static string ByteArrayToStringV1(byte[] ba)
		{
			StringBuilder hex = new StringBuilder(ba.Length * 2);

			foreach (byte b in ba)
			{
				hex.AppendFormat("{0:x2}", b);
			}

			return hex.ToString();
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static string ByteArrayToStringV2(byte[] ba)
		{
			string hex = BitConverter.ToString(ba);
			return hex.Replace("-", "");
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string GetBeyanname(string p_Teslimat)
        {
            try
            {
                // Teslimant
                int p_Pos = p_Teslimat.LastIndexOf("-");

                // NewValue
                string p_NewValue = p_Teslimat.Substring(0, p_Pos);

                // Return
                return p_NewValue;
            }

            catch
            {
                // Error
                return string.Empty;
            }
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//
    
    }

	#endregion


	#region //-----------alfaDate-----------//

	public class alfaDate
	{

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string DTFormat = "yyyy-MM-dd  HH:mm:ss";
         
        //-----------------------------------------------------------------------------------------------------------------------------------------//

		public static string GetDateV1(DateTime p_DateTime) // yyyyMMdd
		{
			// Get Values
			string yrs = p_DateTime.Year.ToString("0000");
			string mon = p_DateTime.Month.ToString("00");
			string day = p_DateTime.Day.ToString("00");

			// Return
			return yrs + mon + day;
		}

        //-----------------------------------------------------------------------------------------------------------------------------------------//

		public static string GetDateV2(DateTime p_DateTime) // yyyy-MM-dd
		{
			// Get Values
			string yrs = p_DateTime.Year.ToString("0000");
			string mon = p_DateTime.Month.ToString("00");
			string day = p_DateTime.Day.ToString("00");

			// Return
            return string.Format("{0}-{1}-{2}", yrs, mon, day);
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static DateTime? GetDateV3(string p_DateTime) // 2013-07-31  00:01:06
		{
            if (!string.IsNullOrEmpty(p_DateTime))
            {
                // Get Values
                int yrs = Convert.ToInt16(p_DateTime.Substring(00, 4));
                int mon = Convert.ToInt16(p_DateTime.Substring(05, 2));
                int day = Convert.ToInt16(p_DateTime.Substring(08, 2));
                int hrs = Convert.ToInt16(p_DateTime.Substring(12, 2));
                int min = Convert.ToInt16(p_DateTime.Substring(15, 2));
                int sec = Convert.ToInt16(p_DateTime.Substring(18, 2));

                // Return
                return new DateTime(yrs, mon, day, hrs, min, sec);
            }

            // Return
            else return null;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static string GetTime(DateTime p_DateTime)
		{
			// Get Values
			string hrs = p_DateTime.Hour.ToString("00");
			string min = p_DateTime.Minute.ToString("00");
			string sec = p_DateTime.Second.ToString("00");

			// Return
			return hrs + min + sec;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static int GetTotalSecs(DateTime p_DateTime)
		{
			// Get Values
			int sec_hrs = p_DateTime.Hour * 60 * 60;
			int sec_min = p_DateTime.Minute * 60;
			int sec_sec = p_DateTime.Second * 1;

			// Return
			return sec_hrs + sec_min + sec_sec;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

	}

	#endregion


	#region //-----------alfaVer------------//

	public class alfaVer
	{
		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static string GetAppVersion()
		{
			// Get Versions
			int verMajor = Assembly.GetExecutingAssembly().GetName().Version.Major;
			int verMinor = Assembly.GetExecutingAssembly().GetName().Version.Minor;
			int verBuild = Assembly.GetExecutingAssembly().GetName().Version.Build;
			int verRevis = Assembly.GetExecutingAssembly().GetName().Version.Revision;

			// Return
			return "v" + verMajor + "." + verMinor + "." + verBuild  + "." + verRevis;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//
	}

	#endregion

    	
    #region //-----------alfaSession--------//

	public class alfaSession
	{
		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public string PC;
		public string DB;
		public string SAP;
		public string LocIP;
		public string Active;
		public string OsVer;
		public string AppVer;
		public string NetVer;
		public string User;
		public string Date;
		public string Time;
        public static bool Admin;
        public static bool Liman;
        public static bool Gumruk;
        public static string FullName;
        public static string UserName;
        public static string Lokasyon;

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public alfaSession()
		{
			// PC
            this.PC = System.Net.Dns.GetHostName().ToUpper();

		    // SQL Server
			using (alfaDS ent = new alfaDS())
			{
                this.DB = ent.Context.Connection.DataSource.ToUpper();
			}

			// SAP Server
			this.SAP = "Mode : ?";

			// IP Adress List
			System.Net.IPAddress[] ListIP = System.Net.Dns.GetHostEntry(this.PC).AddressList;

			// Local IP
			foreach (System.Net.IPAddress ip in ListIP)
			{
				if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					this.LocIP = ip.ToString();
				}
			}

			// Operating System Version
			this.OsVer = System.Environment.OSVersion.ToString();

			// Status
			this.Active = "Y";

			// Application Version
			this.AppVer = alfaVer.GetAppVersion();

			// Net Framework Version
			this.NetVer = System.Environment.Version.ToString();

			// UserName
			this.User = "?";

			// GetDT
			DateTime dtNow = DateTime.Now;

			// Date
			this.Date = alfaDate.GetDateV1(dtNow);

			// Time
			this.Time = alfaDate.GetTime(dtNow);

            // Lokasyon
            alfaSession.Lokasyon = alfaCfg.GetLocation();
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public string GetSAP(string p_Client)
		{
			try
			{
				// CursorWait
				alfaMsg.CursorWait();

				// Create Functions
                alfaSAP p_SAP = new alfaSAP();

				// Create Parameters
				WR.ZSD_F_KANTAR_02_V2 prms = new WR.ZSD_F_KANTAR_02_V2();
				prms.I_TEST = System.Net.Dns.GetHostName();

				// Call Service
				WR.ZSD_F_KANTAR_02_V2Response resp = p_SAP.ZSD_F_KANTAR_02_V2(prms);

				// CursorDefult
				alfaMsg.CursorDefult();

				// Return
				return p_Client;

			}
			catch (Exception ex)
			{
				// Error
				alfaMsg.Error(ex);
				return p_Client + " (OFFLINE)";
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public void RefreshLoginDateTime()
		{
			// GetDT
			DateTime dtNow = DateTime.Now;

			// Get Date
			this.Date = alfaDate.GetDateV1(dtNow);

			// Get Time
			this.Time = alfaDate.GetTime(dtNow);
		}

        //-----------------------------------------------------------------------------------------------------------------------------------------//
	}

	#endregion


	#region //-----------alfaCtrl-----------//

	public class alfaCtrl
	{

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void ButtonEnable(SimpleButton p_Button, string p_SkinName)
        {
            p_Button.LookAndFeel.UseDefaultLookAndFeel = false;
            p_Button.LookAndFeel.SkinName = p_SkinName;
            p_Button.Enabled = true;
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void ButtonEnable(SimpleButton p_Button)
		{
			p_Button.LookAndFeel.UseDefaultLookAndFeel = false;
			p_Button.LookAndFeel.SkinName = "Glass Oceans";
			p_Button.Enabled = true;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void ButtonDisable(SimpleButton p_Button)
		{
			p_Button.LookAndFeel.UseDefaultLookAndFeel = false;
            p_Button.LookAndFeel.SkinName = "DevExpress Dark Style";
			p_Button.Enabled = false;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void ButtonEnable(SimpleButton p_Button, Color p_Color)
		{
			p_Button.Enabled = true;
			p_Button.Appearance.BackColor = p_Color;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void ButtonDisable(SimpleButton p_Button, Color p_Color)
		{
			p_Button.Enabled = false;
			p_Button.Appearance.BackColor = p_Color;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void ControlEnable(Control p_Control, Color p_Color)
		{
			p_Control.Enabled = true;
			p_Control.BackColor = p_Color;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void ControlDisable(Control p_Control, Color p_Color)
		{
			p_Control.Enabled = false;
			p_Control.BackColor = p_Color;
		}

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SetButton(SimpleButton p_Button, bool p_Flag)
        {
            if (p_Flag)
            {
                p_Button.LookAndFeel.UseDefaultLookAndFeel = false;
                p_Button.LookAndFeel.SkinName = "Glass Oceans";
                p_Button.Enabled = true;
            }
            else
            {
                p_Button.LookAndFeel.UseDefaultLookAndFeel = false;
                p_Button.LookAndFeel.SkinName = "DevExpress Dark Style";
                p_Button.Enabled = false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SetButtonV2(SimpleButton p_Button, bool p_Flag)
        {
            if (p_Flag)
            {
                // Enable
                if (p_Button.Name == "btnGemiClose") alfaCtrl.ButtonEnable(p_Button, Color.Red);
                                                else alfaCtrl.ButtonEnable(p_Button, Color.Green);
            }
            else
            {
                // Disable
                alfaCtrl.ButtonDisable(p_Button, Color.Gray);
            }
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void SetResult( LabelControl p_txtResult, string p_ResultStr, int p_Status )
		{
			// Set Text
			p_txtResult.Text = p_ResultStr;

			// Set Colors
			switch ( p_Status )
			{
				case 0: p_txtResult.BackColor = Color.Gray; break;
				case 1: p_txtResult.BackColor = Color.Navy; break;
				case 2: p_txtResult.BackColor = Color.Red; break;
			}
		}

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SetStatusText( BarStaticItem p_Control, string p_Message, Color p_Color )
        {
            // Set Message
            p_Control.Caption = p_Message;

            // Set Color
            p_Control.ItemAppearance.Normal.ForeColor = p_Color;
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        public static int SetNetGiris(int p_Tartim1, int p_Tartim2)
        {
            if (p_Tartim1 > p_Tartim2)
                return p_Tartim1 - p_Tartim2;
            else return 0;
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        public static int SetNetCikis(int p_Tartim1, int p_Tartim2)
        {
            if (p_Tartim2 > p_Tartim1)
                return p_Tartim2 - p_Tartim1;
            else return 0;
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//
    
    }

	#endregion


	#region //-----------alfaCfg------------//

	public class alfaCfg
	{
		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void Add(string p_Key, string p_Value)
		{
			try
			{
				// Config
				Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

				// Check For Key
				if(cfgFile.AppSettings.Settings[p_Key] == null)
				{
					// Add Key
					cfgFile.AppSettings.Settings.Add(p_Key, p_Value);

					// Save Changes to File
					cfgFile.Save(ConfigurationSaveMode.Modified);
				}
			}
			catch (Exception ex)
			{
				// Error
				alfaMsg.Error(ex.Message); 
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string Load_LookAndFeel()
        {
            try
            {
				// Incase Add Missing Parameter
				alfaCfg.Add("APP_LookAndFeel", "Dark Side");

				// Config
				Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

				// Set Properties
				string p_Result = cfgFile.AppSettings.Settings["APP_LookAndFeel"].Value;

                // Return
                return p_Result;

            }
			catch (Exception ex)
			{
				// Error
                alfaMsg.Error(ex.Message); return null;
			}
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Save_LookAndFeel(string p_Name)
        {
			try
			{
				// Config
                Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

				// Set Location
                cfgFile.AppSettings.Settings["APP_LookAndFeel"].Value = p_Name;

				// Save Changes
				cfgFile.Save(ConfigurationSaveMode.Modified);
				
				// Force Reload
				ConfigurationManager.RefreshSection("appSettings");

			}
			catch (Exception ex)
			{
				// Error
				alfaMsg.Error(ex.Message);
			}
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void Load_USB_Settings()
		{
			try
			{
				// Incase Add Missing Parameters
				alfaCfg.Add("LED11", "YOK");
				alfaCfg.Add("LED22", "YOK");
				alfaCfg.Add("LED33", "YOK");
				alfaCfg.Add("LED44", "YOK");
				alfaCfg.Add("LED55", "YOK");
				alfaCfg.Add("LED66", "YOK");
				alfaCfg.Add("LED77", "YOK");
				alfaCfg.Add("LED88", "YOK");
                
                // Config
				Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Set Port Properties
                alfaSensor.LED11 = cfgFile.AppSettings.Settings["LED11"].Value;
                alfaSensor.LED22 = cfgFile.AppSettings.Settings["LED22"].Value;
                alfaSensor.LED33 = cfgFile.AppSettings.Settings["LED33"].Value;
                alfaSensor.LED44 = cfgFile.AppSettings.Settings["LED44"].Value;
                alfaSensor.LED55 = cfgFile.AppSettings.Settings["LED55"].Value;
                alfaSensor.LED66 = cfgFile.AppSettings.Settings["LED66"].Value;
                alfaSensor.LED77 = cfgFile.AppSettings.Settings["LED77"].Value;
                alfaSensor.LED88 = cfgFile.AppSettings.Settings["LED88"].Value;

			}
			catch (Exception ex)
			{
				// Error
				alfaMsg.Error(ex.Message);
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void Save_USB_Settings()
		{  
			try
			{
				// Config
				Configuration cfgFile = ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.None );

                // Set Port Properties
                cfgFile.AppSettings.Settings["LED11"].Value = alfaSensor.LED11;
                cfgFile.AppSettings.Settings["LED22"].Value = alfaSensor.LED22;
                cfgFile.AppSettings.Settings["LED33"].Value = alfaSensor.LED33;
                cfgFile.AppSettings.Settings["LED44"].Value = alfaSensor.LED44;
                cfgFile.AppSettings.Settings["LED55"].Value = alfaSensor.LED55;
                cfgFile.AppSettings.Settings["LED66"].Value = alfaSensor.LED66;
                cfgFile.AppSettings.Settings["LED77"].Value = alfaSensor.LED77;
                cfgFile.AppSettings.Settings["LED88"].Value = alfaSensor.LED88;

				// Save Changes to File
				cfgFile.Save(ConfigurationSaveMode.Modified);
				
				// Force Reload
				ConfigurationManager.RefreshSection("appSettings");

			}
			catch (Exception ex)
			{
				// Error
				alfaMsg.Error(ex.Message);
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void Load_PORT_Settings(alfaKntr p_Kntr)
		{
			try
			{
				// Incase Add Missing Parameters
				alfaCfg.Add(p_Kntr.m_KNO + "_Color", Color.Aqua.ToArgb().ToString());
				alfaCfg.Add(p_Kntr.m_KNO + "_PortKntr", "TUNAYLAR");
				alfaCfg.Add(p_Kntr.m_KNO + "_PortAktif", "True");
				alfaCfg.Add(p_Kntr.m_KNO + "_KantarSeriNo", "123456");
                alfaCfg.Add(p_Kntr.m_KNO + "_PortName", "COM1");
				alfaCfg.Add(p_Kntr.m_KNO + "_BaudRate", "9600");
				alfaCfg.Add(p_Kntr.m_KNO + "_DataBits", "8");
				alfaCfg.Add(p_Kntr.m_KNO + "_DiscardNull", "False");
				alfaCfg.Add(p_Kntr.m_KNO + "_DtrEnable", "False");
				alfaCfg.Add(p_Kntr.m_KNO + "_Handshake", "None");
				alfaCfg.Add(p_Kntr.m_KNO + "_Parity", "None");
				alfaCfg.Add(p_Kntr.m_KNO + "_ParityReplace", "63");
				alfaCfg.Add(p_Kntr.m_KNO + "_ReadBufferSize", "4096");
				alfaCfg.Add(p_Kntr.m_KNO + "_ReadTimeout", "-1");
				alfaCfg.Add(p_Kntr.m_KNO + "_ReceivedBytesThreshold", "1");
				alfaCfg.Add(p_Kntr.m_KNO + "_RtsEnable", "False");
				alfaCfg.Add(p_Kntr.m_KNO + "_StopBits", "One");
				alfaCfg.Add(p_Kntr.m_KNO + "_WriteBufferSize", "2048");
				alfaCfg.Add(p_Kntr.m_KNO + "_WriteTimeout", "-1");

				// Config
				Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

				// Set Custom Properties
                p_Kntr.m_Color = Convert.ToInt32(cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_Color"].Value);
				p_Kntr.m_PortKntr = cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_PortKntr"].Value;
                p_Kntr.m_Aktif = Convert.ToBoolean(cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_PortAktif"].Value);
                p_Kntr.m_KantarSeriNo = Convert.ToString(cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_KantarSeriNo"].Value);
                
                // Set Port Properties
                p_Kntr.m_Port.GetPort.PortName = cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_PortName"].Value;
                p_Kntr.m_Port.GetPort.BaudRate = Convert.ToInt16(cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_BaudRate"].Value);
				p_Kntr.m_Port.GetPort.DataBits = Convert.ToInt16(cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_DataBits"].Value);
				p_Kntr.m_Port.GetPort.DiscardNull = Convert.ToBoolean(cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_DiscardNull"].Value);
				p_Kntr.m_Port.GetPort.DtrEnable = Convert.ToBoolean(cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_DtrEnable"].Value);
				p_Kntr.m_Port.GetPort.Handshake = (Handshake)Enum.Parse(typeof(Handshake), cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_Handshake"].Value);
				p_Kntr.m_Port.GetPort.Parity = (Parity)Enum.Parse(typeof(Parity), cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_Parity"].Value);
				p_Kntr.m_Port.GetPort.ParityReplace = Convert.ToByte(cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_ParityReplace"].Value);
				p_Kntr.m_Port.GetPort.ReadBufferSize = Convert.ToInt16(cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_ReadBufferSize"].Value);
				p_Kntr.m_Port.GetPort.ReadTimeout = Convert.ToInt16(cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_ReadTimeout"].Value);
				p_Kntr.m_Port.GetPort.ReceivedBytesThreshold = Convert.ToInt16(cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_ReceivedBytesThreshold"].Value);
				p_Kntr.m_Port.GetPort.RtsEnable = Convert.ToBoolean(cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_RtsEnable"].Value);
				p_Kntr.m_Port.GetPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_StopBits"].Value);
				p_Kntr.m_Port.GetPort.WriteBufferSize = Convert.ToInt16(cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_WriteBufferSize"].Value);
				p_Kntr.m_Port.GetPort.WriteTimeout = Convert.ToInt16(cfgFile.AppSettings.Settings[p_Kntr.m_KNO + "_WriteTimeout"].Value);

			}
			catch (Exception ex)
			{
				// Error
				alfaMsg.Error(ex.Message);
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void Save_PORT_Settings(alfaKntr p_Kntr)
		{  
			try
			{
				// Config
				Configuration cfgFile = ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.None );

				// Set Custom Properties
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_PortKntr"               ].Value = p_Kntr.m_PortKntr;
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_PortAktif"              ].Value = p_Kntr.m_Aktif.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_Color"                  ].Value = p_Kntr.m_Color.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_KantarSeriNo"           ].Value = p_Kntr.m_KantarSeriNo.ToString();

                // Set Port Properties
                cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_PortName"               ].Value = p_Kntr.m_Port.GetPort.PortName.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_BaudRate"               ].Value = p_Kntr.m_Port.GetPort.BaudRate.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_DataBits"               ].Value = p_Kntr.m_Port.GetPort.DataBits.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_DiscardNull"            ].Value = p_Kntr.m_Port.GetPort.DiscardNull.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_DtrEnable"              ].Value = p_Kntr.m_Port.GetPort.DtrEnable.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_Handshake"              ].Value = p_Kntr.m_Port.GetPort.Handshake.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_Parity"                 ].Value = p_Kntr.m_Port.GetPort.Parity.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_ParityReplace"          ].Value = p_Kntr.m_Port.GetPort.ParityReplace.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_ReadBufferSize"         ].Value = p_Kntr.m_Port.GetPort.ReadBufferSize.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_ReadTimeout"            ].Value = p_Kntr.m_Port.GetPort.ReadTimeout.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_ReceivedBytesThreshold" ].Value = p_Kntr.m_Port.GetPort.ReceivedBytesThreshold.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_RtsEnable"              ].Value = p_Kntr.m_Port.GetPort.RtsEnable.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_StopBits"               ].Value = p_Kntr.m_Port.GetPort.StopBits.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_WriteBufferSize"        ].Value = p_Kntr.m_Port.GetPort.WriteBufferSize.ToString();
				cfgFile.AppSettings.Settings[ p_Kntr.m_KNO + "_WriteTimeout"           ].Value = p_Kntr.m_Port.GetPort.WriteTimeout.ToString();

				// Save Changes to File
				cfgFile.Save(ConfigurationSaveMode.Modified);
				
				// Force Reload
				ConfigurationManager.RefreshSection("appSettings");

			}
			catch (Exception ex)
			{
				// Error
				alfaMsg.Error(ex.Message);
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void Save_LOC_Settings(bool p_Value)
		{  
			try
			{
				// Config
				Configuration cfgFile = ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.None );

				// Set Location
                cfgFile.AppSettings.Settings["KROMAN_LIMAN"].Value = p_Value.ToString();

				// Save Changes
				cfgFile.Save(ConfigurationSaveMode.Modified);
				
				// Force Reload
				ConfigurationManager.RefreshSection("appSettings");

			}
			catch (Exception ex)
			{
				// Error
				alfaMsg.Error(ex.Message);
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void Load_SAP_Settings(int p_SelectedClient)
		{
			try
			{
				// Config
				Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

				// Get Settings
				string p_Client110P = cfgFile.AppSettings.Settings["SAP110P"].Value.ToString(); // PROD
				string p_Client150T = cfgFile.AppSettings.Settings["SAP150T"].Value.ToString(); // TEST
				string p_Client110Q = cfgFile.AppSettings.Settings["SAP110Q"].Value.ToString(); // QA

				// Target
				string p_Target = null;

				switch (p_SelectedClient)
				{
					case 0: p_Target = p_Client110P; break;
					case 1: p_Target = p_Client150T; break;
					case 2: p_Target = p_Client110Q; break;
				}

				// Set Target Connection
				KrmKantar2013.Properties.Settings.Default.Kantar2013_WR_service_kantar_functions = p_Target;

			}
			catch (Exception ex)
			{
				// Error
				alfaMsg.Error(ex.Message);
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool CheckLiman()
        {
            try
            {
				// Config
				Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Key
                var p_Key = cfgFile.AppSettings.Settings["KROMAN_LIMAN"];

				// Check For Key
                if (p_Key != null && p_Key.Value == "True")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
			catch (Exception ex)
			{
				// Error
                alfaMsg.Error(ex.Message); return false;
			}
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string GetLocation()
        {
            try
            {
				// Config
				Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                var p_Key = cfgFile.AppSettings.Settings["APP_LOCATION"];

				// Check For Key
                if (p_Key != null)
                {
                    return p_Key.Value.ToUpper().ToString();
                }
                else
                {
                    return "LOKASYON";
                }
            }
			catch (Exception ex)
			{
				// Error
                alfaMsg.Error(ex.Message); return "LOKASYON";
			}
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//
	}

	#endregion


	#region //-----------alfaPort-----------//

	public class alfaPort
	{
		//-----------------------------------------------------------------------------------------------------------------------------------------//

		#region //---Member Fields---//

		private SerialPort m_Port = null;      // Port

		#endregion

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        public alfaPort(SerialPort p_Port)
        {
			// Set Port
			this.m_Port = p_Port;
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		private void CopyProperties( SerialPort portSource, SerialPort portDest )
		{
			portDest.BaudRate               = portSource.BaudRate;
			portDest.DataBits               = portSource.DataBits;
			portDest.DiscardNull            = portSource.DiscardNull;
			portDest.DtrEnable              = portSource.DtrEnable;
			portDest.Handshake              = portSource.Handshake;
			portDest.Parity                 = portSource.Parity;
			portDest.ParityReplace          = portSource.ParityReplace;
			portDest.PortName               = portSource.PortName;
			portDest.ReadBufferSize         = portSource.ReadBufferSize;
			portDest.ReadTimeout            = portSource.ReadTimeout;
			portDest.ReceivedBytesThreshold = portSource.ReceivedBytesThreshold;
			portDest.RtsEnable              = portSource.RtsEnable;
			portDest.StopBits               = portSource.StopBits;
			portDest.WriteBufferSize        = portSource.WriteBufferSize;
			portDest.WriteTimeout           = portSource.WriteTimeout;
		}

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public string Open(bool p_KantarAktif)
        {
            // Check Aktif
            if (p_KantarAktif)
            {
                // Return Open
                return this.Open();
            }
            else
            {
                // Close
                this.Close();

                // Return
                return "ÝPTAL";
            }
        }
        
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public string Open()
        {
            try
            {
                // Close Port
                if (this.m_Port.IsOpen) this.m_Port.Close();

                // Open Port
                this.m_Port.Open();

                // Return
                return "OK";
            }
            catch (Exception ex)
            {
                // Return
                return ex.Message;
            }
        }
				
		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public void Close()
		{
			try
			{
				// Close Port
				if (this.m_Port.IsOpen) this.m_Port.Close();
			}
			catch (Exception ex)
			{
				// Show Error
				alfaMsg.Error(ex.Message);
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public override string ToString()
		{ 
			// Override Method
			return this.m_Port.PortName; 
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public string GetPortName 
		{ 
			get { return this.m_Port.PortName; } 
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public SerialPort GetPort
		{ 
			get { return this.m_Port; } 
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//
	}

	#endregion    


	#region //-----------alfaKntr-----------//

	public class alfaKntr
	{
		//-----------------------------------------------------------------------------------------------------------------------------------------//

		#region //---Member Fields---//

        // Aktif
        public bool m_Aktif = true;

        // Color
        public int m_Color = Color.Aqua.ToArgb();

		// Port
		public alfaPort m_Port = null;
		
		// Port Kantar
		public string m_PortKntr = null;  

		// Kart No
		public string m_KNO = null;

		// Port Flag
		public bool m_PaketStart = false;

		// Port Lock
		public Object m_PortLock = new Object();
		
		// Port Buffer
		public List<byte> m_PaketBuffer = new List<byte>();

		// Status Control
		private BarStaticItem m_Status = null;

		// Log Control
		public MemoEdit m_LOG = null;
			
		// DigitalGauge Live
		public DigitalGauge m_DIG_Live = null;

		// DigitalGauge Test
		public DigitalGauge m_DIG_Test = null;

        // GaugeControl Live
        public GaugeControl m_Gauge_DIG_Live = null;

        // GaugeControl S1
        public GaugeControl m_Gauge_S1 = null;

        // GaugeControl S2
        public GaugeControl m_Gauge_S2 = null;

        // GaugeControl S3
        public GaugeControl m_Gauge_S3 = null;

        // Button Kantar
        public SimpleButton m_Button = null;

        // Panel Kantar
        public PanelControl m_Panel = null;

        // Title
        public string Title { get { return this.GetTitle(); } }

        // SeriNo
        public string m_KantarSeriNo = null;

        // Tartim No
        public string m_TartimNo = null;

        // Error
        public bool m_Error = false;

        // Value
        public int m_Value = 0;

		#endregion
		
		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public alfaKntr(alfaPort p_Port, string p_KNO, BarStaticItem p_Status, MemoEdit p_LOG, DigitalGauge p_LED_Live, DigitalGauge p_LED_Test, GaugeControl p_GaugeLED, GaugeControl p_GaugeS1, GaugeControl p_GaugeS2, GaugeControl p_GaugeS3, SimpleButton p_Button, PanelControl p_Panel )
		{
			// Kantar No
			this.m_KNO = p_KNO;

			// Kantar Port
			this.m_Port = p_Port;

			// Status Control
			this.m_Status = p_Status;

			// Log Control
			this.m_LOG = p_LOG;

			// LED Control
			this.m_DIG_Live = p_LED_Live;

			// LED Control
			this.m_DIG_Test = p_LED_Test;

            // GaugeControl Live
            this.m_Gauge_DIG_Live = p_GaugeLED;

            // GaugeControl S1
            this.m_Gauge_S1 = p_GaugeS1;

            // GaugeControl S2
            this.m_Gauge_S2 = p_GaugeS2;

            // GaugeControl S3
            this.m_Gauge_S3 = p_GaugeS3;

            // Button Control
            this.m_Button = p_Button;

            // Panel Control
            this.m_Panel = p_Panel;

			// Load Settings
			alfaCfg.Load_PORT_Settings(this);
            alfaCfg.Load_USB_Settings();

            // Check Sensor
            if (this.m_Button != null)
            {
                // Set Visibility
                this.m_Button.Visible = this.m_Aktif;
                this.m_Gauge_DIG_Live.Visible = this.m_Aktif;
                this.m_Gauge_S1.Visible = this.m_Aktif;
                this.m_Gauge_S2.Visible = this.m_Aktif;
                this.m_Gauge_S3.Visible = this.m_Aktif;

                // Set Kantar Panel
                if (this.m_Aktif)
                    this.m_Panel.BorderStyle = BorderStyles.NoBorder;
                else this.m_Panel.BorderStyle = BorderStyles.Flat;
            }

            // Gumruk Modu
            if (alfaSession.Gumruk) this.m_Aktif = false;

			// Open Port
            this.SetStatus(this.m_Port.Open(this.m_Aktif));
		}
		
		//-----------------------------------------------------------------------------------------------------------------------------------------//

        private string GetTitle()
        {
            // Create Result
            string p_Result = null;

            // Set Result
            if (this.m_TartimNo == alfaStr.Tartim01)
                 p_Result = String.Format("({0})  {1}", this.m_KNO, alfaStr.Tartim01);
            else p_Result = String.Format("({0})  {1}", this.m_KNO, alfaStr.Tartim02);

            // Return
            return p_Result;
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        public void SetStatus(string p_Message)
        {
            // Set Message
            this.m_Status.Caption = p_Message;

                 // Set Color
                 if (p_Message == "OK")    this.m_Status.ItemAppearance.Normal.ForeColor = Color.Green;
            else if (p_Message == "RESET") this.m_Status.ItemAppearance.Normal.ForeColor = Color.White;
                                      else this.m_Status.ItemAppearance.Normal.ForeColor = Color.Red;
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//
        
        public override string ToString()  
		{ 
			// Override Method
			return this.m_KNO;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

	}

	#endregion    

        
	#region //-----------alfaMode-----------//

	class alfaEdit
	{
		// Fields
		public static int Enabled  = 1;
		public static int Disabled = 0;
	}

	#endregion    


	#region //-----------alfaTable----------//

	public class alfaTable
	{
		// Fields
		public const int Firma = 0;
		public const int Kullanici = 1;
		public const int Lokasyon = 2;
		public const int Malzeme = 3;
		public const int Aciklama = 4;
		public const int Desen = 5;
	}

	#endregion    


    #region //-----------alfaGrid-----------//

    public class alfaGrid
    {

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SetView(GridView p_GridView)
        {
			// Check GridView
            if (p_GridView.Columns.Count == 0) return;

			// Set RowCount
			p_GridView.Columns[0].SummaryItem.DisplayFormat = "Adet: {0}";
			p_GridView.Columns[0].SummaryItem.FieldName = p_GridView.Columns[0].FieldName;
			p_GridView.Columns[0].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;

            foreach (var p_Field in "Tonaj-NetTonaj-Kalan-Tartim1-Tartim2-NetTutar-NetGiris-NetCikis-Adet".Split('-'))
            {
                if (p_GridView.Columns[p_Field] != null)
                {
                    // Integer Values
                    p_GridView.Columns[p_Field].DisplayFormat.FormatString = "{0:0,0}";
                    p_GridView.Columns[p_Field].DisplayFormat.FormatType = FormatType.Custom;

                    // Summary Items
                    if (!"Tartim1-Tartim2".Contains(p_Field))
                    {
                        p_GridView.Columns[p_Field].SummaryItem.FieldName = p_Field;
                        p_GridView.Columns[p_Field].SummaryItem.DisplayFormat = "{0:0,0}";
                        p_GridView.Columns[p_Field].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    }
                }
            }

            // DateTime Values
            foreach (var p_Field in "Zaman1-Zaman2-KayitZamani".Split('-'))
            {
                if (p_GridView.Columns[p_Field] != null)
                {
                    p_GridView.Columns[p_Field].DisplayFormat.FormatType = FormatType.DateTime;
                    p_GridView.Columns[p_Field].DisplayFormat.FormatString = alfaDate.DTFormat;
                }
            }

			// Column ID
			if (p_GridView.Columns["ID"] != null)
			{
				p_GridView.Columns["ID"].OptionsColumn.AllowEdit = false;
			}

            // Guid
            if (p_GridView.Columns["Guid"] != null)
            {
                RepositoryItemTextEdit repItem = new RepositoryItemTextEdit();
                repItem.CharacterCasing = CharacterCasing.Upper;
                p_GridView.Columns["Guid"].ColumnEdit = repItem;
            }

            // Parola
            if (p_GridView.Columns["Parola"] != null)
            {
                RepositoryItemTextEdit repItem = new RepositoryItemTextEdit();
                repItem.UseSystemPasswordChar = true;
                p_GridView.Columns["Parola"].ColumnEdit = repItem;
            }

            // Hide Columns
            if (!alfaSession.Liman) alfaGrid.ColumnHide(p_GridView, "Aciklama3-Aciklama4-GemiNo-AmbarNo-Beyanname-Gumruk-NakilGemi");
                              else  alfaGrid.ColumnHide(p_GridView, "Aciklama3-Aciklama4-YL-YLTartim1-YLTartim2"); 

            // Gumruk Modu
            if (alfaSession.Gumruk) alfaGrid.ColumnHide(p_GridView, "Guid-KayitEden-KayitDurumu-KayitAciklama-KayitZamani-SAP-T1-T2-SapSevkNo-SevkYeri-NakilGemi-SapTeslimat-SapSeferNo-TartimTipi-SapMesaj");

			// Show Footer
			p_GridView.OptionsView.ShowFooter = true;

			// Set View
			p_GridView.OptionsView.ColumnAutoWidth = false;
			p_GridView.BestFitColumns();

            //Group Summmary 
            alfaGrid.GroupSummary(p_GridView);

        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void GroupSummary(GridView p_GridView)
        {
            // GroupFooter Disable
            p_GridView.GroupFooterShowMode = GroupFooterShowMode.Hidden;

            // GroupSum (Count)
            GridGroupSummaryItem p_Count = new GridGroupSummaryItem();
            p_Count.FieldName = "ID";
            p_Count.DisplayFormat = "( Kayýt = {0} ) ";
            p_Count.SummaryType = DevExpress.Data.SummaryItemType.Count;
            
            // GroupSum (NetTutar)
            GridGroupSummaryItem p_NetTutar = new GridGroupSummaryItem();
            p_NetTutar.FieldName = "NetTutar";
            p_NetTutar.DisplayFormat = "( NetTutar = {0} ) ";
            p_NetTutar.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            
            // GroupSum (Adet)
            GridGroupSummaryItem p_Adet = new GridGroupSummaryItem();
            p_Adet.FieldName = "Adet";
            p_Adet.DisplayFormat = "( BaðAdet = {0} ) ";
            p_Adet.SummaryType = DevExpress.Data.SummaryItemType.Sum;

            // Add to GroupSummary
            p_GridView.GroupSummary.Clear();
            p_GridView.GroupSummary.Add(p_Count);
            p_GridView.GroupSummary.Add(p_NetTutar);
            p_GridView.GroupSummary.Add(p_Adet);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SelectRow(GridView p_GridView, int p_RowHandle)
        {
            // Unselect Current Row
            p_GridView.UnselectRow(p_GridView.FocusedRowHandle);

            // Set New Position
            p_GridView.SelectRow(p_RowHandle);
            p_GridView.FocusedRowHandle = p_RowHandle;

            // Focus
            p_GridView.Focus();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void ColumnHide(GridView p_GridView, string p_List)
        {
            foreach (string strColName in p_List.Split('-'))
            {
                if (p_GridView.Columns[strColName] != null)
                {
                    // Hide Columns
                    p_GridView.Columns.ColumnByFieldName(strColName).Visible = false;
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

    }

    #endregion


	#region //-----------alfaVGrid----------//

	public class alfaVGrid 
	{
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SetPropertyGridV1(PropertyGridControl p_Grid, Object p_Object, string p_TartimNo)
        {
            // Set Object
            p_Grid.Rows.Clear();
            p_Grid.RecordWidth = 153;
            p_Grid.SelectedObject = null;
            p_Grid.SelectedObject = p_Object;

            // Hide Rows
            alfaVGrid.RowHide(p_Grid, "Aciklama3-Aciklama4-SapMesaj-AracNo-TartimTipi-GemiNo-PlakaNo-SapFisNo-Zaman1-Zaman2-Tartim1-Tartim2-NetTutar-NetGiris-NetCikis-CiktiNo-FisNo-Sensor-Operator-KantarPC-KantarDB-KNo-SAP-Guid-KayitEden-KayitZamani-KayitAciklama-KayitDurumu-YL-YLTartim1-YLTartim2-T1-T2-Lokasyon-SN1-SN2-OP1-OP2-NakilSeferNo-NakilGemi-VBELN-POSNR");

            if (alfaEntity.SAPResponse01 == null)
            {
                // SQL Mode
                alfaVGrid.RowHide(p_Grid, "SapSeferNo-SapTeslimat-SapSevkNo");

                // SQL Lookups
                alfaVGrid.AddLookup(p_Grid, "Malzeme", "Malzeme", "Malzeme", "Malzeme");
                alfaVGrid.AddLookup(p_Grid, "GemiAdi", "GemiAdi", "GemiAdi", "GemiAdi");
                alfaVGrid.AddLookup(p_Grid, "SevkYeri", "SevkYeri", "SevkYeri", "SevkYeri");
            }
            else
            {
                if (alfaEntity.SAPResponse01.TB_SEFER.Length > 0)
                {
                    // SAP - SapSeferNo
                    alfaVGrid.RowHide(p_Grid, "GemiAdi");
                    alfaVGrid.AddLookup(p_Grid, "SapSeferNo", "SapSeferNo", "SapSeferNo", "GemiAdi");
                }
                else
                {
                    // SQL - GemiAdi
                    alfaVGrid.RowHide(p_Grid, "SapSeferNo");
                    alfaVGrid.AddLookup(p_Grid, "GemiAdi", "GemiAdi", "GemiAdi", "GemiAdi");
                }

                if (alfaEntity.SAPResponse01.TB_TESL.Length > 0)
                {
                    // SAP - SapTeslimat 
                    alfaVGrid.RowHide(p_Grid, "Malzeme");
                    alfaVGrid.AddLookup(p_Grid, "SapTeslimat", "SapTeslimat", "SapTeslimat", "Malzeme");
                }
                else
                {
                    // SQL - Malzeme
                    alfaVGrid.RowHide(p_Grid, "SapTeslimat");
                    alfaVGrid.AddLookup(p_Grid, "Malzeme", "Malzeme", "Malzeme", "Malzeme");
                }

                if (alfaEntity.SAPResponse01.TB_DEPO.Length > 0)
                {
                    // SAP - SapSevkNo 
                    alfaVGrid.RowHide(p_Grid, "SevkYeri");
                    alfaVGrid.AddLookup(p_Grid, "SapSevkNo", "SapSevkNo", "SapSevkNo", "SevkYeri");
                }
                else
                {
                    // SQL - SevkYeri
                    alfaVGrid.RowHide(p_Grid, "SapSevkNo");
                    alfaVGrid.AddLookup(p_Grid, "SevkYeri", "SevkYeri", "SevkYeri", "SevkYeri");
                }
            }

            // Merkez Mode
            if (!alfaSession.Liman) alfaVGrid.RowHide(p_Grid, "AmbarNo-Beyanname-Gumruk-NakilGemiNo");

            // LOCAL Lookups 
            alfaVGrid.AddLookup(p_Grid, "NakilGemiNo", "NakilGemiNo", "NakilGemiNo", "NakilGemi");
            alfaVGrid.AddLookup(p_Grid, "GeldigiYer", "GeldigiYer", "GeldigiYer", "GeldigiYer");
            alfaVGrid.AddLookup(p_Grid, "Beyanname", "Beyanname", "Beyanname", "Beyanname");
            alfaVGrid.AddLookup(p_Grid, "Aciklama1", "Aciklama1", "Aciklama1", "Aciklama1");
            alfaVGrid.AddLookup(p_Grid, "Aciklama2", "Aciklama2", "Aciklama2", "Aciklama2");
            alfaVGrid.AddLookup(p_Grid, "Aciklama3", "Aciklama3", "Aciklama3", "Aciklama3");
            alfaVGrid.AddLookup(p_Grid, "Aciklama4", "Aciklama4", "Aciklama4", "Aciklama4");
            alfaVGrid.AddLookup(p_Grid, "FirmaAdi", "FirmaAdi", "FirmaAdi", "FirmaAdi");
            alfaVGrid.AddLookup(p_Grid, "Nakliye", "Nakliye", "Nakliye", "Nakliye");
            alfaVGrid.AddLookup(p_Grid, "Gumruk", "Gumruk", "Gumruk", "Gumruk");
            alfaVGrid.AddLookup(p_Grid, "Renk", "Renk", "Renk", "Renk");

            // FirmaAdi
            if (alfaEntity.SAPResponse01 != null && !alfaSession.Liman && !string.IsNullOrEmpty(alfaEntity.SAPResponse01.LN_DRM.FIRMA) && p_TartimNo != null)
            {
                p_Grid.Rows[0].ChildRows.GetRowByFieldName("FirmaAdi").Properties.Value = alfaEntity.SAPResponse01.LN_DRM.FIRMA;
            }

            // Add Lookup Values
            alfaVGrid.AddLookupValues(p_Grid);

            // Add First Items (Merkez Mode + SAP Mode)
            if (!alfaSession.Liman && alfaEntity.SAPResponse01 != null) alfaVGrid.AddFirstItems(p_Grid);

            // Filter
            alfaVGrid.ApplyFilter(p_Grid);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void ApplyFilter(PropertyGridControl p_Grid)
        {
            // SapSeferNo
            var RowSapSeferNo = p_Grid.Rows[0].ChildRows.GetRowByFieldName("SapSeferNo");

            // SapTeslimat
            var RowSapTeslimat = p_Grid.Rows[0].ChildRows.GetRowByFieldName("SapTeslimat");

            // Check
            if (RowSapSeferNo != null && RowSapTeslimat != null)
            {
                // SapTeslimat
                var p_LookupTeslimat = (RepositoryItemLookUpEdit)p_Grid.Rows[0].ChildRows.GetRowByFieldName("SapTeslimat").Properties.RowEdit;

                if (p_LookupTeslimat != null && RowSapSeferNo.Properties.Value != null)
                {
                    // Refresh DataSource
                    p_LookupTeslimat.DataSource = alfaEntity.Entity_GetMalzeme(RowSapSeferNo.Properties.Value.ToString());
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SetPropertyGridV2(PropertyGridControl p_Grid, Object p_Object)
        {
            // Set Object
            p_Grid.Rows.Clear();
            p_Grid.RecordWidth = 153;
            p_Grid.SelectedObject = null;
            p_Grid.SelectedObject = p_Object;

            // Row Hide
            alfaVGrid.RowHide(p_Grid, "Yuzde-Kalan-Aciklama3-Aciklama4-SapMesaj-AracNo-TartimTipi-GemiNo-PlakaNo-SapFisNo-Zaman1-Zaman2-Tartim1-Tartim2-NetTutar-NetGiris-NetCikis-CiktiNo-FisNo-Sensor-Operator-KantarPC-KantarDB-KNo-SAP-Guid-KayitEden-KayitZamani-KayitAciklama-KayitDurumu-Durum-NetTonaj-YL-YLTartim1-YLTartim2-T1-T2-SN1-SN2-OP1-OP2-NakilSeferNo-VBELN-POSNR");

            // Merkez Mode
            if (!alfaSession.Liman) alfaVGrid.RowHide(p_Grid, "AmbarNo-Beyanname-Gumruk-NakilGemiNo");

            // Row Readonly
            alfaVGrid.RowReadOnly(p_Grid, "ID-Guid-Tartim1-Tartim2-Zaman1-Zaman2-NetTutar-NetTonaj");

            // Add TextEdits
            alfaVGrid.AddTextEdits(p_Grid);

            // Add Lookup
            alfaVGrid.AddLookup(p_Grid, "GemiLinkNo", "GemiLinkNo", "GemiLinkNo", "GemiAdi");
        }
        
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void AddTextEdits(PropertyGridControl p_Grid)
        {
            foreach (BaseRow row in p_Grid.Rows[0].ChildRows)
            {
                // Check
                if ("Aktif-Admin-Durum-NakilGemi-Listeleme".Contains(row.Properties.Caption)) continue;

                // Add TextEdit
                alfaVGrid.AddTextEdit(p_Grid, row.Properties.Caption);
            }
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void AddTextEdit(PropertyGridControl p_Grid, string p_Field)
        {
            // Check Count
            if (p_Grid.Rows.Count == 0) return;

            // Create Lookup
            RepositoryItemTextEdit repTextEdit = new RepositoryItemTextEdit();

            // UpperCase
            repTextEdit.CharacterCasing = CharacterCasing.Upper;

            // Get Row
            var Row = p_Grid.Rows[0].ChildRows.GetRowByFieldName(p_Field);

            if (Row != null)
            {
                // Password Char
                if (p_Field == "Parola") repTextEdit.UseSystemPasswordChar = true;

                // Assign to Grid
                Row.Properties.RowEdit = repTextEdit;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void RowReadOnly(PropertyGridControl p_Grid, string p_List)
		{ 
			// Check Count
			if (p_Grid.Rows.Count == 0) return;

			foreach (var str in p_List.Split('-'))
			{
				// Get Row
				var row = p_Grid.Rows[0].ChildRows.GetRowByFieldName(str);

				// Make Readonly
				if (row != null) row.Properties.ReadOnly = true;
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void RowHide(PropertyGridControl p_Grid, string p_List)
		{
			// Check Count
			if (p_Grid.Rows.Count == 0) return;

			foreach (var str in p_List.Split('-'))
			{
				// Get Row
				var row = p_Grid.Rows[0].ChildRows.GetRowByFieldName(str);

				// Make Readonly
				if (row != null) row.Visible = false;
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		public static void RowShow(PropertyGridControl p_Grid, string p_List)
		{
			// Check Count
			if (p_Grid.Rows.Count == 0) return;

			foreach (var str in p_List.Split('-'))
			{
				// Get Row
				var row = p_Grid.Rows[0].ChildRows.GetRowByFieldName(str);

				// Make Readonly
				if (row != null) row.Visible = true;
			}
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void AddLookup(PropertyGridControl p_Grid, string p_DataSource, string p_Field, string p_ValueMember, string p_DisplayMember)
        {
            // Check Count
            if (p_Grid.Rows.Count == 0) return;

            // Create Lookup
            RepositoryItemLookUpEdit p_LookUp = new RepositoryItemLookUpEdit();

            if ("SapSeferNo-SapTeslimat-SapSevkNo".Contains(p_Field))
            {
                // EditValueChanged
                p_LookUp.EditValueChanged += new EventHandler(LookUp_EditValueChanged);
            }
            else
            {
                if ("GemiAdi-NakilGemi".Contains(p_Field) && alfaSession.Liman)
                {
                    // NOTHING
                }
                
                // ProcessNewValue
                else p_LookUp.ProcessNewValue += new ProcessNewValueEventHandler(LookUp_ProcessNewValue);

                // KeyUp
                p_LookUp.KeyUp += new KeyEventHandler(p_LookUp_KeyUp);
            }

            // Set LookUp Properties
            p_LookUp.DropDownRows = 10;
            p_LookUp.NullText = string.Empty;
            p_LookUp.ValueMember = p_ValueMember;
            p_LookUp.DisplayMember = p_DisplayMember;
            p_LookUp.SearchMode = SearchMode.AutoComplete;
            p_LookUp.TextEditStyle = TextEditStyles.Standard;
            p_LookUp.CharacterCasing = CharacterCasing.Upper;
            p_LookUp.BestFitMode = BestFitMode.BestFitResizePopup;
            p_LookUp.DataSource = alfaEntity.Entity_Get(p_DataSource);

            // Get Row
            var Row = p_Grid.Rows[0].ChildRows.GetRowByFieldName(p_Field);

            // Add Lookup
            if (Row != null) Row.Properties.RowEdit = p_LookUp;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private static void AddFirstItems(PropertyGridControl p_Grid)
        {
            // Rows
            var RowSapSeferNo = p_Grid.Rows[0].ChildRows.GetRowByFieldName("SapSeferNo");
            var RowGemiAdi = p_Grid.Rows[0].ChildRows.GetRowByFieldName("GemiAdi");
            var RowSapTeslimat = p_Grid.Rows[0].ChildRows.GetRowByFieldName("SapTeslimat");
            var RowMalzeme = p_Grid.Rows[0].ChildRows.GetRowByFieldName("Malzeme");
            var RowSapSevkNo = p_Grid.Rows[0].ChildRows.GetRowByFieldName("SapSevkNo");
            var RowSevkYeri = p_Grid.Rows[0].ChildRows.GetRowByFieldName("SevkYeri");

            // Lookups
            var p_LookupGemi = (RepositoryItemLookUpEdit)RowSapSeferNo.Properties.RowEdit;
            var p_LookupMalz = (RepositoryItemLookUpEdit)RowSapTeslimat.Properties.RowEdit;
            var p_LookupSevk = (RepositoryItemLookUpEdit)RowSapSevkNo.Properties.RowEdit;

            // SapSeferNo
            if (p_LookupGemi != null && RowSapSeferNo.Properties.Value == null && (p_LookupGemi.DataSource as IList).Count == 1)
            {
                RowSapSeferNo.Properties.Value = p_LookupGemi.GetDataSourceValue("SapSeferNo", 0);
                RowGemiAdi.Properties.Value = p_LookupGemi.GetDataSourceValue("GemiAdi", 0);
            }

            // Filter Malzeme
            if (p_LookupMalz != null && RowSapSeferNo.Properties.Value != null) p_LookupMalz.DataSource = alfaEntity.Entity_GetMalzeme(RowSapSeferNo.Properties.Value.ToString());

            // SapTeslimat
            if (p_LookupMalz != null && RowSapTeslimat.Properties.Value == null && (p_LookupMalz.DataSource as IList).Count == 1)
            {
                RowSapTeslimat.Properties.Value = p_LookupMalz.GetDataSourceValue("SapTeslimat", 0);
                RowMalzeme.Properties.Value = p_LookupMalz.GetDataSourceValue("Malzeme", 0);
            }

            // Sevkiyat
            if (p_LookupSevk != null && RowSapSevkNo.Properties.Value == null && (p_LookupSevk.DataSource as IList).Count == 1)
            {
                RowSapSevkNo.Properties.Value = p_LookupSevk.GetDataSourceValue("SapSevkNo", 0);
                RowSevkYeri.Properties.Value = p_LookupSevk.GetDataSourceValue("SevkYeri", 0);
            }
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        private static void AddLookupValues(PropertyGridControl p_Grid)
        {
            foreach (BaseRow Row in p_Grid.Rows[0].ChildRows)
            {
                // Check
                if (Row.Properties.RowEdit == null) continue;

                // Check
                if ("SapSeferNo-SapTeslimat-SapSevkNo".Contains(Row.Properties.FieldName)) continue;

                // LookUp
                var p_LookUp = (RepositoryItemLookUpEdit)Row.Properties.RowEdit;

                if (Row.Properties.Value != null)
                {
                    // Refresh DataSource
                    p_LookUp.DataSource = alfaEntity.Entity_Add_V2(Row.Properties.FieldName, Convert.ToString(Row.Properties.Value));
                }
            }
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        private static void p_LookUp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                // Grid
                var p_Grid = (PropertyGridControl)(sender as LookUpEdit).Parent;

                if (p_Grid.FocusedRow == p_Grid.GetLastVisible())
                {
                    // Select FirstItem
                    p_Grid.FocusFirst();
                }

                else
                {
                    // Next Item
                    p_Grid.FocusNext();
                }
            }
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//

		private static void LookUp_ProcessNewValue(object sender, ProcessNewValueEventArgs e)
		{
            // Check
            if (e.DisplayValue.ToString().Trim() == string.Empty) return;

            // Save Value
            var strNew = (string)e.DisplayValue;

            // Grid
            var p_Grid = (PropertyGridControl)(sender as LookUpEdit).Parent;

            // LookUp
            var p_LookUp = (RepositoryItemLookUpEdit) p_Grid.FocusedRow.Properties.RowEdit;

            // Refresh DataSource
            p_LookUp.DataSource = alfaEntity.Entity_Add_V2(p_Grid.FocusedRow.Properties.FieldName, strNew);

            // Set Value
            (sender as LookUpEdit).EditValue = strNew;
		}

		//-----------------------------------------------------------------------------------------------------------------------------------------//

        private static void LookUp_EditValueChanged(object sender, EventArgs e)
        {
            // LookUpEdit
            LookUpEdit p_Lookup = (sender as LookUpEdit);

            // Check
            if (p_Lookup == null || p_Lookup.EditValue == null) return;

            // Grid
            var p_Grid = (PropertyGridControl)p_Lookup.Parent;

            if (p_Lookup.Properties.ValueMember == "SapSeferNo")
            {
                // GemiAdi
                p_Grid.Rows[0].ChildRows.GetRowByFieldName("GemiAdi").Properties.Value = p_Lookup.Text;

                // SapTeslimat
                var p_LookupTeslimat = (RepositoryItemLookUpEdit)p_Grid.Rows[0].ChildRows.GetRowByFieldName("SapTeslimat").Properties.RowEdit;

                // Refresh DataSource
                if (p_LookupTeslimat != null) p_LookupTeslimat.DataSource = alfaEntity.Entity_GetMalzeme(p_Lookup.EditValue.ToString());

                // Reset Value
                p_Grid.Rows[0].ChildRows.GetRowByFieldName("SapTeslimat").Properties.Value = null;
            }

            else if (p_Lookup.Properties.ValueMember == "SapTeslimat")
            {
                // Malzeme
                p_Grid.Rows[0].ChildRows.GetRowByFieldName("Malzeme").Properties.Value = p_Lookup.Text;

                try
                {
                    // Get SapFisNo
                    string p_SapFisNo = p_Grid.Rows[0].ChildRows.GetRowByFieldName("SapFisNo").Properties.Value.ToString();

                    if (p_SapFisNo.Substring(0, 1) == "3")
                    {
                        // Teslimat
                        string p_Teslimat = p_Lookup.EditValue.ToString();

                        // Beyanname Text
                        string p_NewValue = alfaStr.GetBeyanname(p_Teslimat);

                        // Beyanname Row
                        var p_RowBeyanname = p_Grid.Rows[0].ChildRows.GetRowByFieldName("Beyanname");

                        // LookUp
                        var p_LookUpBeyanname = (RepositoryItemLookUpEdit)p_RowBeyanname.Properties.RowEdit;

                        // Refresh DataSource
                        p_LookUpBeyanname.DataSource = alfaEntity.Entity_Add_V2("Beyanname", p_NewValue);

                        // BeyannameNo
                        p_Grid.Rows[0].ChildRows.GetRowByFieldName("Beyanname").Properties.Value = p_NewValue;
                    }
                }
                catch
                {
                    //NULL
                }
            }

            else if (p_Lookup.Properties.ValueMember == "SapSevkNo")
            {
                // SevkNo
                p_Grid.Rows[0].ChildRows.GetRowByFieldName("SevkYeri").Properties.Value = p_Lookup.Text;
            }
        }

		//-----------------------------------------------------------------------------------------------------------------------------------------//
	}

	#endregion    


	#region //-----------alfaSensor---------//

    class alfaSensor
    {
        public static int OFF = 2;
        public static int FAIL = 0;
        public static int PASS = 1;
        public static string LED11;
        public static string LED22;
        public static string LED33;
        public static string LED44;
        public static string LED55;
        public static string LED66;
        public static string LED77;
        public static string LED88;


        public static int? GetIndex(string p_Value)
        {
                 if (p_Value == LED11) return 1;
            else if (p_Value == LED22) return 2;
            else if (p_Value == LED33) return 3;
            else if (p_Value == LED44) return 4;
            else if (p_Value == LED55) return 5;
            else if (p_Value == LED66) return 6;
            else if (p_Value == LED77) return 7;
            else if (p_Value == LED88) return 8;
            else  return null;
        }
    } 

	#endregion    


	#region //-----------alfaUSB------------//

    class alfaUSB
    {
        public static bool Status;
        public static byte[] Data;
        public static int ChipID;
    }

	#endregion    

    }

 