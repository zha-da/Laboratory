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
    public abstract class Exam
    {
        #region Fields
        internal int take = 0;

        internal string discip;
        /// <summary>
        /// Название дисциплины
        /// </summary>
        public string Discipline
        {
            get { return discip; }
            set { discip = value; }
        }

        internal int maxSc;
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

        internal int questQuan;
        /// <summary>
        /// Общее количество вопросов (не может быть ниже 0)
        /// </summary>
        public int QuestionsQuantity
        {
            get { return questQuan; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine($"Количество вопросов не может быть меньше 0\n");
                    return;
                }
                questQuan = value;
            }
        }

        internal int rightAns;
        /// <summary>
        /// Количество вопросов, на которые получен правильный ответ
        /// </summary>
        public int RightAnswers
        {
            get { return rightAns; }
            protected set
            {
                if (value < 0 || value > QuestionsQuantity)
                {
                    Console.WriteLine($"Недопустимое значение количества правильных ответов (от 0 до {questQuan})");
                    rightAns = -1;
                    return;
                }
                rightAns = value;
            }
        }
        internal int _currentMark;
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
            questQuan = 60;
            discip = "Не выбрано";
            maxSc = 60;
            rightAns = 0;
        }
        /// <summary>
        /// Создает экземпляр класса
        /// </summary>
        /// <param name="discipline">Название дисциплины</param>
        public Exam(string discipline) : this(discipline, 60, 60, 36) { }
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
            questQuan = questionsQuantity;
            maxSc = maxScore;
            rightAns = 0;
        }
        /// <summary>
        /// Создает экземпляр класса
        /// </summary>
        /// <param name="discipline">Название дисциплины</param>
        /// <param name="questionsQuantity">Общее количество вопросов</param>
        public Exam(string discipline, int questionsQuantity)
            : this(discipline, questionsQuantity, questionsQuantity, 0) 
        {
            PassingScore = (int)Math.Round(maxSc * 0.6);
        }
        #endregion


        #region Methods
        /// <summary>
        /// Выводит информацию об экзамене
        /// </summary>
        public virtual void DisplayInfo()
        {
            if (rightAns == -1) Print_UnsuccessfulAttemt();
            else
            {
                Print_Discipline();
                Print_Take();
                Print_Questions();
                Print_RightAns();
                Print_IsPassed();
                Console.WriteLine();
            }
        }

        #region Print methods
        /// <summary>
        /// Выводит сообщение о неудачной попытке
        /// </summary>
        protected virtual void Print_UnsuccessfulAttemt() => Console.WriteLine("Неудачная попытка пройти экзамен\n");
        /// <summary>
        /// Выводит сообщение о названии дисциплины
        /// </summary>
        protected virtual void Print_Discipline() => Console.WriteLine($"Экзамен по дисциплине: {discip}");
        /// <summary>
        /// Выводит сообщение о номере попытки
        /// </summary>
        protected virtual void Print_Take() => Console.WriteLine($"Попытка номер {take}");
        /// <summary>
        /// Выводит сообщение об общем количестве вопросов
        /// </summary>
        protected virtual void Print_Questions() => Console.WriteLine($"Общее количество вопросов: {QuestionsQuantity}");
        /// <summary>
        /// Выводит сообщение о количестве правильных ответов
        /// </summary>
        protected virtual void Print_RightAns() => Console.WriteLine($"Из них правильно: {RightAnswers}");
        /// <summary>
        /// Выводит сообщение о проходном балле
        /// </summary>
        protected virtual void Print_PassingSc() => Console.WriteLine($"Проходной балл: {PassingScore}");
        /// <summary>
        /// Выводит сообщение о том, пройден ли экзамен
        /// </summary>
        protected virtual void Print_IsPassed() => Console.WriteLine($"Экзамен сдан: {IsPassed}");
        #endregion

        /// <summary>
        /// Определяет итоговую оценку
        /// </summary>
        public abstract void CalculateMark();

        /// <summary>
        /// Проводит экзамен у одного человека
        /// </summary>
        /// <param name="rightAnswers">Количество вопросов, на которые получен правильный ответ</param>
        public virtual void TakeExam(int rightAnswers)
        {
            RightAnswers = rightAnswers;
            if (rightAns == -1) return;
            take++;
            CalculateMark();
        }
        /// <summary>
        /// Проводит экзамен у одного человека (количество правильных ответов генерируется случайным образом)
        /// </summary>
        public abstract void TakeExam();
        /// <summary>
        /// Проводит экзамен у одного человека (количество правильных ответов генерируется случайным образом)
        /// </summary>
        public virtual void TakeExamRnd()
        {
            Random rnd = new Random();
            RightAnswers = rnd.Next(0, questQuan);
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
