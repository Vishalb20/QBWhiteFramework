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
            Logger log = new Logger("alert test");
            var qbApp = QuickBooks.GetApp("QuickBooks");
            var qbWindow = QuickBooks.GetAppWindow(qbApp, "QuickBooks");

            var alert = Actions.GetAlertWindow("Alert");

            var elements = alert.Items;

            foreach(var item in elements)
            {
                if(item.GetType().Name.Equals("Label"))
                {
                    var text = item.Name;
                }
            }




        }
    }
}
