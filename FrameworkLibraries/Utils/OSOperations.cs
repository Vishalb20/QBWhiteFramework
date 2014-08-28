using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkLibraries.Utils
{
    public class OSOperations
    {
        public static void KillProcess(string processName)
        {
            foreach (Process p in Process.GetProcesses("."))
            {
                if (p.ProcessName.Contains(processName) || p.ProcessName.Contains(processName.ToUpper()))
                {
                    p.Kill();
                }
            }
        }
    }
}
