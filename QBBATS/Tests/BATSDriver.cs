using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrameworkLibraries.AppLibs.QBDT.WhiteAPI;
using TestStack.White.UIItems.WindowItems;
using System.Windows.Forms;
using System.Collections.Generic;
using FrameworkLibraries.Utils;
using BATS.Tests;

namespace QBBATS.Tests
{
    [TestClass]
    public class BATSDriver
    {
        public static String startupPath = System.IO.Path.GetFullPath("..\\..\\..\\");
        public static Property conf = new Property(startupPath + "\\QBAutomation.properties");
        public static string Execution_Speed = conf.get("ExecutionSpeed");
        public static string UserName = conf.get("QBLoginUserName");
        public static string Password = conf.get("QBLoginPassword");
        public static string DefaultCompanyFile = conf.get("DefaultCompanyFile");
        public static string DefaultCompanyFilePath = startupPath + DefaultCompanyFile;
        public static string TestDataSourceDirectory = conf.get("TestDataSourceDirectory");
        public static string TestDataLocalDirectory = conf.get("TestDataLocalDirectory");

        [TestMethod]
        public void BATSExecutionDriver()
        {
            CreateCompany c = new CreateCompany();
            c.RunCreateCompanyTest();
        }
    }
}
