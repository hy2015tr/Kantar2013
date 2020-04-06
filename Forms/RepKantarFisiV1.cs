using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.Objects.DataClasses;

namespace KrmKantar2013
{
    public partial class RepKantarFisiV1 : DevExpress.XtraReports.UI.XtraReport
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        // Reset String
        string m_reset = "------------";
        string m_space = ":  ";
         
        // Table Arac
        TableAracArsiv m_Arsiv = new TableAracArsiv();

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public RepKantarFisiV1(EntityObject p_Arac, string p_TartimNo)
        {
            // Initialize
            InitializeComponent();

            // Copy Entity
            alfaEntity.Copy_V1(p_Arac, this.m_Arsiv);

            // Reset Values
            this.Reset_Report_Labels();

            // Assign Data
            this.Assign_Report_Labels(p_TartimNo);

            if (!alfaSession.Liman)
            {
                // AmbarNo
                txtAmbarNo.Visible = false;
                lbAmbarNo.Visible = false;
                
                // SeferNo
                txtSeferNo.Visible = false;
                lbSeferNo.Visible = false;
                
                // Beyanname
                txtBeyanname.Visible = false;
                lbBeyanname.Visible = false;
            
                // Gumruk
                txtGumruk.Visible = false;
                lbGumruk.Visible = false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Reset_Report_Labels()
        {
            // Reset Values
            txtAdet.Text       = m_space + m_reset;
            txtAdres.Text      = m_space + m_reset;
            txtAmbarNo.Text    = m_space + m_reset;
            txtAnaFirma.Text   = m_space + m_reset;
            txtBeyanname.Text  = m_space + m_reset;
            txtDateTime.Text   = m_space + m_reset;
            txtFirmaAdi.Text   = m_space + m_reset;
            txtGeldigiYer.Text = m_space + m_reset;
            txtGemiAdi.Text    = m_space + m_reset;
            txtSevkYeri.Text   = m_space + m_reset;
            txtGumruk.Text     = m_space + m_reset;
            txtLocation.Text   = m_space + m_reset;  
            txtMalzeme.Text    = m_space + m_reset;
            txtNakliye.Text    = m_space + m_reset;
            txtNetTutar.Text   = m_space + m_reset;
            txtOperator.Text   = m_space + m_reset; 
            txtPlakaNo.Text    = m_space + m_reset;
            txtRenk.Text       = m_space + m_reset; 
            txtSAPFisNo.Text   = m_space + m_reset;
            txtSeferNo.Text    = m_space + m_reset; 
            txtTartim1.Text    = m_space + m_reset;
            txtTartim2.Text    = m_space + m_reset;
            txtTartimNo.Text   = m_space + m_reset;
            txtVersion.Text    = m_space + m_reset;
            txtZaman1.Text     = m_space + m_reset;
            txtZaman2.Text     = m_space + m_reset;
            txtFisNo.Text    = m_space + m_reset;
            txtAciklama1.Text  = m_space + m_reset;
            txtAciklama2.Text  = m_space + m_reset;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void Assign_Report_Labels(string p_TartimNo)
        {
            // Get Firma
            TableFirma tbFirma = alfaEntity.Firma_GetMain();

            // Set Location
            if (alfaSession.Liman)
            {
                // Location    
                txtLocation.Text = "(Liman)";
            }
            else
            {
                // Location    
                txtLocation.Text = string.Format("({0})", alfaCfg.GetLocation());
            }

            if (tbFirma != null)
            {
                // Set Firma
                txtAnaFirma.Text = string.Format("{0} {1}", tbFirma.FirmaAdi, txtLocation.Text);
                txtAdres.Text = string.Format("{0}  Tel: {1}", tbFirma.Adres, tbFirma.Tel);
            }

            // Adet
            if (m_Arsiv.Adet != null) txtAdet.Text = m_space + m_Arsiv.Adet.ToString();

            // AmbarNo
            if (m_Arsiv.AmbarNo != null) txtAmbarNo.Text = m_space + m_Arsiv.AmbarNo.ToString();

            // Beyanname
            if (!string.IsNullOrEmpty(m_Arsiv.Beyanname)) txtBeyanname.Text = m_space + m_Arsiv.Beyanname;

            // DateTime
            txtDateTime.Text = DateTime.Now.ToString(alfaDate.DTFormat);

            // FirmaAdi
            if (!string.IsNullOrEmpty(m_Arsiv.FirmaAdi)) txtFirmaAdi.Text = m_space + m_Arsiv.FirmaAdi.ToString();

            // GeldigiYer
            if (!string.IsNullOrEmpty(m_Arsiv.GeldigiYer)) txtGeldigiYer.Text = m_space + m_Arsiv.GeldigiYer;

            // GemiAdi
            if (!string.IsNullOrEmpty(m_Arsiv.GemiAdi)) txtGemiAdi.Text = m_space + m_Arsiv.GemiAdi;

            // SevkYeri
            if (!string.IsNullOrEmpty(m_Arsiv.SevkYeri)) txtSevkYeri.Text = m_space + m_Arsiv.SevkYeri;

            // Gumruk
            if (!string.IsNullOrEmpty(m_Arsiv.Gumruk)) txtGumruk.Text = m_space + m_Arsiv.Gumruk;

            // Malzeme
            if (!string.IsNullOrEmpty(m_Arsiv.Malzeme)) txtMalzeme.Text = m_space + m_Arsiv.Malzeme.ToString();

            // Nakliye
            if (!string.IsNullOrEmpty(m_Arsiv.Nakliye)) txtNakliye.Text = m_space + m_Arsiv.Nakliye;

            // Operator
            if (!string.IsNullOrEmpty(m_Arsiv.Operator)) txtOperator.Text = m_space + m_Arsiv.Operator;

            // PlakaNo
            if (!string.IsNullOrEmpty(m_Arsiv.PlakaNo)) txtPlakaNo.Text = m_space + m_Arsiv.PlakaNo;

            // Renk
            if (!string.IsNullOrEmpty(m_Arsiv.Renk)) txtRenk.Text = m_space + m_Arsiv.Renk;
            
            // Sap FisNo
            if (!string.IsNullOrEmpty(m_Arsiv.SapFisNo)) txtSAPFisNo.Text = m_space + m_Arsiv.SapFisNo;

            // Sefer No
            if (m_Arsiv.SapSeferNo!=null) txtSeferNo.Text = m_space + m_Arsiv.SapSeferNo.ToString();

            // Tartim1
            if (m_Arsiv.Tartim1 != null && m_Arsiv.Tartim1.Value != 0) txtTartim1.Text = m_space + m_Arsiv.Tartim1.Value.ToString("0,0 KG");

            // Tartim2
            if (m_Arsiv.Tartim2 != null && m_Arsiv.Tartim2.Value != 0) txtTartim2.Text = m_space + m_Arsiv.Tartim2.Value.ToString("0,0 KG");

            // NetTutar
            if (m_Arsiv.NetTutar != null && m_Arsiv.NetTutar.Value != 0) txtNetTutar.Text = m_space + m_Arsiv.NetTutar.Value.ToString("0,0 KG");

            // TartimNo
            if (!string.IsNullOrEmpty(p_TartimNo)) txtTartimNo.Text = string.Format(" {0}", p_TartimNo);
            
            // Zaman1
            if (m_Arsiv.Zaman1 != null) txtZaman1.Text = m_space + ((DateTime)m_Arsiv.Zaman1).ToString(alfaDate.DTFormat);

            // Zaman2
            if (m_Arsiv.Zaman2 != null) txtZaman2.Text = m_space + ((DateTime)m_Arsiv.Zaman2).ToString(alfaDate.DTFormat);

            // Aciklama1
            if (m_Arsiv.Aciklama1 != null) txtAciklama1.Text = m_space + m_Arsiv.Aciklama1;

            // Aciklama2
            if (m_Arsiv.Aciklama2 != null) txtAciklama2.Text = m_space + m_Arsiv.Aciklama2;

            // Version
            txtVersion.Text = alfaVer.GetAppVersion();

            if (m_Arsiv.FisNo != null)
            {
                // Mukerrer 
                string p_Mukerrer = null;
                
                // Set Mukerrer 
                if (m_Arsiv.CiktiNo > 0) p_Mukerrer = "(M)";
                    
                // FisNo    
                txtFisNo.Text = string.Format("{0} {1} {2}", m_space, m_Arsiv.FisNo, p_Mukerrer);
            }
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
