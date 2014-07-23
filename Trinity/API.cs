using RestSharp.Contrib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Trinity
{
    class API
    {
        public static Dictionary<String, String> GetDictionary()
        {
            string[] values = { "value1", "value2", "value3" };
            Dictionary<string, string> d = new Dictionary<string, string>();
            d.Add("key1", values[0]);
            d.Add("key2", values[1]);
            d.Add("key3", values[2]);
            return d;
        }

        public static HttpWebRequest CreateWebRequest(string endPoint, Int32 contentLength)
        {
            var request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = "POST";
            request.ContentLength = contentLength;
            request.ContentType = "application/x-www-form-urlencoded";
            return request;
        }

        public static string CreateFormattedPostRequest(ICollection values)
        {
            var paramterBuilder = new StringBuilder();
            var counter = 0;

            foreach (var value in values)
            {
                paramterBuilder.AppendFormat("{0}={1}", value, HttpUtility.UrlEncode(value.ToString()));
                if (counter != values.Count - 1)
                {
                    paramterBuilder.Append("&");
                }
                counter++;
            }
            return paramterBuilder.ToString();
        }

        public static void SendMessage(string endPoint, Dictionary<string, string> paramters)
        {
            var populatedEndPoint = CreateFormattedPostRequest(paramters);
            byte[] bytes = Encoding.UTF8.GetBytes(populatedEndPoint);

            HttpWebRequest request = CreateWebRequest(endPoint, bytes.Length);

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseHeader = response.Headers;

                //if (response.StatusCode != HttpStatusCode.OK)
                //{
                //    string message = String.Format("POST failed. Received HTTP {0}", response.StatusCode);
                //    throw new ApplicationException(message);
                //}
            }
        }
    }
}