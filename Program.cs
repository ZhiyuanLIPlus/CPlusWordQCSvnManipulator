using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TiampMan.Tools;

namespace TiampMan
{
    static class Program
    {
        //TODO: Add read Config 
        public static string RepoURL = "http://slxd2004.app.eiffage.loc/operis/svn/tags/";
        public static string LocalPath = "D:\\ZLI\\Bureau\\Livrason\\TAGS\\";
        public static Boolean bCorrectionData = true;
        public static string targetBranch = "";
        public static List<string> shortFileNameList;
        public static UserInfo qcUser;

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GetAppConfig.GetInfosConfiguration();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TiampMan());
        }
    }

    struct UserInfo
    {
        public string fullname { get; set; }
        public string login { get; set; }
        public string pwd { get; set; }
    }


}
