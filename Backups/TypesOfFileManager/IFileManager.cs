using System.Collections.Generic;

namespace Backups.TypesOfFileManager
{
    public interface IFileManager
    {
        void CreateDirectory(string directory);
        void CopyAsZip(List<string> files, string directory, string name);
    }
}