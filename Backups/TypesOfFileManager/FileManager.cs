using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using FileInfo = Backups.Tools.FileInfo;

namespace Backups.TypesOfFileManager
{
    public class FileManager : IFileManager
    {
        public void CreateDirectory(string directory)
        {
            Directory.CreateDirectory(directory);
        }

        public void CopyAsZip(List<string> files, string directory, string name)
        {
            string newDirectory = directory + '\\' + name;
            Directory.CreateDirectory(newDirectory);
            foreach (string currFile in files)
            {
                File.Copy(currFile, newDirectory + '\\' + FileInfo.GetNameOfFile(currFile), true);
            }

            ZipFile.CreateFromDirectory(newDirectory, name);
            Directory.Delete(newDirectory, true);
        }
    }
}