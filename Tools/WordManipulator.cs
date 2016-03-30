using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TiampMan
{
    class WordManipulator
    {
        private static List<string> fileNameList;
        private static string strQCNum;
        private static string strTempFolderPath;
        private static string strTemplateDocFullPath;
        private static string strTargetDocFullPath;

        public static void SetAllProperties(List<string> fList, string sQCNum, string sTempFolderPath) 
        {
            fileNameList = fList;
            strQCNum = sQCNum;
            strTempFolderPath = sTempFolderPath;
            //string path = Directory.GetCurrentDirectory();
            string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            strTemplateDocFullPath = path + "\\template\\OPERIS_FicheSuiviQCXXXXX.doc";
            strTargetDocFullPath = sTempFolderPath + "\\" + "OPERIS_FicheSuiviQC" + sQCNum + ".doc";
        }

        public static WordManipReturnResult CreatFicheSuivi() 
        {
            WordManipReturnResult result = new WordManipReturnResult();
            object qCstring = "QC " + strQCNum;
            object saveAs = strTargetDocFullPath;
            object missing = System.Reflection.Missing.Value;
            object fileName = strTemplateDocFullPath;

            Microsoft.Office.Interop.Word.Application WordApp = new Microsoft.Office.Interop.Word.Application();

            Microsoft.Office.Interop.Word.Document aDoc = null;

            if (File.Exists((string)fileName))
            {
                DateTime today = DateTime.Now;

                object readOnly = false;
                object isVisible = false;
                WordApp.Visible = false;

                try
                {
                    aDoc = WordApp.Documents.Open(ref fileName, ref missing,
                              ref readOnly, ref missing, ref missing, ref missing,
                              ref missing, ref missing, ref missing, ref missing,
                              ref missing, ref isVisible, ref missing, ref missing,
                              ref missing, ref missing);


                    aDoc.Activate();

                    FindAndReplace(WordApp, "QCXXXXX", qCstring);

                    Tables tables = aDoc.Tables;
                    if (tables.Count > 0)
                    {
                        //Get the first table in the document
                        Table table = tables[1];

                        int rowsCount = table.Rows.Count;
                        int coulmnsCount = table.Columns.Count;

                        for (int i = 0; i < fileNameList.Count; i++)
                        {
                            object beforeRow = true;
                            Row row = table.Rows.Add(table.Rows[3]);

                            //FileName
                            row.Cells[1].Range.Text = fileNameList[i];
                            row.Cells[1].WordWrap = true;
                            row.Cells[1].Range.Underline = WdUnderline.wdUnderlineNone;
                            row.Cells[1].Range.Bold = 1;
                            //Extension
                            row.Cells[2].Range.Text = GetExtension(fileNameList[i]);
                            row.Cells[2].WordWrap = true;
                            row.Cells[2].Range.Underline = WdUnderline.wdUnderlineNone;
                            row.Cells[2].Range.Bold = 1;

                        }
                    }
                }
                catch (Exception ex)
                {
                    aDoc.Close(ref missing, ref missing, ref missing);
                    WordApp.Quit(ref missing, ref missing, ref missing);
                    result.bSuccessful = false;
                    result.strMessage = "Error occured in Creating:" + strTargetDocFullPath;
                    return result;
                }



            }
            else
            {
                result.bSuccessful = false;
                result.strMessage = "Can't find Template Doc " + strTemplateDocFullPath;
                return result;
            }

            try
            {
                aDoc.SaveAs2(ref saveAs,ref missing, ref missing, ref missing,
                             ref missing,ref missing,ref missing,ref missing,
                             ref missing,ref missing,ref missing,ref missing,
                             ref missing,ref missing,ref missing,ref missing);

                aDoc.Close(ref missing, ref missing, ref missing);
                WordApp.Quit(ref missing, ref missing, ref missing);
            }
            catch (Exception ex)
            {
                result.bSuccessful = false;
                result.strMessage = "Error occured in Saving:" + strTargetDocFullPath;
                return result;
            }
            result.bSuccessful = true;
            result.strMessage = strTargetDocFullPath;
            return result;
        }

        private static void FindAndReplace(Microsoft.Office.Interop.Word.Application WordApp, object findText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = true;
            object matchSoundLike = false;
            object nmatchAllWordForms = false;
            object forward = false;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = false;
            object replace = 2;
            object wrap = 1;

            WordApp.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord, ref matchWildCards, ref matchSoundLike,
                                           ref nmatchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                                           ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);

        }

        private static string GetExtension(string fullString)
        {
            char spotSeperator = '.';
            string[] cutString = fullString.Split(spotSeperator);
            return cutString[cutString.Length - 1];

        }
    }

    class WordManipReturnResult
    {
        public bool bSuccessful { get; set; }
        public string strMessage { get; set; }

    }
}
