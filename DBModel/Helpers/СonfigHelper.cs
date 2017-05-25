using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DBModel.Helpers
{
    public static class СonfigHelper
    {
        public static string ConnectionString { get; set; }
        public static string NHBConfigPath { get; set; }
        public static string StoragePath { get; set; }
        public static bool Configured { get; set; }

        public static void ConfigFromFile(string Path)
        {
            try
            {
                XDocument doc = XDocument.Load(Path);
                ConnectionString = doc.Element("configuration").Element("connection_string").Value;
                NHBConfigPath = doc.Element("configuration").Element("nhb_config_path").Value;
                StoragePath = doc.Element("configuration").Element("storage_path").Value;
                Configured = true;
            }
            catch { Configured = false; }
        }
    }
}
