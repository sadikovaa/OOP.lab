using System.Collections.Generic;
using System.Linq;
using Backups.Services;

namespace BackupsExtra.Recovery
{
    public static class VirtualRecovery
    {
        public static void Recovery(BackupJob backup, RestorePoint point, string path)
        {
            var jobs = backup.GetJobObjects().ToList();
            foreach (string jobObject in jobs)
            {
                backup.DeleteObject(jobObject);
            }

            List<string> files = point.GetPathsToZips();
            foreach (string file in files)
            {
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