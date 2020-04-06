using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System.Data.Objects.DataClasses;


namespace KrmKantar2013
{
    public partial class FrmTartim : DevExpress.XtraEditors.XtraForm
    {
        //-------------------------------------------------------------------------------------------------------------//

        private bool m_Manuel = false;
        private string m_TartimNo = null;
        private TableAracAktif m_Aktif = null;
        private TableAracYedek m_Yedek = new TableAracYedek();

        //-------------------------------------------------------------------------------------------------------------//

        public FrmTartim(string p_Title, TableAracAktif p_Aktif, string p_TartimNo, bool p_Manuel)
        {
            // Initalize
            InitializeComponent();

            // Set Entity
            this.m_Aktif = p_Aktif;

            // Set Manuel
            this.m_Manuel = p_Manuel;

            // Set TartimNo
            this.m_TartimNo = p_TartimNo;

            // Set Title
            lbTitle.Text = p_Title;

            // Set Editing
            this.SetManuelEditing(p_Manuel);

            // SAP Mode
            if (alfaEntity.SAPResponse01 != null)
            {
                // SapSeferNo - GemiAdi
                if (alfaEntity.SAPResponse01.TB_SEFER.Length > 0 && string.IsNullOrEmpty(p_Aktif.GemiAdi)) p_Aktif.SapSeferNo = null;
                if (alfaEntity.SAPResponse01.TB_SEFER.Length > 0 && string.IsNullOrEmpty(p_Aktif.SapSeferNo)) p_Aktif.GemiAdi = null;

                // SapTeslimat - Malzeme
                if (alfaEntity.SAPResponse01.TB_TESL.Length > 0 && string.IsNullOrEmpty(p_Aktif.Malzeme)) p_Aktif.SapTeslimat = null;
                if (alfaEntity.SAPResponse01.TB_TESL.Length > 0 && string.IsNullOrEmpty(p_Aktif.SapTeslimat)) p_Aktif.Malzeme = null;

                // SapSevkNo - SapSevkYeri
                if (alfaEntity.SAPResponse01.TB_DEPO.Length > 0 && string.IsNullOrEmpty(p_Aktif.SevkYeri)) p_Aktif.SapSevkNo = null;
                if (alfaEntity.SAPResponse01.TB_DEPO.Length > 0 && string.IsNullOrEmpty(p_Aktif.SapSevkNo)) p_Aktif.SevkYeri = null;
            }

            // Set Text
            txtPlakaNo.Text = p_Aktif.PlakaNo;
            txtMalzeme.Text = p_Aktif.Malzeme;
            txtSapFisNo.Text = p_Aktif.SapFisNo;
            txtGemiAdi.Text = p_Aktif.GemiAdi;
            txtNakliye.Text = p_Aktif.Nakliye;
            txtSevkYeri.Text = p_Aktif.SevkYeri;
            txtAmbar.Text = p_Aktif.AmbarNo.ToString();

            // Set Tartim
            txtT01.EditValue = p_Aktif.Tartim1;
            txtT02.EditValue = p_Aktif.Tartim2;
            txtNet.EditValue = Math.Abs((int)p_Aktif.Tartim1 - (int)p_Aktif.Tartim2);

            // Tartim Turu
            radioTartimTipi.Enabled = (p_TartimNo == "T2");

            if (radioTartimTipi.Enabled)
            {
                // Sabit Darali Tartim ( Merkez -> Disable )
                radioTartimTipi.Properties.Items[1].Enabled = alfaSession.Liman;

                // Cift Tartim ( Merkez -> Disable )
                radioTartimTipi.Properties.Items[3].Enabled = (alfaSession.Liman && !p_Aktif.PlakaNo.Contains("."));

                // Set Tartim Tipi
                if (p_Aktif.TartimTipi == "SDARALI") radioTartimTipi.SelectedIndex = 1;

                // Cift Tartim Otomatik Secme
                else if (radioTartimTipi.Properties.Items[3].Enabled && !string.IsNullOrEmpty(p_Aktif.NakilGemi))
                {
                    radioTartimTipi.SelectedIndex = 3;
                }

                // Default
                else radioTartimTipi.SelectedIndex = 0;
            }
                
            // Copy Entity
            alfaEntity.Copy_V1(this.m_Aktif, this.m_Yedek);
        } 

        //-------------------------------------------------------------------------------------------------------------//

        private void SetManuelEditing(bool p_Manuel)
        {
            if (this.m_TartimNo == "T1")
            {
                // Tartım - I (Edit)
                this.SetTartimStatus(txtT01, p_Manuel);
                this.SetTartimStatus(txtT02, false);
            }
            else if (this.m_TartimNo == "T2")
            {
                // Tartım - II (Edit)
                this.SetTartimStatus(txtT01, false);
                this.SetTartimStatus(txtT02, p_Manuel);
            }
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Close
            this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmTartim_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC Close
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            // Control + ENTER
            else if (e.Control == true && e.KeyCode == Keys.Enter)
            {
                // btnSave
                this.btnSave_Click(null, null);
            }

            //###############################################################################//

            // Control + Shift + L
            else if (e.Control == true && e.Shift == true && e.KeyCode == Keys.L)
            {
                if (alfaCfg.CheckLiman())
                {
                    // Set Editing
                    this.SetManuelEditing(true);
                }
            }

            //###############################################################################//
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Tartim Tipi
            this.m_Aktif.TartimTipi = this.radioTartimTipi.EditValue.ToString();
            this.m_Yedek.TartimTipi = this.m_Aktif.TartimTipi;

            if (this.m_TartimNo == "T1") // Tartim - I
            {
                // Guid
                this.m_Aktif.Guid = System.Guid.NewGuid();
                this.m_Yedek.Guid = this.m_Aktif.Guid;

                // FisNo
                this.m_Aktif.FisNo = (int)alfaEntity.GetNextFisNo();
                this.m_Yedek.FisNo = this.m_Aktif.FisNo;

                // Tartim1
                this.m_Aktif.Tartim1 = Convert.ToInt32(txtT01.EditValue);
            }

            else if (this.m_TartimNo == "T2") // Tartim - II
            {
                this.m_Aktif.Tartim2 = Convert.ToInt32(txtT02.EditValue);
                this.m_Aktif.NetTutar = Convert.ToInt32(txtNet.EditValue);
                this.m_Aktif.NetCikis = alfaCtrl.SetNetCikis((int)this.m_Aktif.Tartim1, (int)this.m_Aktif.Tartim2);
                this.m_Aktif.NetGiris = alfaCtrl.SetNetGiris((int)this.m_Aktif.Tartim1, (int)this.m_Aktif.Tartim2);
            }


            //############################################################################################################//
           
            if (this.m_TartimNo == "T1") // Set (T1)
            {
                if (this.m_Manuel) this.m_Aktif.T1 = "M";
                else if (this.m_Aktif.Tartim1 == this.m_Yedek.Tartim1) this.m_Aktif.T1 = "K";
                else this.m_Aktif.T1 = "M";
            }

            else if (this.m_TartimNo == "T2") // Set (T2)
            {
                if (this.m_Manuel) this.m_Aktif.T2 = "M";
                else if (this.m_Aktif.Tartim2 == this.m_Yedek.Tartim2) this.m_Aktif.T2 = "K";
                else this.m_Aktif.T2 = "M";
            }
                    
            //############################################################################################################//
 

            if (!txtT01.Properties.ReadOnly || !txtT02.Properties.ReadOnly)
            {
                // Save Yedek
                alfaEntity.AracYedek_Save(this.m_Aktif, this.m_Yedek, alfaStr.Manuel);
            }

            // DialogResult
            this.DialogResult = DialogResult.OK;

            // Close
            this.Close();
        }

        //-------------------------------------------------------------------------------------------------------------//

        private void FrmTartim_Shown(object sender, EventArgs e)
        {
            // Focus
            lbTitle.Focus();
        }

        //-------------------------------------------------------------------------------------------------------------//

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

        //-------------------------------------------------------------------------------------------------------------//

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

        //-------------------------------------------------------------------------------------------------------------//

    }
}