using Shops.Tools;

namespace Shops.Services
{
    public class Product
    {
        private int _id;
        private string _name;
        public Product(string name)
        {
            _id = ProductIdGenerator.GetId();
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetId()
        {
            return _id;
        }
    }
}