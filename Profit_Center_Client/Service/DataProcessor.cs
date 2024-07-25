using System.Collections.Concurrent;

namespace Profit_Center_Client.Service
{
    class DataProcessor : IDataProcessor
    {
        private readonly BlockingCollection<int> numberQueue;
        private readonly IStatisticsCalculator statisticsCalculator = new StatisticsCalculator();
        private readonly IUdpReceiver receiver;
        private long lostPackets => receiver.GetLostPacketCount();

        public DataProcessor(IUdpReceiver udpReceiver)
        {
            numberQueue = udpReceiver.GetNumberQueue();
            receiver = udpReceiver;

        }

        public void StartProcessing()
        {
            foreach (var number in numberQueue.GetConsumingEnumerable())
            {
                statisticsCalculator.UpdateStatistics(number, lostPackets);
            }
        }

        public IStatisticsCalculator GetStatisticsCalculator()
        {
            return statisticsCalculator;
        }
    }
}