using System;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

namespace CodeAnalysis
{
    public static class Settings
    {
        public const string ProjectName = "Kod Denetim";

        public static bool NoCommentTesterEnabled
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["NoCommentTesterEnabled"]);
            }
        }

        public static string DotNETFramework
        {
            get
            {
                XDocument xdoc = XDocument.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                XElement runtime = xdoc.Root
                    .Elements("startup")
                    .Elements("supportedRuntime")
                    .FirstOrDefault();

                if(runtime == null)
                {
                    return "";
                }

                XAttribute version = runtime.Attribute("version");
                
                if(version == null)
                {
                    return "";
                }

                return version.Value;
            }
        }

        static Settings()
        {

        }

        public static void SaveAppSettings(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();

            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
