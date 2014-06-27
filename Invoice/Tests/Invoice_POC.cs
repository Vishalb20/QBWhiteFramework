using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrameworkLibraries.AppLibs.QBDT;
using FrameworkLibraries.Utils;
using FrameworkLibraries.ActionLibs.QBDT;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White;
using System.Threading;
using FrameworkLibraries.ActionLibs.QBDT.WhiteAPI;
using SilkTest.Ntf;
using System.Collections.Generic;


namespace Invoice.Tests
{
    [TestClass]
    public class Invoice_POC
    {
        public TestStack.White.Application qbApp = null;
        public TestStack.White.UIItems.WindowItems.Window qbWindow = null;
        public static String startupPath = System.IO.Path.GetFullPath("..\\..\\..\\");
        public static Property conf = new Property(startupPath + "\\QBAutomation.properties");
        public string exe = conf.get("QBExePath");
        public string qbLoginUserName = conf.get("QBLoginUserName");
        public string qbLoginPassword = conf.get("QBLoginPassword");
        public Random rand = new Random();
        public int invoiceNumber, poNumber;
        public string testName = "Invoice_POC";
        public string moduleName = "Invoice";
        public string status = "Fail";
        public string exception = "Null";
        public string category = "Null";
        private static SilkTest.Ntf.Desktop desktop = Agent.Desktop;


        [TestInitialize]
        public void TestInitialize()
        {
            qbApp = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.Initialize(exe);
            qbWindow = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.PrepareBaseState(qbApp, qbLoginUserName, qbLoginPassword);
            invoiceNumber = rand.Next(12345, 99999);
            poNumber = rand.Next(12345, 99999);
        }

        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.CreateInvoice(qbApp, qbWindow, "ABC", "100HT", "Accounts Receivable", "Intuit Product Invoice", invoiceNumber,
                    poNumber, "Net 15", "DHL", "FOB", "10", "Charter:Pilot exp Mik", "Test Automation", false);
                //Actions.SelectMenu(qbApp, qbWindow, "Customers", "Create Invoices");
                //List<TestObject> c = FrameworkLibraries.ActionLibs.QBDT.Silk4NetAPI.Actions.GetAllChildElements("//Window[@caption='Create Invoices - Accounts Receivable']");
                //FrameworkLibraries.ActionLibs.QBDT.Silk4NetAPI.Actions.ClickControl("//Control[@windowClassName='QuickBooksSubForm']//Control[3]");    
            }
            catch (Exception e)
            {
                exception = TestResults.TrimExceptionMessage(e.Message);
            }
            finally
            {
                TestResults.GetTestResult(testName, moduleName, status, exception, category);
            }
        }
    }
}
