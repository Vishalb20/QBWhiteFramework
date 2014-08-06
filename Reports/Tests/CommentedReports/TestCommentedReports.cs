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
using System.Collections.Generic;
using FrameworkLibraries.EntityFramework;
using System.Windows;


namespace Reports.Tests.CommentedReports
{
    [TestClass]
    public class TestCommentedReports
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
        public string testName = "TestCommentedReports";
        public string moduleName = "Reports";
        public string exception = "Null";
        public string category = "Null";

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
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.SelectMenu(qbApp, qbWindow, "Reports", "Company & &Financial", "Profit & Loss &Standard");
                TestStack.White.UIItems.WindowItems.Window profitAndLossWindow = FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Profit  Loss");
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(profitAndLossWindow, "Maximize");
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(profitAndLossWindow, "Comment on Report");
                TestStack.White.UIItems.WindowItems.Window commentOnProfitAndLossWindow = FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetChildWindow(qbWindow, "Comment on Report: Profit  Loss");
                
                List<IUIItem> allPanes = FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.GetAllPanels(commentOnProfitAndLossWindow.Items);
                //var p = allPanes[5].Location;
                //var x = p.X + 562;
                //var y = p.Y + 162;
                System.Windows.Point commentsPostion = new System.Windows.Point(1287, 179);
                TestStack.White.InputDevices.IMouse m = TestStack.White.Desktop.Instance.Mouse;
                m.DoubleClick(commentsPostion);
                m.DoubleClick(commentsPostion);

                //var appWindow_UIA = FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.UIA_GetAppWindow(qbWindow.Name);
                //var chilWindow_UIA = FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.UIA_GetChildWindow(appWindow_UIA, commentOnProfitAndLossWindow.Name);
                //FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.UIA_ClickOnPaneItem(chilWindow_UIA, commentOnProfitAndLossWindow, 1);

                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.SetTextByAutomationID(commentOnProfitAndLossWindow, "CommentTextBox", "Comment from WhiteAPI");
                FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(commentOnProfitAndLossWindow, "Save");
                //FrameworkLibraries.ActionLibs.QBDT.Silk4NetAPI.Actions.CloseAllOpenQBWindows();
            }
            catch (Exception e)
            {
                exception = TestResults.TrimExceptionMessage(e.Message);
            }
            finally
            {
                FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.ExceptionHandler(qbWindow);
                TestResults.GetTestResult(testName, moduleName, exception, category);
            }
        }
    }
}
