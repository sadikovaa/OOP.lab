namespace IsuExtra.Services
{
    public static class StreamIdGenerator
    {
        private static int currentId = 0;

        public static int GetId()
        {
            return currentId++;
        }
    }
}