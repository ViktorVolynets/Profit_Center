namespace Profit_Center_Client.Service
{
    class StatisticsDisplay
    {
        private readonly IStatisticsCalculator statisticsCalculator;

        public StatisticsDisplay(IStatisticsCalculator statisticsCalculator)
        {
            this.statisticsCalculator = statisticsCalculator;
        }

        public void Display()
        {
            double mean, stdDev;
            int mode;
            long receivedPackets, lostPackets;

            statisticsCalculator.GetStatistics(out mean, out stdDev, out mode, out receivedPackets, out lostPackets);

            Console.WriteLine($"Mean: {mean}");
            Console.WriteLine($"Standard Deviation: {stdDev}");
            Console.WriteLine($"Mode: {mode}");
            Console.WriteLine($"Received Packets: {receivedPackets}");
            Console.WriteLine($"Lost Packets: {lostPackets}");
        }
    }
}
