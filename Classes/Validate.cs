using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal_Invoiceing.Classes
{
    /// <summary>
    /// will be used to validate our program arguments before it gets sent
    /// </summary>
    static class Validate
    {
        public static bool CheckArgs(string[] args)
        {
            if (args[0] == string.Empty)
            {
                throw new Exception("No Customer Name");
            }
            if (args[1] == string.Empty)
            {
                throw new Exception("No Customer Surname");
            }
            if(args[2] == string.Empty)
            {
                throw new Exception("No Customer Email Entered");
            }
            if(!float.TryParse(args[3], out _))
            {
                throw new Exception("Invalid amount entered");
            }
            if(args[4] == string.Empty)
            {
                throw new Exception("No invoicer firstname");
            }
            if(args[5] == string.Empty)
            {
                throw new Exception("No invoicer lastname");
            }
            if(args[6] == string.Empty)
            {
                throw new Exception("No invoicer street");
            }
            if(args[7] == string.Empty)
            {
                throw new Exception("No invoicer town");
            }
            if(args[8] == string.Empty)
            {
                throw new Exception("Invalid invoicer state");
            }
            if(args[9] == string.Empty)
            {
                throw new Exception("Invalid invoicer postcode");
            }
            if(args[10] == string.Empty)
            {
                throw new Exception("Invalid invoicer country");
            }
            if(args[11] == string.Empty)
            {
                throw new Exception("Invalid invoicer countryCode");
            }
            if(args[12] == string.Empty)
            {
                throw new Exception("Invalid invoicer Phone");
            }
            if(args[13] == string.Empty)
            {
                throw new Exception("Invalid invoicer Website entered");
            }
            if(args[17] == string.Empty)
            {
                throw new Exception("No Reference for invoice supplied");
            }
            return true;
        }
    }
}
