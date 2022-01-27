using System.Collections.Generic;
using Backups.Services;
using Backups.TypeOfStorage;
using Backups.TypesOfFileManager;
using BackupsExtra.Cleaning;
using BackupsExtra.Logging;
using BackupsExtra.Recovery;
using BackupsExtra.Service;
using BackupsExtra.Tools;

namespace BackupsExtra.ExtraFileManager
{
    public class ExtraFileManager : IExtraFileManager
    {
        public void CreateDirectory(string directory)
        {
            new FileManager().CreateDirectory(directory);
        }

        public void CopyAsZip(List<string> files, string directory, string name)
        {
            new FileManager().CopyAsZip(files, directory, name);
        }

        public void SaveState(BackupsExtraService backups, string path)
        {
            FileSavingState.SaveState(backups, path);
        }

        public void UpdateState(string pathToFileOfState, IStorage typeOfStorage, IExtraFileManager fileSystem, IAlgorithmForCleaning clear, bool needTime)
        {
            FileSavingState.UpdateState(pathToFileOfState, typeOfStorage, fileSystem, clear, needTime);
        }

        public void Logging(string report, bool isNeedTimeCode)
        {
            new FileLogging(ReportMessages.NameOfLog).RecordReport(report, isNeedTimeCode);
        }

        public void Recovery(BackupJob backup, RestorePoint point, string path)
        {
            FileRecovery.Recovery(backup, point, path);
        }
    }
}