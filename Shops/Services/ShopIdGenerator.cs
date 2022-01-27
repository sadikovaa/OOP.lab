namespace Shops.Services
{
    public static class ShopIdGenerator
    {
        private static int currentIden = 0;

        public static int GetIdentifier()
        {
            return currentIden++;
        }
    }
}