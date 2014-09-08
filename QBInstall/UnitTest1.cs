using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrameworkLibraries.AppLibs.QBDT.WhiteAPI;
using FrameworkLibraries.ActionLibs.QBDT.WhiteAPI;
using FrameworkLibraries.Utils;
using TestStack.White;

namespace QBInstall
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Logger log = new Logger("Payments");
            var qbApp = QuickBooks.GetApp("QuickBooks");
            var qbWindow = QuickBooks.GetAppWindow(qbApp, "QuickBooks");

            var allWin = qbWindow.ModalWindows();

            var QBwin = Actions.GetChildWindow(qbWindow, "QuickBooks Payments");
            var win = Actions.GetChildWindow(qbWindow, "Record Merchant Service Deposits");

            Actions.ClickElementByAutomationID(Actions.GetChildWindow(qbWindow, "Record Merchant Service Deposits"), "FILTER_UNMATCHED");

            //Actions.SelectMenu(qbApp, qbWindow, "Banking", "Make Merchant Service Deposits");
            //var windows = qbWindow.ModalWindows();

            //var merchWindow = Actions.GetChildWindow(qbWindow, "Record Merchant Service Deposits");
            //var uiaWindow = Actions.UIA_GetAppWindow(qbWindow.Name);
            //var uiaMerchWindow = Actions.UIA_GetChildWindow(uiaWindow, "Record Merchant Service Deposits");
            //var homeWindow = Actions.GetChildWindow(qbWindow, "Record Merchant Service Deposits");
            //var elements = homeWindow.Items;

            //Actions.WaitForElementVisible



        }
    }
}
