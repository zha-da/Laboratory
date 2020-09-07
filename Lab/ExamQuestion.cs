using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    /// <summary>
    /// Класс экзаминационного вопроса
    /// </summary>
    public class ExamQuestion
    {
        private string qest;
        /// <summary>
        /// Вопрос
        /// </summary>
        public string Question
        {
            get { return qest; }
            set { qest = value; }
        }
        private int rightAns;
        /// <summary>
        /// Номер правильного ответа
        /// </summary>
        public int RightAnswer
        {
            get { return rightAns; }
            set
            {
                if (value < 0 || value > 100) return;
                rightAns = value;
            }
        }

        private int ansQuant;
        /// <summary>
        /// Количество возможных ответов на вопрос
        /// </summary>
        public int AnswerQuantity
        {
            get { return ansQuant; }
            set
            {
                if (value < 0 || value > 100) return;
                ansQuant = value;
            }
        }
        private bool asnCorr;
        /// <summary>
        /// Получен ли правильный ответ на вопрос
        /// </summary>
        public bool AnsweredCorrectly
        {
            get { return asnCorr; }
            set { asnCorr = value; }
        }
        public void AnswerQuestion()
    }
}
