namespace Banks.Tools
{
    public static class BankIdGenerator
    {
        private static int _currentId = 0;

        public static int GetId()
        {
            return _currentId++;
        }
    }
}