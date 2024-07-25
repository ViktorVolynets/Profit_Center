using Profit_Center_Client.Helper;

namespace Profit_Center_Client
{
    class Program
    {
        static void Main()
        {
            string configFilePath = "clientConfig.xml";
            ConfigManager configManager = new ConfigManager(configFilePath);
            Client client = new Client(configManager);

            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    client.DisplayStatistics();
                }
            }
        }
    }
}
