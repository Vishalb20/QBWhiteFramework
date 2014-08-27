using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrameworkLibraries.AppLibs.QBDT.WhiteAPI;
using FrameworkLibraries.ActionLibs.QBDT.WhiteAPI;
using FrameworkLibraries.Utils;

namespace QBBATS.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Logger log = new Logger("Payments Test");
            var qbApp = QuickBooks.GetApp("QuickBooks");
            var qbWindow = QuickBooks.GetAppWindow(qbApp, "QuickBooks");
            var uiaWindow = Actions.UIA_GetAppWindow(qbWindow.Name);
            var payWindow = Actions.GetChildWindow(qbWindow, "QuickBooks Payments");
            Actions.UIA_ClickTextByName(uiaWindow, payWindow, "Information Technology (Computers, Software)");
        }
    }
}
