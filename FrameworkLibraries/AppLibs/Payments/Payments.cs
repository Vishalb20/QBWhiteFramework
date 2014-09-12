using FrameworkLibraries.ActionLibs.WhiteAPI;
using FrameworkLibraries.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using TestStack.White.UIItems.WindowItems;

namespace FrameworkLibraries.AppLibs.Payments
{
    public class Payments
    {
        public static Property conf = Property.GetPropertyInstance();
        public static string Execution_Speed = conf.get("ExecutionSpeed");

        public static void SetValuesOnProcessCreditCardPaymentWindow(Window paymentWin, string ccNumber, string expMonth, string expYear, string nameOnCard, string secCode, string billingAddr, string zipCode)
        {
            try
            {
                Logger.logMessage("---------------------------------------------------------------------------------");

                var paymentPanel = Actions.GetPaneByName(paymentWin, "Quickbooks Payments: Process Credit Card");

                PropertyCondition editCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Edit);
                AutomationElementCollection editElements = paymentPanel.AutomationElement.FindAll(TreeScope.Children, editCondition);
                int count = 0;

                foreach (AutomationElement item in editElements)
                {
                    count = count + 1;
                    TestStack.White.UIItems.TextBox t = new TestStack.White.UIItems.TextBox(item, paymentWin.ActionListener);

                    if (count == 1)
                        t.Text = ccNumber;

                    if (count == 2)
                        t.Text = expMonth;

                    if (count == 3)
                        t.Text = expYear;

                    if (count == 4)
                        t.Text = nameOnCard;

                    if (count == 5)
                        t.Text = secCode;

                    if (count == 6)
                        t.Text = billingAddr;

                    if (count == 7)
                        t.Text = zipCode;
                }

                Thread.Sleep(int.Parse(Execution_Speed));
                Logger.logMessage("---------------------------------------------------------------------------------");
            }
            catch (Exception e)
            {
                String sMessage = e.Message;
                LastException.SetLastError(sMessage);
                throw new Exception(sMessage);
            }

        }
    }
}
