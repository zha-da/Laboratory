using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.Exams
{
    public class FinalExam : Control
    {
        #region Fields
        int _rightAns = 0;
        public override int RightAnswers
        {
            get => _rightAns;
            protected set
            {
                if (value < 0 || value > 5)
                {
                    rightAns = -1;
                    throw new ArgumentOutOfRangeException("Недопустимое значение (меньше 0 или больше 5). Перепроверьте данные");
                }
                _rightAns = value;
            }
        }

        private int _maxTakes = 3;

        public int MaxTakes
        {
            get { return _maxTakes; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Максимальное количество попыток не может быть меньше 0");
                }
                _maxTakes = value;
            }
        }

        private int[] _crits = new int[] { 0, 0, 0, 0, 0 };

        public int[] Criterias
        {
            get { return _crits; }
            set
            {
                int[] tmp = value;
                foreach (int val in tmp)
                {
                    if ( val < 1 || val > GradingScale)
                    {
                        throw new ArgumentOutOfRangeException("Оценка по критерию не может быть ниже 1 " +
                            "или больше максимальной оценки по шкале");
                    }
                }
                _crits = value;
            }
        }

        bool isPassedAlready = false;
        #endregion

        #region Constructors
        public FinalExam(string discipline, int questionsQuantity, int passingScore, int maxTakes)
            :base (discipline, questionsQuantity, passingScore)
        {
            MaxTakes = maxTakes;
        }
        public FinalExam(string discipline, int questionsQuantity)
            : base(discipline, questionsQuantity) { }
        public FinalExam(string discipline)
            : this(discipline, 2) { }
        #endregion

        #region Methods
        public override void TakeExam()
        {
            try
            {
                if (IsPassed)
                {
                    isPassedAlready = true;
                    throw new AlreadyPassedException("Экзамен уже сдан");
                }
                Random rnd = new Random();
                for (int i = 0; i < _crits.Length; i++)
                {
                    _crits[i] = 0;
                }
                for (int i = 0; i < QuestionsQuantity; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        _crits[j] += rnd.Next(2, GradingScale);
                    }
                }

                for (int i = 0; i < 5; i++)
                {
                    _crits[i] = (int)Math.Round((double)_crits[i] / QuestionsQuantity);
                }

                if (take++ > MaxTakes)
                {
                    throw new ExpelledException("Ты отчислен");
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

        public override void CalculateMark()
        {
            for (int i = 0; i < _crits.Length; i++)
            {
                if(_crits[i] >= PassingScore)
                {
                    RightAnswers++;
                }
            }

            CurrentMark = RightAnswers;
            MaxMark = CurrentMark;

            IsPassed = MaxMark >= PassingScore;
        }

        public override void DisplayInfo()
        {
            try
            {
                if (isPassedAlready)
                {
                    throw new AlreadyPassedException("Экзамен уже сдан");
                }
                if (RightAnswers == -1)
                {
                    throw new UnsuccessfulAttemtException("Неудачная попытка сдать зачет. " +
                        "Проверьте правильность данных");
                }
                Console.WriteLine($"Итоговый экзамен по предмету: {Discipline}\n" +
                    $"Общее количество вопросов: {QuestionsQuantity}\n" +
                    $"Текущая оценка: {CurrentMark}\n" +
                    $"Максимальная оценка: {MaxMark}\n" +
                    $"Экзамен сдан: {IsPassed}\n" +
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
        #endregion

        #region Operators

        #endregion
    }
}
