using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Laboratory.AdditionalClasses;
using Laboratory.Exams;
using Laboratory.Exams.Comparers;
using System.IO;
using System.Globalization;
using Laboratory.Exams.Exceptions;
using Microsoft.Win32;
using System.Collections;

namespace Laboratory.Exams
{
    /// <summary>
    /// Класс семестра
    /// </summary>
    public class Semester : IEnumerable<Exam>, IPlugable
    {
        #region getfromfile
        /// <summary>
        /// Получает информацию об экзаменах из файла
        /// </summary>
        /// <param name="directory">Путь к файлу / имя файла</param>
        /// <returns>Список предстоящих экзаменов</returns>
        public static List<Exam> GetFromFile(string directory)
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
                //Console.WriteLine("Файл с подобным именем не найден. Повторите попытку");
                //Console.ReadKey();
            }
            catch (IOException)
            {
                Logger.NewLog($"Ошибка открытия файла с именем {directory}\n");
                //Console.WriteLine("Ошибка открытия файла. Повторите попытку");
                //Console.ReadKey();
            }
            catch (Exception ex)
            {
                Logger.NewLog("Неизвестная ошибка: " + ex.Message + "\n");
                //Console.WriteLine($"Неизвестная ошибка: {ex.Message}\n");
                //Console.ReadKey();
            }
            return null;
        } 
        #endregion

        #region Constructors
        /// <summary>
        /// Создает экземпляр класса Семестр
        /// </summary>
        /// <param name="exams">Список экзаменов</param>
        public Semester(IEnumerable<Exam> exams)
        {
            Exams = new List<Exam>(exams);
        }
        /// <summary>
        /// Создает экзамепляр класса
        /// </summary>
        /// <param name="directory">Путь к файлу / Имя файла</param>
        public Semester(string directory)
        {
            Exams = GetFromFile(directory);
        }
        /// <summary>
        /// Создает экземпляр класса
        /// </summary>
        /// <param name="exams">Список экзаменов</param>
        public Semester(List<Exam> exams)
        {
            Exams = exams;
        }
        /// <summary>
        /// Создает пустой семестр
        /// </summary>
        public Semester()
        {
            Exams = new List<Exam>();
        }
        #endregion

        #region Props
        /// <summary>
        /// Список всех экзаменов
        /// </summary>
        public List<Exam> Exams { get; set; }
        #endregion
        
        #region Methods
        /// <summary>
        /// Убирает все экзамены определенного типа
        /// </summary>
        /// <param name="type">Тип</param>
        public void DeleteAll(Type type)
        {
            Exams.RemoveAll(x => x.GetType() == type);
        }
        private Semester FindAnd(Predicate<Exam> filter)
        {
            if (filter == null) return this;
            Semester res = new Semester();
            foreach (var e in Exams)
            {
                if (filter(e))
                {
                    res.Add(e);
                }
            }
            return res;
        }
        private Semester FindOr(Predicate<Exam> filter)
        {
            if (filter == null) return new Semester(Exams);
            return new Semester(Filter(filter));
        }
        private IEnumerable<Exam> Filter(Predicate<Exam> filter)
        {
            Delegate[] dels = filter.GetInvocationList();
            foreach (var e in Exams)
            {
                foreach (Predicate<Exam> del in dels)
                {
                    if (del(e))
                    {
                        yield return e;
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// Фильтрует список экзаменов по условиям
        /// </summary>
        /// <param name="or_filter">Условия типа ИЛИ</param>
        /// <param name="and_filter">Условия типа И</param>
        /// <returns>Отфильтрованный список экзаменов</returns>
        public Semester Filter(Predicate<Exam> or_filter, Predicate<Exam> and_filter)
        {
            Semester res = FindOr(or_filter);
            res = res.FindAnd(and_filter);
            return res;
        }
        /// <summary>
        /// Добавляет список экзаменов в семестр
        /// </summary>
        /// <param name="exams">Список экзаменов</param>
        public void Add(List<Exam> exams)
        {
            try
            {
                Exams.AddRange(exams);
            }
            catch (ArgumentNullException ex)
            {
                Logger.NewLog(ex.Message);
            }
        }
        /// <summary>
        /// Добавляет один экзамен в семестр
        /// </summary>
        /// <param name="exam">Экзамен</param>
        public void Add(Exam exam)
        {
            try
            {
                Exams.Add(exam);
            }
            catch (ArgumentNullException ex)
            {
                Logger.NewLog(ex.Message);
            }
        }
        /// <summary>
        /// Сравнивает 2 объекта класса семестр
        /// </summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns>Результат сравнения</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Semester))
            {
                return false;
            }
            Semester sem = obj as Semester;
            if (Exams.Count != sem.Exams.Count)
            {
                return false;
            }
            for (int i = 0; i < Exams.Count; i++)
            {
                if (!Exams[i].Equals(sem.Exams[i])) return false;
            }
            return true;
        }
        /// <summary>
        /// Возвращает хэш-код объекта
        /// </summary>
        /// <returns>Хэш-код</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// Отображает список экзаменов в семестре
        /// </summary>
        public void WriteExams()
        {
            foreach (var item in Exams)
            {
                Console.WriteLine(item.ToString());
            }
        }
        /// <summary>
        /// Возвращает перечисление для класса Семестр
        /// </summary>
        /// <returns>Перечисление</returns>
        public IEnumerator<Exam> GetEnumerator()
        {
            return ((IEnumerable<Exam>)Exams).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Exams).GetEnumerator();
        }
        /// <summary>
        /// Получает экзамен Семестра по заданному индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <returns>Экзамен по индексу</returns>
        public Exam this[int index]
        {
            get { return Exams[index]; }
            set { Exams[index] = value; }
        }
        #region addinfo
        /// <summary>
        /// Загружает информацию из файла
        /// </summary>
        public void AddInfo()
        {
            Exams = new List<Exam>();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Текстовые файлы|*.txt";
            bool? res1 = ofd.ShowDialog();
            if (res1 == true)
            {
                Exams = GetFromFile(ofd.FileName);
            }
        } 
        #endregion
        /// <summary>
        /// Вывод информации о семестре
        /// </summary>
        /// <returns>Массив строк с информацией о каждом экзамене</returns>
        public string[] Print()
        {
            try
            {
                //if (Exams.Count == 0)
                //{
                //    Exams = new List<Exam>();
                //    OpenFileDialog ofd = new OpenFileDialog();
                //    ofd.Filter = "Текстовые файлы|*.txt";
                //    bool? res1 = ofd.ShowDialog();
                //    if (res1 == true)
                //    {
                //        Exams = GetFromFile(ofd.FileName);  
                //    }
                //}

                if (Exams.Count == 0)
                {
                    Exams = new List<Exam>();
                }

                string[] res = (from e in Exams select e.ToString()).ToArray();
                return res;

                //string[] res = new string[Exams.Count];
                //for (int i = 0; i < Exams.Count; i++)
                //{
                //    res[i] = Exams[i].ToString();
                //}
                //return res;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
                Logger.NewLog(ex.Message);
            }
            return null;
        }
        /// <summary>
        /// Сортировка по названию предметов
        /// </summary>
        public void SortByString()
        {
            Exams.Sort();
        }
        /// <summary>
        /// Сортировка по количеству вопросов
        /// </summary>
        public void SortByInt()
        {
            Exams.Sort(new ComparerByQuestions());
        }
        #endregion
    }
}
