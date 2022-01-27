namespace Banks.Conditions
{
    public class CreditCondition
    {
        private double _creditLimit;
        private double _commission;

        public CreditCondition(double creditLimit, double commission)
        {
            _creditLimit = creditLimit;
            _commission = commission;
        }

        public double GetCreditLimit()
        {
            return _creditLimit;
        }

        public double GetCommission()
        {
            return _commission;
        }
    }
}