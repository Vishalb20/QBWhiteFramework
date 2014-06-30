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
                                Thread.Sleep(5000);
                                item.Focus();
                                return app;
                            }
                        }
                    }
                }
                
                Process proc = new Process();
                proc.StartInfo.FileName = exePath;
                proc.Start();
                proc.WaitForInputIdle();
                Thread.Sleep(35000);
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
                        break;
                    }
                }

                List<Window> qbWindows = qbWin.ModalWindows();
                foreach (Window item in qbWindows)
                {

                    if (item.Name.Equals("QuickBooks Login"))
                    {
                        Actions.SetFocusOnWindow(item);
                        Actions.SendBCKSPACEToWindow(item);
                        Actions.SetTextByAutomationID(item, "15922", username);
                        Actions.SendTABToWindow(item);
                        Actions.SendKeysToWindow(item, password);
                        Actions.ClickElementByAutomationID(item, "51");
                    }
                    else if (item.Name.Equals("No QuickBooks") || item.Name.Contains("No QuickBooks"))
                    {
                        item.Focus();
                    }
                    else
                    {
                        item.Close();
                        try { Actions.ClickElementByName(Actions.GetChildWindow(qbWin, "Recording Transaction"), "No"); }
                        catch { }
                    }
                }

                Actions.SetFocusOnWindow(qbWin);
                try { Actions.ClickElementByAutomationID(qbWin, "Maximize"); }
                catch (Exception e) { var err = e.Message; }

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
                Window invoiceWindow = Actions.GetWindow(qbWindow, FrameworkLibraries.ObjMaps.QBDT.WhtieAPI.Invoice.Objects.CreateInvoice_Window_Name);

                try
                {Actions.ClickElementByAutomationID(invoiceWindow, FrameworkLibraries.ObjMaps.QBDT.WhtieAPI.Invoice.Objects.MaximizeWindow_Button_AutoID);
                Thread.Sleep(int.Parse(Execution_Speed));}
                catch (Exception)
                {}
                
                //Actions.GetCurrsorToFirstTextBox(invoiceWindow);
                Actions.ClickButtonByAutomationID(invoiceWindow, "PrevBtn");
                Actions.SendKeysToWindow(invoiceWindow, customer);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendKeysToWindow(invoiceWindow, cls);
                Actions.SendTABToWindow(invoiceWindow);
                Actions.SendKeysToWindow(invoiceWindow, account);
                Actions.SendTABToWindow(invoiceWindow);
                //FrameworkLibraries.Utils.KeyStrokeSimulator.SendKeysAsCharacters(template);
                //FrameworkLibraries.Utils.KeyStrokeSimulator.SendKey("{TAB}");
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
                //Actions.SendKeysToWindow(invoiceWindow, itemDesc);
                
                if (markPending)
                { Actions.ClickButtonByAutomationID(invoiceWindow, FrameworkLibraries.ObjMaps.QBDT.WhtieAPI.Invoice.Objects.MarkPending_Button_AutoID); }
                
                Actions.ClickElementByName(invoiceWindow, FrameworkLibraries.ObjMaps.QBDT.WhtieAPI.Invoice.Objects.SaveClsoe_Button_Name);
                
                try { Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Recording Transaction"), "Yes"); }
                catch { }

                try { Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Information Changed"), "No"); }
                catch { }

                Actions.CloseAllChildWindows(qbWindow);
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

        public static void ExceptionHandler()
        {
            TestStack.White.Application app = null;
            Window win = null;
            
            try
            {
                app = GetApp("QuickBooks");
                win = GetAppWindow(app, "QuickBooks");
                do
                {
                    List<Window> modalWin = win.ModalWindows();
                    foreach (Window item in modalWin)
                    {
                        if (item.Name.Contains("Error"))
                        {
                            Actions.ClickElementByName(item, "Don't Send");
                            break;
                        }
                    }
                }
                while (win == null);
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

    }


}
