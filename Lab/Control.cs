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
                    throw new ArgumentOutOfRangeException("Проходной балл не может быть ниже 1");
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
            try
            {
                if (RightAnswers == -1)
                {
                    throw new UnsuccessfulAttemtException("Неудачная попытка сдать контрольную работу. " +
                        "Проверьте правильность данных");
                }
                Console.WriteLine($"Контрольная работа по предмету: {Discipline}\n" +
                    $"Общее количество вопросов: {QuestionsQuantity}\n" +
                    $"Из них правильно: {RightAnswers}\n" +
                    $"Текущая оценка: {CurrentMark}\n" +
                    $"Максимальная оценка: {MaxMark}\n" +
                    $"Контрольная работа сдана: {IsPassed}\n");
            }
            catch (UnsuccessfulAttemtException uex)
            {
                Console.WriteLine(uex.Message);
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка, аварийное завершение работы программы");
                Console.ReadKey();
            }
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
            try
            {
                Random rnd = new Random();
                RightAnswers = rnd.Next(0, QuestionsQuantity);
                CalculateMark();
            }
            catch (ArgumentOutOfRangeException aex)
            {
                Console.WriteLine(aex.Message);
                Console.ReadKey();
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка, аварийное завершение работы программы");
                Console.ReadKey();
            }
        }
        #endregion

        #region Operators

        #endregion
    }
}
