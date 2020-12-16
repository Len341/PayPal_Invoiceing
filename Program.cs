using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayPal_Invoiceing.EF_Model;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace PayPal_Invoiceing
{
    class Program
    {
        static void Main(string[] args)
        {
            //args 1 = custName
            //args 2 = custSurname
            //args 3 = custEmail
            //args 4 = Amount
            //args 5 = invoicerFname
            //args 6 = invoicerLname
            //args 7 = invoicerStreet
            //args 8 = invoicerTown
            //args 9 = invoicerState
            //args 10 = invoicerPostCode
            //args 11 = invoicerCountry
            //args 12 = invoicerCountryCode
            //args 13 = invoicerPhone
            //args 14 = invoicerWebsite
            //args 15 = invoicerAdditionalInfo
            //args 16 = invoicerLogoUrl
            //args 17 = invoicerTaxID
            //args 18 = invoiceRefNumber

            try
            {
                if (!string.IsNullOrEmpty(args[0]))
                {
                    //custName valid, can implement more solid validation
                    if (!string.IsNullOrEmpty(args[1]))
                    {
                        //custSurname entered
                        if (float.TryParse(args[3], out float amount))
                        {
                            //amount is valid float/integer
                            //continue to save values to db
                            using (var ctx = new PayPal_IntegrateEntities())
                            {
                                tbl_customerInvoice invoice = new tbl_customerInvoice()
                                {
                                    customerName = args[0],
                                    customerEmail = args[1],
                                    System_Date = DateTime.Now,
                                    amount = amount
                                };
                                ctx.tbl_customerInvoice.Add(invoice);
                                try
                                {
                                    ctx.SaveChanges();

                                    var allInvoices = (from i in ctx.tbl_customerInvoice
                                                       select i);

                                    foreach (var inv in allInvoices)
                                    {
                                        Console.WriteLine();

                                        Console.WriteLine("-------------------------------");
                                        Console.WriteLine("Invoice Number: " + inv.idtbl_customerInvoice.ToString());
                                        Console.WriteLine("Customer Name: " + inv.customerName);
                                        Console.WriteLine("Customer Email: " + inv.customerEmail);
                                        Console.WriteLine("Amount: " + inv.amount);
                                        Console.WriteLine("Date: " + inv.System_Date.ToString());
                                        Console.WriteLine("-------------------------------");
                                        Console.WriteLine();
                                    }
                                }
                                catch (Exception)
                                {
                                    throw;
                                }
                            }
                        }
                        else
                        {
                            //amount invalid float/integer
                            return;
                        }
                    }
                    else
                    {
                        //custEmail not entered
                        return;
                    }
                }
                else
                {
                    //no custname entered
                    return;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Some arguments are missing");
            }
            Console.ReadKey();

            var client = HTTP.Client.init();
            var response = HTTP.Requests.auth(client);

            string accesstoken = GetAccessKey(response.Content);
            var nextInvoiceRequest = new
                RestRequest("https://api.sandbox.paypal.com/v2/invoicing/generate-next-invoice-number", 
                Method.POST);
            nextInvoiceRequest.AddHeader("Authorization", "Bearer " + accesstoken);

            dynamic invoiceNum = JsonConvert.DeserializeObject(client.Execute(nextInvoiceRequest).Content);
            string nextInvoiceNum = invoiceNum.invoice_number;

            var createInvoiceDraft = new RestRequest("https://api.sandbox.paypal.com/v2/invoicing/invoices/", 
                Method.POST);
            createInvoiceDraft.AddHeader("Authorization", "Bearer " + accesstoken);
            createInvoiceDraft.RequestFormat = DataFormat.Json;
            string requestBody = LoadJson(nextInvoiceNum, args[2], args[4], args[5], args[11], args[12], 
                args[6], args[7], args[8], args[9], args[10], args[16], args[17], args[15], args[14], args[13],
                args[0], args[1], args[18], args[19]);
            createInvoiceDraft.AddParameter("application/json", requestBody, ParameterType.RequestBody);
            JObject draftInvoice = JObject.Parse(client.Execute(createInvoiceDraft).Content);

            //string invoicesList = client.Execute(new RestRequest("https://api.sandbox.paypal.com/v2/invoicing/invoices", 
            //    Method.GET).AddHeader("Authorization", "Bearer "+accesstoken)).Content;

            var sendInvoice = new RestRequest(draftInvoice["href"] + "/send", Method.POST);
            sendInvoice.RequestFormat = DataFormat.Json;
            sendInvoice.AddHeader("Content-Type", "application/json");
            sendInvoice.AddHeader("Authorization", "Bearer " + accesstoken);

            Console.WriteLine(client.Execute(sendInvoice).Content);
            Console.ReadKey();
        }
        public static string LoadJson(string nextinvnum, string recipientemail, string invoicerFname, 
            string invoicerLname, string invoicerCountryCode, string invoicerPhone, string invoicerStreet,
            string invoicerTown, string invoicerState, string invoicerPostCode, string invoicerCountry,
            string invoicerTaxID, string RefNum, string invoicerLogoUrl, string invoicerAdditionalInfo, 
            string invoicerWebsite, string recipientFname, string recipientLname, string recipientCountryCode,
            string recipientPhone)
        {
            using (StreamReader r = new StreamReader("createInvoiceDraft.json"))
            {
                string json = r.ReadToEnd();
                return json.Replace("INVNUM", nextinvnum)
                    .Replace("RECIPIENTEMAIL", recipientemail).Replace("INVOICERFNAME", invoicerFname)
                    .Replace("INVOICERLNAME", invoicerLname).Replace("INVOICERCOUNTRYCODE", invoicerCountryCode)
                    .Replace("INVOICERCOUNTRY", invoicerCountry).Replace("INVOICERSTATE", invoicerState)
                    .Replace("INVOICERPHONE", invoicerPhone).Replace("INVOICERSTREET", invoicerStreet)
                    .Replace("INVOICERTOWN", invoicerTown).Replace("INVOICERPOSTCODE", invoicerPostCode)
                    .Replace("INVOICERTAXID", invoicerTaxID).Replace("REFNUM", RefNum)
                    .Replace("INVOICERLOGOURL", invoicerLogoUrl).Replace("INVOICERADDITIONALINFO", invoicerAdditionalInfo)
                    .Replace("INVOICERWEBSITE", invoicerWebsite).Replace("CUSTFNAME", recipientFname)
                    .Replace("CUSTLNAME", recipientLname).Replace("RECIPIENTPHONE", recipientPhone)
                    .Replace("RECIPIENTCOUNTRYCODE", recipientCountryCode)
                    .Replace("\n", "").Replace("\t", "").Replace("\r", "");
            }
        }
        private static string GetAccessKey(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return parsedJson.access_token;
        }
    }
}
