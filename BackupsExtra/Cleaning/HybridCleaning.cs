using System.Collections.Generic;
using System.Linq;
using Backups.Services;

namespace BackupsExtra.Cleaning
{
    public class HybridCleaning : IAlgorithmForCleaning
    {
        private List<IAlgorithmForCleaning> _algorithms;
        private bool _fitBothLimits;
        private IStrategy _strategy;

        public HybridCleaning(List<IAlgorithmForCleaning> algorithms, bool fitBothLimits, IStrategy strategy)
        {
            _algorithms = algorithms;
            _fitBothLimits = fitBothLimits;
            _strategy = strategy;
        }

        public BackupJob Clearing(BackupJob backup)
        {
            List<RestorePoint> lastPoints = backup.GetRestorePoints();
            var oldPoints = lastPoints.TakeWhile((x, index) => IsOutOfLimit(lastPoints, index)).ToList();
            var newPoints = lastPoints.SkipWhile((x, index) => IsOutOfLimit(lastPoints, index)).ToList();
            backup.SetRestorePoints(_strategy.Update(newPoints, oldPoints));
            return backup;
        }

        public bool IsOutOfLimit(List<RestorePoint> restorePoints, int indexOfPoint)
        {
            bool result;
            if (_fitBothLimits)
            {
                result = true;
                _algorithms.ForEach(x => result = result && x.IsOutOfLimit(restorePoints, indexOfPoint));
                return result;
            }

            result = false;
            _algorithms.ForEach(x => result = result || x.IsOutOfLimit(restorePoints, indexOfPoint));
            return result;
        }
    }
}