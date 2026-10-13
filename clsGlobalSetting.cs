using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorTextAdvance
{
    public class clsGlobalSetting
    {
       public static string key = @"HKEY_CURRENT_USER\Software\TextEditorAdvance";
       public static string name = "Color";
       public static string value = "";
        public static string PathInput = "";
        public static string PathDestinationEncryption = "";
        public static string PathDestinationDecryption = "";
    }
}
