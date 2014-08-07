﻿using System;
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
        public static string UserName = conf.get("QBLoginUserName");
        public static string Password = conf.get("QBLoginPassword");
        public static string CompanyFile = conf.get("DefaultCompanyFile");
        public static string FilePath = startupPath + CompanyFile;

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

        public static Window PrepareBaseState(TestStack.White.Application app)
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
                {
                    Actions.ClickElementByAutomationID(invoiceWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Invoice.Objects.MaximizeWindow_Button_AutoID);
                    Thread.Sleep(int.Parse(Execution_Speed));
                }
                catch (Exception)
                { }

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

                try
                {
                    Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Recording Transaction"), "Yes");
                    Thread.Sleep(2000);
                }
                catch { }

                try { Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Enter Memorized Transactions Later"), "Ok"); }
                catch { }

                try
                {
                    Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Information Changed"), "No");
                    Thread.Sleep(2000);
                }
                catch { }

                try
                {
                    Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Past Transactions"), "No");
                    Thread.Sleep(2000);
                }
                catch { }

                try
                {
                    Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Available Credits"), "No");
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
                Thread.Sleep(2500);
                Window openRestoreCompanyWindow = FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Open or Restore Company");
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(openRestoreCompanyWindow, "Open a company file");
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(openRestoreCompanyWindow, "Next");
                Thread.Sleep(2500);
                Window openCompanyWindow = FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Open a Company");
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.SetTextOnElementByName(openCompanyWindow, "File name:", companyFilePath);
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(openCompanyWindow, "Open");
                Thread.Sleep(20000);

                List<Window> modalWin = null;
                int iteration = 0;

                do
                {
                    modalWin = qbWindow.ModalWindows();
                    iteration = iteration+1;

                    if (iteration <= 2)
                    {
                        foreach (Window item in modalWin)
                        {
                            //QB Login window handler
                            try
                            {
                                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "QuickBooks Login"), "OK");
                                Thread.Sleep(2500);
                            }
                            catch (Exception) { }

                            //Warning window handler
                            try
                            {
                                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Warning"), "OK");
                                Thread.Sleep(2500);
                            }
                            catch (Exception) { }


                            //QB Warning handler
                            try
                            {
                                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Warning"), "Continue");
                                Thread.Sleep(2500);
                            }
                            catch (Exception) { }

                            //Save backup copy window handler
                            try
                            {
                                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Save Backup Copy"), "Save");
                                Thread.Sleep(60000);
                            }
                            catch (Exception) { }

                            //Update to new version window handler - I agree
                            try
                            {
                                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Update Company File for New Version"), "I understand that my company file will be updated to this new version of QuickBooks.");
                                Thread.Sleep(2500);
                            }
                            catch (Exception) { }

                            //Update to new version window handler - Update now
                            try
                            {
                                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Update Company File for New Version"), "Update Now");
                                Thread.Sleep(2500);
                            }
                            catch (Exception) { }

                            //QB Information window handler
                            try
                            {
                                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "QuickBooks Information"), "OK");
                                Thread.Sleep(2500);
                            }
                            catch (Exception) { }

                            //Update company window handler
                            try
                            {
                                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Update Company"), "Yes");
                                Thread.Sleep(60000);
                            }
                            catch (Exception) { }

                            //Update company window handler 2
                            try
                            {
                                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Update Company"), "Continue");
                                Thread.Sleep(60000);
                            }
                            catch (Exception) { }


                            //Create backup copy window handler
                            try
                            {
                                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Create Backup"), "Next");
                                Thread.Sleep(10000);
                            }
                            catch (Exception) { }

                            //Backup options window handler - file path
                            try
                            {
                                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.SetTextByAutomationID(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Backup Options"), "2002", "C:\\");
                                Thread.Sleep(2500);
                            }
                            catch (Exception) { }

                            //Backup options window handler - Ok
                            try { FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Backup Options"), "OK"); Thread.Sleep(2500); }
                            catch (Exception) { }

                            //Quickbooks use this location window handler
                            try
                            {
                                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "QuickBooks"), "Use this Location");
                                Thread.Sleep(2500);
                            }
                            catch (Exception) { }

                            //Alert window handler
                            try
                            {
                                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetAlertWindow("Alert"), "OK");
                                Thread.Sleep(2500);
                            }
                            catch (Exception) { }

                            //Warning window handler
                            try
                            {
                                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Warning"), "Continue");
                                Thread.Sleep(10000);
                            }
                            catch (Exception) { }

                            //Home window handler
                            try { FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Home"), "Close"); }
                            catch (Exception) { }

                            //Enter memorized transaction window handler
                            try
                            {
                                Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Enter Memorized Transactions"), "Enter All Later");
                                Thread.Sleep(5000);
                            }
                            catch { }

                            //Enter memorized transaction window handler
                            try
                            {
                                Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Enter Memorized Transactions"), "OK");
                                Thread.Sleep(5000);
                            }
                            catch { }

                        }
                    }
                    else
                    {
                        ResetQBWindows(qbApp, qbWindow);
                        break;
                    }
                }
                while (modalWin.Count != 0);
            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }

        //**************************************************************************************************************************************************************

        public static void ResetQBWindows(TestStack.White.Application qbApp, Window qbWin)
        {
            List<Window> modalWin = null;

            try
            {
                do
                {
                    modalWin = qbWin.ModalWindows();

                    foreach (Window item in modalWin)
                    {

                        //No company window handler
                        if (item.Name.Contains("No"))
                        {
                            try
                            {
                                FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.OpenOrUpgradeCompanyFile(FilePath, qbApp, qbWin);
                            }
                            catch { }
                        }

                        //Payroll update window handler
                        else if (item.Name.Contains("Payroll Update"))
                        {
                            try
                            {
                                Actions.ClickElementByName(item, "Cancel");
                                Thread.Sleep(5000);
                            }
                            catch { }

                            try
                            {
                                Actions.ClickElementByName(item, "OK");
                                Thread.Sleep(5000);
                            }
                            catch { }

                        }

                        //Intuit payroll services window hadler
                        else if (item.Name.Contains("Intuit Payroll Services"))
                        {
                            try
                            {
                                Actions.ClickElementByName(item, "OK");
                                Thread.Sleep(5000);
                            }
                            catch { }
                        }

                        //Employer services window handler
                        else if (item.Name.Contains("Employer Services"))
                        {
                            try
                            {
                                Actions.ClickElementByName(item, "Cancel");
                                Thread.Sleep(5000);
                            }
                            catch { }
                        }



                        //Enter memorized transactions window handler
                        else if (item.Name.Contains("Enter Memorized Transactions"))
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

                        //Recording transaction window handler
                        else if (item.Name.Contains("Recording Transaction"))
                        {
                            try
                            {
                                Actions.ClickElementByName(item, "No");
                                Thread.Sleep(5000);
                            }
                            catch { }
                        }

                        //Recording transaction window handler
                        else if (item.Name.Contains("Alert"))
                        {
                            try
                            {
                                Actions.ClickElementByName(item, "OK");
                                Thread.Sleep(5000);
                            }
                            catch { }
                        }


                        //Login window handler
                        else if (item.Name.Equals("QuickBooks Login"))
                        {
                            Actions.SetFocusOnWindow(item);
                            Actions.SendBCKSPACEToWindow(item);
                            Actions.SetTextByAutomationID(item, "15922", UserName);
                            Actions.SendTABToWindow(item);
                            Actions.SendKeysToWindow(item, Password);
                            Actions.ClickElementByAutomationID(item, "51");
                            Thread.Sleep(10000);
                        }

                        //Error window handler
                        else if (item.Name.Contains("Error"))
                        {
                            Actions.ClickElementByName(item, "Don't Send");
                            Thread.Sleep(1000);
                        }

                        //QB Setup window handler
                        else if (item.Name.Contains("Setup"))
                        {
                            try
                            {
                                Actions.ClickElementByName(item, "Close");
                                Thread.Sleep(2500);
                            }
                            catch (Exception)
                            { }

                            try
                            {
                                Actions.ClickElementByName(item, "Yes");
                                Thread.Sleep(2500);
                            }
                            catch (Exception)
                            { }

                        }

                        //Warning window handler
                        else if (item.Name.Contains("Warning"))
                        {
                            Actions.ClickElementByName(item, "OK");
                            Thread.Sleep(1000);
                        }

                        else
                        {
                            item.Focus();

                            try { Actions.ClickElementByName(Actions.GetChildWindow(qbWin, "Recording Transaction"), "No"); }
                            catch { }

                            try { Actions.ClickElementByName(item, "Close"); }
                            catch { }

                            try { item.Close(); }
                            catch { }
                        }
                    }
                }
                while (modalWin.Count != 0);
            }

            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }

        //**************************************************************************************************************************************************************

        public static void CreateCompany(TestStack.White.Application qbApp, Window qbWindow, string businessName, string industry)
        {
            try
            {
                var Window_Condition = Actions.CheckWindowExists(qbWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.NoQBCompanyLoaded_Window_Name);

                if (!Window_Condition)
                {
                    Actions.SelectMenu(qbApp, qbWindow, "File", "New Company...");
                    qbApp.WaitWhileBusy();
                    Thread.Sleep(5000);
                }
                else
                {
                    Window NoCompanyLoadedWindow = Actions.GetChildWindow(qbWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.NoQBCompanyLoaded_Window_Name);
                    Actions.ClickElementByAutomationID(NoCompanyLoadedWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.CreateNewCompany_Element_AutoID);
                    qbApp.WaitWhileBusy();
                    Thread.Sleep(5000);
                }

                Window QBSetupWindow = Actions.GetChildWindow(qbWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.QBSetup_Window_Name);
                qbApp.WaitWhileBusy();
                Thread.Sleep(5000);

                Actions.ClickElementByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.ExpressStart_Button_AutoID);
                qbApp.WaitWhileBusy();
                Thread.Sleep(500);

                Actions.SetTextByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.BusinessName_TxtField_AutoID, businessName);
                Actions.SetTextByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.IndustryList_TxtField_AutoID, "Information");
                Thread.Sleep(100);
                Actions.SelectListBoxItemByText(QBSetupWindow, "lstBox_Industry", "Information Technology");
                Actions.SelectComboBoxItemByText(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.TaxStructure_CmbBox_AutoID, "Corporation");
                Actions.SetTextByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.TaxID_TxtField_AutoID, "123-45-6789");
                Actions.SelectComboBoxItemByText(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.HaveEmployees_CmbBox_AutoID, "No");
                Actions.ClickElementByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.Continue_Button_AutoID);
                qbApp.WaitWhileBusy();
                Thread.Sleep(500);

                Actions.SelectComboBoxItemByText(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.StateName_CmbBox_AutoID, "DE");
                Actions.SetTextByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.ZipCode_TxtField_AutoID, "DE123");
                Actions.SetTextByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.Phone_TxtField_AutoID, "6104567890");
                Actions.ClickElementByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.CreateCompany_Button_AutoID);
                qbApp.WaitWhileBusy();
                Thread.Sleep(10000);
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
