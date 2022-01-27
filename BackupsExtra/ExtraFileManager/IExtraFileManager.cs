using Backups.Services;
using Backups.TypeOfStorage;
using Backups.TypesOfFileManager;
using BackupsExtra.Cleaning;
using BackupsExtra.Service;

namespace BackupsExtra.ExtraFileManager
{
    public interface IExtraFileManager : IFileManager
    {
        void SaveState(BackupsExtraService backups, string path);
        void UpdateState(string pathToFileOfState, IStorage typeOfStorage, IExtraFileManager fileSystem, IAlgorithmForCleaning clear, bool needTime);
        void Logging(string report, bool isNeedTimeCode);
        void Recovery(BackupJob backup, RestorePoint point, string path);
    }
}