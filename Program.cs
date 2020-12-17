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
    class Program : HTTP.Requests
    {
        static void Main(string[] args)
        {
            //the values passed into the api call should be of correct format
            //the api does not allow for eg '+27' as a country code but '27'
            //the country should also be 2-3 digits
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
            //2 or 3 digit. E.g SA || ZAF or DE || DEU or USA || US

            //args 12 = invoicerCountryCode
            //2-4 digit number

            //args 13 = invoicerPhone
            //args 14 = invoicerWebsite
            //args 15 = invoicerAdditionalInfo
            //args 16 = invoicerLogoUrl
            //args 17 = invoicerTaxID
            //args 18 = invoiceRefNumber

            try
            {
                if (Classes.Validate.CheckArgs(args))
                {
                    //arguments are valid
                    float amount = 0;
                    float.TryParse(args[3], out amount);

                    using (var ctx = new PayPal_IntegrateEntities())
                    {
                        Customer_Invoice invoice = new Customer_Invoice()
                        {
                            customerFname = args[0],
                            customerEmail = args[1],
                            System_Date = DateTime.Now,
                            amount = amount,
                            customerCountryCode = args[18],
                            customerLname = args[1],
                            customerPhone = args[19],
                            invoicerAdditionalInfo = args[14],
                            invoicerCountry = args[10],
                            invoicerCountryCode = args[11],
                            invoicerFname = args[4],
                            invoicerLname = args[5],
                            invoicerLogoUrl = args[15],
                            invoicerPhone = args[12],
                            invoicerPostCode = args[9],
                            invoicerRefNum = args[17],
                            invoicerState = args[8],
                            invoicerStreet = args[6],
                            invoicerTaxId = args[16],
                            invoicerTown = args[7],
                            invoicerWebsite = args[13]
                        };
                        ctx.Customer_Invoices.Add(invoice);
                        ctx.SaveChanges();

                    }
                }

            var client = HTTP.Client.init();
            var response = auth(client);

            string accesstoken = GetAccessKey(response.Content);
            string nextInvoiceNum = getNextInvoiceNumber(accesstoken, client).Replace("#","");
            //once we have the next available invoice number, we can create a draft

            var draftInvoice = createInvoiceDraft(accesstoken, args, nextInvoiceNum, client);
            //after creation of the draft invoice, only then it can be sent

            //string invoicesList = client.Execute(new RestRequest("https://api.sandbox.paypal.com/v2/invoicing/invoices", 
            //    Method.GET).AddHeader("Authorization", "Bearer "+accesstoken)).Content;
            //the list of invoices can be used to complete other tasks like, update, delete cancel or set reminders

            string link = sendInvoice(draftInvoice, accesstoken, client);

            Console.WriteLine("Heres a link for your recipient: "+link);
            Console.ReadKey();
            }
            catch (Exception ex)
            {
                Classes.Logger.WriteLine(ex.Message, Classes.Logger.LogLevelEnum.ERROR);
                Console.WriteLine("Error Occured, check logs");
                Console.ReadKey();
            }
        }
       
        private static string GetAccessKey(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return parsedJson.access_token;
        }
    }
}
