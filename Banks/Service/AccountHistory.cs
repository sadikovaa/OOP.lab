using System.Collections.Generic;

namespace Banks.Service
{
    public class AccountHistory
    {
        private int _idOfAccount;
        private List<Operation> _history;

        public AccountHistory(int id)
        {
            _idOfAccount = id;
            _history = new List<Operation>();
        }

        public int GetId()
        {
            return _idOfAccount;
        }

        public List<Operation> GetHistory()
        {
            return _history;
        }

        public Operation AddOperation(Operation operation)
        {
            _history.Add(operation);
            return operation;
        }

        public Operation GetLastOperation()
        {
            return _history[_history.Count - 1];
        }

        public void DeleteLastOperation()
        {
            _history.RemoveAt(_history.Count - 1);
        }
    }
}