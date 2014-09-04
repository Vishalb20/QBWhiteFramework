using System;
using FrameworkLibraries.Utils;
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
using System.Reflection;
using System.Diagnostics;


namespace QBInstall
{
    public class UnInstall
    {
        public TestStack.White.Application qbApp = null;
        public TestStack.White.UIItems.WindowItems.Window qbWindow = null;
        public static Property conf = Property.GetPropertyInstance();
        public static string Sync_Timeout = conf.get("SyncTimeOut");
        public static string testName = "UnInstall";

        [Given]
        public void Setup()
        {
            var timeStamp = DateTimeOperations.GetTimeStamp(DateTime.Now);
            Logger log = new Logger(testName + "_" + timeStamp);

            FrameworkLibraries.Utils.OSOperations.CommandLineExecute("control appwiz.cpl");
            Actions.WaitForWindow("Programs and Features", int.Parse(Sync_Timeout));

            var controlPanelWindow = Actions.GetDesktopWindow("Programs and Features");

            var uiaWindow = Actions.UIA_GetAppWindow("Programs and Features");
            Actions.UIA_SetTextByName(uiaWindow, controlPanelWindow, "Search Box", "QuickBooks Pro 2015");
            Actions.UIA_ClickItemByName(uiaWindow, controlPanelWindow, "QuickBooks Pro 2015");
            Actions.UIA_ClickItemByName(uiaWindow, controlPanelWindow, "Uninstall/Change");
            Actions.WaitForWindow("QuickBooks Installation", int.Parse(Sync_Timeout));
            var qbInstallWindow = Actions.GetDesktopWindow("QuickBooks Installation");
            Actions.WaitForElementEnabled(qbInstallWindow, "Next >", int.Parse(Sync_Timeout));

            
        }

        [Fact]
        public void RunCreateCompanyTest()
        {
            this.BDDfy();
        }
    }
}
