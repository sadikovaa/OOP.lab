using Backups.Services;
using Backups.TypeOfStorage;
using Backups.TypesOfFileManager;
using NUnit.Framework;

namespace Backups.Tests
{
    [TestFixture]
    public class BackupsTests
    {
        private BackupJob _backupJob;

        [SetUp]
        public void SetUp()
        {
            _backupJob = new BackupJob(new SplitStorage(), "D:\\test", new VirtualFileManager());
            _backupJob.AddObject("D:\\test\\File1.txt");
            _backupJob.AddObject("D:\\test\\File2.txt"); 
            _backupJob.Save();
        }

        [Test]
        public void Save_RestorePointsHasCreated()
        {
            Assert.AreEqual(_backupJob.GetRestorePoints().Count, 1);
            Assert.AreEqual(_backupJob.GetRestorePoints()[0].GetPathsToZips().Count, 2);
        }

        [Test]
        public void DeleteFile_FileHasDeleted()
        {
            _backupJob.DeleteObject("D:\\test\\File2.txt");
            _backupJob.Save();
            Assert.AreEqual(_backupJob.GetRestorePoints().Count, 2);
            Assert.AreEqual(_backupJob.GetRestorePoints()[0].GetPathsToZips().Count, 2);
            Assert.AreEqual(_backupJob.GetRestorePoints()[1].GetPathsToZips().Count, 1);
        }
    }
}