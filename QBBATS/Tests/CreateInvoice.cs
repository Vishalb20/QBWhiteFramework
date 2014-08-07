using System;
using FrameworkLibraries.AppLibs.QBDT;
using FrameworkLibraries.Utils;
using FrameworkLibraries.ActionLibs.QBDT;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White;
using System.Threading;
using FrameworkLibraries.ActionLibs.QBDT.WhiteAPI;
using FrameworkLibraries.EntityFramework;
using Xunit;
using TestStack.BDDfy;
using FrameworkLibraries.AppLibs.QBDT.WhiteAPI;


namespace BATS.Tests
{
    
    public class CreateInvoice
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
        public string exception = "Null";
        public string category = "Null";
        public static string filePath = startupPath + "falcon.qbw";

        [Given(StepTitle = "Given - QuickBooks App and Window instances are available")]
        public void Setup()
        {
            qbApp = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.Initialize(exe);
            qbWindow = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.PrepareBaseState(qbApp);
            QuickBooks.ResetQBWindows(qbApp, qbWindow);
            invoiceNumber = rand.Next(12345, 99999);
            poNumber = rand.Next(12345, 99999);
        }

        [When(StepTitle = "When - A company file is opened or upgraded successfully for creating a transaction")]
        public void OpenCompanyFile()
        {
            FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.OpenOrUpgradeCompanyFile(filePath, qbApp, qbWindow);
        }

        [Then(StepTitle = "Then - An Invoice should be created successfully")]
        public void CreateInvoiceTest()
        {
            using (SQLCompactDBEntities entity = new SQLCompactDBEntities())
            {
                var testData = entity.Invoice_TestData.Find("CreateInvoice");
                var customer = testData.Customer_Job;
                var clss = testData.Class;
                var account = testData.Account;
                var template = testData.Template;
                var rep = testData.REP;
                var fob = testData.FOB;
                var via = testData.VIA;
                var item = testData.Item;
                var quantity = testData.Qunatity;
                var itemDescription = testData.Description;

                FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.CreateInvoice(qbApp, qbWindow, customer, clss, account, template, invoiceNumber,
                    poNumber, rep, via, fob, quantity, item, itemDescription, false);
            }
        }

        [AndThen(StepTitle = "AndThen - Perform tear down activities to ensure that there are no on-screen exceptions")]
        public void TearDown()
        {
            QuickBooks.ResetQBWindows(qbApp, qbWindow);
        }

        [Fact]
        [Category("P3")]
        public void RunCreateInovoiceTest()
        {
            this.BDDfy();
        }
    }
}
