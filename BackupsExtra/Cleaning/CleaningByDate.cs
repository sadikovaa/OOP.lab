using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Cleaning
{
    public class CleaningByDate : IAlgorithmForCleaning
    {
        private DateTime _date;
        private IStrategy _strategy;

        public CleaningByDate(DateTime date, IStrategy strategy)
        {
            _date = date;
            _strategy = strategy;
        }

        public void SetDateTime(DateTime date)
        {
            _date = date;
        }

        public BackupJob Clearing(BackupJob backup)
        {
            List<RestorePoint> lastPoints = backup.GetRestorePoints();
            var oldPoints = lastPoints.TakeWhile(x => x.GetTime() <= _date).ToList();
            var newPoints = lastPoints.SkipWhile(x => x.GetTime() <= _date).ToList();

            if (oldPoints.Count() == lastPoints.Count())
            {
                throw new BackupsExtraException("You can not delete all points!");
            }

            backup.SetRestorePoints(_strategy.Update(newPoints, oldPoints));
            return backup;
        }

        public bool IsOutOfLimit(List<RestorePoint> restorePoints, int indexOfPoint)
        {
            return restorePoints[indexOfPoint].GetTime() < _date;
        }
    }
}