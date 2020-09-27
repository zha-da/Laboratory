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
    public class Control : Exam
    {
        #region Fields
        private int _grdScale = 5;

        public override int GradingScale
        {
            get => _grdScale;
        }

        private int _maxMark = 0;

        public int MaxMark
        {
            get { return _maxMark; }
            set
            {
                if (_maxMark < value)
                {
                    _maxMark = value;
                }
            }
        }

        private int _passSc = 3;

        public override int PassingScore
        {
            get => _passSc;
            set
            {
                if (value < 1)
                {
                    Console.WriteLine("Проходной балл не может быть ниже 1\n");
                    return;
                }
                _passSc = value;
            }
        }
        #endregion

        #region Constructors
        public Control(string discipline, int questionsQuantity,
            int passingScore)
        {
            Discipline = discipline;
            QuestionsQuantity = questionsQuantity;
            PassingScore = passingScore;
        }

        public Control(string discipline, int questionsQuantity)
            : this(discipline, questionsQuantity, 3) { }

        public Control(string discipline)
            : this(discipline, 30, 3) { }

        public Control() { }
        #endregion

        #region Methods
        /// <summary>
        /// Выводит информацию о попытке сдать зачет
        /// </summary>
        public override void DisplayInfo()
        {
            if (RightAnswers == -1)
            {
                Console.WriteLine("Неудачная попытка сдать зачет. " +
                    "Проверьте правильность данных\n");
                return;
            }
            Console.WriteLine($"Контрольная работа по предмету: {Discipline}\n" +
                $"Общее количество вопросов: {QuestionsQuantity}\n" +
                $"Из них правильно: {RightAnswers}\n" +
                $"Текущая оценка: {CurrentMark}\n" +
                $"Максимальная оценка: {MaxMark}\n" +
                $"Контрольная работа сдана: {IsPassed}\n");
        }

        /// <summary>
        /// Определяет итоговую оценку
        /// </summary>
        public override void CalculateMark()
        {
            double ratio = (double)RightAnswers / QuestionsQuantity;
            CurrentMark = (int)Math.Round(ratio * GradingScale);
            MaxMark = CurrentMark;
            
            IsPassed = MaxMark >= PassingScore;
        }
        /// <summary>
        /// Проводит экзамен у одного человека (количество правильных ответов генерируется случайным образом)
        /// </summary>
        public override void TakeExam()
        {
            Random rnd = new Random();
            RightAnswers = rnd.Next(0, QuestionsQuantity);
            if (RightAnswers == -1) return;
            CalculateMark();
        }
        #endregion

        #region Operators

        #endregion
    }
}
