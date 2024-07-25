namespace Profit_Center_Client.Service
{
    internal interface IStatisticsCalculator
    {
        void GetStatistics(out double meanResult, out double stdDevResult, out int modeResult, out long receivedPacketsResult, out long lostPacketsResult);
        void UpdateStatistics(int newValue, long lostPackets);
    }
}