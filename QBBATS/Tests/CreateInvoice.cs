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
using SilkTest.Ntf;
using System.Collections.Generic;
using FrameworkLibraries.EntityFramework;
using Xunit;


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
        private static SilkTest.Ntf.Desktop desktop = Agent.Desktop;

        
        public CreateInvoice()
        {
            qbApp = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.Initialize(exe);
            qbWindow = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.PrepareBaseState(qbApp, qbLoginUserName, qbLoginPassword);
            invoiceNumber = rand.Next(12345, 99999);
            poNumber = rand.Next(12345, 99999);
        }

        [Fact]
        [Category("P3")]
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
                    var filePath = "C:\\Falcon_Pre_R7.qbw";

                    FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.OpenOrUpgradeCompanyFile(filePath, qbApp, qbWindow);

                    FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.CreateInvoice(qbApp, qbWindow, customer, clss, account, template, invoiceNumber,
                        poNumber, rep, via, fob, quantity, item, itemDescription, false);
                }    
        }
    }
}
