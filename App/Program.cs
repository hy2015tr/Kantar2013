using System;
using System.Threading;
using System.Windows.Forms;

namespace KrmKantar2013
{
    static class Program
    {

        //------------------------------------------------------------------------------------------------------//

        // Mutex
        private static Mutex m_Mutex = null;

        //------------------------------------------------------------------------------------------------------//

        [STAThread]
        static void Main()
        {
            // Check For Double Instance
            // CheckProgramAlreadyRunning();

            // DevExpress Skins
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            
            // Applications
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }

        //------------------------------------------------------------------------------------------------------//

        private static void CheckProgramAlreadyRunning()
        {
            // Create Flag
            bool createdNew = false;

            // Create Mutex
            m_Mutex = new Mutex(false, Application.ProductName, out createdNew);

            // Return If Already One Exists
            if (createdNew == false)
            {
                alfaMsg.Error("KrmKantar2013.Exe þu anda sisteminizde çalýþýyor ...!");
                Environment.Exit(1);
            }
        }

        //------------------------------------------------------------------------------------------------------//

    }
}