using Profit_Center_Client.Helper;
using Profit_Center_Client.Service;

namespace Profit_Center_Client
{
    class Client
    {
        private readonly UdpReceiver udpReceiver;
        private readonly DataProcessor dataProcessor;
        private readonly StatisticsDisplay statisticsDisplay;

        public Client(ConfigManager configManager)
        {
            udpReceiver = new UdpReceiver(configManager.MulticastGroup, configManager.Port);
            dataProcessor = new DataProcessor(udpReceiver);
            statisticsDisplay = new StatisticsDisplay(dataProcessor.GetStatisticsCalculator());

            // Start receiving and processing data
            Task receiveTask = Task.Run(() => udpReceiver.StartReceiving());
            Task processTask = Task.Run(() => dataProcessor.StartProcessing());
        }

        public void DisplayStatistics()
        {
            statisticsDisplay.Display();
        }
    }

}
