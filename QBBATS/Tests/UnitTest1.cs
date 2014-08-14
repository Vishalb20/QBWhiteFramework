using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;
using FrameworkLibraries.AppLibs.QBDT.WhiteAPI;
using TestStack.White;
using TestStack.White.UIItems.WindowItems;
using FrameworkLibraries.ActionLibs.QBDT.WhiteAPI;
using System.Windows.Forms;


namespace QBBATS.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestStack.White.Application qbApp = QuickBooks.GetApp("QuickBooks");
            Window qbWindow = QuickBooks.GetAppWindow(qbApp, "QuickBooks");

            if (Actions.WaitForChildWindow(qbWindow, "Customer", 10000))
                Actions.ClickElementByName(Actions.GetChildWindow(qbWindow, "Customer"), "Close");

            MessageBox.Show("Done..");
        }
    }
}
