using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;

namespace HTTP
{
    class Requests : PayPal_Invoiceing.Services.DatabaseOperations
    {
        public static string auth(RestClient client)
        {
            var request = new RestRequest("oauth2/token", Method.POST, DataFormat.Xml)
                .AddParameter("grant_type", "client_credentials");

            string json = client.Execute(request).Content;
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            if(parsedJson.access_token != null)
            {
                return parsedJson.access_token;
            }
            throw new Exception("The access token could not be retrieved");
        }
        
        protected static void deleteInvoices(RestClient client, string accesstoken, string id = null)
        {
            string invoicesList = client.Execute(new RestRequest("https://api.sandbox.paypal.com/v2/invoicing/invoices",
                              Method.GET).AddHeader("Authorization", "Bearer " + accesstoken)).Content;
            //the list of invoices can be used to complete other tasks like, update, delete cancel or set reminders
            var parsedjson = JObject.Parse(invoicesList)["items"];
            foreach (var item in parsedjson)
            {
                string invID = (item["id"]).ToString();
                if (invID == id)
                {
                    var deleteStatus = client.Execute(new RestRequest("https://api.sandbox.paypal.com/v2/invoicing/invoices/" +
                        invID.ToString()).AddHeader("Content-Type", "application/json")
                        .AddHeader("Authorization", "Bearer " + accesstoken), Method.DELETE).StatusCode;
                    if (deleteStatus.ToString() == "NoContent")
                    {
                        //invoice has been deleted
                    }
                }
            }
        }

        protected static string sendInvoice(JObject draftInvoice, string accesstoken, RestClient client)
        {
            if (draftInvoice["href"].ToString() != null)
            {
                var sendInvoice = new RestRequest(draftInvoice["href"].ToString() + "/send", Method.POST)
                    .AddHeader("Content-Type", "application/json")
                    .AddHeader("Authorization", "Bearer " + accesstoken);

                return JObject.Parse(client.Execute(sendInvoice).Content)["href"].ToString();
            }
            throw new Exception("A draft invoice could not be found");
        }

        public static JObject createInvoiceDraft(string accesstoken, string[] args, string nextInvoiceNum, 
            RestClient client)
        {
            string requestBody = PayPal_Invoiceing.Services.getRequestBody.LoadJson(nextInvoiceNum, args[2], args[4], args[5], args[11], args[12],
                args[6], args[7], args[8], args[9], args[10], args[16], args[17], args[15], args[14], args[13],
                args[0], args[1], args[18], args[19]);

            var request = new RestRequest("https://api.sandbox.paypal.com/v2/invoicing/invoices/", Method.POST)
                .AddHeader("Authorization", "Bearer " + accesstoken)
                .AddParameter("application/json", requestBody, ParameterType.RequestBody);

            JObject draftInvoice = JObject.Parse(client.Execute(request).Content);
            if(draftInvoice.ToString() != string.Empty)
            {
                return draftInvoice;
            }
            throw new Exception("The invoice draft could not be created");
        }

        public static string getNextInvoiceNumber(string accesstoken, RestClient client)
        {
            dynamic invoiceNum = JsonConvert.DeserializeObject(client.Execute(
                new RestRequest("https://api.sandbox.paypal.com/v2/invoicing/generate-next-invoice-number",
                Method.POST).AddHeader("Authorization", "Bearer " + accesstoken)).Content);
            if (invoiceNum.invoice_number != null)
            {
                return invoiceNum.invoice_number;
            }
            throw new Exception("Could not generate a new invoice number");
        }
    }
}
