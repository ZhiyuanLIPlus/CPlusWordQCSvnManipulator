using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpSvn;
using System.Collections.ObjectModel;
using System.IO;

namespace TiampMan
{
    class SVNOperator
    {
        public string RepoURL { get; set; }
        public string LocalPath { get; set; }
        public string nQC { get; set; }
        public string[] filesNames { get; set; }
        public Boolean bCorrectionData { get; set; }
        
        public SVNOperator() 
        {
            RepoURL = Program.RepoURL;
            LocalPath = Program.LocalPath;
            bCorrectionData = Program.bCorrectionData;        
        }

        public SVNReturnResult checkOut() 
        {
            SVNReturnResult returnResult = new SVNReturnResult();
            
            //SvnUpdateResult provides info about what happened during a checkout
            SvnUpdateResult result;

            //SvnCheckoutArgs wraps all of the options for the 'svn checkout' function
            SvnCheckOutArgs args = new SvnCheckOutArgs();

            //TiampMan: CheckOut
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    //SvnUriTarget is a wrapper class for SVN repository URIs
                    SvnUriTarget target = new SvnUriTarget(RepoURL);

                    //this is the where 'svn checkout' actually happens.
                    if (client.CheckOut(target, LocalPath, args, out result))
                    {
                        returnResult.bSuccessful = true;
                        returnResult.e = null;
                    }
                        //MessageBox.Show("Successfully checked out revision " + result.Revision + ".");
                }
                catch (SvnException se)
                {
                    returnResult.bSuccessful = false;
                    returnResult.e = se;
                    //MessageBox.Show(se.Message,"svn checkout error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                catch (UriFormatException ufe)
                {
                    returnResult.bSuccessful = false;
                    returnResult.e = ufe;
                    //MessageBox.Show(ufe.Message,"svn checkout error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            return returnResult;
        }

        public SVNReturnResult addAllFilesSVNInTargetFolder(string targetFolder)
        {
            SVNReturnResult returnResult = new SVNReturnResult();

            //SvnUpdateResult provides info about what happened during a checkout
            //SvnUpdateResult result;

            //TODO:see if we rly need SvnAddArgs wraps all of the options for the 'svn add' function
            //SvnAddArgs args = new SvnAddArgs();
            Collection<SvnStatusEventArgs> changedFiles = new Collection<SvnStatusEventArgs>();

            //TiampMan: Add
            //delete files from subversion that are not in filesystem
            //add files to subversion that are new in filesystem
            //modified files are automatically included as part of the commit
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    client.GetStatus(targetFolder, out changedFiles);

                    foreach (SvnStatusEventArgs changedFile in changedFiles)
                    {
                        if (changedFile.LocalContentStatus == SvnStatus.Missing)
                        {
                            // SVN thinks file is missing but it still exists hence
                            // a change in the case of the filename.
                            
                            if (System.IO.File.Exists(changedFile.Path))
                            {
                                SvnDeleteArgs changed_args = new SvnDeleteArgs();
                                changed_args.KeepLocal = true;
                                client.Delete(changedFile.Path, changed_args);
                            }
                            else
                                client.Delete(changedFile.Path);
                            
                        }
                        if (changedFile.LocalContentStatus == SvnStatus.NotVersioned)
                        {
                            client.Add(changedFile.Path);
                        }
                    }

                    returnResult.bSuccessful = true;
                    returnResult.e = null;
                    //MessageBox.Show("Successfully checked out revision " + result.Revision + ".");
                }
                catch (SvnException se)
                {
                    returnResult.bSuccessful = false;
                    returnResult.e = se;
                    //MessageBox.Show(se.Message,"svn checkout error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                catch (UriFormatException ufe)
                {
                    returnResult.bSuccessful = false;
                    returnResult.e = ufe;
                    //MessageBox.Show(ufe.Message,"svn checkout error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            return returnResult;
        }


        public SVNReturnResult setSVNFilesPropertiesInTargetFolder(string fileNamesInTargetFolder, bool bCorrectionData)
        {
            SVNReturnResult returnResult = new SVNReturnResult();

            //SvnUpdateResult provides info about what happened during a checkout
            //SvnUpdateResult result;

            //TiampMan: SetProperties
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    //if using a file value, copy file to a buffer before setting prop.
                    char fileSeperator = ';';
                    string[] cutFileName = fileNamesInTargetFolder.Split(fileSeperator);
                    if (bCorrectionData)
                    {
                        client.SetProperty(cutFileName[0], "svn:keywords", "R" + nQC + " O0042");
                        client.SetProperty(cutFileName[1], "svn:keywords", "R" + nQC + " O0041");
                    }
                    else
                    {
                        foreach (string fileName in cutFileName)
                        {
                            if (fileName.Length == 0) continue;
                            client.SetProperty(fileName, "svn:keywords", "R" + nQC);
                        }
                    }

                    returnResult.bSuccessful = true;
                    returnResult.e = null;                   
                }
                catch (SvnException se)
                {
                    returnResult.bSuccessful = false;
                    returnResult.e = se;
                }
                catch (FileNotFoundException fnfe)
                {
                    returnResult.bSuccessful = false;
                    returnResult.e = fnfe;
                }
                catch (Exception ex)
                {
                    returnResult.bSuccessful = false;
                    returnResult.e = ex;
                }
            }

            return returnResult;
        }

        public SVNReturnResult svnCommit(string fileNamesInTargetFolder)
        {
            SVNReturnResult returnResult = new SVNReturnResult();

            //SvnUpdateResult provides info about what happened during a checkout
            //SvnUpdateResult result;

            //This object allows us to provide options for 'svn commit'
            SvnCommitArgs args = new SvnCommitArgs();

            //This is how you specify a commit message.
            args.LogMessage = "R"+nQC;

            //This is where results for 'svn commit' are stored
            SvnCommitResult result;

            //TiampMan: SetProperties
            using (SvnClient client = new SvnClient())
            {
                try
                {
                    client.Commit(fileNamesInTargetFolder, args, out result);
                    returnResult.bSuccessful = true;
                    returnResult.e = null;
                }
                catch (SvnException se)
                {
                    returnResult.bSuccessful = false;
                    returnResult.e = se;
                }
                catch (Exception ex)
                {
                    returnResult.bSuccessful = false;
                    returnResult.e = ex;
                }
            }

            return returnResult;
        }
     

    }

    class SVNReturnResult 
    {
        public bool bSuccessful { get; set; }
        public Exception e { get; set; }

    }

}
