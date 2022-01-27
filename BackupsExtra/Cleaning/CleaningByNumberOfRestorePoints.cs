using System.Collections.Generic;
using System.Linq;
using Backups.Services;
using BackupsExtra.Tools;

namespace BackupsExtra.Cleaning
{
    public class CleaningByNumberOfRestorePoints : IAlgorithmForCleaning
    {
        private int _count;
        private IStrategy _strategy;

        public CleaningByNumberOfRestorePoints(int count, IStrategy strategy)
        {
            _count = count;
            _strategy = strategy;
        }

        public BackupJob Clearing(BackupJob backup)
        {
            List<RestorePoint> lastPoints = backup.GetRestorePoints();
            var oldPoints = lastPoints.Take(lastPoints.Count() - _count).ToList();
            var newPoints = lastPoints.Skip(lastPoints.Count() - _count).ToList();

            if (oldPoints.Count() == lastPoints.Count())
            {
                throw new BackupsExtraException("You can not delete all points!");
            }

            backup.SetRestorePoints(_strategy.Update(newPoints, oldPoints));

            return backup;
        }

        public bool IsOutOfLimit(List<RestorePoint> restorePoints, int indexOfPoint)
        {
            return indexOfPoint < restorePoints.Count - _count;
        }
    }
}