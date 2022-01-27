using System;

namespace BackupsExtra.Logging
{
    public static class Logger
    {
        public static string CreateMessage(string messages, bool isNeedTimeCode)
        {
            string prefix = string.Empty;
            if (isNeedTimeCode)
            {
                prefix = Convert.ToString(DateTime.Now);
            }

            return prefix + messages;
        }
    }
}