using System.Collections.Generic;
using Backups.Services;

namespace BackupsExtra.Cleaning
{
    public interface IStrategy
    {
        List<RestorePoint> Update(List<RestorePoint> newPoints, List<RestorePoint> lastPoints);
    }
}