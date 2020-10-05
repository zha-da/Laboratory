using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laboratory.Exams;

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
            FailPassExam bio = new FailPassExam("Биология");

            Console.WriteLine($"{bio.Discipline}\n" +
                $"{bio.GradingScale}\n" +
                $"{bio.QuestionsQuantity}\n" +
                $"{bio.PassingScore}\n" +
                $"{bio.MaxTakes}\n");

            for (int i = 0; i < 3; i++)
            {
                bio.TakeExam();
                bio.DisplayInfo(); 
            }

            Test math = new Test("Математика", "Арифметические операции");
            math.GradingScale = 100;

            Console.WriteLine($"{math.Discipline}\n" +
                $"{math.TestTopic}\n" +
                $"{math.QuestionsQuantity}\n" +
                $"{math.PassingScore}\n" +
                $"{math.GradingScale}\n");

            for (int i = 0; i < 3; i++)
            {
                math.TakeExam();
                math.DisplayInfo();
            }

            Control inf = new Control("Информатика", 12);

            Console.WriteLine($"{inf.Discipline}\n" +
                $"{inf.GradingScale}\n" +
                $"{inf.QuestionsQuantity}\n");

            for (int i = 0; i < 3; i++)
            {
                inf.TakeExam();
                inf.DisplayInfo();
            }

            FinalExam alg = new FinalExam("Алгебра", 2);

            Console.WriteLine($"{alg.Discipline}\n" +
                $"{alg.GradingScale}\n" +
                $"{alg.QuestionsQuantity}\n");

            for (int i = 0; i < 3; i++)
            {
                alg.TakeExam();
                alg.DisplayInfo();
            }

        }

    }
}
