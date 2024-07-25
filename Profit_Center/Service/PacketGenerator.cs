namespace Profit_Center_Server.Service
{
    public class PacketGenerator
    {
        private readonly IUdpMulticastSender sender;
        private readonly int minValue;
        private readonly int maxValue;
        private long packetNumber = 0;
        private readonly Random random = new Random();

        public PacketGenerator(IUdpMulticastSender sender, int minValue, int maxValue)
        {
            this.sender = sender;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public void Start()
        {
            while (true)
            {
                int number = random.Next(minValue, maxValue);
                byte[] numberBytes = BitConverter.GetBytes(number);
                byte[] packetNumberBytes = BitConverter.GetBytes(packetNumber);

                byte[] bytes = new byte[packetNumberBytes.Length + numberBytes.Length];
                Buffer.BlockCopy(packetNumberBytes, 0, bytes, 0, packetNumberBytes.Length);
                Buffer.BlockCopy(numberBytes, 0, bytes, packetNumberBytes.Length, numberBytes.Length);

                sender.Send(bytes);
                packetNumber++;
            }
        }
    }

}
