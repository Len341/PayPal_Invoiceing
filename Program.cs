using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayPal_Invoiceing.EF_Model;
using System.Threading.Tasks;

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
        }
    }
}
