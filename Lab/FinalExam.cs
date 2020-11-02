using System;
using Laboratory.AdditionalClasses;

namespace Laboratory.Exams
{
    /// <summary>
    /// Класс итогового экзамена
    /// </summary>
    public class FinalExam : Control
    {
        #region Fields
        private bool finFail = false;
        /// <summary>
        /// Показывает, что экзамен провален, и попыток сдать его больше нет
        /// </summary>
        public bool ExamFailed
        {
            get { return finFail; }
        }

        int _rightAns = 0;
        /// <summary>
        /// Количество правильных ответов
        /// </summary>
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
        /// <summary>
        /// Количество попыток
        /// </summary>
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
        /// <summary>
        /// Оценка по критериям
        /// </summary>
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
        /// <summary>
        /// Создает экземпляр класса
        /// </summary>
        /// <param name="date">Дата проведения итогового экзамена</param>
        /// <param name="discipline">Дисциплина</param>
        /// <param name="questionsQuantity">Количество вопросов</param>
        /// <param name="passingScore">Проходной балл</param>
        /// <param name="maxTakes">Количество попыток</param>
        public FinalExam(DateTime date, string discipline, int questionsQuantity, int passingScore, int maxTakes)
            :base (date, discipline, questionsQuantity, passingScore)
        {
            try
            {
                MaxTakes = maxTakes;
            }
            catch (ArgumentOutOfRangeException)
            {
                string message = "Введены неверные данные для создания экземпляра класса " +
                                $"итогового экзамена по дисциплине {Discipline}\n" +
                                $"Экземпляр создается со значениями по умолчанию\n";
                Logger.NewLog(message);
                MaxTakes = 3;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Принимает итоговый экзамен
        /// </summary>
        public override void TakeExam()
        {
            try
            {
                if (take++ > MaxTakes)
                {
                    finFail = true;
                    throw new ExpelledException("Попытки сдать экзамен исчерпаны");
                }
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
                CalculateMark();
            }
            catch (ExpelledException eex)
            {
                Logger.NewLog("Попытка сдать экзамен отчисленным учеником\n");
                Console.WriteLine(eex.Message);
                Console.ReadKey();
            }
            catch (ArgumentOutOfRangeException aex)
            {
                Logger.NewLog(aex.Message + "\n");
                Console.WriteLine(aex.Message);
                Console.ReadKey();
            }
            catch (AlreadyPassedException apex)
            {
                Logger.NewLog(apex.Message + "\n");
                Console.WriteLine(apex.Message);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                string message = $"Неизвестная ошибка в {ex.TargetSite.Name} {ex.TargetSite.DeclaringType.Name} {ex.TargetSite.DeclaringType.Namespace}\n";
                Logger.NewLog(message);
            }
        }
        /// <summary>
        /// Рассчитывает оценку
        /// </summary>
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
            HighestMark = CurrentMark;

            IsPassed = HighestMark >= PassingScore;
        }
        /// <summary>
        /// Выводит информацию
        /// </summary>
        public override void DisplayInfo()
        {
            try
            {
                if (take > MaxTakes && !IsPassed)
                {
                    throw new ExpelledException("Вы исчерпали все попытки сдать экзамен и теперь отчислены");
                }
                if (isPassedAlready)
                {
                    return;
                }
                if (RightAnswers == -1)
                {
                    throw new UnsuccessfulAttemtException("Неверные данные. Неудачная попытка сдать экзамен");
                }
                Console.WriteLine($"\nИтоговый экзамен по предмету: {Discipline}\n" +
                    $"Общее количество вопросов: {QuestionsQuantity}\n" +
                    $"Текущая оценка: {CurrentMark}\n" +
                    $"Максимальная оценка: {HighestMark}\n" +
                    $"Экзамен сдан: {IsPassed}\n" +
                    $"Осталось попыток: {MaxTakes - take}\n");
            }
            catch (ExpelledException eex)
            {
                Console.WriteLine(eex.Message);
                Console.ReadKey();
            }
            catch (UnsuccessfulAttemtException uex)
            {
                Logger.NewLog(uex.Message + "\n");
                Console.WriteLine(uex.Message);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                string message = $"Неизвестная ошибка в {ex.TargetSite.Name} {ex.TargetSite.DeclaringType.Name} {ex.TargetSite.DeclaringType.Namespace}\n";
                Logger.NewLog(message);
            }
        }
        #endregion
    }
}
