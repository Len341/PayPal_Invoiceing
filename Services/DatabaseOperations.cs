using PayPal_Invoiceing.EF_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal_Invoiceing.Services
{
    class DatabaseOperations
    {
        protected static void saveInvoiceToDB(string[] args)
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
    }
}
