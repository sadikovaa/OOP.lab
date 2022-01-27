using Backups.Services;
using Backups.TypeOfStorage;
using Backups.TypesOfFileManager;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            BackupJob curJub = new BackupJob(new SingleStorage(), "C:\\test", new FileManager());
            curJub.AddObject("D:\\test\\File1.txt");
            curJub.AddObject("D:\\test\\File2.txt");
            curJub.Save();
        }
    }
}
