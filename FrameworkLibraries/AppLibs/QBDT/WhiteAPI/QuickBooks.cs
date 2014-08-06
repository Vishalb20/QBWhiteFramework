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
using FrameworkLibraries.ActionLibs.QBDT;
using TestStack.White.UIItems;
using System.Windows.Forms;
using System.Windows.Automation;
using TestStack.White.UIItems.Finders;
using FrameworkLibraries.ObjMaps.QBDT;
using FrameworkLibraries.ActionLibs.QBDT.WhiteAPI;

namespace FrameworkLibraries.AppLibs.QBDT.WhiteAPI
{
    public class QuickBooks
    {
        public static String startupPath = System.IO.Path.GetFullPath("..\\..\\..\\");
        public static Property conf = new Property(startupPath + "\\QBAutomation.properties");
        public static string Execution_Speed = conf.get("ExecutionSpeed");

//**************************************************************************************************************************************************************

        public static TestStack.White.Application Initialize(String exePath)
        {
            int processID = 0;
            TestStack.White.Application app = null;
            
            try
            {
                List<Window> allWin = Desktop.Instance.Windows();
                foreach (Window item in allWin)
                {
                    if (item.Name.Contains("Intuit QuickBooks"))
                    {
                        foreach (Process p in Process.GetProcesses("."))
                        {
                            if (p.ProcessName.Contains("QBW32"))
                            {
                                processID = p.Id;
                                app = TestStack.White.Application.Attach(processID);
                                app.WaitWhileBusy();
                                Thread.Sleep(10000);
                                return app;
                            }
                        }
                    }
                }
                
                Process proc = new Process();
                proc.StartInfo.FileName = exePath;
                proc.Start();
                proc.WaitForInputIdle();
                Thread.Sleep(60000);
                foreach (Process p in Process.GetProcesses("."))
                {
                    if (p.ProcessName.Contains("QBW32"))
                    {
                        processID = p.Id;
                    }
                }
                app = TestStack.White.Application.Attach(processID);
                app.WaitWhileBusy();
                Thread.Sleep(5000);
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

        public static Window PrepareBaseState(TestStack.White.Application app, string username, string password)
        {
            Window qbWin = null;
            try
            {
                List<Window> windows = app.GetWindows();
                foreach (Window item in windows)
                {
                    if (item.Name.Contains("Intuit QuickBooks"))
                    {
                        qbWin = item;
                        Thread.Sleep(5000);
                        break;
                    }
                }

                try
                {
                    Actions.ClickElementByName(Actions.GetChildWindow(qbWin, "No"), "Open");
                    Thread.Sleep(10000);
                }
                catch { }

                try 
                {
                    Actions.ClickElementByName(Actions.GetChildWindow(qbWin, "Enter Memorized Transactions"), "Enter All Later");
                    Thread.Sleep(5000);
                }
                catch { }

                try
                {
                    Actions.ClickElementByName(Actions.GetChildWindow(qbWin, "Enter Memorized Transactions"), "OK");
                    Thread.Sleep(5000);
                }
                catch { }


                List<Window> qbWindows = qbWin.ModalWindows();
                foreach (Window item in qbWindows)
                {
                    if (item.Name.Equals("No"))
                    {
                        try
                        {
                            item.Focus();
                            Actions.ClickElementByName(Actions.GetChildWindow(qbWin, "No"), "Open");
                            Thread.Sleep(10000);
                        }
                        catch { }
                    }

                    else if (item.Name.Equals("QuickBooks Login"))
                    {
                        Actions.SetFocusOnWindow(item);
                        Actions.SendBCKSPACEToWindow(item);
                        Actions.SetTextByAutomationID(item, "15922", username);
                        Actions.SendTABToWindow(item);
                        Actions.SendKeysToWindow(item, password);
                        Actions.ClickElementByAutomationID(item, "51");
                    }
                    else if (item.Name.Equals("Enter Memorized Transactions") || item.Name.Contains("Enter Memorized Transactions"))
                    {
                        try
                        {
                            Actions.ClickElementByName(Actions.GetChildWindow(qbWin, "Enter Memorized Transactions"), "Enter All Later");
                            Thread.Sleep(5000);
                        }
                        catch { }

                        try
                        {
                            Actions.ClickElementByName(Actions.GetChildWindow(qbWin, "Enter Memorized Transactions"), "OK");
                            Thread.Sleep(5000);
                        }
                        catch { }
                    }
                    else
                    {
                        item.Focus();
                        
                        try { FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(item, "Close"); }
                        catch { }
                        
                        try { item.Close(); }
                        catch { }
                        
                        try { Actions.ClickElementByName(Actions.GetChildWindow(qbWin, "Recording Transaction"), "No"); }
                        catch { }
                        
                        try { Actions.ClickElementByName(Actions.GetChildWindow(qbWin, "Enter Memorized Transactions Later"), "Ok"); }
                        catch { }
                    }
                }

                Actions.SetFocusOnWindow(qbWin);
                try { Actions.ClickElementByAutomationID(qbWin, "Maximize"); }
                catch (Exception e) { var err = e.Message; }

                try
                {
                    Actions.ClickElementByName(Actions.GetChildWindow(qbWin, "Enter Memorized Transactions"), "Enter All Later");
                    Thread.Sleep(5000);
                }
                catch { }

                try
                {
                    Actions.ClickElementByName(Actions.GetChildWindow(qbWin, "Enter Memorized Transactions"), "OK");
                    Thread.Sleep(5000);
                }
                catch { }

                try
                {
                    Actions.ClickElementByName(Actions.GetChildWindow(qbWin, "No"), "Open");
                    Thread.Sleep(10000);
                }
                catch { }

                return qbWin;

            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static bool CreateInvoice(TestStack.White.Application qbApp, Window qbWindow, String customer, String cls, String account, String template, int invoiceNumber, int poNumber, String terms, String via, String fob, String quatity, String item, String itemDesc, bool markPending)
        {
            try
            {
                Actions.SelectMenu(qbApp, qbWindow, "Customers", "Create Invoices");
                Thread.Sleep(int.Parse(Execution_Speed));
                Window invoiceWindow = Actions.GetWindow(qbWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Invoice.Objects.CreateInvoice_Window_Name);

                try
                {Actions.ClickElementByAutomationID(invoiceWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Invoice.Objects.MaximizeWindow_Button_AutoID);
                Thread.Sleep(int.Parse(Execution_Speed));}
                catch (Exception)
                {}
                
                Actions.ClickButtonByAutomationID(invoiceWindow, "PrevBtn");
                Actions.SendKeysToWindow(invoiceWindow, customer);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendKeysToWindow(invoiceWindow, cls);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendKeysToWindow(invoiceWindow, account);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendKeysToWindow(invoiceWindow, template);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendBCKSPACEToWindow(invoiceWindow);
                Actions.SendKeysToWindow(invoiceWindow, Convert.ToString(invoiceNumber));
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendBCKSPACEToWindow(invoiceWindow);
                Actions.SendKeysToWindow(invoiceWindow, Convert.ToString(poNumber));
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendKeysToWindow(invoiceWindow, terms);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendKeysToWindow(invoiceWindow, via);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendKeysToWindow(invoiceWindow, fob);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendKeysToWindow(invoiceWindow, quatity);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendKeysToWindow(invoiceWindow, item);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendSHIFT_ENDToWindow(invoiceWindow);
                
                if (markPending)
                { Actions.ClickButtonByAutomationID(invoiceWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Invoice.Objects.MarkPending_Button_AutoID); }
                
                Actions.ClickElementByName(invoiceWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Invoice.Objects.SaveClsoe_Button_Name);
                
                try { Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Recording Transaction"), "Yes");
                Thread.Sleep(2000);
                }
                catch { }

                try { Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Information Changed"), "No");
                Thread.Sleep(2000);
                }
                catch { }

                try { Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Past Transactions"), "No");
                Thread.Sleep(2000);
                }
                catch { }

                try { Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Available Credits"), "No");
                Thread.Sleep(2000);
                }
                catch { }

                return true;

            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }

//**************************************************************************************************************************************************************

        public static void ExceptionHandler(Window qbWindow)
        {
            try
            {
                do
                {
                    List<Window> modalWin = qbWindow.ModalWindows();
                    foreach (Window item in modalWin)
                    {
                        if (item.Name.Contains("Error"))
                        {
                            Actions.ClickElementByName(item, "Don't Send");
                            break;
                        }
                    }
                }
                while (qbWindow == null);
            }
            catch (Exception e)
            {
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
                            if (p.ProcessName.Contains("QBW32"))
                            {
                                processID = p.Id;
                                app = TestStack.White.Application.Attach(processID);
                                app.WaitWhileBusy();
                                Thread.Sleep(1000);
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

        public static void OpenOrUpgradeCompanyFile(string companyFilePath, TestStack.White.Application qbApp, Window qbWindow)
        {
            try
            {
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.SelectMenu(qbApp, qbWindow, "File", "Open or Restore Company...");
                Thread.Sleep(5000);
                Window openRestoreCompanyWindow = FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Open or Restore Company");
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(openRestoreCompanyWindow, "Open a company file");
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(openRestoreCompanyWindow, "Next");
                Thread.Sleep(5000);
                Window openCompanyWindow = FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Open a Company");
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.SetTextOnElementByName(openCompanyWindow, "File name:", companyFilePath);
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(openCompanyWindow, "Open");
                Thread.Sleep(20000);

                try { FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "QuickBooks Login"), "OK"); }
                catch (Exception) { }

                try { FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Warning"), "Continue"); }
                catch (Exception) { }

                try
                {
                    FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Save Backup Copy"), "Save");
                    Thread.Sleep(60000);
                }
                catch (Exception) { }

                try
                {
                    FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Update Company File for New Version"), "I understand that my company file will be updated to this new version of QuickBooks.");
                    Thread.Sleep(1000);
                }
                catch (Exception) { }

                try
                {
                    FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Update Company File for New Version"), "Update Now");
                    Thread.Sleep(1000);
                }
                catch (Exception) { }

                try { FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "QuickBooks Information"), "OK"); }
                catch (Exception) { }

                try { FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Save Backup Copy"), "Save"); }
                catch (Exception) { }

                try
                {
                    FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Update Company"), "Yes");
                    Thread.Sleep(30000);
                }
                catch (Exception) { }

                try { FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Create Backup"), "Next"); }
                catch (Exception) { }

                try { FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.SetTextByAutomationID(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Backup Options"), "2002", "C:\\"); }
                catch (Exception) { }

                try { FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Backup Options"), "OK"); }
                catch (Exception) { }

                try { FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "QuickBooks"), "Use this Location"); }
                catch (Exception) { }

                try
                {
                    FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Save Backup Copy"), "Save");
                    Thread.Sleep(60000);
                }
                catch (Exception) { }

                try
                {
                    FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Update Company"), "Yes");
                    Thread.Sleep(60000);
                }
                catch (Exception) { }

                try { FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetAlertWindow("Alert"), "OK"); }
                catch (Exception) { }

                try
                {
                    FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Warning"), "Continue");
                    Thread.Sleep(10000);
                }
                catch (Exception) { }

                try
                {
                    Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Enter Memorized Transactions"), "Enter All Later");
                    Thread.Sleep(5000);
                }
                catch (Exception) { }


                try { Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Enter Memorized Transactions"), "OK");
                Thread.Sleep(5000);
                }
                catch { }
            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }

    }

//**************************************************************************************************************************************************************

}
