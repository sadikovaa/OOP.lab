namespace Server.Service.Staff
{
    public class StaffIdGenerator
    {
        private static int currentId = 0;

        public static int GetId()
        {
            return currentId++;
        }
    }
}