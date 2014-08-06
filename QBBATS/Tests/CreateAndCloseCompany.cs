using System;
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


namespace BATS.Tests
{
    public class CreateAndCloseCompany : IDisposable
    {
        public TestStack.White.Application qbApp = null;
        public TestStack.White.UIItems.WindowItems.Window qbWindow = null;
        public static String startupPath = System.IO.Path.GetFullPath("..\\..\\..\\");
        public static Property conf = new Property(startupPath + "\\QBAutomation.properties");
        public string exe = conf.get("QBExePath");
        public string qbLoginUserName = conf.get("QBLoginUserName");
        public string qbLoginPassword = conf.get("QBLoginPassword");
        public Random rand = new Random();
        public string testName = "CreateAndCloseCompany";
        public string moduleName = "BATS";
        public string exception = "Null";
        public string category = "Null";

        public CreateAndCloseCompany()
        {
            qbApp = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.Initialize(exe);
            qbWindow = FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.PrepareBaseState(qbApp, qbLoginUserName, qbLoginPassword);
        }

        [Fact]
        [Category("P1")]
        public void CreateAndCloseCompanyTest()
        {
                string BusinessName = "White" + rand.Next(1234, 8976);

                var Window_Condition = Actions.CheckWindowExists(qbWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.NoQBCompanyLoaded_Window_Name);

                if (!Window_Condition)
                {
                    Actions.SelectMenu(qbApp, qbWindow, "File", "New Company...");
                    qbApp.WaitWhileBusy();
                    Thread.Sleep(5000);
                }
                else
                {
                    Window NoCompanyLoadedWindow = Actions.GetChildWindow(qbWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.NoQBCompanyLoaded_Window_Name);

                    //Select - Create a new company
                    Actions.ClickElementByAutomationID(NoCompanyLoadedWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.CreateNewCompany_Element_AutoID);
                    qbApp.WaitWhileBusy();
                    Thread.Sleep(5000);
                }

                Window QBSetupWindow = Actions.GetChildWindow(qbWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.QBSetup_Window_Name);
                qbApp.WaitWhileBusy();
                Thread.Sleep(5000);

                Actions.ClickElementByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.ExpressStart_Button_AutoID);
                qbApp.WaitWhileBusy();
                Thread.Sleep(500);

                Actions.SetTextByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.BusinessName_TxtField_AutoID, BusinessName);
                Actions.SetTextByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.IndustryList_TxtField_AutoID, "Information");
                Thread.Sleep(100);
                Actions.SelectListBoxItemByText(QBSetupWindow, "lstBox_Industry", "Information Technology");
                Actions.SelectComboBoxItemByText(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.TaxStructure_CmbBox_AutoID, "Corporation");
                Actions.SetTextByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.TaxID_TxtField_AutoID, "123-45-6789");
                Actions.SelectComboBoxItemByText(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.HaveEmployees_CmbBox_AutoID, "No");
                Actions.ClickElementByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.Continue_Button_AutoID);
                qbApp.WaitWhileBusy();
                Thread.Sleep(500);

                Actions.SelectComboBoxItemByText(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.StateName_CmbBox_AutoID, "DE");
                Actions.SetTextByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.ZipCode_TxtField_AutoID, "DE123");
                Actions.SetTextByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.Phone_TxtField_AutoID, "6104567890");
                Actions.ClickElementByAutomationID(QBSetupWindow, FrameworkLibraries.ObjMaps.QBDT.WhiteAPI.Common.Objects.CreateCompany_Button_AutoID);
                qbApp.WaitWhileBusy();
                Thread.Sleep(10000);
                Actions.CloseAllChildWindows(qbWindow);
                Thread.Sleep(10000);
                var winTitleOfNewCompany = qbWindow.Title;
                Assert.Equal(winTitleOfNewCompany, qbWindow.Title);
                Actions.SelectMenu(qbApp, qbWindow, "File", "Close Company");
                Thread.Sleep(10000);
                Assert.NotEqual(winTitleOfNewCompany, qbWindow.Title);
        }

        public void Dispose()
        {
            FrameworkLibraries.AppLibs.QBDT.WhiteAPI.QuickBooks.ExceptionHandler(qbWindow);
        }
    }
}
