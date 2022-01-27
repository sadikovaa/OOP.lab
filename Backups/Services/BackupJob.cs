using System.Collections.Generic;
using Backups.TypeOfStorage;
using Backups.TypesOfFileManager;

namespace Backups.Services
{
    public class BackupJob
    {
        private List<string> _jobObjects = new List<string>();
        private IStorage _typeOfStorage;
        private List<RestorePoint> _restorePoints = new List<RestorePoint>();
        private string _directory;
        private IFileManager _fileSystem;

        public BackupJob(IStorage storage, string directory, IFileManager fileSystem)
        {
            _typeOfStorage = storage;
            _directory = directory;
            _fileSystem = fileSystem;
            _fileSystem.CreateDirectory(directory);
        }

        public string AddObject(string path)
        {
                _jobObjects.Add(path);
                return path;
        }

        public void DeleteObject(string path)
        {
            _jobObjects.Remove(path);
        }

        public void Save()
        {
            _restorePoints.Add(_typeOfStorage.Save(_jobObjects, _directory, _restorePoints.Count + 1, _fileSystem));
        }

        public List<RestorePoint> GetRestorePoints()
        {
            return _restorePoints;
        }

        public List<string> GetJobObjects()
        {
            return _jobObjects;
        }

        public string GetDirectory()
        {
            return _directory;
        }

        public void SetRestorePoints(List<RestorePoint> restorePoints)
        {
            _restorePoints = restorePoints;
        }

        public void Clear()
        {
            _restorePoints = new List<RestorePoint>();
        }

        public void ChangeDirectory(string newDirectory)
        {
            _directory = newDirectory;
            _fileSystem.CreateDirectory(_directory);
        }
    }
}