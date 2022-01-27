using System;
using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopService : IShopsService
    {
        private List<Shop> _shops;

        public ShopService()
        {
            _shops = new List<Shop>();
        }

        public Shop RegisterShop(Shop shop)
        {
            foreach (var currShop in _shops)
            {
                if (currShop.GetId() == shop.GetId())
                {
                    throw new ShopsException("The store is already registered!");
                }
            }

            _shops.Add(shop);
            Shop newShop = _shops[_shops.Count - 1];
            return newShop;
        }

        public Shop SearchShop(int shopId)
        {
            foreach (var i in _shops)
            {
                if (i.GetId() == shopId)
                {
                    return i;
                }
            }

            throw new ShopsException("The shop does not exist!");
        }

        public ProductForSale DeliverProducts(int shopId, Product product, int count, int price)
        {
            return SearchShop(shopId).DeliverProducts(product, count, price);
        }

        public void SellProduct(int shopId, Person person, Product product)
        {
            SearchShop(shopId).SellProduct(person, product);
        }

        public void SellBatchProducts(int shopId, Person person, Product product, int number)
        {
            SearchShop(shopId).SellBatchProducts(person, product, number);
        }

        public void ChangePrice(int shopId, Product product, int price)
        {
            SearchShop(shopId).ChangePrice(product, price);
        }

        public Shop SearchTheCheapestBatch(Product product, int count)
        {
            int min_price = int.MaxValue;
            Shop shop = null;
            foreach (var i in _shops)
            {
                ProductForSale curr = i.SearchTheProduct(product);
                if (curr != null)
                {
                    if (curr.GetPrice() < min_price && curr.GetCount() >= count)
                    {
                        min_price = curr.GetPrice();
                        shop = i;
                    }
                }
            }

            if (shop != null)
            {
                return shop;
            }

            throw new ShopsException("Not enough products in any shops!");
        }
    }
}