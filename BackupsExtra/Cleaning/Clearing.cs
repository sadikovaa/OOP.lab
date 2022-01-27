using System.Collections.Generic;
using Backups.Services;

namespace BackupsExtra.Cleaning
{
    public class Clearing : IStrategy
    {
        public List<RestorePoint> Update(List<RestorePoint> newPoints, List<RestorePoint> lastPoints)
        {
            return newPoints;
        }
    }
}