using System.Collections.Generic;

namespace Laboratory.Exams.Comparers
{
    /// <summary>
    /// Компаратор по дисциплинам
    /// </summary>
    public class ComparerByDiscipline : IComparer<Exam>
    {
        /// <summary>
        /// Сравнивает два экзамена по названиям дисциплин
        /// </summary>
        /// <param name="x">Экзамен 1</param>
        /// <param name="y">Экзамен 2</param>
        /// <returns>Результат сравнения двух дисциплин по алфавиту</returns>
        public int Compare(Exam x, Exam y)
        {
            return x.Discipline.CompareTo(y.Discipline);
        }
    }
}
