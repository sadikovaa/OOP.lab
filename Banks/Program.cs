using Banks.Service;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var userApplication = new Application(CentralBankService.GetCentralBankInstance());
            userApplication.Start();
        }
    }
}
