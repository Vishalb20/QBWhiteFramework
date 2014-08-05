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
using Xunit.Extensions;

namespace BATS.Tests
{
    public class UpgradeFile : IDisposable
    {
        public TestStack.White.Application qbApp = null;
        public TestStack.White.UIItems.WindowItems.Window qbWindow = null;
        public Thread ExceptionHandler = null;
        public static String startupPath = System.IO.Path.GetFullPath("..\\..\\..\\");
        public static Property conf = new Property(startupPath + "\\QBAutomation.properties");
        public string exe = conf.get("QBExePath");
        public string qbLoginUserName = conf.get("QBLoginUserName");
        public string qbLoginPassword = conf.get("QBLoginPassword");
        public string companyFilePath = null;
        public Random rand = new Random();
        public string testName = "UpgradeCompanyFile";
        public string moduleName = "BATS";
        public string exception = "Null";
        public string category = "Null";

        public UpgradeFile()
        {
            qbApp = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.Initialize(exe);
            qbWindow = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.PrepareBaseState(qbApp, qbLoginUserName, qbLoginPassword);
        }

        [Theory]
        [Category("P1")]
        [PropertyData("TestData", PropertyType = typeof(UpgradeTestDataSource))]
        public void UpgradeFileTest(string fileName)
        {
                companyFilePath = startupPath + fileName;
                FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.OpenOrUpgradeCompanyFile(companyFilePath, qbApp, qbWindow);
        }

        public void Dispose()
        {
            FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.ExceptionHandler();
        }
    }
}
