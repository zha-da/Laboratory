using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laboratory.Exams;
using System.IO;
using Laboratory.AdditionalClasses;

namespace Laboratory
{
    class Program
    {
        #region v1
        //static void Main(string[] args)
        //{
        //    Exam mathExam = new Test("Математика", "Арифметические операции", 45);

        //    mathExam.TakeExam(28);
        //    mathExam.DisplayInfo();

        //    mathExam.TakeExam(48);
        //    mathExam.DisplayInfo();

        //    mathExam.TakeExam(50);
        //    mathExam.DisplayInfo();

        //    mathExam.TakeExam(43);
        //    mathExam.DisplayInfo();

        //    Exam bioEx = new Test("Биология", "Царство Растения", 12);
        //    for (int i = 0; i < 2; i++)
        //    {
        //        bioEx.TakeExamRnd();
        //        bioEx.DisplayInfo();
        //    }

        //    double res = mathExam + bioEx;
        //    Console.WriteLine($"Общий результат за оба экзамена: {res}");
        //    Console.ReadKey();
        //} 
        #endregion

        #region v2
        static void Main(string[] args)
        {

            try
            {
            icoe:
                Console.WriteLine("Имя файла:");
                string directory = Console.ReadLine();
                try
                {
                    using (StreamReader sr = new StreamReader(directory))
                    {
                        List<Exam> vs = new List<Exam>(4);
                        while (!sr.EndOfStream)
                        {
                            string[] newEx;
                            switch (sr.ReadLine())
                            {
                                case "зачет":
                                    newEx = new string[4];
                                    for (int i = 0; i < 4; i++)
                                    {
                                        newEx[i] = sr.ReadLine();
                                    }
                                    vs.Add(new FailPassExam(newEx[0], int.Parse(newEx[1]), int.Parse(newEx[2]), int.Parse(newEx[3])));
                                    break;
                                case "тест":
                                    newEx = new string[5];
                                    for (int i = 0; i < 5; i++)
                                    {
                                        newEx[i] = (sr.ReadLine());
                                    }
                                    vs.Add(new Test(newEx[0], newEx[1], int.Parse(newEx[2]), int.Parse(newEx[3]), int.Parse(newEx[4])));
                                    break;
                                case "контрольная":
                                    newEx = new string[3];
                                    for (int i = 0; i < 3; i++)
                                    {
                                        newEx[i] = sr.ReadLine();
                                    }
                                    vs.Add(new Control(newEx[0], int.Parse(newEx[1]), int.Parse(newEx[2])));
                                    break;
                                case "экзамен":
                                    newEx = new string[4];
                                    for (int i = 0; i < 4; i++)
                                    {
                                        newEx[i] = (sr.ReadLine());
                                    }
                                    vs.Add(new FinalExam(newEx[0], int.Parse(newEx[1]), int.Parse(newEx[2]), int.Parse(newEx[3])));
                                    break;
                            }
                        }

                        foreach (Exam ex in vs)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                ex.TakeExam();
                                ex.DisplayInfo();
                            }
                        }
                    }
                    Console.ReadKey();
                }
                catch (FileNotFoundException)
                {
                    Logger.NewLog("Файл не найден\n");
                    Console.WriteLine("Файл с подобным именем не найден. Повторите попытку");
                    Console.ReadKey();
                    goto icoe;
                }
                catch (DirectoryNotFoundException)
                {
                    Logger.NewLog("Путь не найден\n");
                    Console.WriteLine("Путь не найден. Повторите попытку");
                    Console.ReadKey();
                    goto icoe;
                }
                catch (IOException)
                {
                    Logger.NewLog("Ошибка открытия файла\n");
                    Console.WriteLine("Ошибка открытия файла. Повторите попытку");
                    Console.ReadKey();
                    goto icoe;
                }
            }
            catch (Exception ex)
            {
                Logger.NewLog("Критическая ошибка: " + ex.Message + "\n");
                Console.WriteLine($"Критическая ошибка: {ex.Message} Завершение работы программы\n");
                Console.ReadKey();
            }
        }
        #endregion

        #region v0
        //static void Main(string[] args)
        //{
        //    //FinalExam 

        //    try
        //    {
        //        Exam math = new FinalExam("Математика", -1, -2, 0);

        //        for (int i = 0; i < 5; i++)
        //        {
        //            math.TakeExam();
        //            math.DisplayInfo();
        //        }

        //        Exam bio = new FailPassExam("Биология", -2, -2, 0);

        //        for (int i = 0; i < 4; i++)
        //        {
        //            bio.TakeExam();
        //            bio.DisplayInfo();
        //        }

        //        Console.ReadKey();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"{ex.Message}\n" +
        //            $"{ex.TargetSite.Name} {ex.TargetSite.DeclaringType.Name} {ex.TargetSite.DeclaringType.Namespace}\n" +
        //            $"{ex.Source}\n" +
        //            $"{ex.Data}\n");
        //        foreach (var item in ex.Data)
        //        {
        //            Console.WriteLine(item);
        //        }
        //        Console.ReadKey();
        //    }
        //}
        #endregion
    }
}
