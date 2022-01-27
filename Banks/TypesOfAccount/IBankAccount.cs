using Banks.Conditions;
using Banks.Service;

namespace Banks.TypesOfAccount
{
    public interface IBankAccount
    {
        int GetId();
        Client GetClient();
        double GetBalance();
        bool Withdraw(double sum);
        bool Refill(double sum);
        void DayCommissionDeduction();
        void MonthCommissionDeduction();
        void ChangeConditions(BanksConditions newConditions);
    }
}