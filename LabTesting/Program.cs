using System;
using System.Collections.Generic;
using Laboratory.Exams;
using System.IO;
using Laboratory.AdditionalClasses;
using System.Globalization;

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
            //Console.WriteLine("Имя файла:");
            //string directory = Console.ReadLine();

            Semester kb21 = new Semester("Exams.txt");
            kb21.StartSemester();
        }
        #endregion

        //#region v0
        //static void Main(string[] args)
        //{
        //    //FinalExam 

        //    try
        //    {
        //        //string date = "31 10 2020";
        //        //DateTime res = DateTime.ParseExact(date, "dd MM yyyy", CultureInfo.CurrentCulture);
        //        //Console.WriteLine(res.ToString());

        //        List<Exam> exams = new List<Exam>(4);

        //        Exam math = new FinalExam(new DateTime(2021, 1, 15), "Математика", -1, -2, 2);
        //        exams.Add(math);

        //        Exam bio = new FailPassExam(new DateTime(2020, 12, 19), "Биология", -2, -2, 2);
        //        exams.Add(bio);

        //        Exam inf = new Control(new DateTime(2020, 11, 11), "Информатика", 15, 9);
        //        exams.Add(inf);

        //        Exam hist = new Test(new DateTime(2020, 11, 13), "История", "Правление Николая 2", 20, 15, 100);
        //        exams.Add(hist);

        //        Semester kb21 = new Semester(exams);

        //        kb21.StartSemester(exams);

        //        //Queue<Exam> session = new Queue<Exam>(exams);

        //        //Queue<Exam> retake = new Queue<Exam>();

        //        //exams.Sort();

        //        //DisplayExams(exams);


        //        //for (int i = 0; i < session.Count; i++)
        //        //{
        //        //    Exam current = session.Dequeue();
        //        //    current.TakeExam();
        //        //    if (!current.IsPassed)
        //        //    {
        //        //        retake.Enqueue(current);
        //        //    }
        //        //}


        //        //Console.WriteLine();

        //        //if (retake.Count == 0)
        //        //{
        //        //    Console.WriteLine("Поздравляем! Сессия окончена без долгов!");
        //        //}
        //        //else
        //        //{
        //        //    Console.WriteLine("Вы окончили сессию с долгами:");
        //        //    DisplayExams(retake);
        //        //}

        //        //Console.ReadKey();
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
        //static void DisplayExams(Queue<Exam> exret)
        //{
        //    List<Exam> ex = new List<Exam>();

        //    for (int i = 0; i < exret.Count; i++)
        //    {
        //        ex.Add(exret.Dequeue());
        //    }

        //    DisplayExams(ex);
        //}
        //static void DisplayExams(IEnumerable<Exam> exams)
        //{
        //    foreach (Exam exam in exams)
        //    {
        //        if (exam is FinalExam)
        //        {
        //            Console.Write("Экзамен ");
        //        }
        //        else if (exam is FailPassExam)
        //        {
        //            Console.Write("Зачет ");
        //        }
        //        else if (exam is Control)
        //        {
        //            Console.Write("Контрольная работа ");
        //        }
        //        else if (exam is Test)
        //        {
        //            Console.Write("Тест ");
        //        }
        //        Console.WriteLine($"по дисциплине {exam.Discipline}");
        //    }
        //}
        //#endregion
    }
}
