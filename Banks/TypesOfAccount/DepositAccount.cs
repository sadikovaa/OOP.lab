using System;
using Banks.Conditions;
using Banks.Service;
using Banks.Tools;

namespace Banks.TypesOfAccount
{
    public class DepositAccount : IBankAccount
    {
        private int _id;
        private Client _client;
        private double _balance;
        private DateTime _expiration;
        private double _percentOnDeposit;
        private double _percentSum;
        public DepositAccount(Client client, double initialSum, DateTime expiration, DepositCondition condition)
        {
            _id = AccountIdGenerator.GetId();
            _client = client;
            _balance = initialSum;
            _expiration = expiration;
            _percentOnDeposit = condition.GetPercent(initialSum);
            _percentSum = 0;
        }

        public int GetId()
        {
            return _id;
        }

        public Client GetClient()
        {
            return _client;
        }

        public double GetBalance()
        {
            return _balance;
        }

        public DateTime GetExpiration()
        {
            return _expiration;
        }

        public bool Withdraw(double sum)
        {
            if (_expiration > DateTime.Now || sum > _balance)
            {
                return false;
            }

            _balance -= sum;
            return true;
        }

        public bool Refill(double sum)
        {
            _balance += sum;
            return true;
        }

        public void DayCommissionDeduction()
        {
            _percentSum += (_balance * _percentOnDeposit) / (100 * Constants.DaysInYear);
        }

        public void MonthCommissionDeduction()
        {
            DayCommissionDeduction();
            _balance += _percentSum;
            _percentSum = 0;
        }

        public void ChangeConditions(BanksConditions newConditions)
        {
            _percentOnDeposit = newConditions.GetDepositCondition().GetPercent(_balance);
        }
    }
}