﻿using System;
using Laboratory.AdditionalClasses;
using Laboratory.Exams.Comparers;

namespace Laboratory.Exams
{
    /// <summary>
    /// Класс для зачета
    /// </summary>
    public class Control : Exam
    {
        #region Fields
        private int _grdScale = 5;

        /// <summary>
        /// Шкала оценивания
        /// </summary>
        public override int GradingScale
        {
            get => _grdScale;
        }

        private int _maxMark = 0;
        /// <summary>
        /// Максимальная полученная оценка
        /// </summary>
        public int HighestMark
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
        #endregion

        #region Constructors
        /// <summary>
        /// Создает экземпляр класса
        /// </summary>
        /// <param name="date">Дата проведения контрольной работы</param>
        /// <param name="discipline">Дисциплина</param>
        /// <param name="questionsQuantity">Количество вопросов</param>
        /// <param name="passingScore">Проходной балл</param>
        public Control(DateTime date, string discipline, int questionsQuantity,
            int passingScore)
            : base(date)
        {
            try
            {
                Discipline = discipline;
                QuestionsQuantity = questionsQuantity;
                PassingScore = passingScore;
            }
            catch (ArgumentOutOfRangeException)
            {
                string message = "Введены неверные данные для создания экземпляра класса " +
                                $"контрольной работы по дисциплине {Discipline}\n" +
                                $"Экземпляр создается со значениями по умолчанию\n";
                Logger.NewLog(message);
                QuestionsQuantity = 30;
                PassingScore = 18;
            }
        }
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
                    $"Максимальная оценка: {HighestMark}\n" +
                    $"Контрольная работа сдана: {IsPassed}\n");
            }
            catch (UnsuccessfulAttemtException uex)
            {
                Logger.NewLog(uex.Message + "\n");
            }
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
            HighestMark = CurrentMark;
            

            IsPassed = RightAnswers >= PassingScore;
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
            catch (Exception ex)
            {
                string message = $"Неизвестная ошибка в {ex.TargetSite.Name} {ex.TargetSite.DeclaringType.Name} {ex.TargetSite.DeclaringType.Namespace}\n";
                Logger.NewLog(message);
            }
        }
        #endregion
    }
}
