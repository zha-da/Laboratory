using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laboratory.AdditionalClasses;
using Laboratory.Exams;
using Laboratory.Exams.Comparers;
using ConsoleMenu;
using System.IO;
using System.Globalization;
using Laboratory.Exams.Exceptions;

namespace Laboratory.Exams
{
    /// <summary>
    /// Класс семестра
    /// </summary>
    public class Semester
    {
        /// <summary>
        /// Получает информацию об экзаменах из файла
        /// </summary>
        /// <param name="directory">Путь к файлу / имя файла</param>
        /// <returns>Список предстоящих экзаменов</returns>
        public List<Exam> GetExamsFromFile(string directory)
        {
            try
            {
                List<Exam> vs = new List<Exam>(4);
                using (StreamReader sr = new StreamReader(directory))
                {

                    while (!sr.EndOfStream)
                    {
                        string[] newEx;
                        DateTime date;
                        switch (sr.ReadLine())
                        {
                            case "зачет":
                                newEx = new string[5];
                                for (int i = 0; i < 5; i++)
                                {
                                    newEx[i] = sr.ReadLine();
                                }
                                date = DateTime.ParseExact(newEx[0], "dd MM yyyy", CultureInfo.CurrentCulture);
                                vs.Add(new FailPassExam(date, newEx[1], int.Parse(newEx[2]), int.Parse(newEx[3]), int.Parse(newEx[4])));
                                break;
                            case "тест":
                                newEx = new string[6];
                                for (int i = 0; i < 6; i++)
                                {
                                    newEx[i] = (sr.ReadLine());
                                }
                                date = DateTime.ParseExact(newEx[0], "dd MM yyyy", CultureInfo.CurrentCulture);
                                vs.Add(new Test(date, newEx[1], newEx[2], int.Parse(newEx[3]), int.Parse(newEx[4]), int.Parse(newEx[5])));
                                break;
                            case "контрольная":
                                newEx = new string[4];
                                for (int i = 0; i < 4; i++)
                                {
                                    newEx[i] = sr.ReadLine();
                                }
                                date = DateTime.ParseExact(newEx[0], "dd MM yyyy", CultureInfo.CurrentCulture);
                                vs.Add(new Control(date, newEx[1], int.Parse(newEx[2]), int.Parse(newEx[3])));
                                break;
                            case "экзамен":
                                newEx = new string[5];
                                for (int i = 0; i < 5; i++)
                                {
                                    newEx[i] = (sr.ReadLine());
                                }
                                date = DateTime.ParseExact(newEx[0], "dd MM yyyy", CultureInfo.CurrentCulture);
                                vs.Add(new FinalExam(date, newEx[1], int.Parse(newEx[2]), int.Parse(newEx[3]), int.Parse(newEx[4])));
                                break;
                        }
                    }
                }
                return vs;
            }
            catch (FileNotFoundException)
            {
                Logger.NewLog($"Файл с именем {directory} не найден\n");
                Console.WriteLine("Файл с подобным именем не найден. Повторите попытку");
                Console.ReadKey();
            }
            catch (IOException)
            {
                Logger.NewLog($"Ошибка открытия файла с именем {directory}\n");
                Console.WriteLine("Ошибка открытия файла. Повторите попытку");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Logger.NewLog("Неизвестная ошибка: " + ex.Message + "\n");
                Console.WriteLine($"Неизвестная ошибка: {ex.Message}\n");
                Console.ReadKey();
            }
            return null;
        }

        #region Constructors
        /// <summary>
        /// Создает экзамепляр класса
        /// </summary>
        /// <param name="directory">Путь к файлу / Имя файла</param>
        public Semester(string directory)
        {
            Exams = GetExamsFromFile(directory);
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
        /// <summary>
        /// Создает экземпляр класса
        /// </summary>
        /// <param name="exams">Список экзаменов</param>
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
        #endregion

        #region Props
        /// <summary>
        /// Список всех экзаменов
        /// </summary>
        public List<Exam> Exams { get; set; }
        /// <summary>
        /// Список всех тестов
        /// </summary>
        public List<Exam> Tests { get; set; }
        /// <summary>
        /// Список итоговых экзаменов
        /// </summary>
        public List<Exam> Finals { get; set; }
        /// <summary>
        /// Очередь для тестов
        /// </summary>
        public Queue<Exam> TestsQueue { get; private set; }
        /// <summary>
        /// Очередь для экзаменов
        /// </summary>
        public Queue<Exam> FinalsQueue { get; private set; } 
        #endregion
        /// <summary>
        /// Начать семестр
        /// </summary>
        public void StartSemester()
        {
            try
            {
                if (Exams == null)
                {
                    throw new SemesterEmptyException("Семестр пуст");
                }
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

                sem.AddPoint(new MenuPoint("Сдать тесты", TakeTests));

                sem.AddPoint(new MenuPoint("Вывести результаты работ за весь семестр", DisplayTests));

                sem.AddPoint(new MenuPoint("Сдать сессию", TakeExams));

                sem.AddPoint(new MenuPoint("Вывести результаты экзаменов", DisplayExams));

                sem.AddPoint(new MenuPoint("Завершить работу программы"));

                sem.RunMenu();
            }
            catch (SemesterEmptyException sex)
            {
                Logger.NewLog(sex.Message);
                Console.WriteLine(sex.Message);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Logger.NewLog("Неизвестная ошибка" + ex.Message + "\n");
                Console.WriteLine($"Неизвестная ошибка {ex.Message}\n");
                Console.ReadKey();
            }
        }
        void TakeExams()
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
        void TakeTests()
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
        void DisplayExams(Queue<Exam> exret)
        {
            Exam[] ex = new Exam[exret.Count];
            exret.CopyTo(ex, 0);
            DisplayExams(ex, false);
        }
        void DisplayExams()
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
        void DisplayTests()
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
        void DisplayExams(IEnumerable<Exam> exams, bool StNeed)
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
