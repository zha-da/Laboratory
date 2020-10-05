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
                    throw new ArgumentOutOfRangeException("Проходной балл не может быть ниже 1");
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
                    throw new ArgumentOutOfRangeException("Количество попыток не может быть ниже 1");
                }
                _mTakes = value;
            }
        }

        bool isPassedAlready = false;
        #endregion

        #region Constructors
        public FailPassExam(string discipline, int questionsQuantity,
            int passingScore, int maxTakes)
        {
            Discipline = discipline;
            QuestionsQuantity = questionsQuantity;
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
            try
            {
                if (IsPassed)
                {
                    isPassedAlready = true;
                    throw new AlreadyPassedException("Зачет уже сдан");
                }
                Random rnd = new Random();
                RightAnswers = rnd.Next(0, QuestionsQuantity);
                if (take++ > MaxTakes)
                {
                    throw new ExpelledException("Чел, ты отчислен");
                }
                CalculateMark();
            }
            catch (ExpelledException eex)
            {
                Console.WriteLine(eex.Message);
                Console.ReadKey();
            }
            catch (ArgumentOutOfRangeException aex)
            {
                Console.WriteLine(aex.Message);
                Console.ReadKey();
            }
            catch (AlreadyPassedException apex)
            {
                Console.WriteLine(apex.Message);
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка, аварийное завершение работы программы");
                Console.ReadKey();
            }
        }
        public override void DisplayInfo()
        {
            try
            {
                if (isPassedAlready)
                {
                    throw new AlreadyPassedException("Зачет уже сдан");
                }
                if (RightAnswers == -1)
                {
                    throw new UnsuccessfulAttemtException("Неудачная попытка пройти экзамен. Проверьте правильность данных");
                }
                Console.WriteLine($"Зачет по дисциплине: {Discipline}\n" +
                    $"Общее количество вопросов: {QuestionsQuantity}\n" +
                    $"Из них правильно: {RightAnswers}\n" +
                    $"Зачет сдан: {IsPassed}\n" +
                    $"Осталось попыток: {MaxTakes - take}\n");
            }
            catch (UnsuccessfulAttemtException uex)
            {
                Console.WriteLine(uex.Message);
                Console.ReadKey();
            }
            catch (AlreadyPassedException apex)
            {
                Console.WriteLine(apex.Message);
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка, аварийное завершение работы программы");
                Console.ReadKey();
            }
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
