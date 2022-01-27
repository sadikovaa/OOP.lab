using System;
using System.Collections.Generic;
using Backups.Services;
using Backups.TypesOfFileManager;

namespace Backups.TypeOfStorage
{
    public class SingleStorage : IStorage
    {
        public RestorePoint Save(List<string> jobObjects, string directory, int count, IFileManager fileSystem)
        {
            string fileName = directory + '\\' + "Saved_" + count + ".zip";
            fileSystem?.CopyAsZip(jobObjects, directory, fileName);
            return new RestorePoint(DateTime.Now, fileName);
        }
    }
}