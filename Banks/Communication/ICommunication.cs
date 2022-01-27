using Banks.Service;

namespace Banks.Communication
{
    public interface ICommunication
    {
        void Message(Client currClient, string message);
    }
}