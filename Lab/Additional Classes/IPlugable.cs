using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.AdditionalClasses
{
    /// <summary>
    /// Интерфейс плагина
    /// </summary>
    public interface IPlugable
    {
        #region addinfo
        /// <summary>
        /// Загрузка информации
        /// </summary>
        void AddInfo(); 
        #endregion
        /// <summary>
        /// Вывод информации
        /// </summary>
        /// <returns>Массив строк, которые нужно вывести</returns>
        string[] Print();
        /// <summary>
        /// Сортировка по строковому полю
        /// </summary>
        void SortByString();
        /// <summary>
        /// Сортировка по числовому полю
        /// </summary>
        void SortByInt();
    }
}
