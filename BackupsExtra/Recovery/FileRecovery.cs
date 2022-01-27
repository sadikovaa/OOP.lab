using System.Collections.Generic;
using System.IO.Compression;
using Backups.Services;

namespace BackupsExtra.Recovery
{
    public static class FileRecovery
    {
        public static void Recovery(BackupJob backup, RestorePoint point, string path)
        {
            backup.GetJobObjects().ForEach(backup.DeleteObject);
            List<string> files = point.GetPathsToZips();
            foreach (string file in files)
            {
                ZipFile.ExtractToDirectory(file, path);
                backup.AddObject(GetNameOfFile(file));
            }
        }

        private static string GetNameOfFile(string directory)
        {
            int index = directory.LastIndexOf('_');
            return directory.Substring(index + 1);
        }
    }
}