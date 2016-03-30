using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpSvn;

namespace TiampMan
{
    public partial class TiampMan : Form
    {

        public TiampMan()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.tbRepoURI.Text = Program.RepoURL;
            this.tbLocalPath.Text = Program.LocalPath;
        }

        private void ButtonFire_Click(object sender, EventArgs e)
        {
            //string[] test = tBQCComments.Lines;

            //Check the necessary values
            if (tbLocalPath.Text.Length == 0 || tbRepoURI.Text.Length == 0 || tbFileName.Text.Length == 0 || cBBranch.Text.Length == 0 || tbQCNum.Text.Length == 0)
            {
                MessageBox.Show("Please fill out all necessary fields.");
                return;
            }
            labelMessage.Text = "Message SVN: Begin!";
            labelMessage.Refresh();
            
            SVNOperator svnOperator = new SVNOperator();
            SVNReturnResult svnResult;
            labelMessage.Text = "Message SVN: CheckOuting...";
            labelMessage.Refresh();
            
            //CheckOut           
            
            svnOperator.LocalPath = tbLocalPath.Text;
            svnOperator.RepoURL = tbRepoURI.Text;
            svnResult = svnOperator.checkOut();

            if (svnResult.bSuccessful)
            {
                labelMessage.Text = "Message SVN: CheckOut OK!";
                labelMessage.Refresh();
            }
            else
            {
                labelMessage.Text = "Message SVN: CheckOut Error! ";
                labelMessage.Refresh();
                return;
            }
            
            
            //FileOperator

            //Creation of temp Folder
            string pathTempFolder = Program.LocalPath + "\\Temp";
            FileOperator.CreateFolder(pathTempFolder);
            Program.shortFileNameList = new List<string>();
            string fileNamesInTempFolder = "";

            //if it's data correction we need to create a text file
            if (rBData.Checked)
            {
                labelMessage.Text = "Message SVN: Creating ReadMe file for the macro...";
                labelMessage.Refresh();
                //GetTiampFileName
                string readMeFileName = "";
                string tiampFileName = "";
                tiampFileName = FileOperator.GetFileNameWithOutExtension(tbFileName.Text);
                readMeFileName = "ReadMe_" + tiampFileName + ".txt";
                labelMessage.Refresh();

                labelMessage.Text = "Message SVN:" + readMeFileName;
                labelMessage.Refresh();

                //Write ReadMeFile
                labelMessage.Text = "Message SVN: Writing in" + readMeFileName + "...";
                labelMessage.Refresh();
                List<string> linesToWrite = new List<string>();
                string dossierCode;
                switch (cBBranch.Text)
                {
                    case "FORCLUM": dossierCode = "MODELE_FORCL";
                        break;
                    case "TP": dossierCode = "MODELE_TVXPB";
                        break;
                    case "BATIMENT": dossierCode = "MODELE_BATIM";
                        break;
                    case "EIFFEL": dossierCode = "MODELE_EIFFE";
                        break;
                    default: dossierCode = "";
                        break;
                }
                linesToWrite.Add(" ");
                linesToWrite.Add("Correction pour l'anomalie " + tbQCNum.Text);
                linesToWrite.Add(" ");
                linesToWrite.Add("	* A appliquer uniquement en PROD");
                linesToWrite.Add(" ");
                linesToWrite.Add("Procédure :");
                linesToWrite.Add(" ");
                linesToWrite.Add("A/ Se connecter par le compte Administrateur en passant par le dossier " + dossierCode);
                linesToWrite.Add("B/ Dans le menu général / \"Les commandes\", sélectionner la macro : " + tiampFileName + " qui doit se trouver dans la rubrique \"Admin Correction\"");
                linesToWrite.Add("C/ Cliquer sur le bouton \"Corriger\"");
                linesToWrite.Add("D/ Fermer la macro.");
                linesToWrite.Add("E/ Aller dans le menu des macros et supprimer la macro :" + tiampFileName + ".xtiamp");

                FileOperator.WriteAllLines(FileOperator.Combine(pathTempFolder, readMeFileName), linesToWrite);

                fileNamesInTempFolder = fileNamesInTempFolder + FileOperator.Combine(pathTempFolder, readMeFileName) + ";";
                labelMessage.Text = "Message SVN: " + readMeFileName + " Done!";
                labelMessage.Refresh();
            }

            labelMessage.Text = "Message SVN: Copying files in temp folder";
            labelMessage.Refresh();
            fileNamesInTempFolder = fileNamesInTempFolder + FileOperator.CopyMoveFile(tbFileName.Text, pathTempFolder);
            Program.shortFileNameList.AddRange(FileOperator.DispatherTiampFileNamesToShortFileName(fileNamesInTempFolder));
            labelMessage.Text = "Message SVN: Copying files completed!";
            labelMessage.Refresh();

            //SVN add
            //--Copy to target file
            
            string targetFolder = tbLocalPath.Text + "\\OPT\\" + cBBranch.Text;
            string fileNamesInTargetFolder = "";
            labelMessage.Text = "Message SVN: Copying files to target folder: " + targetFolder;
            labelMessage.Refresh();
            fileNamesInTargetFolder = fileNamesInTargetFolder + FileOperator.CopyMoveFile(fileNamesInTempFolder, targetFolder);
            labelMessage.Text = "Message SVN: Copying files in target folder: " + targetFolder + "Completed!";
            labelMessage.Refresh();
            //--Add files
            svnResult = svnOperator.addAllFilesSVNInTargetFolder(targetFolder);
            if (svnResult.bSuccessful)
            {
                labelMessage.Text = "Message SVN: Add files OK!";
                labelMessage.Refresh();
            }
            else
            {
                labelMessage.Text = "Message SVN: Add files Errors!" + svnResult.e.Message;
                return;
            }

            //SVN setProperties
            svnOperator.nQC = tbQCNum.Text;
            svnResult = svnOperator.setSVNFilesPropertiesInTargetFolder(fileNamesInTargetFolder, rBData.Checked);
            if (svnResult.bSuccessful)
            {
                labelMessage.Text = "Message SVN: Set SVN keywors OK!";
                labelMessage.Refresh();
            }
            else
            {
                labelMessage.Text = "Message SVN: Set SVN keywors Error! ";
                labelMessage.Refresh();
                return;
            }

            //SVN commit
            labelMessage.Text = "Message SVN: Commiting...";
            labelMessage.Refresh();
            svnResult = svnOperator.svnCommit(targetFolder);
            if (svnResult.bSuccessful)
            {
                labelMessage.Text = "Message SVN: Commit OK!";
                labelMessage.Refresh();
            }
            else
            {
                labelMessage.Text = "Message SVN: Commit Error!";
                labelMessage.Refresh();
                return;
            }
            
            //Generation of doc file
            string strDocFileFullPath = "";
            if(cBFicheSuivi.Checked)
            {
                lbMessageQC.Text = "Message QC: Creating FicheSuivi...";
                lbMessageQC.Refresh();

                WordManipReturnResult oDocResult;
                WordManipulator.SetAllProperties(Program.shortFileNameList, tbQCNum.Text, pathTempFolder);
                oDocResult = WordManipulator.CreatFicheSuivi();
                if (!oDocResult.bSuccessful)
                {
                    lbMessageQC.Text = "Message QC: " + oDocResult.strMessage;
                    lbMessageQC.Refresh();
                    FileOperator.DeleteFolder(pathTempFolder, true);
                    return;                   
                }
                else
                {
                    strDocFileFullPath = oDocResult.strMessage;
                    lbMessageQC.Text = "Message QC:" + strDocFileFullPath + " created!";
                    lbMessageQC.Refresh();
                }
            }
           
            //Update QC
            if (cBTouchQC.Checked)
            {
                lbMessageQC.Text = "Message QC: Ready to touch QC...";
                lbMessageQC.Refresh();
                tBQCComments.Enabled = false;
                try
                {
                    QCManipulator.SetAllProperties(tBQCComments.Lines, tbQCNum.Text, tbTAG.Text, lbMessageQC, cBFicheSuivi.Checked, strDocFileFullPath);
                    lbMessageQC.Text = "Message QC:" + QCManipulator.UpdateQC();
                    lbMessageQC.Refresh();
                }
                catch (QCEXCEPTION qcex)
                {
                    lbMessageQC.Text = "Error QC:" + qcex.Message;
                    lbMessageQC.Refresh();
                }
                catch (DISCONNECTIONEXCEPTION ex)
                {
                    lbMessageQC.Text = "Error QC:" + ex.Message;
                    lbMessageQC.Refresh();
                }
                catch (Exception ex)
                {
                    lbMessageQC.Text = "Error QC: Fail to modify QC!";
                    lbMessageQC.Refresh();
                }
                finally 
                {
                    tBQCComments.Enabled = true;
                }
            }
            //Delete temp files
            FileOperator.DeleteFolder(pathTempFolder,true);

        }

        private void tbTAG_TextChanged_1(object sender, EventArgs e)
        {
            tbLocalPath.Text = Program.LocalPath + tbTAG.Text;
            tbRepoURI.Text = Program.RepoURL + tbTAG.Text;
        }

        private void rBData_CheckedChanged(object sender, EventArgs e)
        {
            Program.bCorrectionData = rBData.Checked;
            tbFileName.Text = "";
            //MessageBox.Show(Program.bCorrectionData.ToString());

            //Fonds
            if (!rBData.Checked)
            {
                cBBranch.SelectedIndex = 0;
                ofdFileSelector.Multiselect = true;
            }
            else //Donées
            {
                ofdFileSelector.Multiselect = false;
            }
        }

        private void ButtonBrower_Click(object sender, EventArgs e)
        {
            tbFileName.Text = "";
            if (ofdFileSelector.ShowDialog() == DialogResult.OK)
            {
                // Read the files
                foreach (String file in ofdFileSelector.FileNames)
                {
                    tbFileName.Text = tbFileName.Text + file + ";";
                    //string test = FileOperator.getFileName(file);
                }
            }
        }

        private void ButtonQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cBTouchQC_CheckedChanged(object sender, EventArgs e)
        {
            if (cBTouchQC.Checked)
	        {
                tBQCComments.Enabled = true;
	        }
            else
            {
                tBQCComments.Enabled = false;
            }

        }


    }
}
