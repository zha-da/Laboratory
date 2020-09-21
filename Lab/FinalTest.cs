using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.Exams
{
    /// <summary>
    /// Класс итогового экзамена
    /// </summary>
    class FinalTest : Test
    {
        #region Fields
        private int _maxTakes = 3;
        public int MaxTakes
        {
            get => _maxTakes;
            protected set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Общее количество попыток не может быть меньше 0\n");
                    return;
                }
                _maxTakes = value;
            }
        }
        
        #endregion
    }
}
