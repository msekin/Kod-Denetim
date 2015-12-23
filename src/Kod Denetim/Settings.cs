using System;
using System.Configuration;

namespace CodeAnalysis
{
    public static class Settings
    {
        public static int MaxParamCount
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["MaxParamCount"]);
            }
        }

        public static int NestedIfDepth
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["NestedIfDepth"]);
            }
        }

        public static int FunctionMaxLength
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["FunctionMaxLength"]);
            }
        }

        public static bool NoCommentTesterEnabled
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["NoCommentTesterEnabled"]);
            }
        }
    }
}
