using Banks.Conditions;
using Banks.Service;
using Banks.Tools;

namespace Banks.TypesOfAccount
{
    public class CreditAccount : IBankAccount
    {
        private int _id;
        private Client _client;
        private double _balance;
        private CreditCondition _conditions;
        private double _commisionSum;
        public CreditAccount(Client client, double initialSum, CreditCondition conditions)
        {
            _id = AccountIdGenerator.GetId();
            _client = client;
            _balance = initialSum;
            _conditions = conditions;
            _commisionSum = 0;
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

        public bool Withdraw(double sum)
        {
            if ((_balance - sum) > _conditions.GetCreditLimit())
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
            if (_balance < 0)
            {
                _commisionSum += _conditions.GetCommission();
            }
        }

        public void MonthCommissionDeduction()
        {
            DayCommissionDeduction();
            _balance += _commisionSum;
            _commisionSum = 0;
        }

        public void ChangeConditions(BanksConditions newConditions)
        {
            _conditions = newConditions.GetCreditCondition();
        }

        private void MakeHistoryOfCommision()
        {
        }
    }
}