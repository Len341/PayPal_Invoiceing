using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayPal_Invoiceing.EF_Model;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;
using System.IO;

namespace PayPal_Invoiceing
{
    class Program
    {
        static void Main(string[] args)
        {
            //args 1 = custName
            //args 2 = custEmail
            //args 3 = Amount
            try
            {
                if (!string.IsNullOrEmpty(args[0]))
                {
                    //custName valid, can implement more solid validation
                    if (!string.IsNullOrEmpty(args[1]))
                    {
                        //custEmail entered
                        if (float.TryParse(args[2], out float amount))
                        {
                            //amount is valid float/integer
                            //continue to save values to db
                            using (var ctx = new PayPal_IntegrateEntities())
                            {
                                tbl_customerInvoice invoice = new tbl_customerInvoice()
                                {
                                    customerName = args[0],
                                    customerEmail = args[1],
                                    amount = amount,
                                    System_Date = DateTime.Now
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
                            //amount is invalid
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
                Console.WriteLine("No arguments provided");
            }
            Console.ReadKey();

            var client = HTTP.Client.init();
            var response = HTTP.Requests.auth(client);

            string accesstoken = FormatJson(response.Content);
            var nextInvoiceRequest = new
                RestRequest("https://api.sandbox.paypal.com/v2/invoicing/generate-next-invoice-number", Method.POST);
            nextInvoiceRequest.AddHeader("Authorization", "Bearer " + accesstoken);

            dynamic invoiceNum = JsonConvert.DeserializeObject(client.Execute(nextInvoiceRequest).Content);
            string nextInvoiceNum = invoiceNum.invoice_number;

            var createInvoiceDraft = new RestRequest("https://api.sandbox.paypal.com/v2/invoicing/invoices/", Method.POST);
            createInvoiceDraft.AddHeader("Authorization", "Bearer " + accesstoken);
            createInvoiceDraft.RequestFormat = DataFormat.Json;
            string requestBody = LoadJson(nextInvoiceNum, "leonard.lb341@gmail.com");
            createInvoiceDraft.AddParameter("application/json", requestBody, ParameterType.RequestBody);
            dynamic draftInvoice = JsonConvert.DeserializeObject(client.Execute(createInvoiceDraft).Content);
            string invoiceSendLink = draftInvoice.href+"/send";

            var sendInvoice = new RestRequest(invoiceSendLink, Method.POST);
            sendInvoice.RequestFormat = DataFormat.Json;
            sendInvoice.AddHeader("Content-Type", "application/json");
            sendInvoice.AddHeader("Authorization", "Bearer " + accesstoken);

            Console.WriteLine(client.Execute(sendInvoice).Content);
            Console.ReadKey();
        }
        public static string LoadJson(string nextinvnum, string recipientemail)
        {
            using (StreamReader r = new StreamReader("createInvoiceDraft.json"))
            {
                string json = r.ReadToEnd();
                return json.Replace("INVNUM", nextinvnum)
                    .Replace("RECIPIENTEMAIL", recipientemail).Replace("\n","").Replace("\t", "")
                    .Replace("\r", "");
            }
        }
        private static string FormatJson(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return parsedJson.access_token;
        }
    }
}
