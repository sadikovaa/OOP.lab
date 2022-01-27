using System;

namespace BackupsExtra.Logging
{
    public class ConsoleLogging : ITypeOfLogging
    {
        public void RecordReport(string report, bool isNeedTimeCode)
        {
            Console.WriteLine(Logger.CreateMessage(report, isNeedTimeCode));
        }
    }
}