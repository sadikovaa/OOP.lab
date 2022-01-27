namespace Server.Service.Reports
{
    public class ReportIdGenerator
    {
        private static int currentId = 0;

        public static int GetId()
        {
            return currentId++;
        }
    }
}