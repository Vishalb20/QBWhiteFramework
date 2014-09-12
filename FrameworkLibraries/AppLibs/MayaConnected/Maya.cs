using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using FrameworkLibraries.Utils;
using TestStack.White.UIItems.WindowItems;
using TestStack.White;
using FrameworkLibraries.ActionLibs;
using TestStack.White.UIItems;
using System.Windows.Forms;
using System.Windows.Automation;
using TestStack.White.UIItems.Finders;
using FrameworkLibraries.ObjMaps.QBDT;
using FrameworkLibraries.ActionLibs.WhiteAPI;
using System.IO;
using System.Reflection;

namespace FrameworkLibraries.AppLibs.MayaConnected
{
    public class Maya
    {
        public static Property conf = Property.GetPropertyInstance();
        public static string Execution_Speed = conf.get("ExecutionSpeed");
        public static string Sync_Timeout = conf.get("SyncTimeOut");
        public static string ResetWindow_Timeout = conf.get("ResetWindowTimeOut");
        public static string UserName = conf.get("QBLoginUserName");
        public static string Password = conf.get("QBLoginPassword");
        public static string DefaultCompanyFile = conf.get("DefaultCompanyFile");
        public static string DefaultCompanyFilePath = DefaultCompanyFile;
        public static string TestDataSourceDirectory = conf.get("TestDataSourceDirectory");
        public static string TestDataLocalDirectory = conf.get("TestDataLocalDirectory");
        public static string QbwINI = conf.get("QBW.ini");

        //**************************************************************************************************************************************************************

        public static TestStack.White.Application Initialize(String exePath)
        {
            Logger.logMessage("Initialize " + exePath);

            var accessiblity = FrameworkLibraries.Utils.FileOperations.CheckForStringInFile(QbwINI, "QBACCESSIBILITY=1");
            if (!accessiblity)
            {
                Logger.logMessage("QBAccessiblity settings not availble in - " + QbwINI);
                Logger.logMessage("Trying to set QBACCESSIBILITY=1 and kill any existing QBW32 process..");
                FileOperations.AppendStringToFile(QbwINI, "[ACCESSIBILITY]");
                FileOperations.AppendStringToFile(QbwINI, "QBACCESSIBILITY=1");
                OSOperations.KillProcess("QBW32");
            }

            int processID = 0;
            TestStack.White.Application app = null;

            try
            {
                List<Window> allWin = Desktop.Instance.Windows();
                foreach (Window item in allWin)
                {
                    if (item.Name.Contains("QuickBooks"))
                    {
                        foreach (Process p in Process.GetProcesses("."))
                        {
                            if (p.ProcessName.Contains("QBW32") || p.ProcessName.Contains("qbw32"))
                            {
                                processID = p.Id;
                                app = TestStack.White.Application.Attach(processID);
                                app.WaitWhileBusy();
                                Actions.WaitForAppWindow("QuickBooks", int.Parse(Sync_Timeout));
                                Logger.logMessage("Existing QB instance found..!!");
                                return app;
                            }
                        }
                    }
                }

                Logger.logMessage("No existing QB instance, so launching - " + exePath);
                Process proc = new Process();
                proc.StartInfo.FileName = exePath;
                proc.Start();
                Thread.Sleep(7500);

                //Alert window handler
                if (Actions.CheckDesktopWindowExists("Alert"))
                    Actions.CheckForAlertAndClose("Alert");

                //Crash handler
                if (Actions.CheckDesktopWindowExists("QuickBooks - Unrecoverable Error"))
                    Actions.QBCrashHandler();
                
                try
                {
                    Logger.logMessage("---------------Try-Catch Block------------------------");
                    Actions.WaitForAppWindow("QuickBooks", int.Parse(Sync_Timeout));
                }
                catch (Exception) { }
                Thread.Sleep(int.Parse(Execution_Speed));
                foreach (Process p in Process.GetProcesses("."))
                {
                    if (p.ProcessName.Contains("QBW32") || p.ProcessName.Contains("qbw32"))
                    {
                        processID = p.Id;
                    }
                }
                app = TestStack.White.Application.Attach(processID);
                app.WaitWhileBusy();
                Thread.Sleep(int.Parse(Execution_Speed));

                Logger.logMessage("Initialize " + exePath + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");

                return app;
            }
            catch (Exception e)
            {
                Logger.logMessage("Initialize " + exePath + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");

                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }

        //**************************************************************************************************************************************************************

        public static Window PrepareBaseState(TestStack.White.Application app)
        {
            Window qbWin = null;

            try
            {
                List<Window> windows = app.GetWindows();
                foreach (Window item in windows)
                {
                    if (item.Name.Contains("QuickBooks"))
                    {
                        qbWin = item;
                        Thread.Sleep(int.Parse(Execution_Speed));
                        break;
                    }
                }

                Logger.logMessage("PrepareBaseState " + app + " - Sucessful");
                Logger.logMessage(qbWin.Title);
                Logger.logMessage("------------------------------------------------------------------------------");

                return qbWin;

            }
            catch (Exception e)
            {
                Logger.logMessage("PrepareBaseState " + app + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");

                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


        //**************************************************************************************************************************************************************

        public static TestStack.White.Application GetApp(string appName)
        {
            int processID = 0;
            TestStack.White.Application app = null;

            try
            {
                List<Window> allWin = Desktop.Instance.Windows();
                foreach (Window item in allWin)
                {
                    if (item.Name.Contains(appName))
                    {
                        foreach (Process p in Process.GetProcesses("."))
                        {
                            if (p.ProcessName.Contains("UI") || p.ProcessName.Contains("ui"))
                            {
                                processID = p.Id;
                                app = TestStack.White.Application.Attach(processID);
                                //app.WaitWhileBusy();
                                Thread.Sleep(int.Parse(Execution_Speed));
                                break;
                            }
                        }
                    }
                }

                return app;
            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }

        //**************************************************************************************************************************************************************

        public static Window GetAppWindow(TestStack.White.Application app, string winName)
        {
            Window win = null;

            try
            {
                List<Window> allWin = app.GetWindows();

                foreach (Window item in allWin)
                {
                    if (item.Name.Contains(winName))
                    {
                        win = item;
                        break;
                    }
                }

                return win;
            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }

        //**************************************************************************************************************************************************************



    }

}
