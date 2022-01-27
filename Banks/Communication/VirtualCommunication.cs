using Banks.Service;

namespace Banks.Communication
{
    public class VirtualCommunication : ICommunication
    {
        public void Message(Client currClient, string message)
        {
            // this function has empty body,
            // because this implementation of communication with client
            // only ASSUMES notification
        }
    }
}