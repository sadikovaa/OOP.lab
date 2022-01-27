namespace Server.Service.Tasks
{
    public static class TaskIdGenerator
    {
        private static int currentId = 0;

        public static int GetId()
        {
            return currentId++;
        }
    }
}