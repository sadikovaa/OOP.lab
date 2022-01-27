using Shops.Tools;

namespace Shops.Services
{
    public class ProductForSale
    {
        private Product _product;
        private int _count;
        private int _price;

        public ProductForSale(Product product, int count, int price)
        {
            _product = product;
            _count = count;
            _price = price;
        }

        public string GetName()
        {
            return _product.GetName();
        }

        public int GetId()
        {
            return _product.GetId();
        }

        public int GetCount()
        {
            return _count;
        }

        public int GetPrice()
        {
            return _price;
        }

        public void ChangePrice(int newPrice)
        {
            _price = newPrice;
        }

        public void AddCount(int count)
        {
            _count += count;
            if (_count < 0)
            {
                throw new ShopsException("Products can't be below zero");
            }
        }
    }
}