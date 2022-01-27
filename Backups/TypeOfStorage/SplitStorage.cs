using System;
using System.Collections.Generic;
using Backups.Services;
using Backups.TypesOfFileManager;
using FileInfo = Backups.Tools.FileInfo;

namespace Backups.TypeOfStorage
{
    public class SplitStorage : IStorage
    {
        public RestorePoint Save(List<string> jobObjects, string directory, int count, IFileManager fileSystem)
        {
            List<string> storages = new List<string>();
            foreach (string jobObject in jobObjects)
            {
                string currJob = directory + '\\' + FileInfo.GetNameOfFile(jobObject) + '_' + count + ".zip";
                fileSystem?.CopyAsZip(new List<string>(jobObjects), directory, currJob);
                storages.Add(currJob);
            }

            return new RestorePoint(DateTime.Now, storages);
        }
    }
}