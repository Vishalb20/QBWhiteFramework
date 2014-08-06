﻿using System;
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
using TestStack.BDDfy;

namespace BATS.Tests
{
    public class BddfyUpgradeFile
    {
        public TestStack.White.Application qbApp = null;
        public TestStack.White.UIItems.WindowItems.Window qbWindow = null;
        public Thread ExceptionHandler = null;
        public static String startupPath = System.IO.Path.GetFullPath("..\\..\\..\\");
        public static Property conf = new Property(startupPath + "\\QBAutomation.properties");
        public string exe = conf.get("QBExePath");
        public string qbLoginUserName = conf.get("QBLoginUserName");
        public string qbLoginPassword = conf.get("QBLoginPassword");
        public static string companyFilePath = null;
        public Random rand = new Random();
        public string testName = "UpgradeCompanyFile";
        public string moduleName = "BATS";
        public string exception = "Null";
        public string category = "Null";

        [Given(StepTitle = "Given - QuickBooks App and Window instances are available")]
        public void Setup()
        {
            qbApp = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.Initialize(exe);
            qbWindow = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.PrepareBaseState(qbApp, qbLoginUserName, qbLoginPassword);
        }


        [Then(StepTitle = "Then - A company file should be opened or upgraded successfully")]
        public void UpgradeCompanyFile()
        {
            FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.OpenOrUpgradeCompanyFile(companyFilePath, qbApp, qbWindow);
        }

        [AndThen(StepTitle = "AndThen - Perform tear down activities to ensure that there are no on-screen exceptions")]
        public void TearDown()
        {
            FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.ExceptionHandler(qbWindow);
        }

        [Theory]
        [Category("P1")]
        [PropertyData("TestData", PropertyType = typeof(UpgradeTestDataSource))]
        public void RunUpgradeCompanyFileTest(string fileName)
        {
            companyFilePath = startupPath + fileName;
            this.BDDfy();
        }
        
    }
}