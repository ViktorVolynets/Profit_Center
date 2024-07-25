using System.Collections.Concurrent;

namespace Profit_Center_Client.Service
{
    internal interface IUdpReceiver
    {
        long GetLostPacketCount();
        BlockingCollection<int> GetNumberQueue();
        void StartReceiving();
    }
}