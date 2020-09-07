using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    /// <summary>
    /// Класс экзамена
    /// </summary>
    public class Exam
    {
        #region Fields
        private int exTime;
        /// <summary>
        /// Длительность экзамена
        /// </summary>
        public int ExamTime
        {
            get { return exTime; }
            set
            {
                if (value < 0) return;
                exTime = value;
            }
        }
        private string discip;
        /// <summary>
        /// Название дисциплины
        /// </summary>
        public string Discipline
        {
            get { return discip; }
            set { discip = value; }
        }
        private int maxSc;
        /// <summary>
        /// Максимальная отметка (не может быть ниже 0).
        /// </summary>
        public int MaximumScore
        {
            get { return maxSc; }
            set
            {
                if (value < 0) return;
                maxSc = value;
            }
        }
        private int quetsQuan;
        /// <summary>
        /// Количество вопросов
        /// </summary>
        public int QuestionsQuantity
        {
            get { return quetsQuan; }
            set { quetsQuan = value; }
        }
        /// <summary>
        /// Вопросы к экзамену
        /// </summary>
        public List<ExamQuestion> Questions { get; set; }
        #endregion


        #region Constructors
        /// <summary>
        /// Создает экземпляр класса с параметрами по умолчанию
        /// </summary>
        public Exam()
        {
            exTime = 45;
            discip = "Не выбрано";
            maxSc = 5;
        }
        /// <summary>
        /// Создает экземпляр класса
        /// </summary>
        /// <param name="discipline">Название дисциплины</param>
        public Exam(string discipline)
        {
            exTime = 45;
            this.discip = discipline;
            maxSc = 5;
        }
        /// <summary>
        /// Создает экземпляр класса
        /// </summary>
        /// <param name="maximumScore">Максимальная отметка за экзамен</param>
        /// <param name="discipline">Название дисциплины</param>
        /// <param name="examTime">Длительность экзамена</param>
        public Exam(int maximumScore, string discipline, int examTime)
        {
            maxSc = maximumScore;
            discip = discipline;
            exTime = examTime;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Выводит всю информацию об экзамене
        /// </summary>
        public void DisplayInfo()
        {

        }
        /// <summary>
        /// Проводит экзамен у группы учеников
        /// </summary>
        public void TakeAnExam( )
        {

        }
        /// <summary>
        /// Определяет итоговую оценку
        /// </summary>
        public double CalculateMark()
        {
            double res = 0;
            foreach (ExamQuestion q in Questions)
            {
                if (q.AnsweredCorrectly) res++;
            }
            return res / quetsQuan;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сравнивает 2 экзамена по количеству вопросов
        /// </summary>
        /// <param name="e1">Экзамен 1</param>
        /// <param name="e2">Экзамен 2</param>
        /// <returns>Результат сравнения по количеству вопросов</returns>
        public static bool operator > (Exam e1, Exam e2)
        {
            return e1.quetsQuan > e2.quetsQuan;
        }
        /// <summary>
        /// Сравнивает 2 экзамена по количеству вопросов
        /// </summary>
        /// <param name="e1">Экзамен 1</param>
        /// <param name="e2">Экзамен 2</param>
        /// <returns>Результат сравнения по количеству вопросов</returns>
        public static bool operator < (Exam e1, Exam e2)
        {
            return e1.quetsQuan < e2.quetsQuan;
        }
        #endregion
    }
}
