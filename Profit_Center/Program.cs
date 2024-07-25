using Profit_Center_Server.Helper;
using Profit_Center_Server.Service;

namespace Profit_Center_Server
{
    class Program
    {
        static void Main()
        {
            // Load configuration
            var configLoader = new ConfigManager("serverConfig.xml");

            // Create sender
            var sender = new UdpMulticastSender(configLoader.MulticastGroup, configLoader.Port);

            // Create packet generator
            var packetGenerator = new PacketGenerator(sender, configLoader.MinValue, configLoader.MaxValue);

            // Start generating and sending packets
            packetGenerator.Start();
        }
    }
}