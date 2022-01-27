namespace Banks.Tools
{
    public class OperationIdGenerator
    {
        private static long _currentId = 0;

        public static long GetId()
        {
            return _currentId++;
        }
    }
}