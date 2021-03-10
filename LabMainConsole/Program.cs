using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace LabMainConsole
{
    class Program
    {
        static char[] separator = new char[]
        {
            ' ','.',',','!',':',';','?','<','>','(',')','=','+','-','\"','\'', '\\', '/', '«', '»', '\r', '\n'
        };
        static Dictionary<string, int> dictionary = new Dictionary<string, int>();
        static Queue<string> wordqueue = new Queue<string>();
        static ReaderWriterLock rwl = new ReaderWriterLock();

        static void Main(string[] args)
        {
            var path = ConfigurationManager.AppSettings["path"];
            var length = ConfigurationManager.AppSettings["length"];

            Task readfile = new Task(() =>
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string nl;
                    while ((nl = sr.ReadLine()) != null)
                    {
                        foreach (var word in nl.Split(separator, StringSplitOptions.RemoveEmptyEntries))
                        {
                            rwl.AcquireWriterLock(10);
                            try
                            {
                                wordqueue.Enqueue(word);
                            }
                            finally
                            {
                                rwl.ReleaseWriterLock();
                            }
                        }
                    }
                }
            });
            Task readqueue = new Task(() =>
            {
                while (!readfile.IsCompleted || wordqueue.Count != 0)
                {
                    if (wordqueue.Count == 0) continue;
                    string newword;
                    rwl.AcquireWriterLock(10);
                    try
                    {
                        newword = wordqueue.Dequeue();
                    }
                    finally
                    {
                        rwl.ReleaseWriterLock();
                    }
                    Analyze(newword.ToLower(), int.Parse(length));
                }
            });

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Task[] tasks = new Task[] { readfile, readqueue };
            foreach (var task in tasks)
            {
                task.Start();
            }
            Task.WaitAll(tasks);

            //string[] words = new StreamReader(path).ReadToEnd().Split(separator, StringSplitOptions.RemoveEmptyEntries);

            ////using (StreamReader sr = new StreamReader(path))
            ////{
            ////    Console.WriteLine(sr.ReadToEnd());
            ////}
            //foreach (var word in words)
            //{
            //    Analyze(word.ToLower(), int.Parse(length));
            //}

            sw.Stop();

            Console.WriteLine($"время выполнения = {sw.ElapsedMilliseconds} - {sw.ElapsedTicks}");
            Console.WriteLine();
            Console.WriteLine($"{dictionary.Count} - {length}-грамм всего");
            int i = 0;
            foreach (var pair in dictionary)
            {
                Console.WriteLine(pair.Key + " : " + pair.Value);
                i++;
            }
            Console.WriteLine(i);
            Console.WriteLine();
            //Console.WriteLine($"{path} {length}");
            Console.ReadKey();
        }
        static void Analyze(string word, int n)
        {
            //if (word.Length < n) return;

            while (word.Length >= n)
            {
                string gr = word.Substring(0, n);
                if (dictionary.ContainsKey(gr)) dictionary[gr]++;
                else dictionary.Add(gr, 1);

                word = word.Substring(1);
            }
        }
    }
}
