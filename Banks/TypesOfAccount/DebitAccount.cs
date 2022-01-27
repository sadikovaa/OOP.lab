using Banks.Conditions;
using Banks.Service;
using Banks.Tools;

namespace Banks.TypesOfAccount
{
    public class DebitAccount : IBankAccount
    {
        private int _id;
        private Client _client;
        private double _balance;

        public DebitAccount(Client client, double initialSum)
        {
            _id = AccountIdGenerator.GetId();
            _client = client;
            _balance = initialSum;
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
            if (sum > _balance)
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
            // this function has empty body,
            // because a debit account does not involve commissions
        }

        public void MonthCommissionDeduction()
        {
            // this function has empty body,
            // because a debit account does not involve commissions
        }

        public void ChangeConditions(BanksConditions newConditions)
        {
            // this function has empty body,
            // because a debit account does not involve any conditions
        }
    }
}