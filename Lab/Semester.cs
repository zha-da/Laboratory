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
        #region getfromfile
        /// <summary>
        /// Получает информацию об экзаменах из файла
        /// </summary>
        /// <param name="directory">Путь к файлу / имя файла</param>
        /// <returns>Список предстоящих экзаменов</returns>
        public List<Exam> GetFromFile(string directory)
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
        #endregion

        #region Constructors
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
        #endregion

        #region Props
        /// <summary>
        /// Список всех экзаменов
        /// </summary>
        public List<Exam> Exams { get; set; }
        #endregion

        public void SortByTime()
        {
            Exams.Sort();
        }
        public void SortByAlphabet()
        {
            Exams.Sort(new ComparerByDiscipline());
        }
        /// <summary>
        /// Убирает все экзамены определенного типа
        /// </summary>
        /// <param name="type">Тип</param>
        public void DeleteAll(Type type)
        {
            Exams.RemoveAll(x => x.GetType() == type);
        }
        public void DeleteAll(Predicate<Exam> condition)
        {
            Exams.RemoveAll(condition);
        }
        public List<Exam> ReturnAll(Predicate<Exam> condition)
        {
            return Exams.FindAll(condition);
        }
    }
}
