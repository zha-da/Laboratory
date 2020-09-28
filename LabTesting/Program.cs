using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laboratory.Exams;
using System.IO;

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

        static void Main(string[] args)
        {

            using (StreamReader sr = new StreamReader("Exams.txt"))
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
                //Exam bio = new FailPassExam("Биология");

                //Exam math = new Test("Математика", "Арифметические операции");
                //math.GradingScale = 100;

                //Exam inf = new Control("Информатика", 12);

                //Exam alg = new FinalExam("Алгебра", 2);

                //Exam[] vs = new Exam[] { bio, math, inf, alg };

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

    }
}
