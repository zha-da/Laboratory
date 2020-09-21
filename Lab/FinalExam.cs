using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.Exams
{
    /// <summary>
    /// Класс для зачета
    /// </summary>
    public class FinalExam : Exam
    {
        #region Fields
        private int _maxTakes = 3;
        /// <summary>
        /// Максимальное количество попыток
        /// </summary>
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

        #region Methods
        /// <summary>
        /// Выводит информацию о попытке сдать зачет
        /// </summary>
        public override void DisplayInfo()
        {
            if (RightAnswers == -1) Print_UnsuccessfulAttemt();
            else
            {
                Print_Discipline();
                Print_Take();
                Print_TakesLeft();
                Print_Questions();
                Print_RightAns();
                Print_PassingSc();
                Print_IsPassed();

                Console.WriteLine();
            }
        }
        /// <summary>
        /// Выводит сообщение об оставшихся попытках
        /// </summary>
        protected virtual void Print_TakesLeft()
        {
            Console.WriteLine($"Осталось попыток: {_maxTakes - take}");
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
        /// <summary>
        /// Проводит экзамен у одного человека (количество правильных ответов генерируется случайным образом)
        /// </summary>
        public override void TakeExam()
        {
            if (IsPassed)
            {
                Console.WriteLine("Зачет уже сдан");
                return;
            }
            Random rnd = new Random();
            RightAnswers = rnd.Next(0, QuestionsQuantity);
            if (RightAnswers == -1) return;
            if (take++ > MaxTakes)
            {
                Console.WriteLine("Количество попыток исчерпано\n");
            }
            CalculateMark();
        }
        #endregion
    }
}
