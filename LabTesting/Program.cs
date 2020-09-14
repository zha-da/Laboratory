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
        static void Main(string[] args)
        {
            Exam mathExam = new Exam("Математика", 45);

            mathExam.TakeExam(28);
            mathExam.DisplayInfo();

            mathExam.TakeExam(48);
            mathExam.DisplayInfo();

            mathExam.TakeExam(50);
            mathExam.DisplayInfo();

            mathExam.TakeExam(43);
            mathExam.DisplayInfo();

            Exam bioEx = new Exam("Биология", 12);
            for (int i = 0; i < 2; i++)
            {
                bioEx.TakeExamRnd();
                bioEx.DisplayInfo();
            }

            double res = mathExam + bioEx;
            Console.WriteLine($"Общий результат за оба экзамена: {res}");
            Console.ReadKey();
        }
    }
}
