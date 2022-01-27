namespace Shops.Services
{
    public class Person
    {
        private string _name;
        private int _money;

        public Person(string name, int money)
        {
            _name = name;
            _money = money;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetMoney()
        {
            return _money;
        }

        public void SpendMoney(int money)
        {
            _money -= money;
        }
    }
}