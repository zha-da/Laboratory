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
        internal string _testTopic;
        /// <summary>
        /// Тема теста
        /// </summary>
        public string TestTopic
        {
            get { return _testTopic; }
            set { _testTopic = value; }
        }
        private int succTakes;
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
            protected set { maxMark = value; }
        }

        #endregion


        #region Constructors
        /// <summary>
        /// Создает экземпляр теста с параметрами по умолчанию
        /// </summary>
        public Test() : base()
        {
            _testTopic = "Не задано";
            maxMark = 0;
            succTakes = 0;
        }
        /// <summary>
        /// Создает экземпляр класса тест
        /// </summary>
        /// <param name="discipline">Название дисциплины</param>
        /// <param name="testTopic">Тема теста</param>
        public Test(string discipline, string testTopic) : base(discipline)
        {
            _testTopic = TestTopic;
            maxMark = 0;
            succTakes = 0;
        }
        /// <summary>
        /// Создает экземпляр класса тест
        /// </summary>
        /// <param name="discipline">Название дисциплины</param>
        /// <param name="testTopic">Тема теста</param>
        /// <param name="questionsQuantity">Общее количество вопросов</param>
        public Test(string discipline, string testTopic, int questionsQuantity) : base(discipline, questionsQuantity)
        {
            _testTopic = testTopic;
            maxMark = 0;
            succTakes = 0;
        }
        /// <summary>
        /// Создает экземпляр класса тест
        /// </summary>
        /// <param name="discipline">Название дисциплины</param>
        /// <param name="testTopic">Тема теста</param>
        /// <param name="questionsQuantity">Общее количество вопросов</param>
        /// <param name="maxScore">Максимальная оценка</param>
        /// <param name="passingScore">Проходной балл</param>
        public Test(string discipline, string testTopic, int questionsQuantity, int maxScore, int passingScore)
            : base(discipline, questionsQuantity, maxScore, passingScore)
        {
            _testTopic = testTopic;
            maxMark = 0;
            succTakes = 0;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Выводит информацию о попытке 
        /// </summary>
        public override void DisplayInfo()
        {
            if (RightAnswers == -1) Print_UnsuccessfulAttemt();
            else
            {
                Print_Discipline();
                Print_Topic();
                Print_Take();
                Print_Questions();
                Print_RightAns();
                Print_PassingSc();
                Print_IsPassed();
                Print_SuccessfulTakes();
                Print_MaxMark();

                Console.WriteLine();
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
            CurrentMark = (int)Math.Round(ratio * MaximumScore);
            IsPassed = CurrentMark >= PassingScore;
            if (IsPassed)
            {
                succTakes++;
            }
            if (_currentMark > maxMark)
            {
                maxMark = _currentMark;
            }
        }
        #endregion
    }
}
