using System;
using System.Collections.Generic;
using Backups.TypeOfStorage;
using BackupsExtra.Cleaning;
using BackupsExtra.ExtraFileManager;
using BackupsExtra.Service;
using BackupsExtra.Tools;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    [TestFixture]
    public class BackupsExtraTests
    {
        private BackupsExtraService _backups;

        [SetUp]
        public void Setup()
        {
            _backups = new BackupsExtraService(new SplitStorage(), "D:\\test", new VirtualExtraFileManager(),
                new CleaningByNumberOfRestorePoints(1, new Clearing()), true);
            _backups.AddObject("D:\\test\\File1.txt");
            _backups.AddObject("D:\\test\\File2.txt");
            _backups.Save();
        }

        [Test]
        public void CleaningByNumberOfRestorePoints_PointsAreDeleted()
        {
            _backups.Save();
            _backups.Cleaning();
            Assert.AreEqual(_backups.GetRestorePoints().Count, 1);
        }

        [Test]
        public void CleaningByDate_PointsAreDeleted()
        {
            _backups = new BackupsExtraService(new SplitStorage(), "D:\\test", new VirtualExtraFileManager(),
                new CleaningByDate(new DateTime(2017, 7, 20, 18, 30, 25), new Clearing()), true);
            _backups.AddObject("D:\\test\\File1.txt");
            _backups.AddObject("D:\\test\\File2.txt");
            _backups.Save();
            _backups.GetRestorePoints()[0].SetTime(new DateTime(2015, 7, 20, 18, 30, 25));
            _backups.Save();
            _backups.Cleaning();
            Assert.AreEqual(_backups.GetRestorePoints().Count, 1);
        }

        [Test]
        public void HybridCleaning_PointsAreDeleted()
        {
            _backups = new BackupsExtraService(new SplitStorage(), "D:\\test", new VirtualExtraFileManager(),
                new HybridCleaning(new List<IAlgorithmForCleaning>(){new CleaningByNumberOfRestorePoints(1, new Clearing()),
                    new CleaningByDate( new DateTime(2017, 7, 20, 18, 30, 25), 
                    new Clearing())}, true, new Clearing()), true);
            _backups.AddObject("D:\\test\\File1.txt");
            _backups.AddObject("D:\\test\\File2.txt");
            _backups.Save();
            _backups.GetRestorePoints()[0].SetTime(new DateTime(2015, 7, 20, 18, 30, 25));
            _backups.Save();
            _backups.Cleaning();
            _backups.Cleaning();
            Assert.AreEqual(_backups.GetRestorePoints().Count, 1);
        }

        [Test]
        public void CleaningWithMerge_PointsAreMerged()
        {
            _backups = new BackupsExtraService(new SplitStorage(), "D:\\test", new VirtualExtraFileManager(),
                new CleaningByNumberOfRestorePoints(1, new Merge()), true);
            _backups.AddObject("D:\\test\\File1.txt");
            _backups.AddObject("D:\\test\\File2.txt");
            _backups.Save();
            _backups.DeleteObject("D:\\test\\File2.txt");
            _backups.Save();
            _backups.Cleaning();
            Assert.AreEqual(_backups.GetRestorePoints()[0].GetPathsToZips().Count, 2);
        }

        [Test]
        public void TryToDeleteAllPoints()
        {
            _backups = new BackupsExtraService(new SplitStorage(), "D:\\test", new VirtualExtraFileManager(),
                new CleaningByDate(new DateTime(2022, 7, 20, 18, 30, 25), new Clearing()), true);
            _backups.AddObject("D:\\test\\File1.txt");
            _backups.AddObject("D:\\test\\File2.txt");
            _backups.Save();
            _backups.Save();
            Assert.Catch<BackupsExtraException>(() =>
            {
                _backups.Cleaning();
            });
        }

        [Test]
        public void DeleteFile_Recovery()
        {
            _backups.DeleteObject("D:\\test\\File2.txt");
            _backups.FileRecovery(_backups.GetRestorePoints()[0].GetTime(), "");
            Assert.AreEqual(_backups.GetJobObjects().Count, 2);
        }
    }
}