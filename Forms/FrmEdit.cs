using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Data.Objects.DataClasses;


namespace KrmKantar2013
{
    public partial class FrmEdit : DevExpress.XtraEditors.XtraForm
    {
        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private string m_TartimNo = null;
        private TableAracAktif m_Aktif = null;
        private TableAracYedek m_Yedek = new TableAracYedek();

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        public FrmEdit(string p_Title, TableAracAktif p_Aktif, string p_TartimNo)
        {
            // Initalize
            InitializeComponent();

            // Set Entity
            this.m_Aktif = p_Aktif;

            // Set TartimNo
            this.m_TartimNo = p_TartimNo;

            // Set Title
            lbTitle.Text = p_Title;

            // Disable Editing
            this.SetTartimStatus(txtT01, false);
            this.SetTartimStatus(txtT02, false);

            // Call Sap Service
            alfaEntity.SAP_ZKNT_F_KANTAR_V2(this.m_Aktif.SapFisNo);
            
            // Set Object
            alfaVGrid.SetPropertyGridV1(propGrid, p_Aktif, null);

            // Set Text
            txtPlakaNo.Text = p_Aktif.PlakaNo;
            txtMalzeme.Text = p_Aktif.Malzeme;
            txtSapFisNo.Text = p_Aktif.SapFisNo;
            txtGemiAdi.Text = p_Aktif.GemiAdi;
            txtNakliye.Text = p_Aktif.Nakliye;
            txtSevkYeri.Text = p_Aktif.SevkYeri;
            txtAmbar.Text = p_Aktif.AmbarNo.ToString();
            
            // Reset Aciklama
            txtAciklama.Text = string.Empty;

            // Set Tartim
            txtT01.EditValue = p_Aktif.Tartim1;
            txtT02.EditValue = p_Aktif.Tartim2;
            txtNet.EditValue = p_Aktif.NetTutar;

            // Set Edit Mode
            propGrid.OptionsBehavior.Editable = true;

            // Disable btnSave
            alfaCtrl.ButtonDisable(btnSave);

            // Copy Entity
            alfaEntity.Copy_V1(this.m_Aktif, this.m_Yedek);
        } 

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close
            this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void FrmTartim_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC Close
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            // Control + ENTER
            else if (e.Control == true && e.KeyCode == Keys.Enter && btnSave.Enabled == true)
            {
                // btnSave
                this.btnSave_Click(null, null);
            }

            //############################################################################################################//

            // Control + Shift + L
            else if (e.Control == true && e.Shift == true && e.KeyCode == Keys.L)
            {
                if (alfaCfg.CheckLiman())
                {
                    // Enable Editing
                    this.SetTartimStatus(txtT01, true);
                    if (this.m_TartimNo == "T2") this.SetTartimStatus(txtT02, true);
                }
            }

            //############################################################################################################//
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void btnSave_Click(object sender, EventArgs e)
        {
            // ReTransfer Data
            this.m_Aktif.SAP = false;
            
            // Kayit Aciklama
            this.m_Aktif.KayitAciklama = txtAciklama.Text;

            // Tartim1
            this.m_Aktif.Tartim1 = Convert.ToInt32(txtT01.EditValue);

            try
            {
                // VBELN - POSNR
                this.m_Aktif.VBELN = Convert.ToString(this.m_Aktif.SapTeslimat.Split('-')[0]);
                this.m_Aktif.POSNR = Convert.ToString(this.m_Aktif.SapTeslimat.Split('-')[1]);
            }
            catch
            {
                //NULL
            }

            if (alfaSession.Liman)
            {
                // Get GemiNo
                int p_GemiNo = alfaEntity.Gemi_GetID(this.m_Aktif.GemiAdi);

                // Set GemiNo
                if (p_GemiNo != 0) this.m_Aktif.GemiNo = p_GemiNo;

                // Nakil SeferNo
                this.m_Aktif.NakilSeferNo = alfaEntity.Get_NakilSerferNo(Convert.ToInt32(this.m_Aktif.NakilGemiNo));
            }

            // Tartim2
            if (this.m_TartimNo == "T2")
            {
                this.m_Aktif.Tartim2 = Convert.ToInt32(txtT02.EditValue);
                this.m_Aktif.NetTutar = Convert.ToInt32(txtNet.EditValue);
                this.m_Aktif.NetCikis = alfaCtrl.SetNetCikis((int)this.m_Aktif.Tartim1, (int)this.m_Aktif.Tartim2);
                this.m_Aktif.NetGiris = alfaCtrl.SetNetGiris((int)this.m_Aktif.Tartim1, (int)this.m_Aktif.Tartim2);
            }

            //############################################################################################################//
                
            // Set (T1)
            if (this.m_Aktif.Tartim1 != this.m_Yedek.Tartim1) this.m_Aktif.T1 = "M"; 
            
            // Set (T2)
            if (this.m_Aktif.Tartim2 != this.m_Yedek.Tartim2) this.m_Aktif.T2 = "M";

            //############################################################################################################//

            // Save Yedek
            alfaEntity.AracYedek_Save(this.m_Aktif, this.m_Yedek, alfaStr.Update);

            // DialogResult
            this.DialogResult = DialogResult.OK;

            // Close
            this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void FrmTartim_Shown(object sender, EventArgs e)
        {
            // Focus
            propGrid.Focus();
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void SetTartimStatus(SpinEdit p_Edit, bool p_Status)
        {
            if (p_Status)
            {
                // Enable Manual Tartim
                p_Edit.ForeColor = Color.Red;
                p_Edit.Properties.ReadOnly = false;
            }
            else
            {
                // Disable Manual Tartim
                p_Edit.ForeColor = Color.Aqua;
                p_Edit.Properties.ReadOnly = true;
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void txtALL_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                // Get Values
                int T01 = Convert.ToInt32(txtT01.EditValue);
                int T02 = Convert.ToInt32(txtT02.EditValue);
                
                // Calculate Net Value
                int NET = Math.Abs(T01 - T02);

                // Set Net Value
                txtNet.EditValue = NET;
            }
            catch 
            {  
                // EMPTY
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void txtAciklama_KeyUp(object sender, KeyEventArgs e)
        {
            // BtnSave Status
            alfaCtrl.SetButton(btnSave, txtAciklama.Text.Trim().Length > 0);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

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
                alfaEntity.SAPResponse01 = p_SAP.ZKNT_F_KANTAR_V2(prms);

                // CursorDefult
                alfaMsg.CursorDefult();
            }

            catch (Exception ex)
            {
                // Message
                alfaMsg.Error(ex);
            }
        }
        
        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void FrmEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Reset
            alfaEntity.SAPResponse01 = null;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

        private void propGrid_FocusedRowChanged(object sender, DevExpress.XtraVerticalGrid.Events.FocusedRowChangedEventArgs e)
        {
            if (propGrid.FocusedRow == propGrid.GetFirstVisible())
            {
                // Focus Aciklama
                txtAciklama.Select();
            }
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------//

    }
}