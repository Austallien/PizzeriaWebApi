using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace PizzeriaApiTest
{
    class Operation
    {
        readonly static string BaseUri = "https://localhost:5001/pizzeria";

        public static object GetOrderList()
        {
            WebRequest request = WebRequest.Create(BaseUri+"/order");
            request.Method = "GET";
            var webStream = request.GetResponse().GetResponseStream();

            var data = new StreamReader(webStream).ReadToEnd();

            return data;
        }

        public static object GetOrderItem(int id)
        {
            WebRequest request = WebRequest.Create(BaseUri + "/order");
            request.Method = "GET";
            var webStream = request.GetResponse().GetResponseStream();

            //JsonDocument data = JsonDocument.Parse(new StreamReader(webStream).ReadToEnd());
            var data = new StreamReader(webStream).ReadToEnd();
            return data;
        }
    }
}
