using System.Collections.Generic;
namespace Backups.TypesOfFileManager
{
    public class VirtualFileManager : IFileManager
    {
        public void CreateDirectory(string directory)
        {
            // this function has empty body,
            // because this implementation of IFileManager doesn't work with computer's File System.
        }

        public void CopyAsZip(List<string> files, string directory, string name)
        {
            // this function has empty body,
            // because this implementation of IFileManager doesn't work with computer's File System.
        }
    }
}