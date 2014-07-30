using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrameworkLibraries.Utils;
using System.Windows.Automation;
using System.Windows.Forms;
using FrameworkLibraries.ActionLibs.QBDT;
using TestStack.White.UIItems.WindowItems;
using System.Threading;
using TestStack.White.UIItems.Finders;
using FrameworkLibraries.ActionLibs.QBDT.WhiteAPI;
using FrameworkLibraries;
using System.Collections.Generic;
using TestStack.White.UIItems;

namespace QBBATS.Tests
{
    [TestClass]
    public class UpgradeFile
    {
        public TestStack.White.Application qbApp = null;
        public TestStack.White.UIItems.WindowItems.Window qbWindow = null;
        public Thread ExceptionHandler = null;
        public static String startupPath = System.IO.Path.GetFullPath("..\\..\\..\\");
        public static Property conf = new Property(startupPath + "\\QBAutomation.properties");
        public string exe = conf.get("QBExePath");
        public string qbLoginUserName = conf.get("QBLoginUserName");
        public string qbLoginPassword = conf.get("QBLoginPassword");
        public string companyFilePath = startupPath + "Bel_R5.qbw";
        public Random rand = new Random();
        public string testName = "UpgradeCompanyFile";
        public string moduleName = "BATS";
        public string exception = "Null";
        public string category = "Null";

        [TestInitialize]
        public void TestInitialize()
        {
            qbApp = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.Initialize(exe);
            qbWindow = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.PrepareBaseState(qbApp, qbLoginUserName, qbLoginPassword);
        }

        [TestMethod]
        public void TestMethod1()
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
                Window updateCompanyFileWindow = FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Update Company File for New Version");
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(updateCompanyFileWindow, "I understand that my company file will be updated to this new version of QuickBooks.");
                //updateCompanyFileWindow.Items[1].Click();
                //FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.SendTABToWindow(updateCompanyFileWindow);
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(updateCompanyFileWindow, "Update Now");
                
                try { FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "QuickBooks Information"), "OK"); }
                catch(Exception){}

                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Create Backup"), "Next");

                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.SetTextByAutomationID(FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Create Backup"), "2002", "C:\\");


            }
            catch (Exception e)
            {
                exception = TestResults.TrimExceptionMessage(e.Message);
            }
        }

        [TestCleanup]
        public void TestFinalize()
        {
            FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.ExceptionHandler();
            TestResults.GetTestResult(testName, moduleName, exception, category);
        }
    }
}
