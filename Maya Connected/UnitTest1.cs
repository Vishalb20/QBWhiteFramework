using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FrameworkLibraries.ActionLibs.WhiteAPI;
using FrameworkLibraries.Utils;

namespace Maya_Connected
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Logger log = new Logger("Maya");

            var mayaApp = FrameworkLibraries.AppLibs.MayaConnected.Maya.GetApp("WPF.MDI Example");
            var mayaWindow = Actions.GetDesktopWindow("WPF.MDI Example");

            Actions.SelectMenu(mayaApp, mayaWindow, "Windows", "Add Window");

            var childWindows = mayaWindow.ModalWindows();



            foreach (var win in childWindows)
            {
                //Actions.ClickElementByAutomationID(win, "MaximizeButton");
                var elements = win.Items;
            }



        }
    }
}
