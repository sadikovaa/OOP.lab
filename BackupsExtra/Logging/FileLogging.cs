using System.IO;

namespace BackupsExtra.Logging
{
    public class FileLogging : ITypeOfLogging
    {
        private string _pathOfLog;
        public FileLogging(string path)
        {
            File.Create(path);
            _pathOfLog = path;
        }

        public void RecordReport(string report, bool isNeedTimeCode)
        {
            File.AppendAllText(_pathOfLog, Logger.CreateMessage(report, isNeedTimeCode));
        }
    }
}