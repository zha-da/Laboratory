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
    public class FailPassExam : Exam
    {
        #region Fields
        private int _grdScale = 1;
        public override int GradingScale
        {
            get => _grdScale;
        }

        private int _passSc = 18;
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

        private int _mTakes = 3;

        public int MaxTakes
        {
            get { return _mTakes; }
            set
            {
                if (value < 1)
                {
                    Console.WriteLine("Количество попыток не может быть ниже 1\n");
                    return;
                }
                _mTakes = value;
            }
        }

        bool isPassedAlready = false;
        #endregion

        #region Constructors
        public FailPassExam(string discipline, int questionsQuantity,
            int passingScore, int maxTakes)
            : base(discipline, questionsQuantity)
        {
            PassingScore = passingScore;
            MaxTakes = maxTakes;
        }

        public FailPassExam(string discipline, int questionsQuantity,
            int passingScore)
            : this(discipline, questionsQuantity, passingScore, 3) { }

        public FailPassExam(string discipline, int questionsQuantity)
            : this(discipline, questionsQuantity, 0, 3)
        {
            PassingScore = (int)Math.Round(questionsQuantity * 0.6);
        }

        public FailPassExam(string discipline)
            : this(discipline, 30, 18, 3) { }

        public FailPassExam()
        {
            QuestionsQuantity = 30;
            PassingScore = 18;
        }
        #endregion

        #region Methods
        public override void TakeExam()
        {
            if (IsPassed)
            {
                isPassedAlready = true;
                return;
            }
            Random rnd = new Random();
            RightAnswers = rnd.Next(0, QuestionsQuantity);
            if (RightAnswers == -1) return;
            if (take++ > MaxTakes)
            {
                Console.WriteLine("Количество попыток исчерпано. Незачет\n");
                return;
            }
            CalculateMark();
        }
        public override void DisplayInfo()
        {
            if (isPassedAlready)
            {
                Console.WriteLine("Зачет уже сдан\n");
                return;
            }
            if (RightAnswers == -1)
            {
                Console.WriteLine("Неудачная попытка сдать зачет. " +
                    "Проверьте правильность данных\n");
                return;
            }
            Console.WriteLine($"Зачет по дисциплине: {Discipline}\n" +
                $"Общее количество вопросов: {QuestionsQuantity}\n" +
                $"Из них правильно: {RightAnswers}\n" +
                $"Зачет сдан: {IsPassed}\n" +
                $"Осталось попыток: {MaxTakes - take}\n");

        }
        public override void CalculateMark()
        {
            IsPassed = RightAnswers >= PassingScore;
        }
        #endregion

        #region Operators
        public static bool operator + (FailPassExam exam1, FailPassExam exam2)
        {
            return exam1.IsPassed & exam2.IsPassed;
        }
        #endregion
    }
}
