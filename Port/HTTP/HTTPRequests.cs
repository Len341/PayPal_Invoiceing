using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace HTTP
{
    class Requests
    {
        public static IRestResponse auth(RestClient client)
        {
            var request = new RestRequest("oauth2/token", Method.POST, DataFormat.Xml)
                .AddParameter("grant_type", "client_credentials");

            return client.Execute(request);
        }

        protected static string sendInvoice(JObject draftInvoice, string accesstoken, RestClient client)
        {
            var sendInvoice = new RestRequest(draftInvoice["href"].ToString() + "/send", Method.POST)
                .AddHeader("Content-Type", "application/json")
                .AddHeader("Authorization", "Bearer " + accesstoken);

            return JObject.Parse(client.Execute(sendInvoice).Content)["href"].ToString();
        }

        public static JObject createInvoiceDraft(string accesstoken, string[] args, string nextInvoiceNum, 
            RestClient client)
        {
            string requestBody = PayPal_Invoiceing.Classes.getRequestBody.LoadJson(nextInvoiceNum, args[2], args[4], args[5], args[11], args[12],
                args[6], args[7], args[8], args[9], args[10], args[16], args[17], args[15], args[14], args[13],
                args[0], args[1], args[18], args[19]);

            var request = new RestRequest("https://api.sandbox.paypal.com/v2/invoicing/invoices/", Method.POST)
                .AddHeader("Authorization", "Bearer " + accesstoken)
                .AddParameter("application/json", requestBody, ParameterType.RequestBody);

            JObject draftInvoice = JObject.Parse(client.Execute(request).Content);
            return draftInvoice;
        }

        public static string getNextInvoiceNumber(string accesstoken, RestClient client)
        {
            dynamic invoiceNum = JsonConvert.DeserializeObject(client.Execute(
                new RestRequest("https://api.sandbox.paypal.com/v2/invoicing/generate-next-invoice-number",
                Method.POST).AddHeader("Authorization", "Bearer " + accesstoken)).Content);
            return invoiceNum.invoice_number;
        }
    }
}
