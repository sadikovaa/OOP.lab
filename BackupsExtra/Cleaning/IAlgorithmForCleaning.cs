using System.Collections.Generic;
using Backups.Services;

namespace BackupsExtra.Cleaning
{
    public interface IAlgorithmForCleaning
    {
        BackupJob Clearing(BackupJob backup);

        bool IsOutOfLimit(List<RestorePoint> restorePoints, int indexOfPoint);
    }
}