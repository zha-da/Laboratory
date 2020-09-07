using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    /// <summary>
    /// Класс экзамена
    /// </summary>
    class Exam
    {
        #region Fields
        private int exTime;

        public int ExamTime
        {
            get { return exTime; }
            set
            {
                if (value < 0) return;
                exTime = value;
            }
        }
        private string discip;

        public string Discipline
        {
            get { return discip; }
            set { discip = value; }
        }
        private int maxSc;

        public int MaximumScore
        {
            get { return maxSc; }
            set { maxSc = value; }
        }
        #endregion
        public Exam()
        {
            exTime = 45;
            discip = "Не выбрано";
            maxSc = 5;
        }
        public Exam(string discipline)
        {
            exTime = 45;
            this.discip = discipline;
            maxSc = 5;
        }
        public Exam(int maximumScore, string discipline, int examTime)
        {
            maxSc = maximumScore;
            discip = discipline;
            exTime = examTime;
        }
    }
}
