using System;

namespace Bussiness.CCS
{
    public class DatabaseLogger:ILogger
    {
        public void Log()
        {
            Console.WriteLine("Veritabanına Loglandı.");
        }
    }
}