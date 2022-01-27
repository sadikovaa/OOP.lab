namespace Backups.Tools
{
    public static class FileInfo
    {
        public static string GetNameOfFile(string directory)
        {
            int index = directory.LastIndexOf('\\');
            if (index < 0 || index == directory.Length - 1)
            {
                throw new BackupsException("Directory name is not correct!");
            }

            return directory.Substring(index + 1);
        }
    }
}