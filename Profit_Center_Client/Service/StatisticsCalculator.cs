namespace Profit_Center_Client.Service
{
    class StatisticsCalculator : IStatisticsCalculator
    {
        private readonly object statsLock = new object();
        private long receivedPackets = 0;
        private long lostPackets = 0;
        private double mean = 0.0;
        private double m2 = 0.0;
        private int mode;
        private int[] modeCounts = new int[1000000]; // Adjust size based on expected range of values

        public void UpdateStatistics(int newValue, long lostPackets)
        {
            lock (statsLock)
            {
                receivedPackets++;
                long count = receivedPackets;
                double delta = newValue - mean;
                mean += delta / count;
                double delta2 = newValue - mean;
                m2 += delta * delta2;

                if (newValue < modeCounts.Length)
                {
                    modeCounts[newValue]++;
                    if (modeCounts[newValue] > modeCounts[mode])
                    {
                        mode = newValue;
                    }
                }

                this.lostPackets = lostPackets;
            }
        }

        public void GetStatistics(out double meanResult, out double stdDevResult, out int modeResult, out long receivedPacketsResult, out long lostPacketsResult)
        {
            lock (statsLock)
            {
                double variance = m2 / (receivedPackets - 1);
                stdDevResult = Math.Sqrt(variance);
                meanResult = mean;
                modeResult = mode;
                receivedPacketsResult = receivedPackets;
                lostPacketsResult = lostPackets;
            }
        }
    }
}
