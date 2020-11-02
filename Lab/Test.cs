using System;
using Laboratory.AdditionalClasses;

namespace Laboratory.Exams
{
    /// <summary>
    /// Класс контрольной
    /// </summary>
    public class Test : Exam
    {
        #region Fields
        private string _testTopic = "Не задано";
        /// <summary>
        /// Тема теста
        /// </summary>
        public string TestTopic
        {
            get { return _testTopic; }
            set { _testTopic = value; }
        }
        private int succTakes = 0;
        /// <summary>
        /// Количество успешных попыток
        /// </summary>
        public int SuccessfulTakes
        {
            get { return succTakes; }
            protected set { succTakes = value; }
        }
        private int maxMark;
        /// <summary>
        /// Максимальная полученная отметка
        /// </summary>
        public int MaxMark
        {
            get { return maxMark; }
            protected set
            {
                if (maxMark < value)
                {
                    maxMark = value;
                }
            }
        }

        #endregion


        #region Constructors
        /// <summary>
        /// Создает экземпляр класса
        /// </summary>
        /// <param name="date">Дата проведения теста</param>
        /// <param name="discipline">Дисциплина</param>
        /// <param name="testTopic">Тема теста</param>
        /// <param name="questionsQuantity">Количество вопросов</param>
        /// <param name="passingScore">Проходной балл</param>
        /// <param name="gradingScale">Шкала оценивания</param>
        public Test(DateTime date, string discipline, string testTopic, 
            int questionsQuantity, int passingScore, int gradingScale)
            : base(date)
        {
            try
            {
                Discipline = discipline;
                TestTopic = testTopic;
                QuestionsQuantity = questionsQuantity;
                PassingScore = passingScore;
                GradingScale = gradingScale;
            }
            catch (ArgumentOutOfRangeException)
            {
                string message = $"Введены неверные данные для создания экземпляра класса " +
                                $"теста по дисциплине {Discipline}\n" +
                                $"Экземпляр создается со значениями по умолчанию\n";
                Logger.NewLog(message);
                QuestionsQuantity = 30;
                PassingScore = 18;
                GradingScale = 100;
            }
        }
        #endregion


        #region Methods
        /// <summary>
        /// Выводит информацию о попытке 
        /// </summary>
        public override void DisplayInfo()
        {
            try
            {
                if (RightAnswers == -1)
                {
                    throw new UnsuccessfulAttemtException("Неверные данные. Неудачная попытка пройти тест\n");
                }
                Console.WriteLine($"Тест по дисциплине: {Discipline}\n" +
                    $"Тема теста: {TestTopic}\n" +
                    $"Общее количество вопросов: {QuestionsQuantity}\n" +
                    $"Из них правильно: {RightAnswers}\n" +
                    $"Текущая оценка: {CurrentMark} \\ {GradingScale}\n" +
                    $"Высшая оценка: {MaxMark} \\ {GradingScale}\n" +
                    $"Количество успешных попыток: {SuccessfulTakes}\n");
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
        #region Print methods
        /// <summary>
        /// Выводит сообщение о лучшем результате
        /// </summary>
        protected virtual void Print_MaxMark()
        {
            Console.WriteLine($"Лучший результат: {maxMark}");
        }
        /// <summary>
        /// Выводит сообщение об удачных попытках
        /// </summary>
        protected virtual void Print_SuccessfulTakes()
        {
            Console.WriteLine($"Количество удачных попыток: {succTakes}");
        }
        /// <summary>
        /// Выводит сообщение о названии дисциплины
        /// </summary>
        protected override void Print_Discipline()
        {
            Console.WriteLine($"Тест по дисциплине: {discip}");
        }
        /// <summary>
        /// Выводит сообщение о теме теста
        /// </summary>
        protected virtual void Print_Topic()
        {
            Console.WriteLine($"Тема теста: {_testTopic}");
        }
        /// <summary>
        /// Выводит сообщение о том, пройден ли экзамен
        /// </summary>
        protected override void Print_IsPassed()
        {
            Console.WriteLine($"Тест сдан: {IsPassed}");
        }
        #endregion

        /// <summary>
        /// Проводит экзамен у одного человека (количество правильных ответов генерируется случайным образом)
        /// </summary>
        public override void TakeExam()
        {
            try
            {
                Random rnd = new Random();
                RightAnswers = rnd.Next(0, QuestionsQuantity);
                take++;
                CalculateMark();
            }
            //catch (ArgumentOutOfRangeException aex)
            //{
            //    Console.WriteLine(aex.Message);
            //    Console.ReadKey();
            //}
            catch (Exception ex)
            {
                string message = $"Неизвестная ошибка в {ex.TargetSite.Name} {ex.TargetSite.DeclaringType.Name} {ex.TargetSite.DeclaringType.Namespace}\n";
                Logger.NewLog(message);
            }
        }
        /// <summary>
        /// Определяет итоговую оценку
        /// </summary>
        public override void CalculateMark()
        {
            double ratio = (double)RightAnswers / QuestionsQuantity;
            CurrentMark = (int)Math.Round(ratio * GradingScale);
            IsPassed = CurrentMark >= PassingScore;
            if (IsPassed)
            {
                succTakes++;
            }
            MaxMark = CurrentMark;
        }
        #endregion
    }
}
