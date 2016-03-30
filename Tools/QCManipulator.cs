using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TDAPIOLELib;
using System.IO;

namespace TiampMan
{
    class QCManipulator
    {
        //var myValue = types.FirstOrDefault(x => x.Value == "one").Key;
        private static Dictionary<int, string> STATUSSET;

        private static string QCURL = "http://sntr0033.eiffage.loc:8080/qcbin/";
        private static string[] commentsToAdd;
        private static string strQCNum;
        private static string strTAGNum;
        private static string strTargetStatus;
        private static int intTargetStatus;
        private static string RES_TECH_DEFAUT = Program.qcUser.login;
        private static string RES_FONC_DEFAUT = "sfakhir";
        private static string ASSIGNETO_DEFAUT = "cellule pc";
        private static UserInfo USER = Program.qcUser;
        private static bool addDocFile;
        private static string strDocFullPath;
        private static Label lbMessage;
        private static int MAXTRY_UPDATE_STATUS = 15;
        private static Bug bug;

        public static void SetAllProperties(string[] cToAdd, string sQCNum, string sTAGNum, Label labelMessage, bool cBFicheSuivi, string sDocPullPath)
        {
            STATUSSET = new Dictionary<int, string>()
            {
                {1, "01_Nouvelle"},
                {2, "02_Ouverte"},
                {3, "03_Abandonnée"},
                {4, "04_Prise en compte"},
                {5, "05_A rejeter"},
                {6, "06_A traiter"},
                {7, "07_En correction"},
                {8, "08_A contrôler"},
                {9, "09_A tester"},
                {10, "10_En test"},
                {11, "11_Testée"},
                {12, "12_A livrer"},
                {13, "13_A valider"}
            };

            commentsToAdd = cToAdd;
            strQCNum = sQCNum;
            strTAGNum = sTAGNum;
            lbMessage = labelMessage;
            addDocFile = cBFicheSuivi;
            strDocFullPath = sDocPullPath;
            //Define Target status from the tag name
            //QUALIF_.. : 9; OPR: 12
            string[] cutTagName = sTAGNum.Split('_');
            if (cutTagName[0] == "QUALIF")
            {
                strTargetStatus = STATUSSET[9];
                intTargetStatus = 9;
            }
            else if (cutTagName[0] == "OPR")
            {
                strTargetStatus = STATUSSET[12];
                intTargetStatus = 12;
            }
            else
            {
                throw new QCEXCEPTION("Can't define Target Status! Please check your tag name.");
            }
        }

        public static string UpdateQC()
        {
            lbMessage.Text = "Message QC: Begin to update QC...";
            lbMessage.Refresh();

            TDAPIOLELib.TDConnection td = new TDAPIOLELib.TDConnection();
            //Intialisation QCConnector
            try
            {
                td.InitConnectionEx(QCURL);
                td.ConnectProjectEx("OPERIS", "EIFFAGE_CSC", USER.login, USER.pwd);
            }
            catch (Exception)
            {
                CloseTDConnection(td);
                throw new QCEXCEPTION("Failed to initialise QC Connector!");
            }

            if (!td.ProjectConnected)
            {
                CloseTDConnection(td);
                throw new QCEXCEPTION("Failed to connect to QC!");
            }


            lbMessage.Text = "Message QC: Connect to QC OK! Loading QC " + strQCNum + " ...";
            lbMessage.Refresh();

            BugFactory bf = (BugFactory)td.BugFactory;
            TDFilter filter = (TDFilter)(bf.Filter);
            filter["BG_BUG_ID"] = strQCNum;
            List def = bf.NewList(filter.Text);

            if (def.Count == 0)
            {
                CloseTDConnection(td);
                throw new QCEXCEPTION("QC Num not found!");
            } 
            else
            {
                lbMessage.Text = "Message QC: QC " + strQCNum + " loaded!";
                lbMessage.Refresh();
            }

            foreach (Bug bugTemp in def)  // I know this also I need to modify
            {
                bug = bugTemp;
            }

            if (bug.IsLocked)
            {
                CloseTDConnection(td);
                throw new QCEXCEPTION("QC is Locked!");                
            }

            lbMessage.Text = "Message QC: Updating QC " + strQCNum + "...";
            lbMessage.Refresh();
            //Check of Responable Fonc et Tech
            //Responable Fonc et Tech
            string responsable_f = bug["BG_USER_39"];
            string responsable_t = bug["BG_USER_40"];
            if (responsable_f == "" || responsable_f == String.Empty || responsable_f == null)
            {
                DialogResult dialogResult = MessageBox.Show("No functional responsable found in this QC, would you like to add \"sfakhir\" as Responsable fonctionel?", 
                                                            "No Responsable fonctionnel", MessageBoxButtons.YesNo);
                if(dialogResult == DialogResult.Yes)
                {
                    bug["BG_USER_39"] = RES_FONC_DEFAUT;
                }
                else if (dialogResult == DialogResult.No)
                {
                    CloseTDConnection(td);
                    throw new QCEXCEPTION("No Responsable fonctionnel! Update QC failed!");
                }
            }

            if (responsable_t == "" || responsable_t == String.Empty || responsable_t == null)
            {
                DialogResult dialogResult = MessageBox.Show("No technical responsable found in this QC, would you like to add " + RES_TECH_DEFAUT + " as Responsable technique?",
                                                            "No Responsable technique", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    bug["BG_USER_40"] = RES_TECH_DEFAUT;
                }
                else if (dialogResult == DialogResult.No)
                {
                    CloseTDConnection(td);
                    throw new QCEXCEPTION("No Responsable technique! Update QC failed!");
                }
            }

            //Assigne to
            bug.AssignedTo = ASSIGNETO_DEFAUT;
            
            //Tag livraison
            string tagLivraison = bug["BG_USER_28"];
            tagLivraison = strTAGNum + ";" + tagLivraison;
            bug["BG_USER_28"] = tagLivraison;

            //data livraison prévu
            DateTime now = DateTime.Now;

            bug["BG_USER_08"] = now;

            //Objets techniques livrés - BG_USER_24
            string objetLivre = bug["BG_USER_24"];
            objetLivre = "Xtiamp;" + objetLivre;
            bug["BG_USER_24"] = objetLivre;

            //1st Update
            lbMessage.Text = "Message QC: Updating QC " + strQCNum + " ObjectTech/Assign To/Date Livraison/Tag...";
            lbMessage.Refresh();
            try
            {
                bug.Post();
            }
            catch (Exception ex)
            {
                CloseTDConnection(td);
                throw new QCEXCEPTION("Error occured when updating ObjectTech/Assign To/Date Livraison/Tag!");
            }

            //Add comments to QC
            if (commentsToAdd.Length > 0)
            {
                AddCommentsInQC(bug);
                //Post Comments in QC
                try
                {
                    bug.Post();
                }
                catch (Exception)
                {
                    CloseTDConnection(td);
                    throw new QCEXCEPTION("Error occured when updating QC comments!");
                }
            }

            //Add FicheSuivi
            if (addDocFile)
            {
                lbMessage.Text = "Message QC: Updating QC " + strQCNum + " FicheSuivi...";
                lbMessage.Refresh();
                try
                {
                    AttachmentFactory attachFact = bug.Attachments;
                    Attachment attachObj = attachFact.AddItem(System.DBNull.Value);
                    if (!File.Exists(strDocFullPath))
                    {
                        CloseTDConnection(td);
                        throw new QCEXCEPTION("FicheSuive File not found in Temp File! Contact ZLI!");
                    }

                    attachObj.FileName = strDocFullPath;
                    IExtendedStorage ExStrg = attachObj.AttachmentStorage;
                    ExStrg.ClientPath = strDocFullPath;
                    ExStrg.Save("OPERIS_FicheSuiviQC" + strQCNum + ".doc", true);
                    attachObj.Type = 1;
                    attachObj.Post();
                    attachObj.Refresh(); 
                }
                catch (QCEXCEPTION ex)
                {
                    CloseTDConnection(td);
                    throw ex;
                }
                catch (Exception)
                {
                    CloseTDConnection(td);
                    throw new QCEXCEPTION("Error occured when adding FicheSuivi!");
                }              
            }

            //Change status
            try
            {
                lbMessage.Text = "Message: Updating QC " + strQCNum + " Status...";
                lbMessage.Refresh();
                string qcStatusBefore = bug["BG_USER_02"];
                if (qcStatusBefore == strTargetStatus)
                {
                    CloseTDConnection(td);
                    throw new QCEXCEPTION("QC Status is already :" + strTargetStatus  + ", no need to change!");
                }
                var statusKey = STATUSSET.FirstOrDefault(x => x.Value == qcStatusBefore).Key;
                //Status > 13 : Change to 13
                if (statusKey == 0) 
                {
                    bug["BG_USER_02"] = STATUSSET[13];
                    try
                    {
                        bug.Post();
                    }
                    catch (Exception)
                    {
                        CloseTDConnection(td);
                        throw new QCEXCEPTION("Failed to change Stauts from Unknown to 13_A valider!");
                    }
                    statusKey = 13;
                }

                //Status ==  1: Exception
                if (statusKey == 1)
                {
                    CloseTDConnection(td);
                    throw new QCEXCEPTION("QC status is 01_Nouvelle! Please contact Eiffage for change it!");
                }

                //Status == 2 : Change to 4
                if (statusKey == 2)
                {
                    bug["BG_USER_02"] = STATUSSET[4];
                    try
                    {
                        bug.Post();
                    }
                    catch (Exception)
                    {
                        CloseTDConnection(td);
                        throw new QCEXCEPTION("Failed to change Stauts to 04_Prise en compte!");
                    }
                    statusKey = 4;
                }

                int timeTry = 0;
                int tempStatusKey;
                do
                {
                    tempStatusKey = statusKey;
                    if (statusKey < intTargetStatus)
                    {
                        statusKey++;
                    }
                    else if (statusKey > intTargetStatus)
                    {
                        statusKey--;
                    }

                    bug["BG_USER_02"] = STATUSSET[statusKey];
                    
                    try
                    {
                        bug.Post();
                    }
                    catch (Exception)
                    {
                        CloseTDConnection(td);
                        throw new QCEXCEPTION("Failed to change Stauts from "+ STATUSSET[tempStatusKey] + " to " + STATUSSET[statusKey]);
                    }

                    timeTry++;
                }
                while ((statusKey != intTargetStatus) && (timeTry < MAXTRY_UPDATE_STATUS));

                if (timeTry == MAXTRY_UPDATE_STATUS)
                {
                    CloseTDConnection(td);
                    throw new QCEXCEPTION("Failed to change Stauts, exceeds max try times");
                }
            }
            catch (QCEXCEPTION ex)
            {
                CloseTDConnection(td);
                throw ex;
            }
            catch (Exception)
            {
                CloseTDConnection(td);
                throw new QCEXCEPTION("Error occured when updating QC Status");
            }

            CloseTDConnection(td);
            return "Update QC Ok!";
        }

        private static void AddCommentsInQC(Bug bug)
        {
            string header;
            string oldComments = bug["BG_DEV_COMMENTS"];
            string newComments;

            if (oldComments != null)
                header = "<br><font color=\"#000080\"><b>________________________________________</b></font><br>";
            else
                header = "";
            string strDate;
            strDate = "/" + DateTime.Now.Year;
            if (DateTime.Now.Month < 10)
            {
                strDate = "0" + DateTime.Now.Month + strDate;
            }
            else
            {
                strDate =  DateTime.Now.Month + strDate;
            }

            if (DateTime.Now.Day < 10)
            {
                strDate = "0" + DateTime.Now.Day + "/" + strDate;
            }
            else
            {
                strDate = DateTime.Now.Day + "/" + strDate;
            }
            header = header + "<font color=\"#000080\"><b>" + USER.fullname + " &lt;" + USER.login + "&gt;, " + strDate + ": </b></font>";

            string comments = "";
            foreach (string commentsLine in commentsToAdd)
            {
                comments = comments + "<br>" + commentsLine;
            }

            comments = header + comments;

            if (oldComments == null)
            {
                newComments = "<html><body>" + comments + "</body></html>";
            }
            else
            {
                newComments = oldComments.Replace("</body>", comments + "</body>");
            }

            bug["BG_DEV_COMMENTS"] = newComments;

        }

        private static void CloseTDConnection(TDAPIOLELib.TDConnection td) 
        {
            try
            {
                td.DisconnectProject();
                td.ReleaseConnection();
            }
            catch (Exception)
            {
                throw new DISCONNECTIONEXCEPTION("Fail to disconnect from QC!");
            }
        }
    }
    
    //Exception when updating QC
    public class QCEXCEPTION : Exception
    {
        public QCEXCEPTION()
        {
        }

        public QCEXCEPTION(string message)
            : base(message)
        {
        }

        public QCEXCEPTION(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    //Exception when disconnect from QC
    public class DISCONNECTIONEXCEPTION : Exception
    {
        public DISCONNECTIONEXCEPTION()
        {
        }

        public DISCONNECTIONEXCEPTION(string message)
            : base(message)
        {
        }

        public DISCONNECTIONEXCEPTION(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

}
