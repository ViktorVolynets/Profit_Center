using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

namespace Profit_Center_Client.Service
{
    class UdpReceiver : IUdpReceiver
    {
        private readonly string multicastGroup;
        private readonly int port;
        private readonly BlockingCollection<int> numberQueue = new BlockingCollection<int>();
        private long lostPackets = 0;
        private long expectedPacketNumber = 0;

        public UdpReceiver(string multicastGroup, int port)
        {
            this.multicastGroup = multicastGroup;
            this.port = port;
        }

        public void StartReceiving()
        {
            using (UdpClient udpClient = new UdpClient())
            {
                udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));
                udpClient.JoinMulticastGroup(IPAddress.Parse(multicastGroup));

                while (true)
                {
                    try
                    {
                        IPEndPoint remoteEndPoint = null;
                        byte[] bytes = udpClient.Receive(ref remoteEndPoint);

                        long packetNumber = BitConverter.ToInt64(bytes, 0);
                        int number = BitConverter.ToInt32(bytes, 8);

                        if (packetNumber > expectedPacketNumber)
                        {
                            lostPackets += packetNumber - expectedPacketNumber;
                            expectedPacketNumber = packetNumber + 1;
                        }
                        else if (packetNumber < expectedPacketNumber)
                        {
                            // Handle out-of-order packets
                        }
                        else
                        {
                            expectedPacketNumber++;
                            numberQueue.Add(number);
                        }
                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine($"SocketException: {ex.Message}");
                        lostPackets++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception: {ex.Message}");
                        lostPackets++;
                    }
                }
            }
        }

        public BlockingCollection<int> GetNumberQueue()
        {
            return numberQueue;
        }

        public long GetLostPacketCount()
        {
            return lostPackets;
        }
    }
}
