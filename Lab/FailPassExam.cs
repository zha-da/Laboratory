using System;
using Laboratory.AdditionalClasses;

namespace Laboratory.Exams
{
    /// <summary>
    /// Класс итогового экзамена
    /// </summary>
    public class FailPassExam : Exam
    {
        #region Fields
        /// <summary>
        /// Показывает, что экзамен провален, и попыток сдать его больше нет
        /// </summary>
        public bool ExamFailed { get; private set; } = false;

        private int _grdScale = 1;
        /// <summary>
        /// Шкала оценивания
        /// </summary>
        public override int GradingScale
        {
            get => _grdScale;
        }

        private int _passSc = 18;
        /// <summary>
        /// Проходной балл
        /// </summary>
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
        /// <summary>
        /// Количество попыток
        /// </summary>
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
        /// <summary>
        /// Создает экземпляр класса
        /// </summary>
        /// <param name="date">Дата проведения зачета</param>
        /// <param name="discipline">Дисциплина</param>
        /// <param name="questionsQuantity">Количество вопросов</param>
        /// <param name="passingScore">Проходной балл</param>
        /// <param name="maxTakes">количество попыток</param>
        public FailPassExam(DateTime date, string discipline, int questionsQuantity,
            int passingScore, int maxTakes)
            : base(date)
        {
            try
            {
                Discipline = discipline;
                QuestionsQuantity = questionsQuantity;
                PassingScore = passingScore;
                MaxTakes = maxTakes;
            }
            catch (ArgumentOutOfRangeException)
            {
                string message = "Введены неверные данные для создания экземпляра класса " +
                                $"зачета по дисциплине {Discipline}\n" +
                                $"Экземпляр создается со значениями по умолчанию\n";
                Logger.NewLog(message);
                QuestionsQuantity = 30;
                PassingScore = 18;
                MaxTakes = 3;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Принимает зачет
        /// </summary>
        public override void TakeExam()
        {
            try
            {
                if (Take++ > MaxTakes)
                {
                    ExamFailed = true;
                    throw new ExpelledException("Попытки сдать зачет исчерпаны");
                }
                if (IsPassed)
                {
                    isPassedAlready = true;
                    throw new AlreadyPassedException("Зачет уже сдан");
                }
                Random rnd = new Random();
                RightAnswers = rnd.Next(0, QuestionsQuantity);

                CalculateMark();
            }
            catch (ExpelledException eex)
            {
                Console.WriteLine(eex.Message);
                Console.ReadKey();
            }
            //catch (ArgumentOutOfRangeException aex)
            //{
            //    Console.WriteLine(aex.Message);
            //    Console.ReadKey();
            //}
            catch (AlreadyPassedException apex)
            {
                Logger.NewLog(apex.Message + "\n");
                Console.WriteLine(apex.Message);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                string message = $"Неизвестная ошибка в {ex.TargetSite.Name} " +
                    $"{ex.TargetSite.DeclaringType.Name} {ex.TargetSite.DeclaringType.Namespace}\n";
                Logger.NewLog(message);
            }
        }

        /// <summary>
        /// Выводит информацию
        /// </summary>
        public override void DisplayInfo()
        {
            try
            {
                if (Take > MaxTakes && !IsPassed)
                {
                    throw new ExpelledException("Вы исчерпали все попытки сдать зачет и теперь отчислены");
                }
                if (isPassedAlready)
                {
                    return;
                }
                if (RightAnswers == -1)
                {
                    throw new UnsuccessfulAttemtException("Неудачная попытка пройти экзамен. Проверьте правильность данных");
                }
                Console.WriteLine($"\nЗачет по дисциплине: {Discipline}\n" +
                    $"Общее количество вопросов: {QuestionsQuantity}\n" +
                    $"Из них правильно: {RightAnswers}\n" +
                    $"Зачет сдан: {IsPassed}\n" +
                    $"Осталось попыток: {MaxTakes - Take}\n");
            }
            catch (ExpelledException eex)
            {
                Logger.NewLog("Ученик отчислен за несдачу зачета\n");
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
                string message = $"Неизвестная ошибка в {ex.TargetSite.Name} " +
                    $"{ex.TargetSite.DeclaringType.Name} {ex.TargetSite.DeclaringType.Namespace}\n";
                Logger.NewLog(message);
            }
        }

        /// <summary>
        /// Вычисляет оценку
        /// </summary>
        public override void CalculateMark()
        {
            IsPassed = RightAnswers >= PassingScore | RightAnswers == QuestionsQuantity;
        }
        #endregion

        #region Operators

        /// <summary>
        /// Складывает результаты двух зачетов
        /// </summary>
        /// <param name="exam1">Зачет 1</param>
        /// <param name="exam2">Зачет 2</param>
        /// <returns>Сданы ли оба зачета</returns>
        public static bool operator +(FailPassExam exam1, FailPassExam exam2)
        {
            return exam1.IsPassed & exam2.IsPassed;
        }
        #endregion
    }
}
