using System.Collections.Generic;
using Banks.Communication;
using Banks.Conditions;
using Banks.Service;
using Banks.TypesOfAccount;
using NUnit.Framework;

namespace Banks.Tests
{
    [TestFixture]
    public class BanksTest
    {
        private CentralBankService _centralBank;

        [SetUp]
        public void Setup()
        {
            _centralBank = CentralBankService.GetCentralBankInstance();
            _centralBank.AddBank(new Bank("Sber",
                new BanksConditions(new CreditCondition(-1000, 10),
                    new DepositCondition(new List<ConditionForPercent>() {new ConditionForPercent(100, 3)}), 1000, 100),
                new VirtualCommunication()));
        }

        [Test]

        public void AddBank_BackHasAdded()
        {
            _centralBank.AddBank(new Bank("noSber",
                new BanksConditions(new CreditCondition(-1000, 10),
                    new DepositCondition(new List<ConditionForPercent>() {new ConditionForPercent(100, 3)}), 1000, 100),
                new VirtualCommunication()));
            Assert.AreEqual(_centralBank.GetBanks().Count, 2);
        }
        
        [Test]

        public void CreateClient_ClientHasAdded()
        {
            Client newClient = new Client.ClientBuilder().NameSurname("Alsu", "Sadikova").PhoneNumber("89117078336").GetClient();
            _centralBank.GetBanks()[0].RegistrationOfClient(newClient);
            Assert.AreNotEqual(_centralBank.GetBanks()[0].FindClient(newClient.GetId()), null);
        }

        [Test]

        public void CreateAccount_AccountHasCreated()
        {
            Client newClient = new Client.ClientBuilder().NameSurname("Alsu", "Sadikova").PhoneNumber("89117078336").GetClient();
            _centralBank.GetBanks()[0].RegistrationOfClient(newClient);
            _centralBank.GetBanks()[0].CreateDebitAccount(newClient.GetId(), 100);
            Assert.AreEqual(_centralBank.GetBanks()[0].GetAccounts().Count, 1);
        }

        [Test]

        public void TransferMoney()
        {
            Client newClient = new Client.ClientBuilder().NameSurname("Alsu", "Sadikova").PhoneNumber("89117078336").GetClient();
            _centralBank.GetBanks()[0].RegistrationOfClient(newClient);
            IBankAccount account1 = _centralBank.GetBanks()[0].CreateDebitAccount(newClient.GetId(), 100);
            IBankAccount account2 = _centralBank.GetBanks()[0].CreateCreditAccount(newClient.GetId(), 0);
            _centralBank.GetBanks()[0].TransferBetweenAccount(account1.GetId(), account2.GetId(), 100);
            Assert.AreEqual(account1.GetBalance(), 0);
            Assert.AreEqual(account2.GetBalance(), 100);
        }
    }
}