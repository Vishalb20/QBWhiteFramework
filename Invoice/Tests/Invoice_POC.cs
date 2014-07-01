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
using FrameworkLibraries.EntityFramework;


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
                using (SQLCompactDBEntities entity = new SQLCompactDBEntities())
                {
                    var TestData = entity.Invoice_TestData.Find("Invoice_POC");
                    var Customer = TestData.Customer_Job;
                    var Class = TestData.Class;
                    var Account = TestData.Account;
                    var Template = TestData.Template;
                    var REP = TestData.REP;
                    var FOB = TestData.FOB;
                    var VIA = TestData.VIA;
                    var Item = TestData.Item;
                    var Quantity = TestData.Qunatity;
                    var ItemDescription = TestData.Description;

                    FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.CreateInvoice(qbApp, qbWindow, Customer, Class, Account, Template, invoiceNumber,
                        poNumber, REP, VIA, FOB, Quantity, Item, ItemDescription, false);

                    FrameworkLibraries.ActionLibs.QBDT.Silk4NetAPI.Actions.CloseAllOpenQBWindows();
                }    
            }
            catch (Exception e)
            {
                exception = TestResults.TrimExceptionMessage(e.Message);
            }
            finally
            {
                FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.ExceptionHandler();
                TestResults.GetTestResult(testName, moduleName, status, exception, category);
            }
        }
    }
}
