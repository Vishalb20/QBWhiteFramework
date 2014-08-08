using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkLibraries.Utils
{
    public class FileOperations
    {
        public static void DeleteAllFilesInDirectory(string dir)
        {
            string[] filePaths = Directory.GetFiles(dir);
            foreach (string filePath in filePaths)
                File.Delete(filePath);
        }

        public static void CopyAllFilesToDirectory(string source, string destination)
        {
            string[] filePaths = Directory.GetFiles(source);
            foreach (string filePath in filePaths)
                File.Copy(filePath, destination);
        }

    }
}
