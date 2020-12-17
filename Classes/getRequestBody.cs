using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PayPal_Invoiceing.Classes
{
    static class getRequestBody
    {
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
    }
}
