using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrameworkLibraries.Utils
{
    public class FileOperations
    {
        public static void DeleteAllFilesInDirectory(string dir)
        {
            string[] filePaths = Directory.GetFiles(dir);
            foreach (string filePath in filePaths)
            {
                try
                {
                    File.GetAccessControl(filePath);
                    File.Delete(filePath);
                }
                catch(Exception)
                {
                }
            }

        }

        public static void CopyAllFilesToDirectory(string source, string destination)
        {
            string destinationFile = null;

            string[] filePaths = Directory.GetFiles(source);
            foreach (string filePath in filePaths)
            {
                string[] split = filePath.Split('\\');
                foreach(string s in split)
                {
                    if (s.Contains(".qbw") || s.Contains(".QBW"))
                    {
                        destinationFile = s;
                        break;
                    }
                }
                File.Copy(filePath, destination+destinationFile, true);
            }
        }

    }
}
