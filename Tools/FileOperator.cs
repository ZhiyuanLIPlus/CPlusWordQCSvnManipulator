using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TiampMan
{
    /// <summary>
    /// Classe contenu toutes les fonctions necessaires à la gestion des fichiers/dossiers (création, supression, information fichier etc )
    /// </summary>
    public static class FileOperator
    {
        /// <summary>
        /// Vérifie que le nom donné en paramètre puisse être enregistré sur le disque
        /// </summary>
        /// <param name="FileName">Nom à vérifier</param>
        /// <returns>Nom validé</returns>
        public static string CheckFileOrFolderName(string FileName)
        {
            string _invalidCharsRegex = "[" + new string(System.IO.Path.GetInvalidFileNameChars()).Replace(@"\", @"\\") + "]";

            if (FileName == null)
                return null;
            return System.Text.RegularExpressions.Regex.Replace(FileName, _invalidCharsRegex, "_");
        }

        /// <summary>
        /// Permet de récupérer toutes les lignes dans un fichier
        /// </summary>
        /// <param name="pathFile">Chemin du fichier à lire</param>
        /// <returns></returns>
        public static string[] ReadAllLines(string pathFile)
        {
            try
            {
                return System.IO.File.ReadAllLines(pathFile, Encoding.Default);
            }
            catch (ArgumentNullException ex)
            {
                throw (new ArgumentNullException(ex.Message));
            }
            catch (ArgumentException ex)
            {
                throw (new ArgumentException(ex.Message));
            }
            catch (PathTooLongException ex)
            {
                throw (new PathTooLongException(ex.Message));
            }
            catch (DirectoryNotFoundException ex)
            {
                throw (new DirectoryNotFoundException(ex.Message));
            }
            catch (FileNotFoundException ex)
            {
                throw (new FileNotFoundException(ex.Message));
            }
            catch (IOException ex)
            {
                throw (new IOException(ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                throw (new UnauthorizedAccessException(ex.Message));
            }
            catch (NotSupportedException ex)
            {
                throw (new NotSupportedException(ex.Message));
            }
            catch (System.Security.SecurityException ex)
            {
                throw (new System.Security.SecurityException(ex.Message));
            }
        }

        /// <summary>
        /// Ecrite dans un fichier les données transmises en paramètres
        /// </summary>
        /// <param name="entirePath">Chemin du fichier à écrire</param>
        /// <param name="dataToWrite">Données à écrire</param>
        public static void WriteAllLines(string entirePath, List<string> dataToWrite)
        {
            try
            {
                WriteAllLines(entirePath, dataToWrite, false);
            }
            catch (ObjectDisposedException ex)
            {
                throw (new ObjectDisposedException(ex.Message));
            }
            catch (ArgumentNullException ex)
            {
                throw (new ArgumentNullException(ex.Message));
            }
            catch (DirectoryNotFoundException ex)
            {
                throw (new DirectoryNotFoundException(ex.Message));
            }
            catch (System.Security.SecurityException ex)
            {
                throw (new System.Security.SecurityException(ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                throw (new UnauthorizedAccessException(ex.Message));
            }
            catch (NotSupportedException ex)
            {
                throw (new NotSupportedException(ex.Message));
            }
            catch (ArgumentException ex)
            {
                throw (new ArgumentException(ex.Message));
            }
            catch (PathTooLongException ex)
            {
                throw (new PathTooLongException(ex.Message));
            }
            catch (IOException ex)
            {
                throw (new IOException(ex.Message));
            }
        }

        /// <summary>
        /// Ecrite dans un fichier les données transmises en paramètres
        /// </summary>
        /// <param name="entirePath">Chemin du fichier à écrire</param>
        /// <param name="dataToWrite">Données à écrire</param>
        /// <param name="Append">Ajoute au fichier ou non</param>
        public static void WriteAllLines(string entirePath, List<string> dataToWrite, bool Append)
        {
            StreamWriter monStreamWriter = null;
            try
            {
                if (Directory.Exists(new FileInfo(entirePath).DirectoryName))
                {
                    monStreamWriter = new StreamWriter(entirePath, Append);
                    foreach (String Line in dataToWrite)
                    {
                        monStreamWriter.WriteLine(Line);
                    }
                }
            }
            catch (ObjectDisposedException ex)
            {
                throw (new ObjectDisposedException(ex.Message));
            }
            catch (ArgumentNullException ex)
            {
                throw (new ArgumentNullException(ex.Message));
            }
            catch (DirectoryNotFoundException ex)
            {
                throw (new DirectoryNotFoundException(ex.Message));
            }
            catch (System.Security.SecurityException ex)
            {
                throw (new System.Security.SecurityException(ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                throw (new UnauthorizedAccessException(ex.Message));
            }
            catch (NotSupportedException ex)
            {
                throw (new NotSupportedException(ex.Message));
            }
            catch (ArgumentException ex)
            {
                throw (new ArgumentException(ex.Message));
            }
            catch (PathTooLongException ex)
            {
                throw (new PathTooLongException(ex.Message));
            }
            catch (IOException ex)
            {
                throw (new IOException(ex.Message));
            }
            finally
            {
                if (monStreamWriter != null)
                    monStreamWriter.Close();
            }
        }

        /// <summary>
        /// Permet de créer un dossier
        /// </summary>
        /// <param name="pathFolder">Chemin du dossier à créer</param>
        public static String CreateFolder(string pathFolder)
        {
            try
            {
                if (!Directory.Exists(pathFolder))
                    Directory.CreateDirectory(pathFolder);
                return pathFolder;
            }
            catch (DirectoryNotFoundException ex)
            {
                throw (new DirectoryNotFoundException(ex.Message));
            }
            catch (PathTooLongException ex)
            {
                throw (new PathTooLongException(ex.Message));
            }
            catch (IOException ex)
            {
                throw (new IOException(ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                throw (new UnauthorizedAccessException(ex.Message));
            }
            catch (ArgumentNullException ex)
            {
                throw (new ArgumentNullException(ex.Message));
            }
            catch (ArgumentException ex)
            {
                throw (new ArgumentException(ex.Message));
            }
            catch (NotSupportedException ex)
            {
                throw (new NotSupportedException(ex.Message));
            }
        }

        /// <summary>
        /// Permet de créer un dossier
        /// </summary>
        /// <param name="path">Chemin de destionation</param>
        /// <param name="folderName">Nom du dossier</param>
        public static void CreateFolder(string path, string folderName)
        {
            string pathFolder = FileOperator.Combine(path, folderName);
            FileOperator.CreateFolder(pathFolder);
        }

        /// <summary>
        /// Permet de créer un fichier
        /// </summary>
        /// <param name="pathFile">Chemin du fichier à créer</param>
        public static FileStream CreateFile(string pathFile)
        {
            try
            {
                return File.Create(pathFile);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw (new UnauthorizedAccessException(ex.Message));
            }
            catch (ArgumentNullException ex)
            {
                throw (new ArgumentNullException(ex.Message));
            }
            catch (ArgumentException ex)
            {
                throw (new ArgumentException(ex.Message));
            }
            catch (DirectoryNotFoundException ex)
            {
                throw (new DirectoryNotFoundException(ex.Message));
            }
            catch (PathTooLongException ex)
            {
                throw (new PathTooLongException(ex.Message));
            }
            catch (IOException ex)
            {
                throw (new IOException(ex.Message));
            }
            catch (NotSupportedException ex)
            {
                throw (new NotSupportedException(ex.Message));
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        /// <summary>
        /// Permet de créer un fichier
        /// </summary>
        /// <param name="path">Chemin du fichier à créer</param>
        /// <param name="nameFile">Nom du fichier (et son type)</param>
        /// <returns></returns>
        public static FileStream CreateFile(string path, string nameFile)
        {
            string pathFile = FileOperator.Combine(path, nameFile);
            return FileOperator.CreateFile(pathFile);
        }

        /// <summary>
        /// Combine les deux chemins en paramètre
        /// </summary>
        /// <param name="path1">Chemin 1</param>
        /// <param name="path2">Chemin 2</param>
        public static string Combine(string path1, string path2)
        {
            try
            {
                return System.IO.Path.Combine(path1, path2);
            }
            catch (ArgumentNullException ex)
            {
                throw (new ArgumentNullException(ex.Message));
            }
            catch (ArgumentException ex)
            {
                throw (new ArgumentException(ex.Message));
            }
        }

        /// <summary>
        /// Permet de supprimer un dossier (et sous dossier)
        /// </summary>
        /// <param name="pathFolder">Chemin du dossier à supprimer</param>
        public static void DeleteFolder(string pathFolder)
        {
            DeleteFolder(pathFolder, false);
        }

        /// <summary>
        /// Permet de supprimer un dossier (et sous dossier)
        /// </summary>
        /// <param name="pathFolder">Chemin du dossier à supprimer</param>
        /// <param name="Recursif">true si il doit supprimer les fichiers et sous dossiers sinon false</param>
        public static void DeleteFolder(string pathFolder, bool Recursif)
        {
            try
            {
                if (System.IO.Directory.Exists(pathFolder))
                    System.IO.Directory.Delete(pathFolder, Recursif);
            }
            catch (PathTooLongException ex)
            {
                throw (new PathTooLongException(ex.Message));
            }
            catch (DirectoryNotFoundException ex)
            {
                throw (new DirectoryNotFoundException(ex.Message));
            }
            catch (IOException ex)
            {
                throw (new IOException(ex.Message));
            }
            catch (ArgumentNullException ex)
            {
                throw (new ArgumentNullException(ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                throw (new UnauthorizedAccessException(ex.Message));
            }
            catch (ArgumentException ex)
            {
                throw (new ArgumentException(ex.Message));
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
        }

        /// <summary>
        /// Retourne le chemin complet du dossier ou fichier
        /// </summary>
        /// <param name="path">Chemin relatif à transformer en chemin absolue</param>
        /// <returns></returns>
        public static string GetFullPath(string path)
        {
            try
            {
                return Path.GetFullPath(path);
            }
            catch (ArgumentNullException ex)
            {
                throw (new ArgumentNullException(ex.Message));
            }
            catch (ArgumentException ex)
            {
                throw (new ArgumentException(ex.Message));
            }
            catch (System.Security.SecurityException ex)
            {
                throw (new System.Security.SecurityException(ex.Message));
            }
            catch (NotSupportedException ex)
            {
                throw (new NotSupportedException(ex.Message));
            }
            catch (PathTooLongException ex)
            {
                throw (new PathTooLongException(ex.Message));
            }
        }

        /// <summary>
        /// Permet d'ouvrir un fichier et de pouvoir écrire
        /// </summary>
        /// <param name="pathFile">Chemin du fichier</param>
        /// <returns></returns>
        public static FileStream OpenRead(string pathFile)
        {
            try
            {
                return File.OpenRead(pathFile);
            }
            catch (ArgumentNullException ex)
            {
                throw (new ArgumentNullException(ex.Message));
            }
            catch (ArgumentException ex)
            {
                throw (new ArgumentException(ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                throw (new UnauthorizedAccessException(ex.Message));
            }
            catch (PathTooLongException ex)
            {
                throw (new PathTooLongException(ex.Message));
            }
            catch (DirectoryNotFoundException ex)
            {
                throw (new DirectoryNotFoundException(ex.Message));
            }
            catch (FileNotFoundException ex)
            {
                throw (new FileNotFoundException(ex.Message));
            }
            catch (NotSupportedException ex)
            {
                throw (new NotSupportedException(ex.Message));
            }
        }

        /// <summary>
        /// Permet d'obtenir tous les chemins des fichiers dans le dossier
        /// </summary>
        /// <param name="pathFolder">Chemin du dossier</param>
        /// <param name="filtre">Filtre permettant d'obtenir certain fichier
        /// Exemples : "*.zip" permet d'obtenir tous les fichiers zip
        ///            "*.* permet d'obtenir tous les types de fichiers</param>
        /// <param name="option">Option de recherche, on peut par exemple récupérer tous les fichiers dans les sous dossiers</param>
        /// <returns></returns>
        public static string[] GetFiles(string pathFolder, string filtre, SearchOption option)
        {
            try
            {
                return Directory.GetFiles(pathFolder, filtre, option);
            }
            catch (ArgumentNullException ex)
            {
                throw (new ArgumentNullException(ex.Message));
            }
            catch (ArgumentException ex)
            {
                throw (new ArgumentException(ex.Message));
            }
            catch (DirectoryNotFoundException ex)
            {
                throw (new DirectoryNotFoundException(ex.Message));
            }
            catch (PathTooLongException ex)
            {
                throw (new PathTooLongException(ex.Message));
            }
            catch (UnauthorizedAccessException ex)
            {
                throw (new UnauthorizedAccessException(ex.Message));
            }
        }

        /// <summary>
        /// Permet de récuperer les informations d'un fichier (nom, date de création etc ...)
        /// </summary>
        /// <param name="pathFolder">chemin du dossier</param>
        /// <returns></returns>
        public static DirectoryInfo DirectoryInfo(string pathFolder)
        {
            try
            {
                return new DirectoryInfo(pathFolder);
            }
            catch (ArgumentNullException ex)
            {
                throw (new ArgumentNullException(ex.Message));
            }
            catch (System.Security.SecurityException ex)
            {
                throw (new System.Security.SecurityException(ex.Message));
            }
            catch (PathTooLongException ex)
            {
                throw (new PathTooLongException(ex.Message));
            }
            catch (ArgumentException ex)
            {
                throw (new ArgumentException(ex.Message));
            }
        }

        //"D:\AZE\t1.xtiamp" -> "t1"
        public static string GetFileNameWithOutExtension(string fullFileName)
        {
            string returnString;
            char fileSeperator = '\\';
            char spotSeperator = '.';
            string[] cutString = fullFileName.Split(fileSeperator);

            returnString = cutString[cutString.Length - 1].Split(spotSeperator)[0];
            return returnString;
        }

        //"D:\AZE\t1.xtiamp" -> "t1.xtiamp"
        public static string GetFileNameWithExtension(string fullFileName)
        {
            string returnString;
            char fileSeperator = '\\';
            string[] cutString = fullFileName.Split(fileSeperator);

            returnString = cutString[cutString.Length - 1];
            return returnString;
        }

        //"D:\AZE\t1.txt;C:\QSDAZ\t2.xtiamp" ---> "D:\Temp\t1.txt;D:\Temp\t2.xtiamp"
        public static string CopyMoveFile(string source_fileNames, string pathFolder) 
        { 
            char fileSeperator = ';';
            string[] cutFileName = source_fileNames.Split(fileSeperator);
            string fileNamesInTargetFolderPath = "";
            foreach (string fileName in cutFileName)
            {
                if (fileName.Length == 0) continue;
                string tiampName = GetFileNameWithExtension(fileName);
                string targetFullFilePath = Combine(pathFolder, tiampName);
                try
                {
                    System.IO.File.Copy(fileName, targetFullFilePath, true);
                    fileNamesInTargetFolderPath = fileNamesInTargetFolderPath + targetFullFilePath + ";";

                }
                catch (Exception ex)
                {
                    throw (new Exception(ex.Message));
                }
            }

            return fileNamesInTargetFolderPath;       
        }

        //D:\AZE\t1.xtiamp;D:\QSDAZ\t2.xtiamp ---> List<string> ["t1.xtiamp","t2.xtiamp"]
        public static List<string> DispatherTiampFileNamesToShortFileName(string fullFileNames) 
        {
            List<string> returnList = new List<string>();
            char fileSeperator = ';';
            string[] cutFileName = fullFileNames.Split(fileSeperator);
            foreach (string fileName in cutFileName)
            {
                if (fileName.Length == 0) continue;
                string tiampName = GetFileNameWithExtension(fileName);
                returnList.Add(tiampName);
            }

            return returnList;
        }

     }

}
