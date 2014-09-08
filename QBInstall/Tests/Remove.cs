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


namespace QBInstall.Tests
{
    public class Remove
    {
        public TestStack.White.Application qbApp = null;
        public TestStack.White.UIItems.WindowItems.Window qbWindow = null;
        public static Property conf = Property.GetPropertyInstance();
        public static int Sync_Timeout = int.Parse(conf.get("SyncTimeOut"));
        public static string testName = "UnInstall";

        [Given]
        public void Setup()
        {
            var timeStamp = DateTimeOperations.GetTimeStamp(DateTime.Now);
            Logger log = new Logger(testName + "_" + timeStamp);

            //Repair
            QuickBooks.RepairOrUnInstallQB("QuickBooks Pro 2015", false, true);
        }

        [Fact]
        public void RunQBRemoveTest()
        {
            this.BDDfy();
        }
    }
}
