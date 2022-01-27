using NUnit.Framework;
using Shops.Services;
using Shops.Tools;

namespace Shops.Tests
{
    public class Tests
    {
        private IShopsService _shopsService;
        
        [SetUp]
        public void Setup()
        {
            _shopsService = new ShopService();
        }

        [Test]
        public void RegisterShop_AddProductsForSale()
        {
            Shop currShop = _shopsService.RegisterShop(new Shop("ClownTown", "Kolomyazhskaya, 15"));
            Product currProduct = new Product("RedNose");
            _shopsService.DeliverProducts(currShop.GetId(),currProduct, 2, 300);
            ProductForSale currProductForSale = _shopsService.SearchShop(currShop.GetId()).SearchTheProduct(currProduct);
            Assert.AreEqual(currProductForSale.GetPrice(), 300);
            Assert.AreEqual(currProductForSale.GetCount(), 2);
        }

        [Test]
        public void ChangePrice_PriceHasChanged()
        {
            Shop currShop = _shopsService.RegisterShop(new Shop("ClownTown", "Kolomyazhskaya, 15"));
            Product currProduct = new Product("RedNose");
            _shopsService.DeliverProducts(currShop.GetId(),currProduct, 2, 300);
            _shopsService.ChangePrice(currProduct.GetId(), currProduct, 600);
            ProductForSale currProductForSale = _shopsService.SearchShop(currShop.GetId()).SearchTheProduct(currProduct);
            Assert.AreEqual(currProductForSale.GetPrice(), 600);
            Assert.AreEqual(currProductForSale.GetCount(), 2);
        }
        
        [Test]
        public void SearchTheCheapestBatch_NotEnoughProducts()
        {
            Product currProduct = new Product("RedNose");
            Shop currShop = _shopsService.RegisterShop(new Shop("ClownTown", "Kolomyazhskaya, 15"));
            _shopsService.DeliverProducts(currShop.GetId(),currProduct, 2, 300);
            Shop newShop = _shopsService.RegisterShop(new Shop("CircusForEveryone", "Lanskoe Shosse, 17"));
            _shopsService.DeliverProducts(newShop.GetId(),currProduct, 2, 5000);
            Assert.Catch<ShopsException>(() =>
            {
                _shopsService.SearchTheCheapestBatch(currProduct, 3);
            });
        }
        
        [Test]
        public void SellBatchProducts_NotEnoughProducts()
        {
            Product currProduct = new Product("RedNose");
            Shop currShop = _shopsService.RegisterShop(new Shop("ClownTown", "Kolomyazhskaya, 15"));
            _shopsService.DeliverProducts(currShop.GetId(),currProduct, 2, 300);
            Person customer = new Person("DKholopov", 999);
            Assert.Catch<ShopsException>(() =>
            {
                _shopsService.SellBatchProducts(currShop.GetId(), customer, currProduct, 3);
            });
        }
        
        [Test]
        public void SellBatchProducts_NotEnoughMoney()
        {
            Product currProduct = new Product("RedNose");
            Shop currShop = _shopsService.RegisterShop(new Shop("ClownTown", "Kolomyazhskaya, 15"));
            _shopsService.DeliverProducts(currShop.GetId(),currProduct, 2, 300);
            Person customer = new Person("DKholopov", 228);
            Assert.Catch<ShopsException>(() =>
            {
                _shopsService.SellBatchProducts(currShop.GetId(), customer, currProduct, 2);
            });
            
        }
    }
}