using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Threading;

namespace Trinity
{
    [TestClass]
    public class APITest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var formData = API.GetDictionary();
            API.SendMessage("http://pdevbiuos106.corp.intuit.net:8080/trinity/v1/sbg-cls-IPDNextGen", formData);
        }
    }
}
