using System.Collections.Generic;

namespace Laboratory.Exams.Comparers
{
    /// <summary>
    /// Класс компаратора для экземпляров класса Тест и его наследников
    /// </summary>
    public class ComparerByQuestions : IComparer<Test>
    {
        /// <summary>
        /// Сравнивает два теста
        /// </summary>
        /// <param name="x">Тест 1</param>
        /// <param name="y">Тест 2</param>
        /// <returns>Результат сравнения двух тестов по количеству вопросов</returns>
        public int Compare(Test x, Test y)
        {
            if (x.QuestionsQuantity > y.QuestionsQuantity)
                return 1;
            else if (x.QuestionsQuantity < y.QuestionsQuantity)
                return -1;
            else
                return 0;
        }
    }
}
