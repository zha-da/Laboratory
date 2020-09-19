using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.Exams
{
    /// <summary>
    /// Класс теста
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

        #endregion


        #region Constructors
        /// <summary>
        /// Создает экземпляр теста с параметрами по умолчанию
        /// </summary>
        public Test() : base()
        {
            _testTopic = "Не задано";
        }
        /// <summary>
        /// Создает экземпляр класса тест
        /// </summary>
        /// <param name="discipline">Название дисциплины</param>
        /// <param name="testTopic">Тема теста</param>
        public Test(string discipline, string testTopic) : base(discipline)
        {
            _testTopic = TestTopic;
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
        }
        #endregion


        #region Methods
        /// <summary>
        /// Выводит информацию о попытке 
        /// </summary>
        public override void DisplayInfo()
        {
            if (RightAnswers == -1) Console.WriteLine("Неудачная попытка\n");
            else Console.WriteLine($"Тест по дисциплине: {discip}\n" +
                $"Тема теста: {_testTopic}\n" +
                $"Общее количество вопросов: {quetsQuan}\n" +
                $"Из них правильно: {rightAns}\n" +
                $"Попытка №{take}\n" +
                $"Итоговая оценка: {_currentMark}\n" +
                $"Тест сдан: {IsPassed}\n");
        }
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
        }
        #endregion
    }
}
