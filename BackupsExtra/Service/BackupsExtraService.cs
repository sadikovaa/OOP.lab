using System;
using System.Collections.Generic;
using Backups.Services;
using Backups.TypeOfStorage;
using BackupsExtra.Cleaning;
using BackupsExtra.ExtraFileManager;
using BackupsExtra.Logging;
using BackupsExtra.Tools;

namespace BackupsExtra.Service
{
    public class BackupsExtraService
    {
        private BackupJob _backupJob;
        private IAlgorithmForCleaning _algorithmForCleaning;
        private IExtraFileManager _fileManager;
        private bool _needTimeCode;

        public BackupsExtraService(IStorage storage, string directory, IExtraFileManager fileSystem, IAlgorithmForCleaning cleaning, bool needTimeCode)
        {
            _backupJob = new BackupJob(storage, directory, fileSystem);
            _algorithmForCleaning = cleaning;
            _fileManager = fileSystem;
            _needTimeCode = needTimeCode;
        }

        public BackupsExtraService(string pathToFileOfState, IStorage typeOfStorage, IExtraFileManager fileSystem, IAlgorithmForCleaning clear, bool needTime)
        {
            _fileManager = new ExtraFileManager.ExtraFileManager();
            _fileManager.UpdateState(pathToFileOfState, typeOfStorage, fileSystem, clear, needTime);
        }

        public string AddObject(string path)
        {
            _backupJob.AddObject(path);
            return path;
        }

        public void DeleteObject(string path)
        {
            _backupJob.DeleteObject(path);
        }

        public void Save()
        {
            _backupJob.Save();
            _fileManager.Logging(ReportMessages.CreatePoint, _needTimeCode);
        }

        public void Clear()
        {
            _backupJob.Clear();
        }

        public void ChangeDirectory(string newDirectory)
        {
            _backupJob.ChangeDirectory(newDirectory);
        }

        public void SaveState(string path)
        {
            _fileManager.SaveState(this, path);
        }

        public void Cleaning()
        {
            _backupJob = _algorithmForCleaning.Clearing(_backupJob);
            _fileManager.Logging(ReportMessages.MergePoints, _needTimeCode);
        }

        public void FileRecovery(DateTime date, string path)
        {
            foreach (var point in _backupJob.GetRestorePoints())
            {
                if (point.GetTime() == date)
                {
                    _fileManager.Recovery(_backupJob, point, path);
                    return;
                }
            }

            throw new BackupsExtraException("There is no point at this time!");
        }

        public List<string> GetJobObjects()
        {
            return _backupJob.GetJobObjects();
        }

        public string GetDirectory()
        {
            return _backupJob.GetDirectory();
        }

        public List<RestorePoint> GetRestorePoints()
        {
            return _backupJob.GetRestorePoints();
        }

        public void SetBackupJob(BackupJob backupJob)
        {
            _backupJob = backupJob;
        }

        public void SetRestorePoints(List<RestorePoint> points)
        {
            _backupJob.SetRestorePoints(points);
        }
    }
}