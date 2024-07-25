namespace Profit_Center_Client.Service
{
    internal interface IDataProcessor
    {
        IStatisticsCalculator GetStatisticsCalculator();
        void StartProcessing();
    }
}