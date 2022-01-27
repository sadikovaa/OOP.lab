using System.Collections.Generic;
using Banks.Tools;

namespace Banks.Service
{
    public class CentralBankService
    {
        private static CentralBankService _centralBankInstance;
        private List<Bank> _banks;
        private CentralBankService()
        {
            _banks = new List<Bank>();
        }

        public static CentralBankService GetCentralBankInstance()
        {
            if (_centralBankInstance == null)
                _centralBankInstance = new CentralBankService();
            return _centralBankInstance;
        }

        public Bank AddBank(Bank newBank)
        {
            _banks.Add(newBank);
            return newBank;
        }

        public List<Bank> GetBanks()
        {
            return _banks;
        }

        public Bank FindBank(int id)
        {
            foreach (var bank in _banks)
            {
                if (bank.GetId() == id)
                {
                    return bank;
                }
            }

            return null;
        }

        public Bank GetBank(int id)
        {
            Bank bank = FindBank(id);
            if (bank == null)
            {
                throw new BanksException("The bank does not exist!");
            }

            return bank;
        }

        public void EndOfDay()
        {
            foreach (var bank in _banks)
            {
                bank.DayCommissionDeduction();
            }
        }

        public void EndOfMonth()
        {
            foreach (var bank in _banks)
            {
                bank.MonthCommissionDeduction();
            }
        }
    }
}