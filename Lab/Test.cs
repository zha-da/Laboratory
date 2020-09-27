using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.Exams
{
    /// <summary>
    /// Класс контрольной
    /// </summary>
    public class Test : Exam
    {
        #region Fields
        private string _testTopic = "Не задано";
        /// <summary>
        /// Тема теста
        /// </summary>
        public string TestTopic
        {
            get { return _testTopic; }
            set { _testTopic = value; }
        }
        private int succTakes = 0;
        /// <summary>
        /// Количество успешных попыток
        /// </summary>
        public int SuccessfulTakes
        {
            get { return succTakes; }
            protected set { succTakes = value; }
        }
        private int maxMark;
        /// <summary>
        /// Максимальная полученная отметка
        /// </summary>
        public int MaxMark
        {
            get { return maxMark; }
            protected set
            {
                if (maxMark < value)
                {
                    maxMark = value;
                }
            }
        }

        #endregion


        #region Constructors
        public Test(string discipline, string testTopic, 
            int questionsQuantity, int passingScore, int gradingScale)
        {
            Discipline = discipline;
            TestTopic = testTopic;
            QuestionsQuantity = questionsQuantity;
            PassingScore = passingScore;
            GradingScale = gradingScale;
        }

        public Test(string discipline, string testTopic,
            int questionsQuantity, int passingScore)
            : this (discipline, testTopic, questionsQuantity, passingScore, 0)
        {
            GradingScale = 100;
        }

        public Test(string discipline, string testTopic,
            int questionsQuantity)
            : this(discipline, testTopic, questionsQuantity, 0)
        {
            PassingScore = (int)Math.Round(QuestionsQuantity * 0.6);
        }

        public Test(string discipline, string testTopic)
            : this(discipline, testTopic, 30, 18, 30) { }

        public Test() { }
        #endregion


        #region Methods
        /// <summary>
        /// Выводит информацию о попытке 
        /// </summary>
        public override void DisplayInfo()
        {
            if (RightAnswers == -1)
            {
                Console.WriteLine("Неудачная попытка пройти тест." +
                    "Проверьте правильность данных\n");
                return;
            }
            else
            {
                Console.WriteLine($"Тест по дисциплине: {Discipline}\n" +
                    $"Тема теста: {TestTopic}\n" +
                    $"Общее количество вопросов: {QuestionsQuantity}\n" +
                    $"Из них правильно: {RightAnswers}\n" +
                    $"Текущая оценка: {CurrentMark} \\ {GradingScale}\n" +
                    $"Высшая оценка: {MaxMark} \\ {GradingScale}\n" +
                    $"Количество успешных попыток: {SuccessfulTakes}\n");
            }
        }
        #region Print methods
        /// <summary>
        /// Выводит сообщение о лучшем результате
        /// </summary>
        protected virtual void Print_MaxMark()
        {
            Console.WriteLine($"Лучший результат: {maxMark}");
        }
        /// <summary>
        /// Выводит сообщение об удачных попытках
        /// </summary>
        protected virtual void Print_SuccessfulTakes()
        {
            Console.WriteLine($"Количество удачных попыток: {succTakes}");
        }
        /// <summary>
        /// Выводит сообщение о названии дисциплины
        /// </summary>
        protected override void Print_Discipline()
        {
            Console.WriteLine($"Тест по дисциплине: {discip}");
        }
        /// <summary>
        /// Выводит сообщение о теме теста
        /// </summary>
        protected virtual void Print_Topic()
        {
            Console.WriteLine($"Тема теста: {_testTopic}");
        }
        /// <summary>
        /// Выводит сообщение о том, пройден ли экзамен
        /// </summary>
        protected override void Print_IsPassed()
        {
            Console.WriteLine($"Тест сдан: {IsPassed}");
        }
        #endregion

        /// <summary>
        /// Проводит экзамен у одного человека (количество правильных ответов генерируется случайным образом)
        /// </summary>
        public override void TakeExam()
        {
            Random rnd = new Random();
            RightAnswers = rnd.Next(0, QuestionsQuantity);
            if (RightAnswers == -1) return;
            take++;
            CalculateMark();
        }
        /// <summary>
        /// Проводит экзамен у одного человека
        /// </summary>
        /// <param name="rightAnswers">Количество вопросов, на которые получен правильный ответ</param>
        public override void TakeExam(int rightAnswers)
        {
            RightAnswers = rightAnswers;
            if (RightAnswers == -1) return;
            take++;
            CalculateMark();
        }
        /// <summary>
        /// Определяет итоговую оценку
        /// </summary>
        public override void CalculateMark()
        {
            double ratio = (double)RightAnswers / QuestionsQuantity;
            CurrentMark = (int)Math.Round(ratio * GradingScale);
            IsPassed = CurrentMark >= PassingScore;
            if (IsPassed)
            {
                succTakes++;
            }
            MaxMark = CurrentMark;
        }
        #endregion
    }
}
