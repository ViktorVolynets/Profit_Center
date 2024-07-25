namespace Profit_Center_Server.Service
{
    public interface IUdpMulticastSender
    {
        void Send(byte[] data);
    }
}