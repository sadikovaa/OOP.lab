using System.Collections.Generic;
using Backups.Services;
using Backups.Tools;
using Backups.TypeOfStorage;
using Backups.TypesOfFileManager;
using BackupsExtra.Cleaning;
using BackupsExtra.Logging;
using BackupsExtra.Recovery;
using BackupsExtra.Service;

namespace BackupsExtra.ExtraFileManager
{
    public class VirtualExtraFileManager : IExtraFileManager
    {
        public void CreateDirectory(string directory)
        {
            new VirtualFileManager().CreateDirectory(directory);
        }

        public void CopyAsZip(List<string> files, string directory, string name)
        {
            new VirtualFileManager().CopyAsZip(files, directory, name);
        }

        public void SaveState(BackupsExtraService backups, string path)
        {
            // this function has empty body,
            // because this implementation of IFileManager doesn't work with computer's File System.
        }

        public void UpdateState(string pathToFileOfState, IStorage typeOfStorage, IExtraFileManager fileSystem, IAlgorithmForCleaning clear, bool needTime)
        {
            throw new BackupsException("You are in virtual mode and cannot load the state!");
        }

        public void Logging(string report, bool isNeedTimeCode)
        {
            new ConsoleLogging().RecordReport(report, isNeedTimeCode);
        }

        public void Recovery(BackupJob backup, RestorePoint point, string path)
        {
            VirtualRecovery.Recovery(backup, point, path);
        }
    }
}