using System;
using System.Text;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using KrmKantar2013.WR;

namespace KrmKantar2013
{

    #region //----------- AlfaDS -----------------------//

    public class alfaDS : IDisposable
    {
        //--------------------------------------------------------------------//

        public static string m_ConnStr = null;

        public KROMAN2013Entities Context = null;

        //--------------------------------------------------------------------//

        public alfaDS()
        {
            if (alfaDS.m_ConnStr == null)
            {
                // Get Connection
                alfaDS.m_ConnStr = alfaEntity.ConnStr_DeCrypt();

                if (alfaDS.m_ConnStr == null)
                {
                    // Message
                    alfaMsg.Error("ERROR = SQL ConnectionString is Not Valid ...!");

                    // Close Application
                    System.Environment.Exit(1);
                }
            }

            // Create Entity Context
            this.Context = new KROMAN2013Entities(alfaDS.m_ConnStr);

            // Disable LazyLoading
            this.Context.ContextOptions.LazyLoadingEnabled = false;
        }

        //--------------------------------------------------------------------//

        public alfaDS(string p_ConnStr)
        {
            // Create Entity Context
            this.Context =  new KROMAN2013Entities(p_ConnStr);

            // Disable LazyLoading
            this.Context.ContextOptions.LazyLoadingEnabled = false;
        }

        //--------------------------------------------------------------------//

        public void Dispose()
        {
            // Dispose
            this.Context.Dispose();
        }

        //--------------------------------------------------------------------//
    }

    # endregion 



    #region //----------- AlfaModel --------------------//
    
    //---------------------------------------------------------------//
    
    public partial class TableGemiAmbar
    {
        public int Yuzde { get; set; }
        public int Kalan { get; set; }
    } 

    //---------------------------------------------------------------//

    public partial class TableGemiHareket
    {
        public int Yuzde { get; set; }
        public int Kalan { get; set; }
    } 

    //---------------------------------------------------------------//
    
    public partial class ListGemiV1
    {
        public int ID { get; set; }
        public string GemiAdi { get; set; }
        public string Malzeme { get; set; }
        public string Acenta { get; set; }
        public int Tonaj { get; set; }
        public int NetTonaj { get; set; }
        public int Kalan { get; set; }
        public int Yuzde { get; set; }
    } 

    //---------------------------------------------------------------//

    public partial class ListGemiV2
    {
        public int ID { get; set; }
        public string GemiAdi { get; set; }
        public string SapSeferNo { get; set; }
        public string TripNo { get; set; }
        public string Malzeme { get; set; }
        public string Acenta { get; set; }
        public int AmbarSayisi { get; set; }
        public DateTime? Zaman1 { get; set; }
        public DateTime? Zaman2 { get; set; }
        public int Tonaj { get; set; }
        public int NetTonaj { get; set; }
        public int Kalan { get; set; }
        public bool NakilGemi { get; set; }
        public bool Durum { get; set; }
        public bool Listeleme { get; set; }
        public int GemiLinkNo { get; set; }

    } 

    //---------------------------------------------------------------//
    
    public partial class ListAmbarV1
    {
        public int No    { get; set; }
        public int Tonaj { get; set; }
        public int NetTonaj { get; set; }
        public int Kalan { get; set; }
        public int Yuzde { get; set; }
    } 
    
    //---------------------------------------------------------------//

    public class DataLOG
    {
        public string ITEM_NAME { get; set; }
        public string ERROR_TEXT { get; set; }
        public string DATETIME { get; set; }
        public string USERNAME { get; set; }
    }
    
    //---------------------------------------------------------------//


    #endregion  
    


    #region //----------- alfaEntity -------------------//


    public static class alfaEntity
    {
    
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static WR.ZKNT_F_KANTAR_V2Response   SAPResponse01 = null; // Response01
        public static WR.ZSD_F_KANTAR_01_V2Response SAPResponse02 = null; // Response02
        
        public static string EntityObjectName = "KROMAN2013Entities";   // EntityName

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static int GetNextFisNo()
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                int p_SeqFisNo = DS.Context.ExecuteStoreQuery<int>("SELECT NEXT VALUE FOR SeqFisNo", null).First();

                // Return
                return p_SeqFisNo;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static int GetNextAracNo(int p_GemiNo)
        {
            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // Query
                    int p_AracNo = DS.Context.TableAracArsiv.Where(tt => tt.GemiNo == p_GemiNo).Max(tt => tt.AracNo).Value;

                    // Return
                    return p_AracNo + 1;
                }
            }
            catch
            {
                return 1;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static DataTable KargoPlani(int? p_GemiNo)
        {
            // Check
            if (p_GemiNo == null || p_GemiNo == 0) return null;

            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // Get Ambars
                    var p_Ambars = DS.Context.TableAracArsiv.Where(tt => tt.GemiNo == p_GemiNo && tt.AmbarNo != null).Select(tt => new { tt.AmbarNo }).Distinct();

                    // Create Renks
                    List<string> p_Renks = new List<string>();

                    // Get Renks
                    foreach (var p_Row in DS.Context.TableAracArsiv.Where(tt => tt.GemiNo == p_GemiNo && tt.Renk != null).Select(tt => new { tt.Renk }).Distinct())
                    {
                        p_Renks.Add(p_Row.Renk);
                    }

                    // Add Column
                    p_Renks.Add("TOTAL");

                    // Create TableResult
                    DataTable p_TableResult = new DataTable("TABLE_RESULT");

                    // Add Column
                    p_TableResult.Columns.Add("RENKLER");

                    foreach (var p_Ambar in p_Ambars)
                    {
                        // Add Columns
                        DataColumn col = new DataColumn(p_Ambar.AmbarNo.ToString());
                        p_TableResult.Columns.Add(col);
                    }

                    // Add Column
                    p_TableResult.Columns.Add("TOTAL");


                    foreach (var p_Renk in p_Renks) // ---------------------Loop Collect Data ----------------//
                    {
                        // Create Row
                        DataRow row = p_TableResult.NewRow();

                        // Add Renk
                        row["RENKLER"] = p_Renk;

                        // Total Value
                        int? p_TotalAdet = 0;
                        int? p_TotalAgirlik = 0;

                        foreach (var p_Ambar in p_Ambars)
                        {
                            // Value
                            int? p_Adet = 0;
                            int? p_Agirlik = 0;

                            if (p_Renk == "TOTAL")
                            {
                                // Get Adet
                                p_Adet = DS.Context.TableAracArsiv.Where(tt => tt.GemiNo == p_GemiNo && tt.AmbarNo == p_Ambar.AmbarNo).Sum(tt => tt.Adet);

                                // Get Agirlik
                                p_Agirlik = DS.Context.TableAracArsiv.Where(tt => tt.GemiNo == p_GemiNo && tt.AmbarNo == p_Ambar.AmbarNo).Sum(tt => tt.NetTutar);
                            }
                            else
                            {
                                // Get Adet
                                p_Adet = DS.Context.TableAracArsiv.Where(tt => tt.GemiNo == p_GemiNo && tt.AmbarNo == p_Ambar.AmbarNo && tt.Renk == p_Renk).Sum(tt => tt.Adet);

                                // Get Agirlik
                                p_Agirlik = DS.Context.TableAracArsiv.Where(tt => tt.GemiNo == p_GemiNo && tt.AmbarNo == p_Ambar.AmbarNo && tt.Renk == p_Renk).Sum(tt => tt.NetTutar);
                            }

                            // Check
                            if (p_Adet == null) p_Adet = 0;
                            if (p_Agirlik == null) p_Agirlik = 0;

                            // Add Value
                            row[p_Ambar.AmbarNo.ToString()] = string.Format("{0:0,0} Adet \n {1:0,0} Kg", p_Adet, p_Agirlik);

                            // Add Total
                            p_TotalAdet += p_Adet;
                            p_TotalAgirlik += p_Agirlik;
                        }

                        // Assign Total
                        row["TOTAL"] = string.Format("{0:0,0} Adet \n {1:0,0} Kg", p_TotalAdet, p_TotalAgirlik);

                        // Reset
                        p_TotalAdet = 0;
                        p_TotalAgirlik = 0;

                        // Add Row
                        p_TableResult.Rows.Add(row);
                    }

                    // Return
                    return p_TableResult;

                }
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void AddLog(string p_Name, string p_Message, List<DataLOG> p_ListLOG)
        {
            // Create
            DataLOG p_Log = new DataLOG();

            // Assign
            p_Log.ITEM_NAME = p_Name;
            p_Log.ERROR_TEXT = p_Message;
            p_Log.USERNAME = alfaSession.UserName;
            p_Log.DATETIME = DateTime.Now.ToString();

            // Add
            p_ListLOG.Add(p_Log);
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static dynamic SearchHelp(string p_Value, string p_FieldName, DateTime p_Date1, DateTime p_Date2)
        {
            using (alfaDS DS = new alfaDS())
            {
                if (p_FieldName == "GemiAdi")
                {
                    // Query
                    var qry = from tt in DS.Context.TableAracArsiv where tt.Zaman1 > p_Date1 && tt.Zaman1 < p_Date2 select new { tt.GemiAdi };

                    // Filter
                    if (!string.IsNullOrEmpty(p_Value)) qry = qry.Where(tt=> tt.GemiAdi.StartsWith(p_Value));

                    // return
                    return qry.Distinct().ToList().OrderBy(tt => tt.GemiAdi);
                }

                else if (p_FieldName == "FirmaAdi")
                {
                    // Query
                    var qry = from tt in DS.Context.TableAracArsiv where tt.Zaman1 > p_Date1 && tt.Zaman1 < p_Date2 select new { tt.FirmaAdi };

                    // Filter
                    if (!string.IsNullOrEmpty(p_Value)) qry = qry.Where(tt=> tt.FirmaAdi.StartsWith(p_Value));

                    // return
                    return qry.Distinct().ToList().OrderBy(tt=> tt.FirmaAdi);
                }

                else if (p_FieldName == "Malzeme")
                {
                    // Query
                    var qry = from tt in DS.Context.TableAracArsiv where tt.Zaman1 > p_Date1 && tt.Zaman1 < p_Date2 select new {tt.Malzeme};

                    // Filter
                    if (!string.IsNullOrEmpty(p_Value)) qry = qry.Where(tt => tt.Malzeme.StartsWith(p_Value));

                    // return
                    return qry.Distinct().ToList().OrderBy(tt=> tt.Malzeme);
                }

                else if (p_FieldName == "Nakliye")
                {
                    // Query
                    var qry = from tt in DS.Context.TableAracArsiv where tt.Zaman1 > p_Date1 && tt.Zaman1 < p_Date2 select new {tt.Nakliye};

                    // Filter
                    if (!string.IsNullOrEmpty(p_Value)) qry = qry.Where(tt => tt.Nakliye.StartsWith(p_Value));

                    // return
                    return qry.Distinct().ToList().OrderBy(tt=> tt.Nakliye);
                }


                else if (p_FieldName == "SevkYeri")
                {
                    // Query
                    var qry = from tt in DS.Context.TableAracArsiv where tt.Zaman1 > p_Date1 && tt.Zaman1 < p_Date2 select new {tt.SevkYeri};

                    // Filter
                    if (!string.IsNullOrEmpty(p_Value)) qry = qry.Where(tt => tt.SevkYeri.StartsWith(p_Value));

                    // return
                    return qry.Distinct().ToList().OrderBy(tt=> tt.SevkYeri);
                }

                else if (p_FieldName == "Operator")
                {
                    // Query
                    var qry = from tt in DS.Context.TableAracArsiv where tt.Zaman1 > p_Date1 && tt.Zaman1 < p_Date2 select new {tt.Operator};

                    // Filter
                    if (!string.IsNullOrEmpty(p_Value)) qry = qry.Where(tt => tt.Operator.StartsWith(p_Value));

                    // return
                    return qry.Distinct().ToList().OrderBy(tt=> tt.Operator);
                }

                // Return
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableAracAktif> AracAktif_GetList_V1(string p_PlakaNo)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Query Entity
                    var qry = from cc in ent.Context.TableAracAktif
                              where cc.PlakaNo == p_PlakaNo
                              select cc;

                    // Get Result
                    List<TableAracAktif> ListArac = qry.ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return ListArac;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableAracAktif> AracAktif_GetList_V2(string p_SapFisNo)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Query Entity
                    var qry = from tt in ent.Context.TableAracAktif
                              where tt.SapFisNo == p_SapFisNo
                              select tt;

                    // Get Result
                    List<TableAracAktif> ListArac = qry.ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return ListArac;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableAracAktif> AracAktif_GetList_V3(Guid p_Guid)
        { 
            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Query
                    var qry = from tt in DS.Context.TableAracAktif where tt.Guid == p_Guid select tt;

                    // Get Result
                    var ListAktif = qry.ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return ListAktif;
                }
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableAracAktif> AracAktif_GetList()
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Result
                    List<TableAracAktif> tmpList = ent.Context.TableAracAktif.OrderByDescending(s => s.Zaman1).ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return tmpList;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
                
                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableAracAktif> AracAktif_Get(Guid? p_Guid)
        {
            try 
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Object
                    var qry = ent.Context.TableAracAktif.Where(tt => tt.Guid == p_Guid);

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return qry.ToList();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool Update_SapMesaj(string p_PlakaNo, string p_SapMesaj)
        {
            try
            {
                // Check
                if (string.IsNullOrEmpty(p_PlakaNo)) return false;

                using (alfaDS DS = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Query
                    var qry = from tt in DS.Context.TableAracAktif where tt.PlakaNo == p_PlakaNo select tt;

                    // Check
                    if (qry.Count() == 0) return false;

                    // AracAktif
                    TableAracAktif p_Arac = qry.First();

                    // Set Mesaj
                    p_Arac.SapMesaj = p_SapMesaj;

                    // Save
                    DS.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Pass
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool AracAktif_Add(TableAracAktif p_Arac)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Add Item
                    ent.Context.TableAracAktif.AddObject(p_Arac);
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Pass
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool AracAktif_To_AracArsiv(TableAracAktif p_Aktif)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Create Arsiv
                    TableAracArsiv p_Arsiv = new TableAracArsiv();

                    // Copy Entity
                    alfaEntity.Copy_V1(p_Aktif, p_Arsiv);

                    // Normal Tartim
                    p_Arsiv.TartimTipi = "NORMAL";
                    
                    // Add to Arsiv
                    ent.Context.TableAracArsiv.AddObject(p_Arsiv);

                    // Save
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Pass
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool AracAktif_Del(string p_PlakaNo)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Object
                    var obj = ent.Context.TableAracAktif.Where(tt => tt.PlakaNo == p_PlakaNo).First();

                    // Delete Object
                    ent.Context.TableAracAktif.DeleteObject(obj);
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Pass
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void AracAktif_Del(TableAracAktif p_Arac)
        {
            try
            {   
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Delete Object
                    ent.Context.TableAracAktif.DeleteObject(p_Arac);
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool AracAktif_Update(TableAracAktif p_Arac)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Object
                    var obj = ent.Context.TableAracAktif.Where(tt => tt.PlakaNo == p_Arac.PlakaNo).First();

                    // Copy Entity
                    alfaEntity.Copy_V1(p_Arac, obj);

                    // Save
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Pass
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool Login(string p_User, string p_Pass, ref alfaSession p_Session)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Query
                    var qry = from tt in ent.Context.TableKullanici where tt.Kullanici == p_User select tt;

                    // Get List
                    List<TableKullanici> tbUser = qry.ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Check User
                    if (tbUser.Count == 0)
                    {
                        alfaMsg.Error("Kullanýcý Ýsmi bulunamadý !");
                        return false;
                    }

                    // Check Pass
                    else if (tbUser[0].Parola != p_Pass)
                    {
                        alfaMsg.Error("Yanlýþ Parola Girdiniz !"); return false;
                    }
                    else
                    {
                        // Admin
                        alfaSession.Admin = tbUser[0].Admin;
                        
                        // AdSoyad
                        alfaSession.FullName = tbUser[0].AdSoyad;

                        // UserName
                        alfaSession.UserName = tbUser[0].Kullanici.Trim();
                        
                        // Sucess
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool CheckUser(string p_User, string p_Pass)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Query
                    var qry = from tt in ent.Context.TableKullanici where tt.Kullanici == p_User select tt;

                    // Get List
                    List<TableKullanici> tbUser = qry.ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Check User
                    if (tbUser.Count == 0)
                    {
                        alfaMsg.Error("Kullanýcý Ýsmi bulunamadý !");
                        return false;
                    }

                    // Check Pass
                    else if (tbUser[0].Parola != p_Pass)
                    {
                        alfaMsg.Error("Yanlýþ Parola Girdiniz !"); return false;
                    }
                    else
                    {
                        // Sucess
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool UpdateUserPassword(string p_User, string p_PassOld, string p_PassNew)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Query
                    var qry = from tt in ent.Context.TableKullanici
                              where tt.Kullanici == p_User
                                 && tt.Parola == p_PassOld
                              select tt;

                    List<TableKullanici> tbUser = qry.ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Check User
                    if (tbUser.Count == 0)
                    {
                        alfaMsg.Error("Eski Parola Yanlýþ ...!"); return false;
                    }

                    // Set New Password
                    tbUser[0].Parola = p_PassNew;

                    // Save
                    ent.Context.SaveChanges();

                    // Sucess
                    alfaMsg.Info("Parola Baþarýlý Bir Þekilde Deðiþmiþtir ...!");  return true;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void AracArsiv_Del(Guid p_Guid)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Object
                    var obj = ent.Context.TableAracArsiv.Where(tt => tt.Guid == p_Guid).First();

                    // Delete Object
                    ent.Context.TableAracArsiv.DeleteObject(obj);
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void AracArsiv_Del(TableAracArsiv p_Arac)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Delete Object
                    ent.Context.TableAracArsiv.DeleteObject(p_Arac);
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static TableAracArsiv AracArsiv_SahaTartim(TableAracAktif p_AracAktif)
        { 
            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get PlakaNo
                    string p_PlakaNo = p_AracAktif.PlakaNo.Substring(0, p_AracAktif.PlakaNo.Length - 1);

                    // Get Object
                    var p_AracArsiv = DS.Context.TableAracArsiv.Where(tt => tt.PlakaNo == p_PlakaNo && tt.SapFisNo == p_AracAktif.SapFisNo).First();

                    // Update Times
                    p_AracArsiv.Zaman1 = p_AracAktif.Zaman1.Value.AddMinutes(-20);
                    p_AracArsiv.Zaman2 = p_AracAktif.Zaman1.Value.AddMinutes(-05);

                    // Tartim2 Esitleme
                    p_AracArsiv.Tartim1 = p_AracAktif.Tartim1;
                    p_AracArsiv.Tartim2 = p_AracAktif.Tartim2;
                    p_AracArsiv.NetCikis = p_AracAktif.NetCikis;
                    p_AracArsiv.NetGiris = p_AracAktif.NetGiris;
                    p_AracArsiv.NetTutar = p_AracAktif.NetTutar;

                    // Save
                    DS.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return p_AracArsiv;
                }
            }
            catch //(Exception ex)
            {
                // Error
                // alfaMsg.Error(ex); 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableAracArsiv> AracArsiv_Get(Guid? p_Guid)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Object
                    var qry = ent.Context.TableAracArsiv.Where(tt => tt.Guid == p_Guid);

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return qry.ToList();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableAracArsiv> AracArsiv_GetList(Guid p_Guid)
        {
            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Query
                    var qry = from tt in DS.Context.TableAracArsiv where tt.Guid == p_Guid select tt;

                    // Get Result
                    var ListArsiv = qry.ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return ListArsiv;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableAracArsiv> AracArsiv_GetList(DateTime p_Date1, DateTime p_Date2, int p_GemiID, int p_AmbarNo)
        {
            using (alfaDS ent = new alfaDS())
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Query
                var qry = from tt in ent.Context.TableAracArsiv
                          where tt.GemiNo == p_GemiID
                             && tt.AmbarNo == p_AmbarNo
                          select tt;

                // Get Result
                var tmpList = qry.OrderByDescending(tt => tt.Zaman2).ToList();

                // CursorDefult
                alfaMsg.CursorDefult();

                // Return
                return tmpList;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableAracArsiv> AracArsiv_GetList_FromSQL(DateTime p_Date1, DateTime p_Date2, string p_PlakaNo, bool? p_SAP, string p_Guid)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Query
                    var qry = from tt in ent.Context.TableAracArsiv where tt.Zaman2 > p_Date1 && tt.Zaman2 < p_Date2 select tt;

                    // Optional Prm1
                    if (string.IsNullOrEmpty(p_PlakaNo) == false) qry = qry.Where(pp => pp.PlakaNo.StartsWith(p_PlakaNo));

                    // Optional Prm2
                    if (p_SAP != null) qry = qry.Where(pp => pp.SAP == p_SAP );
                    
                    // Optional Prm3
                    if (string.IsNullOrEmpty(p_Guid) == false)
                    {
                        Guid tmpGuid = Guid.Parse(p_Guid);
                        qry = qry.Where(pp => pp.Guid == tmpGuid);
                    }

                    // Get Result
                    List<TableAracArsiv> tmpList = qry.OrderByDescending(tt=> tt.Zaman2).ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return tmpList;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableAracArsiv> AracArsiv_GetList(DateTime p_Date1,
                                                             DateTime p_Date2,
                                                             string p_PlakaNo,
                                                             string p_SapFisNo,
                                                             string p_GemiAdi,
                                                             string p_FirmaAdi,
                                                             string p_Malzeme,
                                                             string p_Nakliye,
                                                             string p_SevkYeri,
                                                             string p_Operator,
                                                             string p_T1,
                                                             string p_T2)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Query
                    var qry = from tt in ent.Context.TableAracArsiv where tt.Zaman2 > p_Date1 && tt.Zaman2 < p_Date2 select tt;

                    // Optional Prm01
                    if (!string.IsNullOrEmpty(p_PlakaNo)) qry = qry.Where(pp => pp.PlakaNo.StartsWith(p_PlakaNo));

                    // Optional Prm02
                    if (!string.IsNullOrEmpty(p_SapFisNo)) qry = qry.Where(pp => pp.SapFisNo.StartsWith(p_SapFisNo));

                    // Optional Prm03
                    if (!string.IsNullOrEmpty(p_GemiAdi)) qry = qry.Where(pp => pp.GemiAdi == p_GemiAdi);

                    // Optional Prm04
                    if (!string.IsNullOrEmpty(p_FirmaAdi)) qry = qry.Where(pp => pp.FirmaAdi == p_FirmaAdi);
                    
                    // Optional Prm05
                    if (!string.IsNullOrEmpty(p_Malzeme)) qry = qry.Where(pp => pp.Malzeme == p_Malzeme);

                    // Optional Prm06
                    if (!string.IsNullOrEmpty(p_Nakliye)) qry = qry.Where(pp => pp.Nakliye == p_Nakliye);

                    // Optional Prm07
                    if (!string.IsNullOrEmpty(p_SevkYeri)) qry = qry.Where(pp => pp.SevkYeri == p_SevkYeri);

                    // Optional Prm08 - Prm09
                    if (!string.IsNullOrEmpty(p_T1) && !string.IsNullOrEmpty(p_T2)) qry = qry.Where(pp => pp.T1 == p_T1 || pp.T2 == p_T2);

                    // Optional Prm08 - Prm09
                    else if (!string.IsNullOrEmpty(p_T1) && string.IsNullOrEmpty(p_T2)) qry = qry.Where(pp => pp.T1 == p_T1);

                    // Optional Prm08 - Prm09
                    else if (string.IsNullOrEmpty(p_T1) && !string.IsNullOrEmpty(p_T2)) qry = qry.Where(pp => pp.T2 == p_T2);

                    // Optional Prm10
                    if (!string.IsNullOrEmpty(p_Operator)) qry = qry.Where(pp => pp.Operator == p_Operator);

                    // Get Result
                    List<TableAracArsiv> tmpList = qry.OrderByDescending(tt => tt.Zaman2).ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return tmpList;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }


        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void AracYedek_Save(TableAracAktif p_Master, TableAracYedek p_Yedek, string p_KayitDurumu)
        {
            // Save Yedek
            using (alfaDS DS = new alfaDS())
            {
                // Set Yedek
                p_Yedek.KayitEden = alfaSession.UserName;
                p_Yedek.KayitZamani = DateTime.Now;
                p_Yedek.KayitDurumu = p_KayitDurumu;

                // Set Orginal
                p_Master.KayitEden = p_Yedek.KayitEden;
                p_Master.KayitZamani = p_Yedek.KayitZamani;
                p_Master.KayitDurumu = alfaStr.Current;

                // Save Record
                DS.Context.TableAracYedek.AddObject(p_Yedek);
                DS.Context.SaveChanges();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableAracYedek> AracYedek_GetList(Guid p_Guid)
        {
            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Query
                    var qry = from tt in DS.Context.TableAracYedek where tt.Guid == p_Guid select tt;

                    // Get Result
                    var ListYedek = qry.OrderBy(tt => tt.ID).ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return ListYedek;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableAracYedek> AracYedek_GetList(DateTime? p_Date1, DateTime? p_Date2, string p_PlakaNo, string p_Guid, string p_T1, string p_T2)
        {
            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Query
                    var qry = from tt in DS.Context.TableAracYedek select tt;

                    // Optional Prm1
                    if (p_Date1 != null) qry = qry.Where(pp => pp.KayitZamani > p_Date1);
                    if (p_Date2 != null) qry = qry.Where(pp => pp.KayitZamani < p_Date2);

                    // Optional Prm2
                    if (!string.IsNullOrEmpty(p_PlakaNo)) qry = qry.Where(pp => pp.PlakaNo.StartsWith(p_PlakaNo));

                    // Optional Prm3
                    if (!string.IsNullOrEmpty(p_Guid))
                    {
                        Guid tmpGuid = Guid.Parse(p_Guid);
                        qry = qry.Where(pp => pp.Guid == tmpGuid);
                    }

                    // Optional Prm4
                    if (!string.IsNullOrEmpty(p_T1)) qry = qry.Where(pp => pp.T1 == p_T1);

                    // Optional Prm5
                    if (!string.IsNullOrEmpty(p_T2)) qry = qry.Where(pp => pp.T2 == p_T2);

                    // Get Result
                    var ListYedek = qry.OrderBy(tt => tt.ID).ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return ListYedek;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool AracArsiv_Update(TableAracAktif p_Arac)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Object
                    var obj = ent.Context.TableAracArsiv.Where(tt => tt.Guid == p_Arac.Guid).First();

                    // Copy Entity
                    alfaEntity.Copy_V1(p_Arac, obj);

                    // Save
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Pass
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool AracArsiv_Update(TableAracArsiv p_Arac)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Object
                    var obj = ent.Context.TableAracArsiv.Where(tt => tt.Guid == p_Arac.Guid).First();

                    // Copy Entity
                    alfaEntity.Copy_V1(p_Arac, obj);

                    // Save
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Pass
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool SAP_Delete_ARAC(TableAracArsiv p_Arac)
        {
            try
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Create SAP
                alfaSAP p_SAP = new alfaSAP();

                // Create Params
                WR.ZSD_F_KANTAR_03_V2 prms = new WR.ZSD_F_KANTAR_03_V2();

                // Create SAP Record
                WR.ZSD_S_ARAC sapArac = new WR.ZSD_S_ARAC();

                // Copy Entites
                alfaEntity.CopySAP(p_Arac, sapArac);

                // Assign Record
                prms.I_RECORD = sapArac;
                prms.I_DELETE = "X";

                // Call Service
                WR.ZSD_F_KANTAR_03_V2Response resp = p_SAP.ZSD_F_KANTAR_03_V2(prms);

                // CursorDefult
                alfaMsg.CursorDefult();

                // Pass 
                return true;
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool SAP_Transfer_ARAC()
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Query
                    var qry = from tt in ent.Context.TableAracArsiv where tt.SAP == false select tt;

                    foreach (var rowArac in qry.ToList())
                    {
                        // Create SAP
                        alfaSAP p_SAP = new alfaSAP();

                        // Create Params
                        WR.ZSD_F_KANTAR_03_V2 prms = new WR.ZSD_F_KANTAR_03_V2();

                        // Create SAP Record
                        WR.ZSD_S_ARAC sapArac = new WR.ZSD_S_ARAC();

                        // Copy Entites
                        alfaEntity.CopySAP(rowArac, sapArac);

                        // Set Lokasyon
                        sapArac.LOKASYON = alfaCfg.GetLocation();

                        // Assign Record
                        prms.I_RECORD = sapArac;
                        prms.I_DELETE = string.Empty;

                        // Call Service
                        WR.ZSD_F_KANTAR_03_V2Response resp = p_SAP.ZSD_F_KANTAR_03_V2(prms);

                        // Update Flag
                        if (resp.E_SUBRC == "00") rowArac.SAP = true;
                    }

                    // SaveChanges
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Pass 
                    return true;
                }

            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SQL_to_SAP_Arac(List<TableAracArsiv> p_ListAracSQL, List<DataLOG> p_ListLog)
        {
            using (alfaDS ent = new alfaDS())
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Loop Arac List
                foreach (var p_AracSQL in p_ListAracSQL.ToList())
                {
                    // Create SAP
                    alfaSAP p_SAP = new alfaSAP();

                    // Create Params
                    WR.ZSD_F_KANTAR_03_V2 prms = new WR.ZSD_F_KANTAR_03_V2();

                    // Create SAP Record
                    WR.ZSD_S_ARAC p_AracSAP = new WR.ZSD_S_ARAC();

                    try
                    {
                        // Copy Entites
                        alfaEntity.CopySAP(p_AracSQL, p_AracSAP);

                        // Assign Record
                        prms.I_RECORD = p_AracSAP;
                        prms.I_DELETE = string.Empty;

                        // Call Service
                        WR.ZSD_F_KANTAR_03_V2Response resp = p_SAP.ZSD_F_KANTAR_03_V2(prms);

                        // Update Flag
                        p_AracSQL.SAP = (resp.E_SUBRC == "00");

                        // SaveChanges
                        ent.Context.SaveChanges();
                    }

                    catch (Exception ex)
                    {
                        // Add Log
                        alfaEntity.AddLog(p_AracSQL.Guid.ToString(), alfaMsg.GetError(ex), p_ListLog); 
                    }
                }

                // CursorDefult
                alfaMsg.CursorDefult();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SQL_to_SAP_Gemi(List<TableGemiHareket> p_ListGemiSQL, List<DataLOG> p_ListLog)
        {
            using (alfaDS ent = new alfaDS())
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Loop Gemi List
                foreach (var p_GemiSQL in p_ListGemiSQL.ToList())
                {
                    // Create SAP
                    alfaSAP p_SAP = new alfaSAP();

                    // Create Params
                    WR.ZSD_F_KANTAR_04_V2 prms = new WR.ZSD_F_KANTAR_04_V2();

                    // Create SAP Record
                    WR.ZSD_S_GEMI p_GemiSAP = new WR.ZSD_S_GEMI();

                    // Copy Entites
                    alfaEntity.CopySAP(p_GemiSQL, p_GemiSAP);

                    // Assign Record
                    prms.I_RECORD = p_GemiSAP;

                    try
                    {
                        // Call Service
                        WR.ZSD_F_KANTAR_04_V2Response resp = p_SAP.ZSD_F_KANTAR_04_V2(prms);
                    }

                    catch (Exception ex)
                    {
                        // Add Log
                        alfaEntity.AddLog(p_GemiSQL.ID.ToString(), alfaMsg.GetError(ex), p_ListLog); 
                    }
                }

                // CursorDefult
                alfaMsg.CursorDefult();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SAP_to_SQL_Gemi(ZSD_S_GEMI[] p_ListGemiSAP, List<DataLOG> p_ListLog)
        {
            using (alfaDS ent = new alfaDS())
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Loop Arac List
                foreach (var p_GemiSAP in p_ListGemiSAP.ToList())
                {
                    // Check
                    if (ent.Context.TableGemiHareket.Where(tt => tt.ID == p_GemiSAP.ID).Count() > 0)
                    {
                        // Add Log
                        alfaEntity.AddLog(p_GemiSAP.ID.ToString(), "KAYIT_MEVCUT", p_ListLog); continue;
                    }

                    // Create Arac
                    var p_GemiSQL = new TableGemiHareket();

                    // Set Fields
                    p_GemiSQL.ID = p_GemiSAP.ID;
                    p_GemiSQL.Acenta = p_GemiSAP.ACENTA;
                    p_GemiSQL.AmbarSayisi = p_GemiSAP.AMBARSAYISI;
                    p_GemiSQL.Durum = (p_GemiSAP.DURUM == "X");
                    p_GemiSQL.GemiAdi = p_GemiSAP.GEMIADI;
                    p_GemiSQL.GemiLinkNo = p_GemiSAP.GEMILINKNO;
                    p_GemiSQL.Listeleme = (p_GemiSAP.LISTELEME == "X");
                    p_GemiSQL.Malzeme = p_GemiSAP.MALZEME;
                    p_GemiSQL.NakilGemi = (p_GemiSAP.NAKILGEMI == "X");
                    p_GemiSQL.SapSeferNo = p_GemiSAP.SAPSEFERNO;
                    p_GemiSQL.Tonaj = p_GemiSAP.TONAJ;
                    p_GemiSQL.TripNo = p_GemiSAP.TRIPNO;
                    p_GemiSQL.Zaman1 = alfaDate.GetDateV3(p_GemiSAP.ZAMAN1);
                    p_GemiSQL.Zaman2 = alfaDate.GetDateV3(p_GemiSAP.ZAMAN2);

                    try
                    {
                        // Add
                        ent.Context.TableGemiHareket.AddObject(p_GemiSQL);

                        // Save
                        ent.Context.SaveChanges();
                    }

                    catch (Exception ex)
                    {
                        // Add Log
                        alfaEntity.AddLog(p_GemiSAP.ID.ToString(), alfaMsg.GetError(ex), p_ListLog);
                    }

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SAP_to_SQL_Arac(ZSD_S_ARAC[] p_ListAracSAP, List<DataLOG> p_ListLog)
        {
            using (alfaDS ent = new alfaDS())
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Loop Arac List
                foreach (var p_AracSAP in p_ListAracSAP.ToList())
                {
                    // Get GUID
                    var p_Guid = Guid.Parse(p_AracSAP.GUID);

                    // Check
                    if (ent.Context.TableAracArsiv.Where(tt => tt.Guid == p_Guid).Count() > 0)
                    {
                        // Add Log
                        alfaEntity.AddLog(p_AracSAP.GUID, "KAYIT_MEVCUT", p_ListLog); continue;
                    }

                    // Create Arac
                    var p_AracSQL = new TableAracArsiv();

                    // Set Fields
                    p_AracSQL.Guid = p_Guid;
                    p_AracSQL.Aciklama1 = p_AracSAP.ACIKLAMA1;
                    p_AracSQL.Aciklama2 = p_AracSAP.ACIKLAMA2;
                    p_AracSQL.Aciklama3 = p_AracSAP.ACIKLAMA3;
                    p_AracSQL.Aciklama4 = p_AracSAP.ACIKLAMA4;
                    p_AracSQL.Gumruk = p_AracSAP.GUMRUK;
                    p_AracSQL.Irsaliye = p_AracSAP.IRSALIYE;
                    p_AracSQL.KantarDB = p_AracSAP.KANTARDB;
                    p_AracSQL.KantarPC = p_AracSAP.KANTARPC;
                    p_AracSQL.KayitAciklama = p_AracSAP.KAYITACIKLAMA;
                    p_AracSQL.KayitDurumu = p_AracSAP.KAYITDURUMU;
                    p_AracSQL.KayitEden = p_AracSAP.KAYITEDEN;
                    p_AracSQL.KayitZamani = alfaDate.GetDateV3(p_AracSAP.KAYITZAMANI);
                    p_AracSQL.KNo = p_AracSAP.KNO;
                    p_AracSQL.Lokasyon = p_AracSAP.LOKASYON;
                    p_AracSQL.Malzeme = p_AracSAP.MALZEME;
                    p_AracSQL.NakilGemi = p_AracSAP.NAKILGEMI;
                    p_AracSQL.NakilGemiNo = p_AracSAP.NAKILGEMINO;
                    p_AracSQL.NakilSeferNo = p_AracSAP.NAKILSEFERNO;
                    p_AracSQL.Nakliye = p_AracSAP.NAKLIYE;
                    p_AracSQL.NetCikis = p_AracSAP.NETCIKIS;
                    p_AracSQL.NetGiris = p_AracSAP.NETGIRIS;
                    p_AracSQL.NetTutar = p_AracSAP.NETTUTAR;
                    p_AracSQL.OP1 = p_AracSAP.OP1;
                    p_AracSQL.OP2 = p_AracSAP.OP2;
                    p_AracSQL.Operator = p_AracSAP.OPERATOR;
                    p_AracSQL.PlakaNo = p_AracSAP.PLAKANO;
                    p_AracSQL.POSNR = p_AracSAP.POSNR;
                    p_AracSQL.Renk = p_AracSAP.RENK;
                    p_AracSQL.SAP = true;
                    p_AracSQL.SapFisNo = p_AracSAP.SAPFISNO;
                    p_AracSQL.SapMesaj = p_AracSAP.SAPMESAJ;
                    p_AracSQL.SapSeferNo = p_AracSAP.SAPSEFERNO;
                    p_AracSQL.SapSevkNo = p_AracSAP.SAPSEVKNO;
                    p_AracSQL.SapTeslimat = p_AracSAP.SAPTESLIMAT;
                    p_AracSQL.Sensor = p_AracSAP.SENSOR;
                    p_AracSQL.SevkYeri = p_AracSAP.SEVKYERI;
                    p_AracSQL.SN1 = p_AracSAP.SN1;
                    p_AracSQL.SN2 = p_AracSAP.SN2;
                    p_AracSQL.T1 = p_AracSAP.T1;
                    p_AracSQL.T2 = p_AracSAP.T2;
                    p_AracSQL.Tartim1 = p_AracSAP.TARTIM1;
                    p_AracSQL.Tartim2 = p_AracSAP.TARTIM2;
                    p_AracSQL.TartimTipi = p_AracSAP.TARTIMTIPI;
                    p_AracSQL.VBELN = p_AracSAP.VBELN;
                    p_AracSQL.Zaman1 = alfaDate.GetDateV3(p_AracSAP.ZAMAN1);
                    p_AracSQL.Zaman2 = alfaDate.GetDateV3(p_AracSAP.ZAMAN2);

                    try
                    {
                        // Add
                        ent.Context.TableAracArsiv.AddObject(p_AracSQL);

                        // Save
                        ent.Context.SaveChanges();
                    }

                    catch (Exception ex)
                    {
                        // Add Log
                        alfaEntity.AddLog(p_AracSAP.GUID, alfaMsg.GetError(ex), p_ListLog); 
                    }

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool SAP_Transfer_GEMI(TableGemiHareket p_Gemi, string p_Delete)
        {
            try
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Create SAP
                alfaSAP p_SAP = new alfaSAP();

                // Create Params
                WR.ZSD_F_KANTAR_04_V2 prms = new WR.ZSD_F_KANTAR_04_V2();

                // Create SAP Record
                WR.ZSD_S_GEMI sapGemi = new WR.ZSD_S_GEMI();

                // Copy Entites
                alfaEntity.CopySAP(p_Gemi, sapGemi);

                // Assign Record
                prms.I_RECORD = sapGemi;
                prms.I_DELETE = p_Delete;

                // Call Service
                WR.ZSD_F_KANTAR_04_V2Response resp = p_SAP.ZSD_F_KANTAR_04_V2(prms);

                // CursorDefult
                alfaMsg.CursorDefult();

                // Pass 
                return true;
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SAP_ZKNT_F_KANTAR_V2(string p_SapFisNo)
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

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void SAP_ZSD_F_KANTAR_01_V2(string p_Zaman1, string p_Zaman2, string p_Arac, string p_Gemi)
        {
            try
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Create SAP
                alfaSAP p_SAP = new alfaSAP();

                // Create Parameters
                WR.ZSD_F_KANTAR_01_V2 prms = new WR.ZSD_F_KANTAR_01_V2();

                // Set Parameter
                prms.P_ZAMAN1 = p_Zaman1;
                prms.P_ZAMAN2 = p_Zaman2;
                prms.P_GET_ARAC = p_Arac;
                prms.P_GET_GEMI = p_Gemi;

                // Return Tables
                prms.P_ARAC = new WR.ZSD_S_ARAC[0];
                prms.P_GEMI = new WR.ZSD_S_GEMI[0];

                // Call Service
                alfaEntity.SAPResponse02 = p_SAP.ZSD_F_KANTAR_01_V2(prms);

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

        public static List<TableDesen> Desen_GetList()
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Result
                    List<TableDesen> tmpList = ent.Context.TableDesen.ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return tmpList;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool Desen_Add(TableDesen p_Desen)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Add Item
                    ent.Context.TableDesen.AddObject(p_Desen);
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Pass
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Desen_Update(TableDesen p_Desen)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Object
                    var obj = ent.Context.TableDesen.Where(tt => tt.Desen == p_Desen.Desen).First();

                    // Copy Entity
                    alfaEntity.Copy_V1(p_Desen, obj);

                    // SaveChanges
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableFirma> Firma_GetList()
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Result
                    List<TableFirma> tmpList = ent.Context.TableFirma.ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return tmpList;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static TableFirma Firma_GetMain()
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Result
                    List<TableFirma> listFirma = ent.Context.TableFirma.Where(tt => tt.Aktif == true).ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    if (listFirma.Count > 0) return listFirma[0]; else return null;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static TableFirma Firma_Get(string p_FirmaAdi)
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Result
                    List<TableFirma> listFirma = ent.Context.TableFirma.Where(tt => tt.FirmaAdi == p_FirmaAdi).ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    if (listFirma.Count > 0) return listFirma[0]; else return null;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Firma_Add(string p_Firma)
        {
            // Check Empty String
            if (p_Firma.Trim() == string.Empty) return;

            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Check for Existing Record
                    if (ent.Context.TableFirma.Where(tt => tt.FirmaAdi == p_Firma).ToList().Count > 0) return;

                    // Create Object
                    TableFirma dt = new TableFirma();
                    dt.FirmaAdi = p_Firma;

                    // Add Object
                    ent.Context.TableFirma.AddObject(dt);

                    // SaveChanges
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); 
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableKullanici> Kullanici_GetList()
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Result
                    List<TableKullanici> tmpList = ent.Context.TableKullanici.ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return tmpList;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableLokasyon> Lokasyon_GetList()
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Result
                    List<TableLokasyon> tmpList = ent.Context.TableLokasyon.ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return tmpList;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableAciklama> Aciklama_GetList()
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Result
                    List<TableAciklama> tmpList = ent.Context.TableAciklama.ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return tmpList;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string GetDefaultGumrukName()
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Result
                    string p_Name = ent.Context.TableGumruk.Where(tt => tt.Aktif == true).First().Gumruk;

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return p_Name;
                }
            }
            catch 
            {
                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Gumruk_Add(string p_Gumruk)
        {
            // Check Empty String
            if (p_Gumruk.Trim() == string.Empty) return;

            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Check for Existing Record
                    if (DS.Context.TableGumruk.Where(tt => tt.Gumruk == p_Gumruk).ToList().Count > 0) return;

                    // Create Object
                    TableGumruk dt = new TableGumruk();
                    dt.Gumruk = p_Gumruk;

                    // Add Object
                    DS.Context.TableGumruk.AddObject(dt);

                    // SaveChanges
                    DS.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Renk_Add(string p_Renk)
        {
            // Check Empty String
            if (p_Renk.Trim() == string.Empty) return;

            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Check for Existing Record
                    if (DS.Context.TableRenk.Where(tt => tt.Renk == p_Renk).ToList().Count > 0) return;

                    // Create Object
                    TableRenk dt = new TableRenk();
                    dt.Renk = p_Renk;

                    // Add Object
                    DS.Context.TableRenk.AddObject(dt);

                    // SaveChanges
                    DS.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Nakliye_Add(string p_Nakliye)
        {
            // Check Empty String
            if (p_Nakliye.Trim() == string.Empty) return;

            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Check for Existing Record
                    if (DS.Context.TableNakliye.Where(tt => tt.Nakliye == p_Nakliye).ToList().Count > 0) return;

                    // Create Object
                    TableNakliye dt = new TableNakliye();
                    dt.Nakliye = p_Nakliye;

                    // Add Object
                    DS.Context.TableNakliye.AddObject(dt);

                    // SaveChanges
                    DS.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Beyanname_Add(string p_Beyanname)
        {
            // Check Empty String
            if (p_Beyanname.Trim() == string.Empty) return;

            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Check for Existing Record
                    if (DS.Context.TableBeyanname.Where(tt => tt.Beyanname == p_Beyanname).ToList().Count > 0) return;

                    // Create Object
                    TableBeyanname dt = new TableBeyanname();
                    dt.Beyanname = p_Beyanname;

                    // Add Object
                    DS.Context.TableBeyanname.AddObject(dt);

                    // SaveChanges
                    DS.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Gemi_Add(string p_GemiAdi)
        {
            // Check Empty String
            if (p_GemiAdi.Trim() == string.Empty) return;

            try
            {
                using (alfaDS DS = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Check for Existing Record
                    if (DS.Context.TableGemi.Where(tt => tt.GemiAdi == p_GemiAdi).ToList().Count > 0) return;

                    // Create Object
                    TableGemi dt = new TableGemi();
                    dt.GemiAdi = p_GemiAdi;

                    // Add Object
                    DS.Context.TableGemi.AddObject(dt);

                    // SaveChanges
                    DS.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static TableGemiHareket Gemi_Get(int p_GemiID)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Get Gemi
                var p_Gemi = DS.Context.TableGemiHareket.Where(tt=>tt.ID ==p_GemiID).First();

                // Return
                return p_Gemi;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static int Gemi_GetID(string p_GemiAdi)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Get GemiID
                var qry = from tt in DS.Context.TableGemiHareket
                          where tt.Durum == false
                             && tt.GemiAdi == p_GemiAdi
                          select tt;

                // Return
                if (qry.Count() == 0) return 0; else return qry.First().ID;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string Get_NakilSerferNo(int p_GemiID)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Get GemiID
                var qry = from tt in DS.Context.TableGemiHareket
                          where tt.ID == p_GemiID
                          select tt;

                // Return
                if (qry.Count() == 0) return null; else return qry.First().SapSeferNo;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string Get_NakilGemiAdi(int p_GemiID)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Get GemiID
                var qry = from tt in DS.Context.TableGemiHareket
                          where tt.ID == p_GemiID
                          select tt;

                // Return
                if (qry.Count() == 0) return null; else return qry.First().GemiAdi;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool Gemi_Check(int p_GemiNo)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query Aktif
                var qryAktif = from tt in DS.Context.TableAracAktif
                              where tt.GemiNo == p_GemiNo 
                              select tt;

                // Query Arsiv
                var qryArsiv = from tt in DS.Context.TableAracArsiv 
                              where tt.GemiNo == p_GemiNo 
                              select tt;

                int p_Count = qryAktif.Count() + qryArsiv.Count();

                // Return
                if (p_Count > 0) return true; else return false;
            }
        }
        
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool Ambar_Check(int p_GemiNo, int p_AmbarNo)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query Aktif
                var qryAktif = from tt in DS.Context.TableAracAktif
                              where tt.GemiNo == p_GemiNo 
                                 && tt.AmbarNo == p_AmbarNo
                              select tt;

                // Query Arsiv
                var qryArsiv = from tt in DS.Context.TableAracArsiv 
                              where tt.GemiNo == p_GemiNo 
                                 && tt.AmbarNo == p_AmbarNo
                             select tt;

                int p_Count = qryAktif.Count() + qryArsiv.Count();

                // Return
                if (p_Count > 0) return true; else return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Lokasyon_Add(string p_Lokasyon)
        {
            // Check Empty String
            if (p_Lokasyon.Trim() == string.Empty) return;

            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Check for Existing Record
                    if (ent.Context.TableLokasyon.Where(tt => tt.Lokasyon == p_Lokasyon).ToList().Count > 0) return;

                    // Create Object
                    TableLokasyon dt = new TableLokasyon();
                    dt.Lokasyon = p_Lokasyon;

                    // Add Object
                    ent.Context.TableLokasyon.AddObject(dt);

                    // SaveChanges
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Aciklama_Add(string p_Aciklama)
        {
            // Check Empty String
            if (p_Aciklama.Trim() == string.Empty) return;

            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Check for Existing Record
                    if (ent.Context.TableAciklama.Where(tt => tt.Aciklama == p_Aciklama).ToList().Count > 0) return;

                    // Create Object
                    TableAciklama dt = new TableAciklama();
                    dt.Aciklama = p_Aciklama;

                    // Add Object
                    ent.Context.TableAciklama.AddObject(dt);

                    // SaveChanges
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableMalzeme> Malzeme_GetList()
        {
            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Get Result
                    List<TableMalzeme> tmpList = ent.Context.TableMalzeme.ToList();

                    // CursorDefult
                    alfaMsg.CursorDefult();

                    // Return
                    return tmpList;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Malzeme_Add(string p_Malzeme)
        {
            // Check Empty String
            if (p_Malzeme.Trim() == string.Empty) return;

            try
            {
                using (alfaDS ent = new alfaDS())
                {
                    // CursorWait
                    alfaMsg.CursorWait();

                    // Check for Existing Record
                    if (ent.Context.TableMalzeme.Where(tt => tt.Malzeme == p_Malzeme).ToList().Count > 0) return;

                    // Create Object
                    TableMalzeme dt = new TableMalzeme();
                    dt.Malzeme = p_Malzeme;

                    // Add Object
                    ent.Context.TableMalzeme.AddObject(dt);

                    // SaveChanges
                    ent.Context.SaveChanges();

                    // CursorDefult
                    alfaMsg.CursorDefult();
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableGemiHareket> GemiHareket_GetList_FromSQL(DateTime p_Date1, DateTime p_Date2, string p_GemiAdi, bool? p_Durum, bool? p_Listeleme, bool p_CalcValues)
        {
            using (alfaDS DS = new alfaDS())
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Query
                var qryGemi = from tt in DS.Context.TableGemiHareket
                              where tt.Zaman1 >= p_Date1
                                 && tt.Zaman1 <= p_Date2
                              select tt;

                // Optional Parameter
                if (p_Durum != null) qryGemi = qryGemi.Where(tt => tt.Durum == p_Durum);

                // Optional Parameter
                if (p_Listeleme != null) qryGemi = qryGemi.Where(tt => tt.Listeleme == p_Listeleme);

                // Optional Parameter
                if (p_GemiAdi != null) qryGemi = qryGemi.Where(tt => tt.GemiAdi.StartsWith(p_GemiAdi));

                if (p_CalcValues)
                {
                    foreach (var row in qryGemi.ToList())
                    {
                        // NetTonaj
                        row.NetTonaj = DS.Context.TableAracArsiv.Where(tt => tt.GemiNo == row.ID).Sum(tt => tt.NetTutar);

                        // Null Values
                        if (row.Tonaj == null) row.Tonaj = 0;
                        if (row.NetTonaj == null) row.NetTonaj = 0;

                        // Yuzde
                        if (row.Tonaj == 0) row.Yuzde = 0; else row.Yuzde = (int)(((double)row.NetTonaj / (double)row.Tonaj) * 100);

                        // Kalan
                        row.Kalan = (int)row.Tonaj - (int)row.NetTonaj;
                    }
                }

                // CursorDefult
                alfaMsg.CursorDefult();

                // Return
                return qryGemi.ToList().OrderByDescending(tt => tt.Zaman2).ToList();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void TableGemiHareket_Update(TableGemiHareket p_Gemi)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Get Object
                var obj = DS.Context.TableGemiHareket.Where(tt => tt.ID == p_Gemi.ID).First();

                // Copy Entity
                alfaEntity.Copy_V1(p_Gemi, obj);

                // Save
                DS.Context.SaveChanges();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void TableGemiAmbar_Update(TableGemiAmbar p_AmbarUpdated, int p_AmbarNo)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Get Object
                var obj = DS.Context.TableGemiAmbar.Where(tt => tt.GemiNo == p_AmbarUpdated.GemiNo && tt.AmbarNo == p_AmbarNo).First();

                // Copy Entity
                alfaEntity.Copy_V1(p_AmbarUpdated, obj);

                // Save
                DS.Context.SaveChanges();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static TableGemiAmbar GemiAmbar_Get(int p_GemiNo, int p_AmbarNo)
        { 
            using (alfaDS DS = new alfaDS())
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Get Ambar List
                var p_Ambar = DS.Context.TableGemiAmbar.Where(tt => tt.GemiNo == p_GemiNo && tt.AmbarNo == p_AmbarNo).First();

                // CursorDefult
                alfaMsg.CursorDefult();

                // Return
                return p_Ambar;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static List<TableGemiAmbar> GemiAmbar_GetList(int p_GemiNo)
        { 
            using (alfaDS DS = new alfaDS())
            {
                // CursorWait
                alfaMsg.CursorWait();

                // Get Ambar List
                var qryAmbar = from tt in DS.Context.TableGemiAmbar where tt.GemiNo == p_GemiNo select tt;

                foreach (var p_Ambar in qryAmbar.ToList())
                {
                    // Get Detail List
                    var qryDetail =  from tt in DS.Context.TableAracArsiv 
                                    where tt.GemiNo == p_GemiNo
                                       && tt.AmbarNo == p_Ambar.AmbarNo
                                    select new { tt.NetTutar };

                    // NetTonaj
                    p_Ambar.NetTonaj = (int)qryDetail.ToList().Sum(tt => tt.NetTutar);

                    // Null Values
                    if (p_Ambar.Tonaj == null) p_Ambar.Tonaj = 0;
                    if (p_Ambar.NetTonaj == null) p_Ambar.NetTonaj = 0;

                    // Yuzde
                    if (p_Ambar.Tonaj == 0) p_Ambar.Yuzde = 0; else p_Ambar.Yuzde = (int)(((double)p_Ambar.NetTonaj / (double)p_Ambar.Tonaj) * 100);

                    // Kalan
                    p_Ambar.Kalan = (int)p_Ambar.Tonaj - (int)p_Ambar.NetTonaj;
                }

                // CursorDefult
                alfaMsg.CursorDefult();

                // Return
                return qryAmbar.OrderBy(tt => tt.AmbarNo).ToList();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void TableGemiHareket_Delete(int p_GemiNo)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query
                var qry = from tt in DS.Context.TableGemiHareket where tt.ID == p_GemiNo select tt;

                // Delete
                DS.Context.TableGemiHareket.DeleteObject(qry.First());

                // SaveChanges
                DS.Context.SaveChanges();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void TableGemiAmbar_Delete(int p_GemiNo, int? p_AmbarNo)
        {
            using (alfaDS DS = new alfaDS())
            {
                // Query for Ambar List
                var qry = from tt in DS.Context.TableGemiAmbar
                          where tt.GemiNo == p_GemiNo
                          select tt;

                // Parameter
                if (p_AmbarNo != null) qry = qry.Where(tt => tt.AmbarNo == p_AmbarNo);

                foreach (TableGemiAmbar p_Ambar in qry.ToList())
                {
                    // Delete
                    DS.Context.TableGemiAmbar.DeleteObject(p_Ambar);
                }

                // SaveChanges
                DS.Context.SaveChanges();
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static string ConnStr_DeCrypt()
        {
            try
            {
                // Config File
                Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Get ConnStr
                string p_ConnStr = cfgFile.ConnectionStrings.ConnectionStrings[alfaEntity.EntityObjectName].ConnectionString;

                // SB EntityConnection
                EntityConnectionStringBuilder sbENT = new EntityConnectionStringBuilder(p_ConnStr);

                // SB SQLConnection
                SqlConnectionStringBuilder sbSQL = new SqlConnectionStringBuilder(sbENT.ProviderConnectionString);

                // DeCrypt Password
                sbSQL.Password = alfaSec.DeCrypt(sbSQL.Password);

                // Set TimeOut
                sbSQL.ConnectTimeout = 10;

                // Assign Back to Entity ConnStr
                sbENT.ProviderConnectionString = sbSQL.ConnectionString;

                // Return
                return sbENT.ConnectionString;

            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return null;
            }
        }
        
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void ConnStr_EnCrypt(string p_ConnStr)
        {
            try
            {
                // SB EntityConnection
                EntityConnectionStringBuilder sbENT = new EntityConnectionStringBuilder(p_ConnStr);

                // SB SQLConnection
                SqlConnectionStringBuilder sbSQL = new SqlConnectionStringBuilder(sbENT.ProviderConnectionString);

                // EnCrypt Password
                sbSQL.Password = alfaSec.EnCrypt(sbSQL.Password);

                // Set TimeOut
                sbSQL.ConnectTimeout = 10;

                // Assign Back to Entity ConnStr
                sbENT.ProviderConnectionString = sbSQL.ConnectionString;

                // Config File
                Configuration cfgFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                // Set Properties
                cfgFile.ConnectionStrings.ConnectionStrings[alfaEntity.EntityObjectName].ConnectionString = sbENT.ConnectionString;

                // Save Changes to File
                cfgFile.Save(ConfigurationSaveMode.Modified);

                // Force Reload
                ConfigurationManager.RefreshSection("connectionStrings");

                // Reset ConnStr
                alfaDS.m_ConnStr = null;

            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); 
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//
        
        public static string ConnStr_Test(string p_ServerName, out string p_Result)
        {
            try
            {
                // Get ConnString
                string p_EntityConnectionString = alfaEntity.ConnStr_DeCrypt();

                // SB EntityConnection
                EntityConnectionStringBuilder sbENT = new EntityConnectionStringBuilder(p_EntityConnectionString);

                // SB SQLConnection
                SqlConnectionStringBuilder sbSQL = new SqlConnectionStringBuilder(sbENT.ProviderConnectionString);

                // Set ServerName
                sbSQL.DataSource = p_ServerName;

                // Set TimeOut
                sbSQL.ConnectTimeout = 10;

                // Assign Back to Entity ConnStr
                sbENT.ProviderConnectionString = sbSQL.ConnectionString;

                // Test Connection
                using( alfaDS ent = new alfaDS(sbENT.ConnectionString))
                {
                    // Wait
                    alfaMsg.CursorWait();

                    // Test
                    var qry = ent.Context.TableKullanici.ToList();

                    // Default
                    alfaMsg.CursorDefult();

                    // Message
                    p_Result = " SQL Server Baþarýlý Bir Þekilde Test Edilmiþtir ...!";

                    // Pass
                    return sbENT.ConnectionString;
                }
            }
            catch (Exception ex)
            {
                // Set Message
                p_Result = ex.Message;
                
                // Return
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static dynamic Entity_GetMalzeme(string p_SapSeferNo)
        {
            try
            {
                // Query
                var qry = from tt in SAPResponse01.TB_TESL
                          where tt.SEFER == p_SapSeferNo
                          select new { SapTeslimat = tt.VBELN + "-" + tt.POSNR, Malzeme = tt.ARKTX };

                // Return
                return qry.OrderBy(tt => tt.SapTeslimat).ToList(); 
            } 
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }
        
        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static dynamic Entity_Get(string p_DataSource)
        {
            try
            {
                // Create Entity
                alfaDS ent = new alfaDS();

                // Switch
                switch (p_DataSource)
                {
                    case "GeldigiYer" : return ent.Context.TableLokasyon.Select(tt => new { GeldigiYer = tt.Lokasyon }).ToList();
                    case "Beyanname"  : return ent.Context.TableBeyanname.Select(tt => new { Beyanname = tt.Beyanname }).ToList();
                    case "Aciklama1"  : return ent.Context.TableAciklama.Select(tt => new { Aciklama1 = tt.Aciklama }).ToList();
                    case "Aciklama2"  : return ent.Context.TableAciklama.Select(tt => new { Aciklama2 = tt.Aciklama }).ToList();
                    case "Aciklama3"  : return ent.Context.TableAciklama.Select(tt => new { Aciklama3 = tt.Aciklama }).ToList();
                    case "Aciklama4"  : return ent.Context.TableAciklama.Select(tt => new { Aciklama4 = tt.Aciklama }).ToList();
                    case "FirmaAdi"   : return ent.Context.TableFirma.Select(tt => new { FirmaAdi = tt.FirmaAdi }).ToList();
                    case "Nakliye"    : return ent.Context.TableNakliye.Select(tt => new { Nakliye = tt.Nakliye }).ToList();
                    case "Gumruk"     : return ent.Context.TableGumruk.Select(tt => new { Gumruk = tt.Gumruk }).ToList();
                    case "Renk"       : return ent.Context.TableRenk.Select(tt => new { Renk = tt.Renk }).ToList();

                    case "GemiAdi": // GemiAdi

                        if (alfaSession.Liman)
                             return ent.Context.TableGemiHareket.Where(tt => tt.Durum == false).Select(tt => new { GemiNo = tt.ID, GemiAdi = tt.GemiAdi }).ToList();
                        else return ent.Context.TableGemi.Select(tt => new { tt.GemiAdi }).ToList();

                    case "Malzeme"     : return ent.Context.TableMalzeme.Select(tt => new { Malzeme = tt.Malzeme }).ToList();
                    case "SevkYeri"    : return ent.Context.TableLokasyon.Select(tt => new { SevkYeri = tt.Lokasyon }).ToList();
                    
                    case "NakilGemiNo" : return ent.Context.TableGemiHareket.Where(tt => tt.NakilGemi == true && tt.Durum == false).Select(tt => new { NakilGemiNo = tt.ID, NakilGemi = tt.GemiAdi, NakilSeferNo = tt.SapSeferNo}).ToList();
                    
                    case "SapSeferNo"  : return alfaEntity.SAPResponse01.TB_SEFER.Select(tt => new { SapSeferNo = tt.SEFER, GemiAdi = tt.GEMIADI }).OrderBy(tt => tt.SapSeferNo).ToList();
                    case "SapTeslimat" : return alfaEntity.SAPResponse01.TB_TESL.Select(tt => new { SapTeslimat = tt.VBELN + "-" + tt.POSNR, Malzeme = tt.ARKTX }).OrderBy(tt => tt.SapTeslimat).ToList();
                    case "SapSevkNo"   : return alfaEntity.SAPResponse01.TB_DEPO.Select(tt => new { SapSevkNo = tt.LGORT, SevkYeri = tt.LGOBE }).ToList();
                    
                    case "GemiLinkNo"  : return ent.Context.TableGemiHareket.Select(tt => new { GemiLinkNo = tt.ID, GemiAdi = tt.GemiAdi, SapSeferNo = tt.SapSeferNo }).ToList();


                    default: return null;
                }
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);

                // Return 
                return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Entity_Add_V1(string p_FieldName, string p_Value)
        {
            try
            {
                // Create Entity
                alfaDS ent = new alfaDS();

                // Switch
                switch (p_FieldName)
                {
                    case "Beyanname"  : alfaEntity.Beyanname_Add(p_Value); break;
                    case "SevkYeri"   : alfaEntity.Lokasyon_Add(p_Value); break;
                    case "GeldigiYer" : alfaEntity.Lokasyon_Add(p_Value); break;
                    case "Aciklama1"  : alfaEntity.Aciklama_Add(p_Value); break;
                    case "Aciklama2"  : alfaEntity.Aciklama_Add(p_Value); break;
                    case "Aciklama3"  : alfaEntity.Aciklama_Add(p_Value); break;
                    case "Aciklama4"  : alfaEntity.Aciklama_Add(p_Value); break;
                    case "Malzeme"    : alfaEntity.Malzeme_Add(p_Value); break;
                    case "Nakliye"    : alfaEntity.Nakliye_Add(p_Value); break;
                    case "Gumruk"     : alfaEntity.Gumruk_Add(p_Value); break;
                    case "FirmaAdi"   : alfaEntity.Firma_Add(p_Value); break;
                    case "GemiAdi"    : alfaEntity.Gemi_Add(p_Value); break;
                    case "Renk"       : alfaEntity.Renk_Add(p_Value); break;
                }
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static dynamic Entity_Add_V2(string p_FieldName, string p_Value)
        {
            try
            {
                // Create Entity
                alfaDS ent = new alfaDS();

                if ("Beyanname" == p_FieldName) //--------------------------------------------------------------//
                {
                    // Get List
                    var p_List = ent.Context.TableBeyanname.ToList();

                    // Create Object
                    TableBeyanname dt = new TableBeyanname();
                    dt.Beyanname = p_Value;

                    // Add Object
                    p_List.Add(dt);

                    // Return
                    return p_List.Select(tt => new { Beyanname = tt.Beyanname }).ToList();
                }

                else if ("SevkYeri-GeldigiYer".Contains(p_FieldName)) //----------------------------------------//
                {
                    // Get List
                    var p_List = ent.Context.TableLokasyon.ToList();

                    // Create Object
                    TableLokasyon dt = new TableLokasyon();
                    dt.Lokasyon = p_Value;

                    // Add Object
                    p_List.Add(dt);

                    // Return
                    if (p_FieldName == "SevkYeri") return p_List.Select(tt => new { SevkYeri = tt.Lokasyon }).ToList();
                                              else return p_List.Select(tt => new { GeldigiYer = tt.Lokasyon }).ToList();
                }

                else if ("Aciklama1-Aciklama2-Aciklama3-Aciklama4".Contains(p_FieldName)) // -------------------//
                {
                    // Get List
                    var p_List = ent.Context.TableAciklama.ToList();

                    // Create Object
                    TableAciklama dt = new TableAciklama();
                    dt.Aciklama = p_Value;

                    // Add Object
                    p_List.Add(dt);

                    // Return
                    if (p_FieldName == "Aciklama1") return p_List.Select(tt => new { Aciklama1 = tt.Aciklama }).ToList(); else
                    if (p_FieldName == "Aciklama2") return p_List.Select(tt => new { Aciklama2 = tt.Aciklama }).ToList(); else
                    if (p_FieldName == "Aciklama3") return p_List.Select(tt => new { Aciklama3 = tt.Aciklama }).ToList(); else
                                                    return p_List.Select(tt => new { Aciklama4 = tt.Aciklama }).ToList();
                }

                else if ("Malzeme" == p_FieldName) // ----------------------------------------------------------//
                {
                    // Get List
                    var p_List = ent.Context.TableMalzeme.ToList();

                    // Create Object
                    TableMalzeme dt = new TableMalzeme();
                    dt.Malzeme = p_Value;

                    // Add Object
                    p_List.Add(dt);

                    // Return
                    return p_List.Select(tt => new { Malzeme = tt.Malzeme }).ToList();
                }

                else if ("Nakliye" == p_FieldName) // ----------------------------------------------------------//
                {
                    // Get List
                    var p_List = ent.Context.TableNakliye.ToList();

                    // Create Object
                    TableNakliye dt = new TableNakliye();
                    dt.Nakliye = p_Value;

                    // Add Object
                    p_List.Add(dt);

                    // Return
                    return p_List.Select(tt => new { Nakliye = tt.Nakliye }).ToList();
                }

                else if ("Gumruk" == p_FieldName) // -----------------------------------------------------------//
                {
                    // Get List
                    var p_List = ent.Context.TableGumruk.ToList();

                    // Create Object
                    TableGumruk dt = new TableGumruk();
                    dt.Gumruk = p_Value;

                    // Add Object
                    p_List.Add(dt);

                    // Return
                    return p_List.Select(tt => new { Gumruk = tt.Gumruk }).ToList();
                }

                else if ("FirmaAdi" == p_FieldName) // ---------------------------------------------------------//
                {
                    // Get List
                    var p_List = ent.Context.TableFirma.ToList();

                    // Create Object
                    TableFirma dt = new TableFirma();
                    dt.FirmaAdi = p_Value;

                    // Add Object
                    p_List.Add(dt);

                    // Return
                    return p_List.Select(tt => new { FirmaAdi = tt.FirmaAdi }).ToList();
                }

                else if ("GemiAdi" == p_FieldName) // ------------------------------------//
                {
                    if (alfaSession.Liman)
                    {
                        // Return
                        return ent.Context.TableGemiHareket.Where(tt => tt.Durum == false).Select(tt => new { GemiNo = tt.ID, GemiAdi = tt.GemiAdi }).ToList();
                    }
                    else
                    {
                        // Get List
                        var p_List = ent.Context.TableGemi.ToList();

                        // Create Object
                        TableGemi dt = new TableGemi();
                        dt.GemiAdi = p_Value;

                        // Add Object
                        p_List.Add(dt);

                        // Return
                        return p_List.Select(tt => new { GemiAdi = tt.GemiAdi }).ToList();
                    }
                }

                else if ("Renk" == p_FieldName) // -------------------------------------------------------------//
                {
                    // Get List
                    var p_List = ent.Context.TableRenk.ToList();

                    // Create Object
                    TableRenk dt = new TableRenk();
                    dt.Renk = p_Value;

                    // Add Object
                    p_List.Add(dt);

                    // Return
                    return p_List.Select(tt => new { Renk = tt.Renk }).ToList();
                }

                else if ("NakilGemiNo" == p_FieldName) // -------------------------------------------------------------//
                {
                    // Get List
                    var p_List = ent.Context.TableGemiHareket.ToList();

                    // Return
                    return p_List.Where(tt => tt.Durum == false && tt.NakilGemi == true).Select(tt => new { NakilGemiNo = tt.ID, NakilGemi = tt.GemiAdi, NakilSeferNo = tt.SapSeferNo }).ToList();
                }


                else if ("GemiLinkNo" == p_FieldName) // -------------------------------------------------------------//
                {
                    // Get List
                    var p_List = ent.Context.TableGemiHareket.ToList();

                    // Return
                    return p_List.Where(tt => tt.Durum == false && tt.NakilGemi == true).Select(tt => new { GemiLinkNo = tt.ID, GemiAdi = tt.GemiAdi, SapSeferNo = tt.SapSeferNo }).ToList();
                }

                else return null;

            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return null;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static void Copy_V1(Object p_Source, Object p_Target)
        {
            try
            {
                // Copy Properties
                foreach (var prop in p_Source.GetType().GetProperties() )
	            {
                    // Check Name
                    if (prop.Name == "EntityState" || prop.Name == "EntityKey") continue;

                    // Get Value
                    var newValue = prop.GetValue(p_Source, null);

                    // Check
                    if (p_Target.GetType().GetProperty(prop.Name) != null)
                    {
                        // Set Value
                        p_Target.GetType().GetProperty(prop.Name).SetValue(p_Target, newValue, null);
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

        public static void Copy_V2(Object p_Source, Object p_Target)
        {  
            try
            {
                // Copy Properties
                foreach (var prop in p_Source.GetType().GetProperties() )
	            {
                    // Check Name
                    if (prop.Name == "EntityState" || prop.Name == "EntityKey") continue;

                    // Get Value
                    var newValue = prop.GetValue(p_Source, null);

                    // Check Empty
                    if (Convert.ToString(newValue) == "*") continue;

                    // Check
                    if (p_Target.GetType().GetProperty(prop.Name) != null)
                    {
                        // Set Value
                        p_Target.GetType().GetProperty(prop.Name).SetValue(p_Target, newValue, null);
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

        public static void CopySAP(Object p_Source, Object p_Target)
        {
            // Get Current Culture
            System.Globalization.CultureInfo p_Culture = System.Threading.Thread.CurrentThread.CurrentCulture;

            try
            {
                // English Culture
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                // Copy Properties
                foreach (var prop in p_Source.GetType().GetProperties())
                {
                    // Check Name
                    if (prop.Name == "EntityState" || prop.Name == "EntityKey") continue;

                    // Get Value
                    var p_Value = prop.GetValue(p_Source, null);

                    // Get Field
                    string p_Field = prop.Name.ToUpper();

                    // Check
                    if (p_Target.GetType().GetProperty(p_Field) == null) continue;

                    // Set Values
                    if (p_Value is System.Guid)
                    {
                        // GUID
                        p_Target.GetType().GetProperty(p_Field).SetValue(p_Target, p_Value.ToString().ToUpper(), null);
                    }

                    else if (p_Value is System.DateTime)
                    {
                        // DateTime
                        p_Target.GetType().GetProperty(p_Field).SetValue(p_Target, ((DateTime)p_Value).ToString("yyyy-MM-dd  HH:mm:ss"), null);
                    }

                    else if (p_Value is System.Boolean)
                    {
                        // Sap Boolean
                        string p_SapBoolean = null;

                        // Set Value
                        if ((bool)p_Value) p_SapBoolean = "X"; else p_SapBoolean = " ";

                        // Boolean
                        p_Target.GetType().GetProperty(p_Field).SetValue(p_Target, p_SapBoolean, null);
                    }

                    else
                    {
                        // Other Values
                        p_Target.GetType().GetProperty(p_Field).SetValue(p_Target, p_Value, null);
                    }
                }
            }

            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex);
            }
            finally
            {
                // Set Culture Before
                System.Threading.Thread.CurrentThread.CurrentUICulture = p_Culture;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

        public static bool Check(EntityObject p_Source, EntityObject p_Target, string p_ExceptFields)
        {
            try
            { 
                // Copy Properties
                foreach (var prop in p_Source.GetType().GetProperties() )
	            {
                    // Check
                    if (p_ExceptFields.Contains(prop.Name)) continue;

                    // Check
                    if (prop.Name == "EntityState" || prop.Name == "EntityKey") continue;

                    // Get Value Source
                    var p_ValueSource = prop.GetValue(p_Source, null);

                    // Get Value Target
                    var p_ValueTarget = p_Target.GetType().GetProperty(prop.Name).GetValue(p_Target, null);

                    // Check1
                    if (p_ValueTarget == null && p_ValueSource == null) continue;

                    // Check2
                    else if (p_ValueTarget == null && p_ValueSource != null) return false;

                    // Check3
                    else if (p_ValueTarget != null && p_ValueSource == null) return false;

                    // Check3
                    if (!p_ValueSource.Equals(p_ValueTarget)) return false;
	            }

                // Return
                return true;
            }
            catch (Exception ex)
            {
                // Error
                alfaMsg.Error(ex); return false;
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------//

    }

    #endregion


}