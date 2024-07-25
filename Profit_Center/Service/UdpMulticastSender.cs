using System.Net;
using System.Net.Sockets;

namespace Profit_Center_Server.Service
{
    public class UdpMulticastSender : IUdpMulticastSender
    {
        private readonly UdpClient udpClient;
        private readonly IPEndPoint multicastEndPoint;

        public UdpMulticastSender(string multicastGroup, int port)
        {
            udpClient = new UdpClient();
            multicastEndPoint = new IPEndPoint(IPAddress.Parse(multicastGroup), port);
        }

        public void Send(byte[] data)
        {
            udpClient.Send(data, data.Length, multicastEndPoint);
        }
    }

}
