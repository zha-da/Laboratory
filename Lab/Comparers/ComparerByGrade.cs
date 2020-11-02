using System.Collections.Generic;

namespace Laboratory.Exams.Comparers
{
    /// <summary>
    /// Компаратор для класса Контрольная работа
    /// </summary>
    public class ComparerByGrade : IComparer<Control>
    {
        /// <summary>
        /// Сравнивает 2 контрольные работы
        /// </summary>
        /// <param name="x">К\р 1</param>
        /// <param name="y">К\р 2</param>
        /// <returns>Результат сравнения двух К\р по высшей полученной оценке</returns>
        public int Compare(Control x, Control y)
        {
            if (x.HighestMark > y.HighestMark)
                return 1;
            else if (x.HighestMark < y.HighestMark)
                return -1;
            else
                return 0;
        }
    }
}
