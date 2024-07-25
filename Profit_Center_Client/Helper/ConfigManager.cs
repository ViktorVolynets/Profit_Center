using System.Xml.Linq;

namespace Profit_Center_Client.Helper
{
    public class ConfigManager
    {
        public string MulticastGroup { get; private set; }
        public int Port { get; private set; }

        public ConfigManager(string configFilePath)
        {
            LoadConfig(configFilePath);
        }

        private void LoadConfig(string configFilePath)
        {
            var config = XElement.Load(configFilePath);
            MulticastGroup = config.Element("MulticastGroup").Value;
            Port = int.Parse(config.Element("Port").Value);
        }
    }

}
