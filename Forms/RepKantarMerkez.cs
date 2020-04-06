using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;

namespace KrmKantar2013
{
    public partial class RepKantarMerkez : XtraReport
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public RepKantarMerkez(List<TableAracArsiv> p_ListArac)
        {
            // Initialize
            InitializeComponent();

            // Set Location
            txtLocation.Text = string.Format("({0})", alfaCfg.GetLocation());

            // Version
            txtVersion.Text = alfaVer.GetAppVersion();

            // DateTime
            txtDateTime.Text = DateTime.Now.ToString(alfaDate.DTFormat);

            // Operator
            txtOperator.Text = alfaSession.FullName;

            // Giren
            int p_GirenNet = 0;
            int p_GirenAdet = 0;
            int p_GirenArac = 0;

            // Cikan
            int p_CikanNet = 0;
            int p_CikanAdet = 0;
            int p_CikanArac = 0;

            // ToplamNet
            int p_ToplamNet = 0;

            foreach (TableAracArsiv p_Arac in p_ListArac)
            {
                // Reset Null Values
                if (p_Arac.Adet == null) p_Arac.Adet = 0;
                if (p_Arac.NetGiris == null) p_Arac.NetGiris = 0;
                if (p_Arac.NetCikis == null) p_Arac.NetCikis = 0;

                // Giren Net
                p_GirenNet += (int)p_Arac.NetGiris;

                // Cikan Net
                p_CikanNet += (int)p_Arac.NetCikis;

                if (p_Arac.NetGiris > 0)
                {
                    p_GirenArac += 1;
                    p_GirenAdet += (int)p_Arac.Adet;
                }
                else if (p_Arac.NetCikis > 0)
                {
                    p_CikanArac += 1;
                    p_CikanAdet += (int)p_Arac.Adet;
                }

                // ToplamNet
                p_ToplamNet += (int)p_Arac.NetTutar;

                // Sensor
                if (p_Arac.Sensor == "VAR") p_Arac.Sensor = string.Empty; else p_Arac.Sensor = "*";
            }

            // Giren Values
            txtGirenNet.Text  = string.Format("{0:0,0} Kg", p_GirenNet);
            txtGirenAdet.Text = string.Format("{0:0,0}", p_GirenAdet);
            txtGirenArac.Text = string.Format("{0:0,0}", p_GirenArac);

            // Cikan Values
            txtCikanNet.Text  = string.Format("{0:0,0} Kg", p_CikanNet);
            txtCikanAdet.Text = string.Format("{0:0,0}", p_CikanAdet);
            txtCikanArac.Text = string.Format("{0:0,0}", p_CikanArac);

            // Toplam Values
            txtToplamNet.Text = string.Format("{0:0,0} Kg", p_ToplamNet);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public void PrintReport()
        {
            // Print
            this.Print();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public void ShowPrintDialog()
        {
            // Print
            this.PrintDialog();
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

    }
}
