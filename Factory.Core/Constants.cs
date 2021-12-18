using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core
{
    public static class Constants
    {
        public static string LOGFILEFOLDER = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Factory\";

        public static string LOGFILEPATH = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\Factory\log.txt";
    }
}
