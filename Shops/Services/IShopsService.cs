namespace Shops.Services
{
    public interface IShopsService
    {
            Shop RegisterShop(Shop shop);
            Shop SearchShop(int shopId);
            ProductForSale DeliverProducts(int shopIdentifier, Product product, int count, int price);
            void SellProduct(int shopIdentifier, Person person, Product product);
            void SellBatchProducts(int shopIdentifier, Person person, Product product, int number);
            void ChangePrice(int shopIdentifier, Product product, int price);
            Shop SearchTheCheapestBatch(Product product, int count);
        }
}