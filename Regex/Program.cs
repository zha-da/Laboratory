using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace RegexLab
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            while(true)
            {
                Console.Write("Открыть файл? (Y|N) ");
                if (Console.ReadLine().ToLower().Equals("n"))
                {
                    break;
                }

                OpenFileDialog ofd = new OpenFileDialog();
                if (ofd.ShowDialog() != DialogResult.OK)
                {
                    continue;
                }

                StreamReader sr = new StreamReader(ofd.FileName);
                string fulltext = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();

                Console.WriteLine(fulltext);

                Console.Write("Введите слово для проверки : ");
                string src = Console.ReadLine();

                Regex regex = new Regex(src);
                MatchCollection mc = regex.Matches(fulltext);

                if (mc.Count % 10 == 1 & (mc.Count % 100) / 10 != 1)
                {
                    Console.WriteLine($"В файле найдено {mc.Count} совпадение");
                }
                else if ((mc.Count % 10 > 1 | mc.Count < 5) & (mc.Count % 100) / 10 != 1)
                {
                    Console.WriteLine($"В файле найдено {mc.Count} совпадения");
                }
                else Console.WriteLine($"В файле найдено {mc.Count} совпадений");

                Console.Write("Заменить даты по условию? (каждая дата в тексте заменяется на следующую) (Y|N) ");
                if (Console.ReadLine().ToLower().Equals("n"))
                {
                    continue;
                }

                regex = new Regex(@"(0[1-9]|[12][0-9]|3[01])[.,](0[1-9]|1[012])[.,](?:19[0-9]{2}|20(?:[01][0-9]|2[01]))");
                mc = regex.Matches(fulltext);
                if (mc.Count != 0)
                {
                    StringBuilder replaces = new StringBuilder(128);
                    foreach (Match match in mc)
                    {
                        string repres = ChangeDate(match);
                        if (string.IsNullOrEmpty(repres)) continue;
                        replaces.Append($"{match} заменено на {repres}\n");
                        fulltext = fulltext.Replace(match.Value, repres);
                    }

                    MessageBox.Show(replaces.ToString());

                    Console.WriteLine(fulltext); 
                }
                else
                {
                    MessageBox.Show("В тексте нет подходящих дат");
                }

                Console.Write("Продолжить? (Y|N) ");
                if (Console.ReadLine().ToLower().Equals("n"))
                {
                    break;
                }
            }
        }

        private static string ChangeDate(Match match)
        {
            //string[] nums = match.ToString().Split('.', ',');

            //List<string> months = new List<string>(new string[] { "01", "03", "05", "07", "08", "10", "12" });

            DateTime dateres;
            if (DateTime.TryParse(match.ToString(), out dateres))
            {
                dateres = dateres.AddDays(1);
                return dateres.ToShortDateString();
            }
            return null;
        }
    }
}
