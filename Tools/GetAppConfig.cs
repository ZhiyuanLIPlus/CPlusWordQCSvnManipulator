using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Configuration;

namespace TiampMan.Tools
{
    public static class GetAppConfig
    {
        /// <summary>
        /// Récupère les informations du fichier de configuration
        /// </summary>
        public static void GetInfosConfiguration()
        {
            try
            {
                Hashtable v_HashTable = (Hashtable)ConfigurationManager.GetSection("Paths/Local");
                Program.LocalPath = ReturnValueHashTable("localTAGFolderPath", v_HashTable);
                Hashtable v_QCUserInfo = (Hashtable)ConfigurationManager.GetSection("QC/UserInfo");
                Program.qcUser.fullname = ReturnValueHashTable("fullname", v_QCUserInfo);
                Program.qcUser.login = ReturnValueHashTable("login", v_QCUserInfo);
                Program.qcUser.pwd = ReturnValueHashTable("password", v_QCUserInfo);

            }
            catch (ConfigurationErrorsException ex)
            {
                string message = ex.Message;
                throw (new ArgumentException());
            }
            catch (ArgumentNullException ex)
            {
                string message = ex.Message;
                throw (new ArgumentException());
            }
            catch (FormatException ex)
            {
                string message = ex.Message;
                throw (new ArgumentException());
            }
            catch (OverflowException ex)
            {
                string message = ex.Message;
                throw (new ArgumentException());
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                throw (new ArgumentException());
            }
        }


        /// <summary>
        /// Retourne la valeur de la Hashtable et vérifie qu'elle soit différente de null
        /// </summary>
        /// <param name="ValueToCheck"></param>
        /// <param name="v_HashTable"></param>
        /// <returns>Retourne la valeur</returns>
        private static string ReturnValueHashTable(string ValueToCheck, Hashtable v_HashTable)
        {
            if (v_HashTable != null && v_HashTable[ValueToCheck] != null)
                return v_HashTable[ValueToCheck].ToString();
            else
            {
                throw (new Exception(ValueToCheck));
            }
        }
    }
}
