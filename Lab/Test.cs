using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    /// <summary>
    /// Класс теста
    /// </summary>
    public class Test : Exam
    {
        #region Fields
        int take = 1;
        #endregion


        #region Constructors
        /// <summary>
        /// Создает экземпляр теста с параметрами по умолчанию
        /// </summary>
        public Test() : base() { }
        /// <summary>
        /// Создает экземпляр класса тест
        /// </summary>
        /// <param name="discipline">Название дисциплины</param>
        public Test(string discipline) : base(discipline) { }
        /// <summary>
        /// Создает экземпляр класса тест
        /// </summary>
        /// <param name="discipline">Название дисциплины</param>
        /// <param name="questionsQuantity">Общее количество вопросов</param>
        public Test(string discipline, int questionsQuantity) : base(discipline, questionsQuantity) { }
        /// <summary>
        /// Создает экземпляр класса тест
        /// </summary>
        /// <param name="discipline">Название дисциплины</param>
        /// <param name="questionsQuantity">Общее количество вопросов</param>
        /// <param name="maxScore">Максимальная оценка</param>
        /// <param name="passingScore">Проходной балл</param>
        public Test(string discipline, int questionsQuantity, int maxScore, int passingScore)
            : base(discipline, questionsQuantity, maxScore, passingScore) { }
        #endregion


        #region Methods
        /// <summary>
        /// Выводит информацию о попытке 
        /// </summary>
        public override void DisplayInfo()
        {
            if (RightAnswers == -1) Console.WriteLine("Неудачная попытка");
            else Console.WriteLine($"Тест по дисциплине: {Discipline}\n" +
                $"Общее количество вопросов: {QuestionsQuantity}\n" +
                $"Из них правильно: {RightAnswers}\n" +
                $"Попытка №{take}\n" +
                $"Итоговая оценка: {CurrentMark}\n" +
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
            base.CalculateMark();
        }
        #endregion
    }
}
