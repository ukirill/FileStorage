using NHibernate;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Xml.Linq;

namespace DBModel.Helpers
{
    public static class СonfigHelper
    {
        public static string ConnectionString { get; set; }
        public static string NHBConfigPath { get; set; }
        public static string StoragePath { get; set; }
        public static bool Configured { get; set; }

        private static string InitializationScriptPath { get; set; }

        public static void ConfigFromFile(string Path)
        {
            try
            {
                XDocument doc = XDocument.Load(Path);
                ConnectionString = doc.Element("configuration").Element("connection_string").Value;
                NHBConfigPath = doc.Element("configuration").Element("nhb_config_path").Value;
                StoragePath = doc.Element("configuration").Element("storage_path").Value;
                InitializationScriptPath = doc.Element("configuration").Element("init_script").Value;
                if (!string.IsNullOrEmpty(InitializationScriptPath))
                    ExecuteInitializeScript(InitializationScriptPath);
                Configured = true;
            }
            catch { Configured = false; }
        }

        private static void ExecuteInitializeScript(string scriptPath)
        {
            string script = System.IO.File.ReadAllText(scriptPath);
            ISession sess = NHHelper.OpenSession();
            using (var conn = new SqlConnection(ConnectionString))
            {
                IEnumerable<string> commandStrings =
                    Regex.Split(script, @"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
                conn.Open();
                foreach (string commandString in commandStrings)
                {
                    if (commandString.Trim() != "")
                    {
                        using (var command = new SqlCommand(commandString, conn))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
            }
            sess.Close();
        }
    }
}
