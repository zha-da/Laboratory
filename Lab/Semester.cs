using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laboratory.AdditionalClasses;
using Laboratory.Exams;
using Laboratory.Exams.Comparers;
using ConsoleMenu;

namespace Laboratory.Exams
{
    public class Semester
    {
        public Semester(List<Exam> exams)
        {
            Exams = exams;
            Finals = new List<Exam>();
            Tests = new List<Exam>();
            TestsQueue = new Queue<Exam>();
            FinalsQueue = new Queue<Exam>();
            foreach (Exam exam in Exams)
            {
                if (exam is FinalExam)
                {
                    Finals.Add(exam);
                    FinalsQueue.Enqueue(exam);
                }
                else
                {
                    Tests.Add(exam);
                    TestsQueue.Enqueue(exam);
                }
            }
        }

        public List<Exam> Exams { get; set; }
        public List<Exam> Tests { get; set; }
        public List<Exam> Finals { get; set; }
        public Queue<Exam> TestsQueue { get; private set; }
        public Queue<Exam> FinalsQueue { get; private set; }

        public void StartSemester(List<Exam> exams)
        {
            Exams = exams;
            Exams.Sort();

            CMenu sem = new CMenu();
            sem.CurrentSettings.ClosingPhrase = "";

            sem.AddPoint(new MenuPoint("Вывести список всех экзаменов", 
                () => DisplayExams(Exams, true)));

            sem.AddPoint(new MenuPoint("Отсортировать экзамены по алфавиту", () =>
            {
                Exams.Sort(new ComparerByDiscipline());
            }));

            sem.AddPoint(new MenuPoint("Отсортировать экзамены по дате", () =>
            {
                Exams.Sort();
            }));

            PointExecutionResult controls = new PointExecutionResult();
            controls.result = TestsQueue;
            PointExecutionResult finals = new PointExecutionResult();
            finals.result = FinalsQueue;

            sem.AddPoint(new MenuPoint("Сдать тесты", TakeTests));

            sem.AddPoint(new MenuPoint("Вывести результаты работ за весь семестр", DisplayTests));

            sem.AddPoint(new MenuPoint("Сдать сессию", TakeExams));

            sem.AddPoint(new MenuPoint("Вывести результаты экзаменов", DisplayExams));

            sem.AddPoint(new MenuPoint("Завершить работу программы"));

            sem.RunMenu();
            

        }

        public void TakeExams()
        {
            if (TestsQueue.Count != 0)
            {
                Console.WriteLine("У вас сданы не все тесты, сдача экзаменов невозможна");
                Console.ReadKey();
                return;
            }
            Queue<Exam> finals_retake = new Queue<Exam>();
            int b = FinalsQueue.Count;
            for (int i = 0; i < b; i++)
            {
                Exam current = FinalsQueue.Dequeue();
                current.TakeExam();
                if (!current.IsPassed)
                {
                    finals_retake.Enqueue(current);
                }
            }
            FinalsQueue = finals_retake;
        }
        public void TakeTests()
        {
            Queue<Exam> test_retake = new Queue<Exam>();
            int b = TestsQueue.Count;
            for (int i = 0; i < b; i++)
            {
                Exam current = TestsQueue.Dequeue();
                current.TakeExam();
                if(!current.IsPassed)
                {
                    test_retake.Enqueue(current);
                }
            }
            TestsQueue = test_retake;
        }
        public void DisplayExams(Queue<Exam> exret)
        {
            Exam[] ex = new Exam[exret.Count];
            exret.CopyTo(ex, 0);
            int b = exret.Count;
            DisplayExams(ex, false);
        }
        public void DisplayExams()
        {
            if (FinalsQueue.Count == 0)
            {
                Console.WriteLine("Все экзамены сданы");
            }
            else
            {
                Console.WriteLine("Несданные экзамены: ");
                DisplayExams(FinalsQueue);
            }
            Console.WriteLine("Общие результаты: ");
            foreach (var e in Finals)
            {
                e.DisplayInfo();
            }
            Console.ReadKey();
        }
        public void DisplayTests()
        {
            if (TestsQueue.Count == 0)
            {
                Console.WriteLine("Все тесты сданы");
            }
            else
            {
                Console.WriteLine("Несданные тесты: ");
                DisplayExams(TestsQueue);
            }
            Console.WriteLine("Общие результаты: ");
            foreach (var e in Tests)
            {
                e.DisplayInfo();
            }
            Console.ReadKey();
        }
        public void DisplayExams(IEnumerable<Exam> exams, bool StNeed)
        {
            if (StNeed)
            {
                Console.WriteLine("Предстоящие экзамены:"); 
            }
            foreach (Exam exam in exams)
            {
                DateTime dt = exam.ExamDate;
                Console.Write($"{dt.Day}.{dt.Month}.{dt.Year}");
                if (exam is FinalExam)
                {
                    Console.Write(" : Экзамен ");
                }
                else if (exam is FailPassExam)
                {
                    Console.Write(" : Зачет ");
                }
                else if (exam is Control)
                {
                    Console.Write(" : Контрольная работа ");
                }
                else if (exam is Test)
                {
                    Console.Write(" : Тест ");
                }
                Console.WriteLine($"по дисциплине {exam.Discipline}");
            }
            Console.ReadKey();
        }
    }
}
