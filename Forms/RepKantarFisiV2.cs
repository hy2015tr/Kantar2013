using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Data.Objects.DataClasses;

namespace KrmKantar2013
{
    public partial class RepKantarFisiV2 : DevExpress.XtraReports.UI.XtraReport
    {
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        // Reset String
        private string m_location = string.Empty;
        private string m_reset = "----";
        private string m_space = " : ";
         
        // Table Arac
        TableAracArsiv m_Arsiv = new TableAracArsiv();

        // SAP01
        private WR.ZKNT_F_KANTAR_V2Response m_SAP01 = null;  // Response01


        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public RepKantarFisiV2(EntityObject p_Arac, string p_TartimNo)
        {
            // Initialize
            InitializeComponent();

            // Copy Entity
            alfaEntity.Copy_V1(p_Arac, this.m_Arsiv);

            // Reset Values
            this.Reset_Report_Labels();

            // Assign Data
            this.Assign_Report_Labels(p_TartimNo);


            if (alfaSession.Liman)
            {
                // SN1
                txtSN1.Visible = false;
                lbSN1.Visible = false;

                // SN2
                txtSN2.Visible = false;
                lbSN2.Visible = false;
            }

            else
            {
                // AmbarNo
                txtAmbarNo.Visible = false;
                lbAmbarNo.Visible = false;

                // SeferNo
                txtSeferNo.Visible = false;
                lbSeferNo.Visible = false;

                // AracNo
                txtAracNo.Visible = false;
                lbAracNo.Visible = false;

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
            txtMalzeme.Text    = m_space + m_reset;
            txtNakliye.Text    = m_space + m_reset;
            txtNetTutar.Text   = m_space + m_reset;
            txtOperator.Text   = m_space + m_reset; 
            txtPlakaNo.Text    = m_space + m_reset;
            txtRenk.Text       = m_space + m_reset; 
            txtSAPFisNo.Text   = m_space + m_reset;
            txtSeferNo.Text    = m_space + m_reset; 
            txtAracNo.Text     = m_space + m_reset; 
            txtTartim1.Text    = m_space + m_reset;
            txtTartim2.Text    = m_space + m_reset;
            txtTartimNo.Text   = m_space + m_reset;
            txtVersion.Text    = m_space + m_reset;
            txtZaman1.Text     = m_space + m_reset;
            txtZaman2.Text     = m_space + m_reset;
            txtFisNo.Text      = m_space + m_reset;
            txtAciklama1.Text  = m_space + m_reset;
            txtAciklama2.Text  = m_space + m_reset;
            txtSN1.Text        = m_space + m_reset;
            txtSN2.Text        = m_space + m_reset;

            // Clear Maktx List
            txtMaktx01.Text = string.Empty;
            txtMaktx02.Text = string.Empty;
            txtMaktx03.Text = string.Empty;
            txtMaktx04.Text = string.Empty;
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
                m_location = "(Liman)";
            }
            else
            {
                // Location    
                m_location = string.Format("({0})", alfaCfg.GetLocation());
            }

            if (tbFirma != null)
            {
                // Set Firma
                txtAnaFirma.Text = string.Format("{0} {1} Tel:{2}", tbFirma.FirmaAdi, m_location, tbFirma.Tel);
                txtAdres.Text = string.Format("{0}", tbFirma.Adres);
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
            if (m_Arsiv.SapSeferNo != null) txtSeferNo.Text = m_space + m_Arsiv.SapSeferNo.ToString();

            // Arac No
            if (m_Arsiv.AracNo != null) txtAracNo.Text = m_space + m_Arsiv.AracNo.ToString();

            // Tartim1
            if (m_Arsiv.Tartim1 != null && m_Arsiv.Tartim1.Value != 0) txtTartim1.Text = m_space + m_Arsiv.Tartim1.Value.ToString("0,0 KG");

            // Tartim2
            if (m_Arsiv.Tartim2 != null && m_Arsiv.Tartim2.Value != 0) txtTartim2.Text = m_space + m_Arsiv.Tartim2.Value.ToString("0,0 KG");

            // NetTutar
            if (m_Arsiv.NetTutar != null && m_Arsiv.NetTutar.Value != 0) txtNetTutar.Text = m_space + m_Arsiv.NetTutar.Value.ToString("0,0 KG");

            // TartimNo
            if (!string.IsNullOrEmpty(p_TartimNo)) txtTartimNo.Text = p_TartimNo;

            // Zaman1
            if (m_Arsiv.Zaman1 != null) txtZaman1.Text = m_space + ((DateTime)m_Arsiv.Zaman1).ToString(alfaDate.DTFormat);

            // Zaman2
            if (m_Arsiv.Zaman2 != null) txtZaman2.Text = m_space + ((DateTime)m_Arsiv.Zaman2).ToString(alfaDate.DTFormat);

            // Aciklama1
            if (m_Arsiv.Aciklama1 != null) txtAciklama1.Text = m_space + m_Arsiv.Aciklama1;

            // Aciklama2
            if (m_Arsiv.Aciklama2 != null) txtAciklama2.Text = m_space + m_Arsiv.Aciklama2;

            // Kantar SN1
            if (m_Arsiv.SN1 != null) txtSN1.Text = m_space + m_Arsiv.SN1;

            // Kantar SN2
            if (m_Arsiv.SN2 != null) txtSN2.Text = m_space + m_Arsiv.SN2;

            // Version
            txtVersion.Text = "  " + alfaVer.GetAppVersion();

            if (m_Arsiv.FisNo != null)
            {
                // Mukerrer 
                string p_Mukerrer = null;

                // Set Mukerrer 
                if (m_Arsiv.CiktiNo > 0) p_Mukerrer = "(M)";

                // FisNo    
                txtFisNo.Text = string.Format("{0}{1} {2}", m_space, m_Arsiv.FisNo, p_Mukerrer);
            }

            // Call SAP Service
            this.SAP_ZKNT_F_KANTAR_V2(m_Arsiv.SapFisNo);

            // Get Maktx List
            if (this.m_SAP01 != null)
            {
                if (this.m_SAP01.TB_YRDDET.Length > 0) txtMaktx01.Text = string.Format("{0}: {1} X {2} = {3} Tenz: {4}", this.m_SAP01.TB_YRDDET[0].MAKTX, this.m_SAP01.TB_YRDDET[0].MENGE, this.m_SAP01.TB_YRDDET[0].KBETR, this.m_SAP01.TB_YRDDET[0].DMBTR, this.m_SAP01.TB_YRDDET[0].TENZL);
                if (this.m_SAP01.TB_YRDDET.Length > 1) txtMaktx02.Text = string.Format("{0}: {1} X {2} = {3} Tenz: {4}", this.m_SAP01.TB_YRDDET[1].MAKTX, this.m_SAP01.TB_YRDDET[1].MENGE, this.m_SAP01.TB_YRDDET[1].KBETR, this.m_SAP01.TB_YRDDET[1].DMBTR, this.m_SAP01.TB_YRDDET[1].TENZL);
                if (this.m_SAP01.TB_YRDDET.Length > 2) txtMaktx03.Text = string.Format("{0}: {1} X {2} = {3} Tenz: {4}", this.m_SAP01.TB_YRDDET[2].MAKTX, this.m_SAP01.TB_YRDDET[2].MENGE, this.m_SAP01.TB_YRDDET[2].KBETR, this.m_SAP01.TB_YRDDET[2].DMBTR, this.m_SAP01.TB_YRDDET[2].TENZL);
                if (this.m_SAP01.TB_YRDDET.Length > 3) txtMaktx04.Text = string.Format("{0}: {1} X {2} = {3} Tenz: {4}", this.m_SAP01.TB_YRDDET[3].MAKTX, this.m_SAP01.TB_YRDDET[3].MENGE, this.m_SAP01.TB_YRDDET[3].KBETR, this.m_SAP01.TB_YRDDET[3].DMBTR, this.m_SAP01.TB_YRDDET[3].TENZL);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        private void SAP_ZKNT_F_KANTAR_V2(string p_SapFisNo)
        {
            // Check
            if (string.IsNullOrEmpty(p_SapFisNo)) return;

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

                // Return
                prms.TB_RET = new WR.BAPIRET2[0];
                prms.TB_YRDDET = new WR.ZMM_S_KANTAR_D[0];

                // Call Service
                this.m_SAP01 = p_SAP.ZKNT_F_KANTAR_V2(prms);

                // CursorDefult
                alfaMsg.CursorDefult();
            }

            catch (Exception ex)
            {
                // Message
                alfaMsg.Error(ex);
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
