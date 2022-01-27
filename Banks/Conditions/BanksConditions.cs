namespace Banks.Conditions
{
    public class BanksConditions
    {
        private CreditCondition _creditCondition;
        private DepositCondition _depositCondition;
        private double _transferLimit;
        private double _limitForUnconfirmedClients;

        public BanksConditions(CreditCondition creditCondition, DepositCondition depositCondition, double transferLimit, double limitForUnconfirmedClients)
        {
            _creditCondition = creditCondition;
            _depositCondition = depositCondition;
            _transferLimit = transferLimit;
            _limitForUnconfirmedClients = limitForUnconfirmedClients;
        }

        public CreditCondition GetCreditCondition()
        {
            return _creditCondition;
        }

        public DepositCondition GetDepositCondition()
        {
            return _depositCondition;
        }

        public double GetTransferLimit()
        {
            return _transferLimit;
        }

        public double GetLimitForUnconfirmedClients()
        {
            return _limitForUnconfirmedClients;
        }

        public void SetCreditCondition(CreditCondition newConditions)
        {
            _creditCondition = newConditions;
        }

        public void SetDepositCondition(DepositCondition newConditions)
        {
            _depositCondition = newConditions;
        }

        public void SetTransferLimit(double newLimit)
        {
            _transferLimit = newLimit;
        }

        public void SetLimitForUnconfirmedClients(double newLimit)
        {
            _limitForUnconfirmedClients = newLimit;
        }
    }
}