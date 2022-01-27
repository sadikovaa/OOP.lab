using System.Collections.Generic;
using Backups.Services;
using Backups.TypesOfFileManager;

namespace Backups.TypeOfStorage
{
    public interface IStorage
    {
        RestorePoint Save(List<string> objects, string directory, int count, IFileManager fileSystem);
    }
}