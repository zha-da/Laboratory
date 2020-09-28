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
                    Console.WriteLine("Недопустимое значение. Перепроверьте данные\n");
                    rightAns = -1;
                    return;
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
                    Console.WriteLine("Количество попыток не может быть меньше 1\n");
                    return;
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
                        Console.WriteLine("Оценка по критерию не может быть ниже 1 " +
                            "или больше максимальной оценки по шкале");
                        return;
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
            if (IsPassed)
            {
                isPassedAlready = true;
                return;
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

            if(take++ > MaxTakes)
            {
                Console.WriteLine("Количество попыток исчерпано. Незачет\n");
                return;
            }

            CalculateMark();
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
            if (isPassedAlready)
            {
                Console.WriteLine("Экзамен уже сдан\n");
                return;
            }
            if (RightAnswers == -1)
            {
                Console.WriteLine("Неудачная попытка сдать зачет. " +
                    "Проверьте правильность данных\n");
                return;
            }
            Console.WriteLine($"Итоговый экзамен по предмету: {Discipline}\n" +
                $"Общее количество вопросов: {QuestionsQuantity}\n" +
                $"Текущая оценка: {CurrentMark}\n" +
                $"Максимальная оценка: {MaxMark}\n" +
                $"Экзамен сдан: {IsPassed}\n" +
                $"Осталось попыток: {MaxTakes - take}\n");
        }
        #endregion

        #region Operators

        #endregion
    }
}
