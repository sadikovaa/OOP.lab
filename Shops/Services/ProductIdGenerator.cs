namespace Shops.Services
{
    public static class ProductIdGenerator
    {
        private static int currentId = 0;

        public static int GetId()
        {
            return currentId++;
        }
    }
}