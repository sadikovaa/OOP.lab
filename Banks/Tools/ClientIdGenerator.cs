namespace Banks.Tools
{
    public static class ClientIdGenerator
    {
        private static int _currentId = 0;

        public static int GetId()
        {
            return _currentId++;
        }
    }
}