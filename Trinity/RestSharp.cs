using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace Trinity
{
    [TestClass]
    public class RestSharp
    {
        [TestMethod]
        public void TestMethod1()
        {
            var client = new RestClient();
            client.BaseUrl = "http://pdevbiuos106.corp.intuit.net:8080/trinity/v1/sbg-cls-IPDNextGen";

            var request = new RestRequest();
            request.Resource = "Hello World";

            IRestResponse response = client.Execute(request);
        }
    }
}
