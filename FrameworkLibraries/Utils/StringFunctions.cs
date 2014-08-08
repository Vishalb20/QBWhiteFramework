using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkLibraries.Utils
{
    public class StringFunctions
    {
        public static string RemoveExtentionFromFileName(string fileName)
        {
            var splitFileName = fileName.Split('.');
            List<string> listFileNames = new List<string>(splitFileName);

            foreach (string item in listFileNames)
            {
                if (item.Equals("qbw") || item.Equals("QBW"))
                {
                    listFileNames.Remove(item);
                    break;
                }
            }
            return listFileNames[0];
        }
    }
}
