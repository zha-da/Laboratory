using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.Exams
{
    /// <summary>
    /// Класс экзамена
    /// </summary>
    public class Exam
    {
        #region Fields
        internal int take = 0;
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

        private int _currentMark;
        /// <summary>
        /// Оценка за экзамен на текущий момент
        /// </summary>
        public int CurrentMark
        {
            get => _currentMark;
            set
            {
                if (value <= 0) _currentMark = 1;
                else _currentMark = value;
            }
        }
        /// <summary>
        /// Сдан ли экзамен
        /// </summary>
        public bool IsPassed { get; set; }
        /// <summary>
        /// Проходной балл (по умолчанию 60% от максимума с округлением до ближайшего целого числа)
        /// </summary>
        public int PassingScore { get; set; }
        #endregion


        #region Constructors
        /// <summary>
        /// Создает экземпляр класса с параметрами по умолчанию
        /// </summary>
        public Exam()
        {
            PassingScore = (int)Math.Round(maxSc * 0.6);
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
        /// <param name="questionsQuantity">Общее количество вопросов</param>
        /// <param name="maxScore">Максимальная оценка за экзамен</param>
        /// <param name="passingScore">Проходной балл</param>
        public Exam(string discipline, int questionsQuantity, int maxScore, int passingScore)
        {
            discip = discipline;
            PassingScore = passingScore;
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
            : this(discipline, questionsQuantity, 5, 3) { }
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
                $"Итоговая оценка: {CurrentMark}\n" +
                $"Экзамен сдан: {IsPassed}\n");
        }
        /// <summary>
        /// Определяет итоговую оценку
        /// </summary>
        public void CalculateMark()
        {
            double ratio = (double)RightAnswers / QuestionsQuantity;
            CurrentMark = (int)Math.Round(ratio * MaximumScore);
            IsPassed = CurrentMark >= PassingScore;
        }

        /// <summary>
        /// Проводит экзамен у одного человека
        /// </summary>
        /// <param name="rightAnswers">Количество вопросов, на которые получен правильный ответ</param>
        public void TakeExam(int rightAnswers)
        {
            RightAnswers = rightAnswers;
            if (rightAns == -1) return;
            take++;
            CalculateMark();
        }
        /// <summary>
        /// Проводит экзамен у одного человека (количество правильных ответов генерируется случайным образом)
        /// </summary>
        private void TakeExam()
        {
            take++;
            TakeExamRnd();
        }
        /// <summary>
        /// Проводит экзамен у одного человека (количество правильных ответов генерируется случайным образом)
        /// </summary>
        public void TakeExamRnd()
        {
            Random rnd = new Random();
            RightAnswers = rnd.Next(0, quetsQuan);
            if (rightAns == -1) return;
            take++;
            CalculateMark();
        }
        #endregion


        #region Operators
        /// <summary>
        /// Сравнивает 2 экзамена по текущим оценкам
        /// </summary>
        /// <param name="e1">Экзамен 1</param>
        /// <param name="e2">Экзамен 2</param>
        /// <returns>Результат сравнения по оценкам</returns>
        public static bool operator >(Exam e1, Exam e2)
        {
            return e1.CurrentMark > e2.CurrentMark;
        }
        /// <summary>
        /// Сравнивает 2 экзамена по текущим оценкам
        /// </summary>
        /// <param name="e1">Экзамен 1</param>
        /// <param name="e2">Экзамен 2</param>
        /// <returns>Результат сравнения по оценкам</returns>
        public static bool operator <(Exam e1, Exam e2)
        {
            return e1.CurrentMark < e2.CurrentMark;
        }
        /// <summary>
        /// Складывает результаты 2х экзаменов
        /// </summary>
        /// <param name="e1">Экзамен 1</param>
        /// <param name="e2">Экзамен 2</param>
        /// <returns>Среднее арифметическое результатов за 2 экзамена</returns>
        public static double operator +(Exam e1, Exam e2)
        {
            return (double)(e1.CurrentMark + e2.CurrentMark) / 2;
        }
        #endregion
    }
}
