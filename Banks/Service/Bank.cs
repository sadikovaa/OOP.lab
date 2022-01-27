using System;
using System.Collections.Generic;
using Banks.Communication;
using Banks.Conditions;
using Banks.Tools;
using Banks.TypesOfAccount;

namespace Banks.Service
{
    public class Bank
    {
        private string _name;
        private int _id;
        private List<Client> _clients;
        private List<IBankAccount> _accounts;
        private BanksConditions _conditions;
        private List<AccountHistory> _accountsHistories;
        private ICommunication _typeOfCommunicationWithClient;

        public Bank(string name, BanksConditions conditions, ICommunication type)
        {
            _name = name;
            _id = BankIdGenerator.GetId();
            _conditions = conditions;
            _clients = new List<Client>();
            _accounts = new List<IBankAccount>();
            _accountsHistories = new List<AccountHistory>();
            _typeOfCommunicationWithClient = type;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public List<IBankAccount> GetAccounts()
        {
            return _accounts;
        }

        public Client FindClient(int clientId)
        {
            foreach (var client in _clients)
            {
                if (client.GetId() == clientId)
                {
                    return client;
                }
            }

            return null;
        }

        public Client GetClient(int clientId)
        {
            Client client = FindClient(clientId);
            if (client == null)
            {
                throw new BanksException("The client does not exist!");
            }

            return client;
        }

        public void ChangeTransactionLimit(int newLimit)
        {
            _conditions.SetTransferLimit(newLimit);
            foreach (var client in _clients)
            {
                if (client.IsSubscribedOnNotification())
                {
                    _typeOfCommunicationWithClient.Message(client, Constants.MessageAboutChangingLimits);
                }
            }
        }

        public void ChangeConditions(BanksConditions newConditions)
        {
            _conditions = newConditions;
            foreach (var client in _clients)
            {
                if (client.IsSubscribedOnNotification())
                {
                    _typeOfCommunicationWithClient.Message(client, Constants.MessageAboutChangingAccountConditions);
                }
            }

            foreach (var account in _accounts)
            {
                account.ChangeConditions(newConditions);
            }
        }

        public Client RegistrationOfClient(Client client)
        {
            _clients.Add(client);
            return client;
        }

        public DebitAccount CreateDebitAccount(int clientId, double initialSum)
        {
            Client client = GetClient(clientId);
            DebitAccount newAccount = new DebitAccount(client, initialSum);
            AddNewAccountInService(client, newAccount);
            return newAccount;
        }

        public DepositAccount CreateDepositAccount(int clientId, double initialSum, DateTime expiration)
        {
            Client client = GetClient(clientId);
            DepositAccount newAccount = new DepositAccount(client, initialSum, expiration, _conditions.GetDepositCondition());
            AddNewAccountInService(client, newAccount);
            return newAccount;
        }

        public CreditAccount CreateCreditAccount(int clientId, double initialSum)
        {
            Client client = GetClient(clientId);
            CreditAccount newAccount = new CreditAccount(client, initialSum, _conditions.GetCreditCondition());
            AddNewAccountInService(client, newAccount);
            return newAccount;
        }

        public bool RefillAccount(int accountId, double sum)
        {
            IBankAccount account = GetAccount(accountId);
            if (account.Refill(sum))
            {
                AddNewOperation(Constants.ATM, account, sum);
                return true;
            }

            return false;
        }

        public bool WithdrawalAccount(int accountId, double sum)
        {
            IBankAccount account = GetAccount(accountId);
            if (IsOperationApproved(account.GetClient(), sum) && account.Withdraw(sum))
            {
                AddNewOperation(account, Constants.ATM, sum);
                return true;
            }

            return false;
        }

        public bool TransferBetweenAccount(int senderId, int recipientId, double sum)
        {
            IBankAccount sender = GetAccount(senderId);
            IBankAccount recipient = GetAccount(recipientId);
            if (IsOperationApproved(sender.GetClient(), sum) && sender.Withdraw(sum) && recipient.Refill(sum))
            {
                AddNewOperation(sender, recipient, sum);
                return true;
            }

            return false;
        }

        public bool CancellationOfLastTransaction(int accountId)
        {
            Operation lastOperation = GetAccountHistory(GetAccount(accountId).GetId()).GetLastOperation();
            if (lastOperation.GetSender().Refill(lastOperation.GetSumOfOperation()) &&
                lastOperation.GetRecipient().Withdraw(lastOperation.GetSumOfOperation()))
            {
                GetAccountHistory(GetAccount(accountId).GetId()).DeleteLastOperation();
                return true;
            }

            return false;
        }

        public void DayCommissionDeduction()
        {
            foreach (var account in _accounts)
            {
                account.DayCommissionDeduction();
            }
        }

        public void MonthCommissionDeduction()
        {
            foreach (var account in _accounts)
            {
                account.MonthCommissionDeduction();
            }
        }

        private void AddNewAccountInService(Client client, IBankAccount newAccount)
        {
            client.CreateBankAccount(newAccount);
            _accounts.Add(newAccount);
            _accountsHistories.Add(new AccountHistory(newAccount.GetId()));
        }

        private IBankAccount GetAccount(int accountId)
        {
            var result = _accounts.Find(account => account.GetId() == accountId);
            if (result == null)
                throw new BanksException("No such client");
            return result;
        }

        private AccountHistory GetAccountHistory(int accountId)
        {
            foreach (var history in _accountsHistories)
            {
                if (history.GetId() == accountId)
                {
                    return history;
                }
            }

            throw new Exception();
        }

        private void AddNewOperation(IBankAccount sender, IBankAccount recipient, double sum)
        {
            Operation newOperation = new Operation(sender, recipient, sum);
            AccountHistory history = GetAccountHistory(recipient.GetId());
            history.AddOperation(newOperation);
        }

        private bool IsOperationApproved(Client sender, double sum)
        {
            if (sum > _conditions.GetTransferLimit())
            {
                return false;
            }

            if (!sender.IsConfirmed() && sum > _conditions.GetLimitForUnconfirmedClients())
            {
                return false;
            }

            return true;
        }
    }
}