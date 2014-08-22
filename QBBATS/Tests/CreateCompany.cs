using System;
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
using Xunit;
using TestStack.BDDfy;
using FrameworkLibraries.AppLibs.QBDT.WhiteAPI;
using System.IO;


namespace BATS.Tests
{
    public class CreateCompany
    {
        public TestStack.White.Application qbApp = null;
        public TestStack.White.UIItems.WindowItems.Window qbWindow = null;
        public static string startupPath = Directory.GetCurrentDirectory();
        //public static String startupPath = System.IO.Path.GetFullPath("..\\..\\..\\");
        public static Property conf = new Property(startupPath + "\\QBAutomation.properties");
        public string exe = conf.get("QBExePath");
        public string qbLoginUserName = conf.get("QBLoginUserName");
        public string qbLoginPassword = conf.get("QBLoginPassword");
        public Random rand = new Random();
        public string testName = "CreateAndCloseCompany";
        public string moduleName = "BATS";
        public string exception = "Null";
        public string category = "Null";

        [Given(StepTitle = "Given - QuickBooks App and Window instances are available")]
        public void Setup()
        {
            MessageBox.Show(startupPath);
            qbApp = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.Initialize(exe);
            qbWindow = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.PrepareBaseState(qbApp);
            QuickBooks.ResetQBWindows(qbApp, qbWindow, false);
        }

        [Then(StepTitle = "Then - Create new company file should be successful")]
        public void CreateAndCloseCompany()
        {
            Actions.SelectMenu(qbApp, qbWindow, "Reports", "Commented Reports");
            Window cr = Actions.GetChildWindow(qbWindow, "Commented Reports");
            Actions.SelectMenu(qbApp, cr, "Commented Reports", "Edit Commented Report");


            string businessName = "White" + rand.Next(1234, 8976);
            QuickBooks.CreateCompany(qbApp, qbWindow, businessName, "Information Technology");
            QuickBooks.ResetQBWindows(qbApp, qbWindow, false);
            var winTitleOfNewCompany = qbWindow.Title;
            Assert.Equal(winTitleOfNewCompany, qbWindow.Title);
        }

        [AndThen(StepTitle = "AndThen - Perform tear down activities to ensure that there are no on-screen exceptions")]
        public void TearDown()
        {
            //QuickBooks.ResetQBWindows(qbApp, qbWindow);
        }

        [Fact]
        [Category("P1")]
        public void RunCreateCompanyTest()
        {
            this.BDDfy();
        }
    }
}
