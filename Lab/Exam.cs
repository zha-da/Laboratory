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
        int take = 1;
        private int exTime;
        /// <summary>
        /// Длительность экзамена
        /// </summary>
        public int ExamTime
        {
            get { return exTime; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Длительность экзамена не может быть меньше 0\n");
                    return;
                }
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
                if (value < 0)
                {
                    Console.WriteLine("Максимальная оценка не может быть ниже 0\n");
                    return;
                }
                maxSc = value;
            }
        }
        private int quetsQuan;
        /// <summary>
        /// Общее количество вопросов (не может быть ниже 0)
        /// </summary>
        public int QuestionsQuantity
        {
            get { return quetsQuan; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine($"Количество вопросов не может быть меньше 0\n");
                    return;
                }
                quetsQuan = value;
            }
        }
        private int rightAns;
        /// <summary>
        /// Количество вопросов, на которые получен правильный ответ
        /// </summary>
        public int RightAnswers
        {
            get { return rightAns; }
            private set
            {
                if (value < 0 || value > QuestionsQuantity)
                {
                    Console.WriteLine($"Недопустимое значение количества правильных ответов (от 0 до {quetsQuan})");
                    rightAns = -1;
                    return;
                }
                rightAns = value;
            }
        }
        #endregion


        #region Constructors
        /// <summary>
        /// Создает экземпляр класса с параметрами по умолчанию
        /// </summary>
        public Exam()
        {
            exTime = 45;
            quetsQuan = 60;
            discip = "Не выбрано";
            maxSc = 5;
            rightAns = 0;
        }
        /// <summary>
        /// Создает экземпляр класса
        /// </summary>
        /// <param name="discipline">Название дисциплины</param>
        public Exam(string discipline) : this(discipline, 45, 60, 5) { }
        /// <summary>
        /// Создает экземпляр класса
        /// </summary>
        /// <param name="discipline">Название дисциплины</param>
        /// <param name="examTime">Длительность экзамена</param>
        /// <param name="questionsQuantity">Общее количество вопросов</param>
        /// <param name="maxScore">Максимальная оценка за экзамен</param>
        public Exam(string discipline, int questionsQuantity ,int examTime, int maxScore)
        {
            discip = discipline;
            exTime = examTime;
            quetsQuan = questionsQuantity;
            maxSc = maxScore;
            rightAns = 0;
        }
        /// <summary>
        /// Создает экземпляр класса
        /// </summary>
        /// <param name="discipline">Название дисциплины</param>
        /// <param name="questionsQuantity">Общее количество вопросов</param>
        public Exam(string discipline, int questionsQuantity) 
            : this(discipline, 45, questionsQuantity, 5) { }
        #endregion


        #region Methods
        /// <summary>
        /// Выводит информацию об экзамене
        /// </summary>
        public void DisplayInfo()
        {
            if (rightAns == -1) Console.WriteLine("Неудачная попытка пройти экзамен\n");
            else Console.WriteLine($"Экзамен по дисциплине: {discip}\n" +
                $"Попытка номер {take}\n" +
                $"Общее количество вопросов: {quetsQuan}\n" +
                $"Из них правильно: {RightAnswers}\n" +
                $"Итоговая оценка: {Math.Round(CalculateMark() * MaximumScore)}\n");
        }
        /// <summary>
        /// Определяет итоговую оценку
        /// </summary>
        public double CalculateMark() => (double)RightAnswers / QuestionsQuantity;
        /// <summary>
        /// Проводит экзамен у одного человека
        /// </summary>
        /// <param name="rightAnswers">Количество вопросов, на которые получен правильный ответ</param>
        public void TakeExam(int rightAnswers)
        {
            RightAnswers = rightAnswers;
            if (rightAns == -1) return;
            take++;
        }
        /// <summary>
        /// Проводит экзамен у одного человека (количество правильных ответов генерируется случайным образом)
        /// </summary>
        public void TakeExam()
        {
            take++;
            TakeExamRnd();
        }
        void TakeExamRnd()
        {
            Random rnd = new Random();
            RightAnswers = rnd.Next(0, quetsQuan);
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
