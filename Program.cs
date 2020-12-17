using System;
using Newtonsoft.Json.Linq;
using PayPal_Invoiceing.Services;
using RestSharp;

namespace PayPal_Invoiceing
{
    class Program : HTTP.Requests
    {
        static void Main(string[] args)
        {
            #region args description
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
            #endregion

            try
            {
                Run(args);
            }
            catch (Exception ex)
            {
                Logger.WriteLine(ex.ToString(), Logger.LogLevelEnum.ERROR);
            }
        }

        private static void Run(string[] args)
        {
            if (Validate.CheckArgs(args))
            {
                saveInvoiceToDB(args);

                var client = HTTP.Client.init();

                string accesstoken = auth(client);
                string nextInvoiceNum = getNextInvoiceNumber(accesstoken, client).Replace("#", "");

                var draftInvoice = createInvoiceDraft(accesstoken, args, nextInvoiceNum, client);
                //after creation of the draft invoice, only then it can be sent

                //deleteInvoices(client, accesstoken , id);

                string link = sendInvoice(draftInvoice, accesstoken, client);

                Console.WriteLine("Heres a link for your recipient: " + link);
                Console.ReadKey();
            }
        }
    }
}
