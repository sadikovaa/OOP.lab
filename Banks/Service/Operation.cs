using Banks.Tools;
using Banks.TypesOfAccount;

namespace Banks.Service
{
    public class Operation
    {
        private long _id;
        private IBankAccount _sender;
        private IBankAccount _recipient;
        private double _sum;

        public Operation(IBankAccount sender, IBankAccount recipient, double sum)
        {
            _id = OperationIdGenerator.GetId();
            _sender = sender;
            _recipient = recipient;
            _sum = sum;
        }

        public IBankAccount GetSender()
        {
            return _sender;
        }

        public IBankAccount GetRecipient()
        {
            return _recipient;
        }

        public double GetSumOfOperation()
        {
            return _sum;
        }

        public long GetId()
        {
            return _id;
        }
    }
}