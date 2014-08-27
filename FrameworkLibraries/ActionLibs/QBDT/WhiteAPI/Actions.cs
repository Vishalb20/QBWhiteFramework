using FrameworkLibraries.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WindowStripControls;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;
using System.IO;
using TestStack.White.UIItems.MenuItems;
using TestStack.White;
using System.Diagnostics;
using Xunit;

namespace FrameworkLibraries.ActionLibs.QBDT.WhiteAPI
{
    public class Actions
    {
        public static Property conf = Property.GetPropertyInstance();
        public static string Execution_Speed = conf.get("ExecutionSpeed");

//**************************************************************************************************************************************************************

        public static bool SelectMenu(TestStack.White.Application app, Window win, string level1, string level2)
        {
            try
            {
                MenuBar qbMenu = app.GetWindow(win.Name).MenuBar;
                qbMenu.MenuItem(level1, level2).Click();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("SelectMenu " + level1 + "->" + level2 + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");
                return true;
            }
            catch (Exception e)
            {
                Logger.logMessage("SelectMenu " + level1 + "->" + level2 + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }

//**************************************************************************************************************************************************************

        public static bool CheckMenuEnabled(TestStack.White.Application app, Window win, string level1)
        {
            try
            {
                MenuBar qbMenu = app.GetWindow(win.Name).MenuBar;
                var status = qbMenu.MenuItem(level1).Enabled;
                Logger.logMessage("CheckMenuEnabled " + level1 + "->" + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");
                return status;
            }
            catch (Exception e)
            {
                Logger.logMessage("CheckMenuEnabled " + level1 + "->" + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static bool SelectMenu(TestStack.White.Application app, Window win, string level1, string level2, string level3)
        {
            try
            {
                MenuBar qbMenu = app.GetWindow(win.Name).MenuBar;
                TestStack.White.UIItems.MenuItems.Menu m1 = qbMenu.MenuItem(level1);
                m1.SubMenu(level2).SubMenu(level3).Click();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("SelectMenu " + level1 + "->" + level2 + "->" + level3 + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");
                return true;
            }
            catch (Exception e)
            {
                Logger.logMessage("SelectMenu " + level1 + "->" + level2 + "->" + level3 + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }

//**************************************************************************************************************************************************************

        public static bool SelectMenu(TestStack.White.Application app, Window win, String[] args)
        {
            try
            {
                MenuBar qbMenu = app.GetWindow(win.Name).MenuBar;

                foreach (String item in args)
                {
                    qbMenu.MenuItem(item).Click();
                    Thread.Sleep(int.Parse(Execution_Speed));
                }
                return true;
            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static Window GetWindow(Window win, String winName)
        {
            Window window = null;

            try
            {
                List<Window> modalWins = win.ModalWindows();
                foreach (Window item in modalWins)
                {
                    Logger.logMessage(item.Name);

                    if(item.Name.Equals(winName) || item.Name.Contains(winName))
                    {
                        window = item;
                        window.Focus();
                        window.DoubleClick();
                        Thread.Sleep(int.Parse(Execution_Speed));
                    }
                }
                Logger.logMessage("GetWindow " + winName + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");
                return window;
                  
            }
            catch (Exception e)
            {
                Logger.logMessage("GetWindow " + winName + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static UIItemCollection GetWindowItems(Window win)
        {
            UIItemCollection items = null;

            try
            {
                items = win.Items;
                foreach(var item in items)
                {
                    Logger.logMessage(item.ToString());
                }
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("GetWindowItems " + win + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

                return items;
            }
            catch (Exception e)
            {
                Logger.logMessage("GetWindowItems " + win + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static void HighLightWindowElements(UIItemCollection collection)
        {
            try
            {
                foreach (IUIItem item in collection)
                {
                    try
                    {
                        item.RightClick();
                        Thread.Sleep(int.Parse(Execution_Speed));
                    }
                    catch (Exception)
                    {
                    }
                }
                Logger.logMessage("HighLightWindowElements " + collection + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("HighLightWindowElements " + collection + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static void ClickWPFButton(Window win, UIItemCollection collection, String text)
        {

            try
            {
                foreach (var item in collection)
                {
                    if (item.GetType().Name.Equals("WPFButton"))
                    {
                        AutomationProperty[] properties = item.AutomationElement.GetSupportedProperties();
                        foreach (AutomationProperty p in properties)
                        {
                            if (item.AutomationElement.GetCurrentPropertyValue(p).Equals(text))
                            {
                                item.Click();
                                Thread.Sleep(int.Parse(Execution_Speed));
                                break;
                            }
                        }
                    }
                    Logger.logMessage("ClickWPFButton " + win + "->" + collection + "->" + text + " - Successful");
                    Logger.logMessage("------------------------------------------------------------------------------");
                    Thread.Sleep(int.Parse(Execution_Speed));
                }
            }
            catch (Exception e)
            {
                Logger.logMessage("ClickWPFButton " + collection + "->" + text + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static bool ClickButtonByOrientation(UIItemCollection item, String identifier)
        {
            AutomationProperty p = AutomationElementIdentifiers.OrientationProperty;
            try
            {
                foreach (IUIItem element in item)
                {
                    if (element.GetType().Name.Contains("Button") || element.GetType().Name.Equals("Button"))
                    {
                        if (element.Name.Equals(identifier) || element.Id.Equals(identifier) || element.PrimaryIdentification.Equals(identifier))
                        {
                            element.Click();
                            Thread.Sleep(int.Parse(Execution_Speed));
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static void HighLightAndShowProperties(UIItemCollection item, String elementType)
        {
            String spy = null;

            try
            {
                foreach (var element in item)
                {
                    if (element.GetType().Name.Contains(elementType) || element.GetType().Name.Equals(elementType))
                    {
                        element.RightClick();
                        var name = element.Name;
                        var id = element.Id;
                        var pid = element.PrimaryIdentification;
                        MessageBox.Show(" Name= " + name + " ID= " + id + " PrimaryID= " + pid + " / ");
                        AutomationProperty[] properties = element.AutomationElement.GetSupportedProperties();
                        foreach (var p in properties)
                        {
                            try
                            {

                                var value = element.AutomationElement.GetCurrentPropertyValue(p);
                                var property = p.ProgrammaticName;
                                spy = spy + " -- " + value + " / " + property;
                            }
                            catch (Exception e)
                            {
                                var err = e.Message;
                            }
                        }
                        MessageBox.Show(spy.ToString());
                        spy = null;
                    }
                }
            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void ShowWindowElementTypes(Window win)
        {
            String spy = null;
            SortedSet<String> Types = new SortedSet<string>();

            try
            {
                UIItemCollection allElements = win.Items;

                foreach (var element in allElements)
                {
                    Types.Add(element.GetType().Name);
                }

                foreach (String item in Types)
                {
                    spy = spy + " / " + item;
                }

                MessageBox.Show(spy);
            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void GetCurrsorToFirstTextBox(Window win)
        {

            try
            {
                UIItemCollection allElements = win.Items;

                foreach (var element in allElements)
                {
                    if(element.GetType().Name.Equals("TextBox"))
                    {
                        element.Focus();
                        element.Click();
                        Thread.Sleep(int.Parse(Execution_Speed));
                        break;
                    }
                }
                Logger.logMessage("GetCurrsorToFirstTextBox " + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("GetCurrsorToFirstTextBox " + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void ClickElementByAutomationID(Window win, String automationID)
        {

            try
            {
                win.Get(SearchCriteria.ByAutomationId(automationID)).Click();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("ClickElementByAutomationID " + automationID + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");
            }
            catch (Exception e)
            {
                Logger.logMessage("ClickElementByAutomationID " + automationID + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }

//**************************************************************************************************************************************************************

        public static bool CheckElementExistsByAutomationID(Window win, String automationID)
        {
            bool exists = false;

            try
            {
                exists = win.Get(SearchCriteria.ByAutomationId(automationID)).Visible;
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("CheckElementExistsByAutomationID " + win + "->" + automationID + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");
                return exists;
            }
            catch (Exception e)
            {
                Logger.logMessage("CheckElementExistsByAutomationID " + win + "->" + automationID + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void ShowWindowElementAutomationIDs(Window win)
        {
            String spy = null;

            try
            {
                UIItemCollection elements = win.Items;

                foreach (IUIItem e in elements)
                {
                    AutomationProperty p = AutomationElementIdentifiers.AutomationIdProperty;
                    spy = spy + " / " + e.AutomationElement.GetCurrentPropertyValue(p).ToString();
                }
                MessageBox.Show(spy);
            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void SetFocusOnWindow(Window win)
        {

            try
            {
                win.Focus();
                win.Click();
                Logger.logMessage("SetFocusOnWindow " + win + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

                Thread.Sleep(int.Parse(Execution_Speed));
            }
            catch (Exception e)
            {
                Logger.logMessage("SetFocusOnWindow " + win + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void ClickElementByName(Window win, String name)
        {

            try
            {
                AutomationProperty p = AutomationElementIdentifiers.NameProperty;
                win.Get(SearchCriteria.ByNativeProperty(p, name)).Click();
                Logger.logMessage("ClickElementByName " + name + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");
                Thread.Sleep(int.Parse(Execution_Speed));
            }
            catch (Exception e)
            {
                Logger.logMessage("ClickElementByName " + win + "->" + name + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");

                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void ClickElementByMatchingName(Window win, String matchingName)
        {

            try
            {
                UIItemCollection allItems = win.Items;
                foreach (IUIItem item in allItems)
                {
                    if(item.Name.Contains(matchingName))
                    {
                        item.Click();
                    }
                }
                Logger.logMessage("ClickElementByMatchingName " + win + "->" + matchingName + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

                Thread.Sleep(int.Parse(Execution_Speed));
            }
            catch (Exception e)
            {
                Logger.logMessage("ClickElementByMatchingName " + win + "->" + matchingName + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");

                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void SetTextOnElementByName(Window win, String name, String value)
        {

            try
            {
                AutomationProperty p = AutomationElementIdentifiers.NameProperty;
                win.Get(SearchCriteria.ByNativeProperty(p, name)).Enter(value);
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("SetTextOnElementByName " + win + "->" + name + "->" + value + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("SetTextOnElementByName " + win + "->" + name + "->" + value + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void ClickButtonByAutomationID(Window win, String automationID)
        {

            try
            {
                TestStack.White.UIItems.Button b = (TestStack.White.UIItems.Button) win.Get(SearchCriteria.ByAutomationId(automationID));
                b.Click();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("ClickButtonByAutomationID " + win + "->" + automationID + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("ClickButtonByAutomationID " + win + "->" + automationID + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void ClickMenuItemByName(Window win, String name)
        {

            try
            {
                AutomationProperty p = AutomationElementIdentifiers.NameProperty;
                TestStack.White.UIItems.MenuItems.Menu b = (TestStack.White.UIItems.MenuItems.Menu)win.Get(SearchCriteria.ByNativeProperty(p, name));
                b.Click();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("ClickMenuItemByName " + win + "->" + name + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("ClickMenuItemByName " + win + "->" + name + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************


        public static void SetTextByAutomationID(Window win, String automationID, String value)
        {

            try
            {
                TestStack.White.UIItems.TextBox t = (TestStack.White.UIItems.TextBox)win.Get(SearchCriteria.ByAutomationId(automationID));
                t.SetValue(value);
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("SetTextByAutomationID " + win + "->" + automationID + "->" + value + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");
            }
            catch (Exception e)
            {
                Logger.logMessage("SetTextByAutomationID " + win + "->" + automationID + "->" + value + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void SetTextByName(Window win, String name, String value)
        {

            try
            {
                AutomationProperty p = AutomationElementIdentifiers.NameProperty;
                TestStack.White.UIItems.TextBox t = (TestStack.White.UIItems.TextBox)win.Get(SearchCriteria.ByNativeProperty(p, name));
                t.SetValue(value);
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("SetTextByName " + win + "->" + name + "->" + value + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("SetTextByName " + win + "->" + name + "->" + value + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void FileOutAutomationIDs(UIItemCollection collection, String fileName)
        {

            try
            {
                FileInfo test = new FileInfo(@fileName);
                string temp = string.Empty;

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@fileName))
                {
                    foreach (IUIItem item in collection)
                    {
                        try
                        {
                            AutomationProperty p = AutomationElementIdentifiers.NameProperty;
                            var element = item.AutomationElement.ToString();
                            var value = item.AutomationElement.GetCurrentPropertyValue(p).ToString();
                            temp = element + " / " + item.GetType().ToString() + " = " + value;
                            file.WriteLine(temp);
                            test.AppendText().WriteLine(temp);
                            test.AppendText().Flush();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                System.IO.File.WriteAllText(@fileName, temp);

            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static void SendTABToWindow(Window window)
        {
            try
            {
                TestStack.White.InputDevices.AttachedKeyboard kb = window.Keyboard;
                kb.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("SendTABToWindow " + window + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("SendTABToWindow " + window + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static void SendKeysToWindow(Window window, String key)
        {
            try
            {
                TestStack.White.InputDevices.AttachedKeyboard kb = window.Keyboard;
                foreach (char c in key)
                {
                    kb.Enter(c.ToString());
                    Thread.Sleep(25);
                }
                Thread.Sleep(200);
                Logger.logMessage("SendKeysToWindow " + window + "->" + key + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("SendKeysToWindow " + window + "->" + key + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static void SendSHIFT_TABToWindow(Window window)
        {
            try
            {
                TestStack.White.InputDevices.AttachedKeyboard kb = window.Keyboard;
                kb.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.SHIFT);
                kb.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.TAB);
                kb.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.SHIFT);
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("SendSHIFT_TABToWindow " + window + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("SendSHIFT_TABToWindow " + window + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static void SendNumbersToWindow(Window window, int input)
        {
            try
            {
                TestStack.White.InputDevices.AttachedKeyboard kb = window.Keyboard;
                foreach (var c in input.ToString())
                {
                    kb.Enter(""+Int32.Parse(c.ToString())+"");
                }
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("SendNumbersToWindow " + window + "->" + input + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("SendNumbersToWindow " + window + "->" + input + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static void SendBCKSPACEToWindow(Window window)
        {
            try
            {
                TestStack.White.InputDevices.AttachedKeyboard kb = window.Keyboard;
                kb.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.BACKSPACE);
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("SendBCKSPACEToWindow " + window + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("SendBCKSPACEToWindow " + window + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");

                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static void CloseAllChildWindows(Window window)
        {
            try
            {
                List<Window> modalWindows = window.ModalWindows();
                foreach (Window win in modalWindows)
                {
                    win.Focus();

                    try { FrameworkLibraries.ActionLibs.QBDT.WhiteAPI.Actions.ClickElementByName(win, "Close"); }
                    catch { }

                    try { win.Close(); }
                    catch { }
                }
                Logger.logMessage("CloseAllChildWindows " + window + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("CloseAllChildWindows " + window + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static void ClickButtonByName(Window win, String name)
        {

            try
            {
                AutomationProperty p = AutomationElementIdentifiers.NameProperty;
                TestStack.White.UIItems.Button b = (TestStack.White.UIItems.Button)win.Get(SearchCriteria.ByNativeProperty(p, name));
                b.Click();
                win.WaitWhileBusy();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("ClickButtonByName " + win + "->" + name + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");
            }
            catch (Exception e)
            {
                Logger.logMessage("ClickButtonByName " + win + "->" + name + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void HighLightWindowElementsAndShowType(Window win)
        {
            
            try
            {
                UIItemCollection allElements = win.Items;
                AutomationProperty p = AutomationElementIdentifiers.NameProperty;

                foreach (var element in allElements)
                {
                    element.RightClick();
                    MessageBox.Show(element.GetType().ToString() + " - " + element.AutomationElement.GetCurrentPropertyValue(p));
                }
            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static List<IUIItem> GetAllGroupBoxes(UIItemCollection collection)
        {
            List<IUIItem> groupBoxes = new List<IUIItem>();

            try
            {
                foreach (IUIItem element in collection)
                {
                    if (element.GetType().Name.Contains("Group") || element.GetType().Equals("Group"))
                    {
                        groupBoxes.Add(element);
                    }
                }
                Logger.logMessage("GetAllGroupBoxes " + collection + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

                return groupBoxes;
            }
            catch (Exception e)
            {
                Logger.logMessage("GetAllGroupBoxes " + collection + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static List<IUIItem> GetAllListItems(UIItemCollection collection)
        {
            List<IUIItem> listItems = new List<IUIItem>();

            try
            {
                foreach (IUIItem element in collection)
                {
                    if (element.GetType().Name.Contains("List") || element.GetType().Equals("List"))
                    {
                        listItems.Add(element);
                    }
                }
                Logger.logMessage("GetAllListItems " + collection + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

                return listItems;
            }
            catch (Exception e)
            {
                Logger.logMessage("GetAllListItems " + collection + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void SelectListBoxItemByText(Window win, String listBoxElementAutoID, String matchText)
        {
            try
            {
                TestStack.White.UIItems.ListBoxItems.ListBox l = win.Get<TestStack.White.UIItems.ListBoxItems.ListBox>(SearchCriteria.ByAutomationId(listBoxElementAutoID));
                List<TestStack.White.UIItems.ListBoxItems.ListItem> k = l.Items;
                foreach (var item in k)
                {
                    if (item.Text.Equals(matchText) || item.Text.Contains(matchText))
                    {
                        item.Focus();
                        //item.SetValue(matchText);
                        item.Click();
                        Thread.Sleep(int.Parse(Execution_Speed));
                    }
                }
                Logger.logMessage("SelectListBoxItemByText " + win + "->" + listBoxElementAutoID + "->" + matchText + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("SelectListBoxItemByText " + win + "->" + listBoxElementAutoID + "->" + matchText + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void GetElementCountOfType(Window win, String type)
        {
            int count = 0;
            SortedSet<String> Types = new SortedSet<string>();

            try
            {
                UIItemCollection allElements = win.Items;

                foreach (var element in allElements)
                {
                    if (element.GetType().Name.Equals(type) || element.GetType().Name.Contains(type))
                        count++;

                }

                MessageBox.Show(type + " elements = " + count.ToString());
            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static List<IUIItem> GetAllTextBoxes(UIItemCollection collection)
        {
            List<IUIItem> textBoxes = new List<IUIItem>();

            try
            {
                foreach (IUIItem element in collection)
                {
                    if (element.GetType().Name.Contains("Text") || element.GetType().Equals("Text"))
                    {
                        TestStack.White.UIItems.TextBox x = (TestStack.White.UIItems.TextBox) element;
                        textBoxes.Add(x);
                    }
                }
                Logger.logMessage("GetAllTextBoxes " + collection + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

                return textBoxes;
            }
            catch (Exception e)
            {
                Logger.logMessage("GetAllTextBoxes " + collection + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static List<IUIItem> GetAllPanels(UIItemCollection collection)
        {
            List<IUIItem> panels = new List<IUIItem>();

            try
            {
                foreach (IUIItem element in collection)
                {
                    if (element.GetType().Name.Contains("Pane") || element.GetType().Equals("Pane"))
                    {
                        TestStack.White.UIItems.Panel x = (TestStack.White.UIItems.Panel)element;
                        panels.Add(x);
                    }
                }
                Logger.logMessage("GetAllPanels " + collection + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

                return panels;
            }
            catch (Exception e)
            {
                Logger.logMessage("GetAllPanels " + collection + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static List<IUIItem> GetAllCheckboxes(UIItemCollection collection)
        {
            List<IUIItem> checkBoxes = new List<IUIItem>();

            try
            {
                foreach (IUIItem element in collection)
                {
                    if (element.GetType().Name.Contains("Check") || element.GetType().Equals("Check"))
                    {
                        TestStack.White.UIItems.CheckBox x = (TestStack.White.UIItems.CheckBox)element;
                        checkBoxes.Add(x);
                    }
                }
                Logger.logMessage("GetAllCheckboxes " + collection + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

                return checkBoxes;
            }
            catch (Exception e)
            {
                Logger.logMessage("GetAllCheckboxes " + collection + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");

                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void UIA_SetFocusOfFirstTextBox(AutomationElement uiaWindow, Window window)
        {
            try
            {
                PropertyCondition textCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Text);
                AutomationElementCollection textBoxes = uiaWindow.FindAll(TreeScope.Descendants, textCondition);
                foreach (AutomationElement textBox in textBoxes)
                {
                    TestStack.White.UIItems.TextBox t = new TestStack.White.UIItems.TextBox(textBox, window.ActionListener);
                    t.Focus();
                    t.Click();
                    Thread.Sleep(int.Parse(Execution_Speed));
                    break;
                }
                Logger.logMessage("UIA_SetFocusOfFirstTextBox " + uiaWindow + "->" + window + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("UIA_SetFocusOfFirstTextBox " + uiaWindow + "->" + window + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static AutomationElement UIA_GetAppWindow(string windowName)
        {
            try
            {
                PropertyCondition windowTypeCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
                PropertyCondition windowAutomationIDCondition = new PropertyCondition(AutomationElement.NameProperty, windowName);
                AndCondition windowCondition = new AndCondition(windowTypeCondition, windowAutomationIDCondition);
                AutomationElement window = AutomationElement.RootElement.FindFirst(TreeScope.Children, windowCondition);
                Logger.logMessage("UIA_GetAppWindow " + windowName + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

                return window;
            }
            catch (Exception e)
            {
                Logger.logMessage("UIA_GetAppWindow " + windowName + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static AutomationElement UIA_GetChildWindow(AutomationElement appWindow, string childWindowName)
        {
            AutomationElement childWindow = null;

            try
            {
                PropertyCondition windowTypeCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
                PropertyCondition windowNameCondition = new PropertyCondition(AutomationElement.NameProperty, childWindowName);
                AndCondition windowCondition = new AndCondition(windowTypeCondition, windowNameCondition);
                AutomationElement window = appWindow.FindFirst(TreeScope.Children, windowCondition);

                AutomationElementCollection windows = appWindow.FindAll(TreeScope.Descendants, windowTypeCondition);

                foreach (AutomationElement w in windows)
                {
                    if (w.Current.Name.Equals(childWindowName) || w.Current.Name.Contains(childWindowName))
                    {
                        childWindow = w;
                        break;
                    }
                }
                Logger.logMessage("UIA_GetChildWindow " + appWindow + "->" + childWindowName + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");

                return childWindow;
            }
            catch (Exception e)
            {
                Logger.logMessage("UIA_GetChildWindow " + appWindow + "->" + childWindowName + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************
        public static Window GetChildWindow(Window mainWindow, string childWindowName)
        {
            Window childWindow = null;

            try
            {
                List<Window> allChildWindows = mainWindow.ModalWindows();

                foreach (Window w in allChildWindows)
                {
                    if (w.Name.Equals(childWindowName) || w.Name.Contains(childWindowName))
                    {
                        childWindow = w;
                        break;
                    }
                }
                Logger.logMessage("GetChildWindow " + mainWindow + "->" + childWindowName + " - Successful");
                Logger.logMessage("------------------------------------------------------------------------------");
                return childWindow;
            }
            catch (Exception e)
            {
                Logger.logMessage("GetChildWindow " + mainWindow + "->" + childWindowName + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static bool WaitForChildWindow(Window mainWindow, string childWindowName, long timeOut)
        {
            Logger.logMessage("WaitForChildWindow " + mainWindow + "->" + childWindowName + " - Begin Sync");
            bool windowFound = false;
            long elapsedTime = 0;
            var stopwatch = Stopwatch.StartNew();

            try
            {
                do
                {
                    if (windowFound)
                        break;

                    elapsedTime = stopwatch.ElapsedMilliseconds;

                    List<Window> allChildWindows = mainWindow.ModalWindows();

                    foreach (Window w in allChildWindows)
                    {
                        try
                        {
                            Actions.ClickElementByName(Actions.GetAlertWindow("Alert"), "OK");
                            Thread.Sleep(int.Parse(Execution_Speed));
                        }
                        catch (Exception) { }

                        try
                        {
                            Actions.ClickElementByName(Actions.GetAlertWindow("Alert"), "No");
                            Thread.Sleep(int.Parse(Execution_Speed));
                        }
                        catch (Exception) { }


                        if (w.Name.Equals(childWindowName) || w.Name.Contains(childWindowName))
                        {
                            windowFound = true;
                            break;
                        }
                    }
                }
                while (elapsedTime<=timeOut);
                Logger.logMessage("WaitForChildWindow " + mainWindow + "->" + childWindowName + " - End Sync");
                Logger.logMessage("------------------------------------------------------------------------------");
                return windowFound;
            }
            catch (Exception e)
            {
                Logger.logMessage("WaitForChildWindow " + mainWindow + "->" + childWindowName + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static bool WaitForAnyChildWindow(Window mainWindow, string currentWindowName, long timeOut)
        {
            Logger.logMessage("WaitForAnyChildWindow " + mainWindow + "->" + currentWindowName + " - Begin Sync");
            bool windowFound = false;
            long elapsedTime = 0;
            var stopwatch = Stopwatch.StartNew();

            try
            {
                do
                {
                    if (windowFound)
                        break;

                    elapsedTime = stopwatch.ElapsedMilliseconds;

                    List<Window> allChildWindows = mainWindow.ModalWindows();

                    foreach (Window w in allChildWindows)
                    {
                        try
                        {
                            Actions.ClickElementByName(Actions.GetAlertWindow("Alert"), "OK");
                            Thread.Sleep(int.Parse(Execution_Speed));
                        }
                        catch (Exception) { }

                        try
                        {
                            Actions.ClickElementByName(Actions.GetAlertWindow("Alert"), "No");
                            Thread.Sleep(int.Parse(Execution_Speed));
                        }
                        catch (Exception) { }


                        if (!w.Name.Equals(currentWindowName) || !w.Name.Contains(currentWindowName))
                        {
                            windowFound = true;
                            break;
                        }
                    }
                }
                while (elapsedTime <= timeOut);
                Logger.logMessage("WaitForAnyChildWindow " + mainWindow + "->" + currentWindowName + " - End Sync");
                Logger.logMessage("------------------------------------------------------------------------------");

                return windowFound;
            }
            catch (Exception e)
            {
                Logger.logMessage("WaitForAnyChildWindow " + mainWindow + "->" + currentWindowName + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");

                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static bool WaitForAppWindow(string appWindowName, long timeOut)
        {
            Logger.logMessage("WaitForAppWindow " + appWindowName + "->" + " - Begin Sync");

            bool windowFound = false;
            long elapsedTime = 0;
            var stopwatch = Stopwatch.StartNew();

            try
            {
                do
                {
                    if (windowFound)
                        break;

                    elapsedTime = stopwatch.ElapsedMilliseconds;

                    List<Window> allChildWindows = Desktop.Instance.Windows();

                    foreach (Window w in allChildWindows)
                    {
                        try
                        {
                            Actions.ClickElementByName(Actions.GetAlertWindow("Alert"), "OK");
                            Thread.Sleep(int.Parse(Execution_Speed));
                        }
                        catch (Exception) { }

                        try
                        {
                            Actions.ClickElementByName(Actions.GetAlertWindow("Alert"), "No");
                            Thread.Sleep(int.Parse(Execution_Speed));
                        }
                        catch (Exception) { }


                        if (w.Name.Equals(appWindowName) || w.Name.Contains(appWindowName))
                        {
                            windowFound = true;
                            Thread.Sleep(int.Parse(Execution_Speed));
                            break;
                        }
                    }
                }
                while (elapsedTime <= timeOut);

                Logger.logMessage("WaitForAppWindow " + appWindowName + "->" + " - End Sync");
                Logger.logMessage("------------------------------------------------------------------------------");
                return windowFound;
            }
            catch (Exception e)
            {
                Logger.logMessage("WaitForAppWindow " + appWindowName + "->" + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void SendSHIFT_ENDToWindow(Window window)
        {
            try
            {
                TestStack.White.InputDevices.AttachedKeyboard kb = window.Keyboard;
                kb.HoldKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.SHIFT);
                kb.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.END);
                kb.LeaveKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.SHIFT);
                Thread.Sleep(200);
                Logger.logMessage("SendSHIFT_ENDToWindow " + window + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("SendSHIFT_ENDToWindow " + window + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }


//**************************************************************************************************************************************************************

        public static void SelectComboBoxItemByText(Window win, String comboBoxAutoID, String matchText)
        {
            try
            {
                TestStack.White.UIItems.ListBoxItems.ComboBox c = win.Get<TestStack.White.UIItems.ListBoxItems.ComboBox>(SearchCriteria.ByAutomationId(comboBoxAutoID));
                var k = c.Items;
                foreach (var item in k)
                {
                    if (item.Text.Equals(matchText) || item.Text.Contains(matchText))
                    {
                        item.Focus();
                        item.Select();
                        Thread.Sleep(int.Parse(Execution_Speed));
                    }
                }
                Logger.logMessage("SelectComboBoxItemByText " + win +  "->" + comboBoxAutoID + "->" + matchText + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("SelectComboBoxItemByText " + win + "->" + comboBoxAutoID + "->" + matchText + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }

//**************************************************************************************************************************************************************

        public static void SelectCheckBox(Window win, String checkBoxAutoID, bool state)
        {
            try
            {
                TestStack.White.UIItems.CheckBox c = win.Get<TestStack.White.UIItems.CheckBox>(SearchCriteria.ByAutomationId(checkBoxAutoID));
                if(state)
                {
                    c.Select();
                    Thread.Sleep(int.Parse(Execution_Speed));
                }
                else
                {
                    c.UnSelect();
                    Thread.Sleep(int.Parse(Execution_Speed));
                }
                Logger.logMessage("SelectCheckBox " + win + "->" + checkBoxAutoID + "->" + state + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");
            }
            catch (Exception e)
            {
                Logger.logMessage("SelectCheckBox " + win + "->" + checkBoxAutoID + "->" + state + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void SelectCheckBoxByName(Window win, String checkBoxName, bool state)
        {
            try
            {
                AutomationProperty p = AutomationElementIdentifiers.NameProperty;
                TestStack.White.UIItems.CheckBox c = win.Get<TestStack.White.UIItems.CheckBox>(SearchCriteria.ByNativeProperty(p, checkBoxName));
                if (state)
                {
                    c.Select();
                    Thread.Sleep(int.Parse(Execution_Speed));
                }
                else
                {
                    c.UnSelect();
                    Thread.Sleep(int.Parse(Execution_Speed));
                }
                Logger.logMessage("SelectCheckBoxByName " + win + "->" + checkBoxName + "->" + state + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("SelectCheckBoxByName " + win + "->" + checkBoxName + "->" + state + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void SelectRadioButtonByName(Window win, String radioButtonName)
        {
            try
            {
                AutomationProperty p = AutomationElementIdentifiers.NameProperty;
                TestStack.White.UIItems.RadioButton r = win.Get<TestStack.White.UIItems.RadioButton>(SearchCriteria.ByNativeProperty(p, radioButtonName));
                r.Select();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("SelectRadioButtonByName " + win + "->" + radioButtonName + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("SelectRadioButtonByName " + win + "->" + radioButtonName + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void SelectRadioButton(Window win, String radioButtonAutoID)
        {
            try
            {
                TestStack.White.UIItems.RadioButton r = win.Get<TestStack.White.UIItems.RadioButton>(SearchCriteria.ByAutomationId(radioButtonAutoID));
                r.Select();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("SelectRadioButton " + win + "->" + radioButtonAutoID + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("SelectRadioButton " + win + "->" + radioButtonAutoID + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************


        public static void SetTextOnElementByAutomationID(Window win, String automationID, String value)
        {

            try
            {
                win.Get(SearchCriteria.ByAutomationId(automationID)).Enter(value);
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("SetTextOnElementByAutomationID " + win + "->" + automationID + "->" + value + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");
            }
            catch (Exception e)
            {
                Logger.logMessage("SetTextOnElementByAutomationID " + win + "->" + automationID + "->" + value + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************


        public static bool CheckWindowExists(Window mainWindow, string childWindowName)
        {
            bool window = false;

            try
            {
                List<Window> allChildWindows = mainWindow.ModalWindows();

                foreach (Window w in allChildWindows)
                {
                    if (w.Name.Equals(childWindowName) || w.Name.Contains(childWindowName))
                    {
                        window = true;
                        Thread.Sleep(int.Parse(Execution_Speed));
                        break;
                    }
                }
                Logger.logMessage("CheckWindowExists " + mainWindow + "->" + childWindowName + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");
                return window;
            }
            catch (Exception e)
            {
                Logger.logMessage("CheckWindowExists " + mainWindow + "->" + childWindowName + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");

                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void SetFocusOnElementByAutomationID(Window win, String automationID)
        {

            try
            {
                win.Get(SearchCriteria.ByAutomationId(automationID)).Focus();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("SetFocusOnElementByAutomationID " + win + "->" + automationID + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");
            }
            catch (Exception e)
            {
                Logger.logMessage("SetFocusOnElementByAutomationID " + win + "->" + automationID + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static List<IUIItem> GetAllButtons(UIItemCollection collection)
        {
            List<IUIItem> buttons = new List<IUIItem>();

            try
            {
                foreach (IUIItem element in collection)
                {
                    if (element.GetType().Name.Contains("Button") || element.GetType().Equals("Button"))
                    {
                        TestStack.White.UIItems.Button x = (TestStack.White.UIItems.Button)element;
                        buttons.Add(x);
                    }
                }
                Logger.logMessage("GetAllButtons " + collection + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");
                return buttons;
            }
            catch (Exception e)
            {
                Logger.logMessage("GetAllButtons " + collection + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static List<IUIItem> GetAllCustomControls(UIItemCollection collection)
        {
            List<IUIItem> customControls = new List<IUIItem>();

            try
            {
                foreach (IUIItem element in collection)
                {
                    if (element.GetType().Name.Contains("Custom") || element.GetType().Equals("Custom"))
                    {
                        var x = (TestStack.White.UIItems.Custom.CustomUIItem)element;
                        customControls.Add(x);
                    }
                }
                Logger.logMessage("GetAllCustomControls " + collection + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");

                return customControls;
            }
            catch (Exception e)
            {
                Logger.logMessage("GetAllCustomControls " + collection + " - Sucessful");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************


        public static void UIA_ClickOnPaneItem(AutomationElement uiaWindow, Window window, int index)
        {
            try
            {
                PropertyCondition paneCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Pane);
                AutomationElementCollection allPanes = uiaWindow.FindAll(TreeScope.Descendants, paneCondition);

                TestStack.White.UIItems.Panel p = new TestStack.White.UIItems.Panel(allPanes[index], window.ActionListener);
                p.Focus();
                p.Click();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("UIA_ClickOnPaneItem " + uiaWindow + "->" + window + "->" + index + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("UIA_ClickOnPaneItem " + uiaWindow + "->" + window + "->" + index + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void UIA_ClickMenuItem(AutomationElement uiaWindow, Window window, string name)
        {
            try
            {
                PropertyCondition condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.MenuItem);

                AutomationElement menuItem = uiaWindow.FindFirst(TreeScope.Descendants, condition);

                TestStack.White.UIItems.MenuItems.Menu p = new TestStack.White.UIItems.MenuItems.Menu(menuItem, window.ActionListener);
                p.Focus();
                p.Click();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("UIA_ClickMenuItem " + uiaWindow + "->" + window + "->" + name + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("UIA_ClickMenuItem " + uiaWindow + "->" + window + "->" + name + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void UIA_ClickItemByName(AutomationElement uiaWindow, Window window, string name)
        {
            try
            {
                AutomationProperty p = AutomationElementIdentifiers.NameProperty;
                PropertyCondition condition = new PropertyCondition(p, name);
                AutomationElement element = uiaWindow.FindFirst(TreeScope.Descendants, condition);

                TestStack.White.UIItems.UIItem e = new TestStack.White.UIItems.UIItem(element, window.ActionListener);
                e.Focus();
                e.Click();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("UIA_ClickItemByName " + uiaWindow + "->" + window + "->" + name + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("UIA_ClickItemByName " + uiaWindow + "->" + window + "->" + name + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void UIA_ClickItemByAutomationID(AutomationElement uiaWindow, Window window, string automationID)
        {
            try
            {
                AutomationProperty p = AutomationElementIdentifiers.AutomationIdProperty;
                PropertyCondition condition = new PropertyCondition(p, automationID);
                AutomationElement element = uiaWindow.FindFirst(TreeScope.Descendants, condition);

                TestStack.White.UIItems.UIItem e = new TestStack.White.UIItems.UIItem(element, window.ActionListener);
                e.Focus();
                e.Click();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("UIA_ClickItemByAutomationID " + uiaWindow + "->" + window + "->" + automationID + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");
            }
            catch (Exception e)
            {
                Logger.logMessage("UIA_ClickItemByAutomationID " + uiaWindow + "->" + window + "->" + automationID + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************
        
        public static void DesktopInstance_ClickElementByName(string name)
        {
            try
            {
                AutomationProperty p = AutomationElementIdentifiers.NameProperty;
                SearchCriteria x = SearchCriteria.ByNativeProperty(p, name);
                var e = TestStack.White.Desktop.Instance.Get(x);
                e.Click();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("DesktopInstance_ClickElementByName " + name + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("DesktopInstance_ClickElementByName " + name + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");

                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static void DesktopInstance_ClickElementByAutomationID(string automationID)
        {
            try
            {
                AutomationProperty p = AutomationElementIdentifiers.AutomationIdProperty;
                SearchCriteria x = SearchCriteria.ByAutomationId(automationID);
                var e = TestStack.White.Desktop.Instance.Get(x);
                e.Click();
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("DesktopInstance_ClickElementByAutomationID " + automationID + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");

            }
            catch (Exception e)
            {
                Logger.logMessage("DesktopInstance_ClickElementByAutomationID " + automationID + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");

                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************


        public static bool DesktopInstance_CheckElementExistsByName(string name)
        {
            try
            {
                AutomationProperty p = AutomationElementIdentifiers.NameProperty;
                SearchCriteria x = SearchCriteria.ByNativeProperty(p, name);
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("DesktopInstance_CheckElementExistsByName " + name + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");
                return TestStack.White.Desktop.Instance.Exists(x);
            }
            catch (Exception e)
            {
                Logger.logMessage("DesktopInstance_CheckElementExistsByName " + name + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");

                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }

//**************************************************************************************************************************************************************

        public static bool DesktopInstance_CheckElementExistsByAutomationID(string automationID)
        {
            try
            {
                AutomationProperty p = AutomationElementIdentifiers.AutomationIdProperty;
                SearchCriteria x = SearchCriteria.ByAutomationId(automationID);
                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("DesktopInstance_CheckElementExistsByAutomationID " + automationID + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");
                return TestStack.White.Desktop.Instance.Exists(x);
            }
            catch (Exception e)
            {
                Logger.logMessage("DesktopInstance_CheckElementExistsByAutomationID " + automationID + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");

                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }
        }

//**************************************************************************************************************************************************************

        public static Window GetAlertWindow(string alertWindowName)
        {
            Window alertWindow = null;
            string alertText = null;

            try
            {
                List<Window> allChildWindows = Desktop.Instance.Windows();

                foreach (Window w in allChildWindows)
                {
                    if (w.Name.Equals(alertWindowName) || w.Name.Contains(alertWindowName))
                    {
                        alertWindow = w;

                        var elements = alertWindow.Items;

                        foreach (var item in elements)
                        {
                            if (item.GetType().Name.Equals("Label"))
                            {
                                alertText = item.Name;
                            }
                        }
                        Logger.logMessage("GetAlertWindow " + alertWindowName + " - Sucessful");
                        Logger.logMessage(alertText);
                        Logger.logMessage("------------------------------------------------------------------------------");
                        break;
                    }
                }

                return alertWindow;
            }
            catch (Exception e)
            {
                Logger.logMessage("GetAlertWindow " + alertWindowName + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************

        public static bool XunitAssertEuqals(string obj1, string obj2)
        {
            try
            {
                Assert.Equal(obj1, obj2);
                Logger.logMessage("XunitAssertEuqals " + obj1 + "->" + obj2 + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");
                return true;
            }
            catch (Exception e)
            {
                Logger.logMessage("XunitAssertEuqals " + obj1 + "->" + obj2 + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************
        public static bool XunitAssertContains(string obj1, string obj2)
        {
            try
            {
                Assert.Contains(obj1, obj2);
                Logger.logMessage("XunitAssertContains " + obj1 + "->" + obj2 + " - Sucessful");
                Logger.logMessage("------------------------------------------------------------------------------");
                return true;
            }
            catch (Exception e)
            {
                Logger.logMessage("XunitAssertContains " + obj1 + "->" + obj2 + " - Failed");
                Logger.logMessage(e.Message);
                Logger.logMessage("------------------------------------------------------------------------------");
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }

//**************************************************************************************************************************************************************



    }
}

