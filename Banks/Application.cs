using System;
using System.Collections.Generic;
using Banks.Service;
using Banks.TypesOfAccount;

namespace Banks
{
    public class Application
    {
        private CentralBankService _centralBank;

        public Application(CentralBankService centralBank)
        {
            _centralBank = centralBank;
        }

        public void SetUpdateCentralBank(CentralBankService centralBank)
        {
            _centralBank = centralBank;
        }

        public void Start()
        {
            Bank bank = ChooseBank();
            Client client = LoginClient(bank);
            WorkingWithAccounts(bank, client);
        }

        private Bank ChooseBank()
        {
            Console.WriteLine("Enter the ID of the bank need you or 0 - if you want to see the ID of all banks");
            while (true)
            {
                int userAnswer = Convert.ToInt32(Console.ReadLine());
                if (userAnswer == 0)
                {
                    List<Bank> banks = _centralBank.GetBanks();
                    foreach (var bank in banks)
                    {
                        Console.WriteLine(bank.GetId() + ":   " + bank.GetName());
                    }
                }
                else
                {
                    Bank currBank = _centralBank.FindBank(userAnswer);
                    if (currBank == null)
                    {
                        Console.WriteLine("Incorrect Bank ID. Try again!");
                    }
                    else
                    {
                        return currBank;
                    }
                }
            }
        }

        private Client LoginClient(Bank currBank)
        {
            Console.WriteLine("Enter you ID or 0 - if you want to register");
            while (true)
            {
                int userAnswer = Convert.ToInt32(Console.ReadLine());
                if (userAnswer == 0)
                {
                    return RegisterClient();
                }

                Client client = currBank.FindClient(userAnswer);
                if (client == null)
                {
                    Console.WriteLine("Incorrect Client ID. Try again!");
                }
                else
                {
                    return client;
                }
            }
        }

        private void WorkingWithAccounts(Bank currBank, Client currClient)
        {
            Console.WriteLine("Hello, " + currClient.GetName() + "!");
            int userAnswer = -1;
            while (userAnswer != 2)
            {
                Console.WriteLine("Enter the number of the desired operation");
                Console.WriteLine("0 - Account operations \n 1 - Subscribe or unsubcribe from notification \n 2 - exit");
                userAnswer = Convert.ToInt32(Console.ReadLine());
                switch (userAnswer)
                {
                    case 0:
                        IBankAccount account = ChooseAccount(currBank, currClient);
                        AccountOperations(currBank, currClient, account);
                        break;
                    case 1:
                        Subcribing(currClient);
                        break;
                }
            }
        }

        private void AccountOperations(Bank currBank, Client currClient, IBankAccount currAccount)
        {
            Console.WriteLine("Hello, " + currClient.GetName() + "!");
            int userAnswer = -1;
            while (userAnswer != 4)
            {
                Console.WriteLine("Enter the number of the desired operation");
                Console.WriteLine("0 - Get a balance \n 1 - withdraw money \n 2 - refill money \n 3 - transfer money \n 5 - exit");
                userAnswer = Convert.ToInt32(Console.ReadLine());
                switch (userAnswer)
                {
                    case 0:
                        Console.WriteLine("Balance:  ", currAccount.GetBalance());
                        break;
                    case 1:
                        CheckTheOperation(Withdraw(currBank, currAccount.GetId()));
                        break;
                    case 2:
                        CheckTheOperation(Refill(currBank, currAccount.GetId()));
                        break;
                    case 3:
                        CheckTheOperation(Transfer(currBank, currAccount.GetId()));
                        break;
                }
            }
        }

        private bool Withdraw(Bank bank, int accountId)
        {
            Console.WriteLine("Enter the sum of withdraw");
            int sum = Convert.ToInt32(Console.ReadLine());
            return bank.WithdrawalAccount(accountId, sum);
        }

        private bool Refill(Bank bank, int accountId)
        {
            Console.WriteLine("Enter the sum of withdraw");
            int sum = Convert.ToInt32(Console.ReadLine());
            return bank.RefillAccount(accountId, sum);
        }

        private bool Transfer(Bank bank, int senderId)
        {
            Console.WriteLine("Enter the ID of recipient");
            int recipientId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the sum of withdraw");
            int sum = Convert.ToInt32(Console.ReadLine());
            return bank.TransferBetweenAccount(senderId, recipientId, sum);
        }

        private void CheckTheOperation(bool hasOperationEnded)
        {
            if (hasOperationEnded)
            {
                Console.WriteLine("The operation was successful");
            }
            else
            {
                Console.WriteLine("the operation failed");
            }
        }

        private Client RegisterClient()
        {
            Console.WriteLine("Enter you name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter you surname: ");
            string surname = Console.ReadLine();
            Console.WriteLine("Enter you phone number: ");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Enter you address or 0 - if you want to skip this step: ");
            string address = Console.ReadLine();
            Console.WriteLine("Enter you passport date or 0 - if you want to skip this step: ");
            string passport = Console.ReadLine();
            Console.WriteLine("You have registered!");
            return new Client.ClientBuilder().NameSurname(name, surname).PhoneNumber(phoneNumber).Address(address).PassportDate(passport).GetClient();
        }

        private IBankAccount ChooseAccount(Bank bank, Client client)
        {
            Console.WriteLine("Enter the ID of the account need you or 0 - if you want to see the ID of all banks");
            while (true)
            {
                int userAnswer = Convert.ToInt32(Console.ReadLine());
                if (userAnswer == 0)
                {
                    List<IBankAccount> accounts = client.GetAccounts();
                    foreach (var account in accounts)
                    {
                        Console.WriteLine(account.GetId());
                    }
                }
                else
                {
                    IBankAccount currAccount = client.FindAccount(userAnswer);
                    if (currAccount == null)
                    {
                        Console.WriteLine("Incorrect Account ID. Try again!");
                    }
                    else
                    {
                        return currAccount;
                    }
                }
            }
        }

        private void Subcribing(Client client)
        {
            Console.WriteLine("Subscription status now:  ", client.IsSubscribedOnNotification());
            Console.WriteLine("Do you want to change? \n 0 - yes \n 1 - go back");
            int userAnswer = Convert.ToInt32(Console.ReadLine());
            if (userAnswer == 0)
            {
                if (client.IsSubscribedOnNotification())
                {
                    client.UnsubscribeToNotofocation();
                }
                else
                {
                    client.SubscribeToNotofocation();
                }
            }
        }
    }
}