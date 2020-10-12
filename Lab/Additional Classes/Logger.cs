using System;
using System.IO;

namespace Laboratory.AdditionalClasses
{
    /// <summary>
    /// Класс логгера
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Создает новую запись в логе
        /// </summary>
        public static void NewLog(string message)
        {
            using (StreamWriter logger = new StreamWriter("logs.txt", true))
            {
                logger.WriteLineAsync($"{DateTime.Now} : {message}");
            }
        }
    }
}
