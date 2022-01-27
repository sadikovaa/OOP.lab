using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Tools;
using Banks.TypesOfAccount;

namespace Banks.Service
{
    public class Client
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _phoneNumber;
        private string _address = null;
        private string _passportData = null;
        private bool _isSubscribedToChangeNotifications = false;
        private List<IBankAccount> _accounts;

        private Client(string name, string surname, string phone = "", string address = "", string passportData = "")
        {
            _name = name;
            _surname = surname;
            _phoneNumber = phone;
            _address = address;
            _passportData = passportData;
            _accounts = new List<IBankAccount>();
            _id = ClientIdGenerator.GetId();
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetSurname()
        {
            return _surname;
        }

        public string GetPhoneNumber()
        {
            return _phoneNumber;
        }

        public string GetAddress()
        {
            return _address;
        }

        public List<IBankAccount> GetAccounts()
        {
            return _accounts;
        }

        public bool IsSubscribedOnNotification()
        {
            return _isSubscribedToChangeNotifications;
        }

        public IBankAccount FindAccount(int id)
        {
            foreach (var account in _accounts)
            {
                if (account.GetId() == id)
                {
                    return account;
                }
            }

            return null;
        }

        public void SubscribeToNotofocation()
        {
            _isSubscribedToChangeNotifications = true;
        }

        public void UnsubscribeToNotofocation()
        {
            _isSubscribedToChangeNotifications = false;
        }

        public bool IsConfirmed()
        {
            return _address != null && _passportData != null;
        }

        public IBankAccount CreateBankAccount(IBankAccount account)
        {
            _accounts.Add(account);
            return _accounts.Last();
        }

        public class ClientBuilder
        {
            private string _name;
            private string _surname;
            private string _phoneNumber;
            private string _address = null;
            private string _passportData = null;

            public ClientBuilder NameSurname(string name, string surname)
            {
                _name = name;
                _surname = surname;
                return this;
            }

            public ClientBuilder PhoneNumber(string number)
            {
                _phoneNumber = number;
                return this;
            }

            public ClientBuilder Address(string address)
            {
                _address = address;
                return this;
            }

            public ClientBuilder PassportDate(string passportDate)
            {
                if (passportDate.Length != 10 || int.TryParse(passportDate, out int num))
                {
                    throw new BanksException("Passport Date is not correct!");
                }

                _passportData = passportDate;
                return this;
            }

            public Client GetClient()
            {
                return new Client(_name, _surname, _phoneNumber, _address, _passportData);
            }
        }
    }
}