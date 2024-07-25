using System.Xml.Linq;

namespace Profit_Center_Server.Helper
{
    public class ConfigManager
    {
        public string MulticastGroup { get; private set; }
        public int Port { get; private set; }
        public int MinValue { get; private set; }
        public int MaxValue { get; private set; }

        public ConfigManager(string configFilePath)
        {
            LoadConfig(configFilePath);
        }

        private void LoadConfig(string configFilePath)
        {
            var config = XElement.Load(configFilePath);
            MulticastGroup = config.Element("MulticastGroup").Value;
            Port = int.Parse(config.Element("Port").Value);
            MinValue = int.Parse(config.Element("MinValue").Value);
            MaxValue = int.Parse(config.Element("MaxValue").Value);
        }
    }
}
