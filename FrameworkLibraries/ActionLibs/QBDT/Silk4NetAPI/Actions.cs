//using SilkTest.Ntf;
//using SilkTest.Ntf.Wpf;
//using SilkTest.Ntf.WindowsForms;
//using System;
//using System.Collections.Generic;
//using System.Threading;
//using FrameworkLibraries.Utils;

//namespace FrameworkLibraries.ActionLibs.QBDT.Silk4NetAPI
//{
//    public class Actions
//    {
//        public static String startupPath = System.IO.Path.GetFullPath("..\\..\\..\\");
//        public static Property conf = new Property(startupPath + "\\QBAutomation.properties");
//        public static string Execution_Speed = conf.get("ExecutionSpeed");
//        private static Desktop desktop = Agent.Desktop;

//        //************************************************************************************************************************************************************
//        public static bool ActivateWindow(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to activate window :" + element);

//                desktop.Window(element).SetActive();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Activate Window : " + element + " successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;

//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool ClickTextOnControl(String element, String value)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to click :" + element + " with text :" + value);
//                desktop.Control(element).TextClick(value);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Click on element :" + element + " with text :" + value + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool ClickTextOnWindow(String element, String value)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to click :" + element + " with text :" + value);
//                desktop.Window(element).TextClick(value);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Click on element :" + element + " with text :" + value + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool ClickWPFButton(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to click :" + element);
//                desktop.WPFButton(element).Select();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Click on element :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool SetTextOnWPFelement(String element, String value)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to set text on :" + element + " with value :" + value);
//                desktop.TextField(element).SetText(value);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Set text on element :" + element + " with value :" + value + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool SelectWPFListboxValue(String element, String value)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to select list :" + element + " with value :" + value);
//                desktop.ListBox(element).Select(value);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Select list :" + element + " with value :" + value + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool SelectWPFComboBoxValue(String element, String value)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to select combo box :" + element + " with value :" + value);
//                desktop.ComboBox(element).Select(value);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Select combo box :" + element + " with value :" + value + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool CheckControlExists(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying check if the control exists :" + element);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return desktop.Exists(element);
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool CloseWindow(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying close window :" + element);
//                desktop.Window(element).Close();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Close window :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static List<TextField> GetAllTextControls(String element)
//        {

//            try
//            {
//                List<TextField> textControls = desktop.FindAll<TextField>(element);
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("There are " + textControls.Count + " text controls in this window");
//                Console.WriteLine("---------------------------------------------------------------------------------");

//                foreach (TextField item in textControls)
//                {
//                    Console.WriteLine("---------------------------------------------------------------------------------");
//                    List<String> property = item.GetPropertyList();
//                    foreach (String i in property)
//                    {
//                        Console.WriteLine(i);
//                    }
//                    Console.WriteLine("---------------------------------------------------------------------------------");
//                }
//                return textControls;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }

//        }

//        //************************************************************************************************************************************************************


//        public static bool CheckWindowExists(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying check if the window exists :" + element);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return desktop.Exists(element);

//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool CheckWPFWindowExists(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying check if the window exists :" + element);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return desktop.WPFWindow(element).Exists();

//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//     /*   public static bool SetText(String element, String value)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to set text on :" + element + " with value :" + value);
//                desktop.TextField(element).SetText(value);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Set text on element :" + element + " with value :" + value + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        } */

//        //************************************************************************************************************************************************************

//        public static bool ClickControl(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to click :" + element);
//                desktop.Control(element).Click();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Click on element :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool MouseLeftclickControl(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to click :" + element);
//                desktop.Control(element).Click(MouseButton.Left);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Click on element :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static List<TestObject> GetAllChildElements(String element)
//        {

//            try
//            {
//                List<TestObject> allelements = desktop.Control(element).GetChildren();
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("There are " + allelements.Count + " child elements in this window");
//                Console.WriteLine("---------------------------------------------------------------------------------");

//                foreach (TestObject item in allelements)
//                {
//                    Console.WriteLine("---------------------------------------------------------------------------------");
//                    Console.WriteLine("Enabled = " + item.GetProperty("Enabled"));
//                    Console.WriteLine("Text = " + item.GetProperty("Text"));
//                    Console.WriteLine("Value = " + item.GetProperty("Value"));
//                    Console.WriteLine("Visible = " + item.GetProperty("Visible"));
//                    Console.WriteLine("---------------------------------------------------------------------------------");
//                }
//                return allelements;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }

//        }
//        //************************************************************************************************************************************************************

//        //public static void TransactionListGridSelection(String column, String matchText, String payee, String account, int iteration)
//        //{
//        //    try
//        //    {
//        //        //TO FIND a COLUMN AND ITS INDEX
//        //        List<WPFToolkitDataGridColumnHeader> columns = desktop.WPFToolkitDataGrid(BankFeeds.Transaction_List_Grid).Columns;
//        //        int c = columns.Count;
//        //        int matchingColumnIndex = 0;
//        //        foreach (WPFToolkitDataGridColumnHeader col in columns)
//        //        {
//        //            if (col.Text.Equals("DOWNLOADED AS"))
//        //            {
//        //                matchingColumnIndex = col.TabIndex;
//        //            }
//        //        }

//        //        // TO FIND THE ROW COUNT
//        //        List<TestObject> checkBoxes = desktop.FindAll<TestObject>(BankFeeds.TRL_Grid_Checkboxes);
//        //        int r = checkBoxes.Count;

//        //        for (int i = 0; i < r - 1; i++)
//        //        {
//        //            desktop.WPFToolkitDataGrid(BankFeeds.Transaction_List_Grid).SelectCell(i, 1);

//        //            if (desktop.WPFToolkitDataGrid(BankFeeds.Transaction_List_Grid).SelectedItemText.Contains(matchText))
//        //            {
//        //                desktop.WPFToolkitDataGrid(BankFeeds.Transaction_List_Grid).SelectCell(i, 7);
//        //                desktop.WPFToolkitDataGrid(BankFeeds.Transaction_List_Grid).SelectCell(i, 6);
//        //               Thread.Sleep(1000);
//        //                desktop.WPFComboBox(BankFeeds.TRL_Grid_Payee_WPFComboBox).TypeKeys(payee);
//        //                desktop.WPFToolkitDataGrid(BankFeeds.Transaction_List_Grid).SelectCell(i, 7);
//        //                desktop.ComboBox(BankFeeds.TRL_Grid_Account_ComboBox).TypeKeys(account);
//        //               Thread.Sleep(1000);
//        //                desktop.WPFComboBox(BankFeeds.TRL_Grid_Action_WPFComboBox).SetFocus();
//        //                desktop.WPFComboBox(BankFeeds.TRL_Grid_Action_WPFComboBox).Click();
//        //                desktop.WPFComboBoxItem(BankFeeds.TRL_Grid_Action_QuickAdd_WPFComboBoxItem).Select();
//        //                break;
//        //            }
//        //        }
//        //    }
//        //    catch (Exception e)
//        //    {
//        //        String sMessage = e.Message;
//        //        LastException.SetLastError(sMessage);
//        //        throw new Exception(sMessage);
//        //    }
//        //}

//        //************************************************************************************************************************************************************

//        public static bool ActivateDialog(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to activate dialog :" + element);

//                desktop.Dialog(element).SetActive();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Activate dialog : " + element + " successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;

//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool ClickButton(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to click :" + element);
//                desktop.PushButton(element).Select();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Click on element :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool CheckExists(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying check if the object exists :" + element);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return desktop.Exists(element);

//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static List<WPFListViewItem> GetAllWPFListViewControls(String element)
//        {

//            try
//            {
//                List<WPFListViewItem> listViewControls = desktop.FindAll<WPFListViewItem>(element);
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("There are " + listViewControls.Count + " text controls in this window");
//                Console.WriteLine("---------------------------------------------------------------------------------");

//                foreach (WPFListViewItem item in listViewControls)
//                {
//                    Console.WriteLine("---------------------------------------------------------------------------------");
//                    List<String> property = item.GetPropertyList();
//                    foreach (String i in property)
//                    {
//                        Console.WriteLine(i);
//                    }
//                    Console.WriteLine("---------------------------------------------------------------------------------");
//                }
//                return listViewControls;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }

//        }

//        //************************************************************************************************************************************************************

//        public static bool SelectWPFListViewItem(List<WPFListViewItem> element, String item)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to select an item :" + item + " form ListView collection :" + element);

//                foreach (WPFListViewItem i in element)
//                {
//                    if (i.GetProperty("Text").ToString().Contains(item))
//                    {
//                        i.Click();
//                        break;
//                    }
//                }

//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Select list :" + element + " with value :" + item + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************


//        public static bool CloseAllOpenQBWindows()
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to close all open windows inside QuickBooks");

//                List<Window> windows = desktop.FindAll<Window>("/Window//Window");
//                List<WPFWindow> wpfwindows = desktop.FindAll<WPFWindow>("//WPFWindow");

//                foreach (Window i in windows)
//                {
//                    i.Close();
//                    Thread.Sleep(int.Parse(Execution_Speed));
//                }

//                foreach (WPFWindow j in wpfwindows)
//                {
//                    j.Click();
//                    j.Close();
//                    Thread.Sleep(int.Parse(Execution_Speed));
//                }


//                Console.WriteLine("Close all windows inside QB Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                desktop.Find<Window>("/Window").Close();
//                //QBInitialize.InitQB();
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool SelectPushButton(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to select push button :" + element);
//                desktop.PushButton(element).Select();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Select push button :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool SelectValueFromWPFToogleControl(String element, String value)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to select value :" + value + " from element : " + element);
//                desktop.WPFToggleButton(element).Check();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                desktop.WPFToggleButton(element).OpenContextMenu();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                //desktop.WPFMenuItem(BankFeeds.TRL_Batch_Actions_Add_WPFMenuItem).Click();
//                //desktop.WPFContextMenu(BankFeeds.TRL_Batch_Actions_ContextMenu).Select(value);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Select value : " + value + " on element : " + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool ClickTextBlock(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to select wpf text block :" + element);
//                desktop.WPFTextBlock(element).Click();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Select wpf text block :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool CheckWPFToggleButton(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to check toggle button :" + element);
//                desktop.WPFToggleButton(element).Check();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Check toggle button :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool SelectContextMenu(String element, String value)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to select value " + value + "from context menu :" + element);
//                desktop.WPFContextMenu(element).Select(value);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Select value from context menu :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool SelectWPFButton(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to select wpf button :" + element);
//                desktop.WPFButton(element).Select();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Select wpf button :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool ClickWPFComboBox(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to click wpf combobox :" + element);
//                desktop.WPFComboBox(element).Click();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("click wpf combobox :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool ClickWPFComboBoxItem(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to click wpf combobox item :" + element);
//                desktop.WPFComboBoxItem(element).Click();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("click wpf combobox item :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool ActivateWPFWindow(String element)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to activate wpf window :" + element);
//                desktop.WPFWindow(element).Activate();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Activate wpf window :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool SetTextOnWPFTextBox(String element, String value)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to set text on wpf textbox :" + element + " with value : " + value);
//                desktop.WPFTextBox(element).SetText(value);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Set text on wpf textbox :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool SelectValueFromWPFListView(String element, String value)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to select wpf list view :" + element + " with value : " + value);
//                desktop.WPFListView(element).Select(value);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Select value from wpf list view :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool SetTextOnWPFPasswordBox(String element, String value)
//        {
//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                Console.WriteLine("Trying to set text on wpf textbox :" + element + " with value : " + value);
//                desktop.WPFPasswordBox(element).SetText(value);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                Console.WriteLine("Set password on wpf password box :" + element + " Successful");
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool SelectWPFComboBoxRelativeToWPFCell(String ComboBox, String GridCell, String CellText, String ComboValue)
//        {

//            try
//            {
//                Console.WriteLine("---------------------------------------------------------------------------------");

//                List<WPFToolkitDataGridCell> GridCells = desktop.FindAll<WPFToolkitDataGridCell>(GridCell);
//                int CellCount = 0;
//                foreach (WPFToolkitDataGridCell cell in GridCells)
//                {
//                    CellCount++;
//                    var text = cell.GetProperty("Text");
                    
//                    if(cell.GetProperty("Text").ToString().Contains("Select existing or create new"))
//                        CellCount--;

//                    if (cell.GetProperty("Text").ToString().Contains(CellText))
//                        break;
//                }


//                List<WPFComboBox> ComboBoxes = desktop.FindAll<WPFComboBox>(ComboBox);
//                int BoxCount = 0;
//                foreach (WPFComboBox box in ComboBoxes)
//                {
//                    BoxCount++;
//                    if (BoxCount.Equals(CellCount))
//                    {
//                        box.Select(ComboValue);
//                        break;
//                    }
//                }

//                Thread.Sleep(int.Parse(Execution_Speed));                
//                Console.WriteLine("---------------------------------------------------------------------------------");
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool ScrollMax(String element)
//        {
//            try
//            {
//                desktop.VerticalScrollBar(element).ScrollToMax();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool ScrollMin(String element)
//        {
//            try
//            {
//                desktop.VerticalScrollBar(element).ScrollToMin();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool ScrollToPoint(String element, int point)
//        {
//            try
//            {
//                desktop.VerticalScrollBar(element).Scroll(point);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************


//        public static bool ClickWindow(String element)
//        {
//            try
//            {
//                desktop.Window(element).Click();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************


//        public static bool ClickScrollBar(String element)
//        {
//            try
//            {
//                desktop.VerticalScrollBar(element).Click();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool ScrollBarPageDown(String element)
//        {
//            try
//            {
//                desktop.VerticalScrollBar(element).PageDown();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool ScrollBarPageDownToLast(String element)
//        {
//            try
//            {
//                desktop.VerticalScrollBar(element).Scroll(1000);
//                for (int i = 0; i <= 5; i++)
//                {
//                    desktop.VerticalScrollBar(element).PageDown();
//                }
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool SetFocusOnFormsWindow(String element)
//        {
//            try
//            {
//                desktop.FormsWindow(element).SetFocus();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool SetText(String element, String value)
//        {
//            try
//            {
//                desktop.TextField(element).SetText(value);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool ClickPushButton(String element)
//        {
//            try
//            {
//                desktop.PushButton(element).Click();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//     /*   public static bool SelectPushButton(String element)
//        {
//            try
//            {
//                desktop.PushButton(element).Select();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        } */

//        //************************************************************************************************************************************************************

//        public static bool SelectRadioList(String element)
//        {
//            try
//            {
//                desktop.RadioList(element).Select();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool CheckCheckBox(String element)
//        {
//            try
//            {
//                desktop.CheckBox(element).Check();
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static String GetTextFromTextField(String element)
//        {
//            try
//            {
//                var text = desktop.TextField(element).Value;
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return text.ToString();
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool WaitForObjectLoad(String element, int wait)
//        {
//            try
//            {
//                desktop.WaitForObject(element, wait);
//                Thread.Sleep(int.Parse(Execution_Speed));
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool FindLabel(String text)
//        {
//            try
//            {
//                List<Label> labels = desktop.FindAll<Label>("//Label");
//                foreach (Label item in labels)
//                {
//                    if (item.Text.Contains(text))
//                    {
//                        return true;
//                    }
//                }
//                return false;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************

//        public static bool CloseFormsWindow(String element)
//        {
//            try
//            {
//                desktop.FormsWindow(element).Close();
//                return true;
//            }
//            catch (Exception e)
//            {
//                String sMessage = e.Message;
//                LastException.SetLastError(sMessage);
//                throw new Exception(sMessage);
//            }
//        }

//        //************************************************************************************************************************************************************


//    }
//}
