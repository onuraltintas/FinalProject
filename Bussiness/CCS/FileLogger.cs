using System;

namespace Bussiness.CCS
{
    public class FileLogger:ILogger
    {
        public void Log()
        {
            Console.WriteLine("Dosyaya Loglandı.");
        }
    }
}
