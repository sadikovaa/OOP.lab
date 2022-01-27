using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Services
{
    public class Shop
    {
        private int _id;
        private string _name;
        private string _address;
        private List<ProductForSale> _products;
        private int _money;

        public Shop(string name, string address)
        {
            _id = ShopIdGenerator.GetIdentifier();
            _name = name;
            _address = address;
            _products = new List<ProductForSale>();
            _money = 0;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GerAddress()
        {
            return _address;
        }

        public ProductForSale DeliverProducts(Product product, int count, int price)
        {
            ProductForSale product1 = SearchTheProduct(product);
            if (product1 == null)
            {
                _products.Add(new ProductForSale(product, count, price));
            }
            else
            {
                product1.AddCount(count);
                product1.ChangePrice(price);
            }

            return product1;
        }

        public void SellProduct(Person person, Product product)
        {
            SellBatchProducts(person, product, 1);
        }

        public void SellBatchProducts(Person person, Product product, int number)
        {
            ProductForSale currProduct = SearchTheProduct(product);
            if (person.GetMoney() < currProduct.GetPrice() * number)
            {
                throw new ShopsException("Not enough money");
            }

            if (currProduct.GetCount() < number)
            {
                throw new ShopsException("Not enough products");
            }

            _money += currProduct.GetPrice() * number;
            person.SpendMoney(currProduct.GetPrice() * number);
            currProduct.AddCount(-number);
        }

        public void ChangePrice(Product product, int price)
        {
            ProductForSale product1 = SearchTheProduct(product);
            if (product1 == null)
            {
                throw new ShopsException("There is no such product in the shop");
            }

            product1.ChangePrice(price);
        }

        public ProductForSale SearchTheProduct(Product product)
        {
            foreach (var i in _products)
            {
                if (i.GetId() == product.GetId())
                {
                    return i;
                }
            }

            return null;
        }
    }
}