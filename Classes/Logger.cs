using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPal_Invoiceing.Classes
{
    public class Logger
    {
        public enum LogLevelEnum
        {
            ERROR = 1,
            INFO = 2,
            DEBUG = 3
        }
        private static LogLevelEnum LogLevel { get; set; } = LogLevelEnum.INFO;

        public static void WriteLine(string text = "", LogLevelEnum level = LogLevelEnum.DEBUG, string additionalDestinationFile = null)
        {
            Write(text + "\n", level, additionalDestinationFile);
        }

        public static void Write(string text, LogLevelEnum level = LogLevelEnum.DEBUG, string additionalDestinationFile = null)
        {
            //save to db
            using(var ctx = new EF_Model.PayPal_IntegrateEntities())
            {
                EF_Model.Log log = new EF_Model.Log()
                {
                    Level = (int)level,
                    System_Date = DateTime.Now,
                    Text = text,
                    Type = (int)level == 1 ? "ERROR" : (int)level == 2 ? "INFO" : "DEBUG"
                };
                try
                {
                    ctx.Logs.Add(log);
                    ctx.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
