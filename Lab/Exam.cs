﻿using System;
using Laboratory.AdditionalClasses;

namespace Laboratory.Exams
{
    /// <summary>
    /// Класс экзамена
    /// </summary>
    public abstract class Exam : IComparable<Exam>
    {
        #region Fields
        /// <summary>
        /// Номер попытки
        /// </summary>
        public int Take { get; set; } = 0;
        /// <summary>
        /// Дата проведения экзамена
        /// </summary>
        public DateTime ExamDate { get; set; } = new DateTime(2020, 10, 31);
        /// <summary>
        /// Дата экзамена
        /// </summary>
        public string RetDate
        {
            get => $"{ExamDate.Day}.{ExamDate.Month}.{ExamDate.Year}";
        }
        /// <summary>
        /// Название дисциплины
        /// </summary>
        public string Discipline { get; set; } = "Не выбрано";

        private int _grdScale = 5;
        /// <summary>
        /// Шкала оценивания
        /// </summary>
        public virtual int GradingScale
        {
            get { return _grdScale; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("Максимальная оценка по шкале не может быть ниже 1");
                }
                _grdScale = value;
            }
        }

        private int questQuan = 30;
        /// <summary>
        /// Общее количество вопросов (не ниже 0)
        /// </summary>
        public int QuestionsQuantity
        {
            get { return questQuan; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"Количество вопросов не может быть меньше 0");
                }
                questQuan = value;
            }
        }

        private int rightAns = 0;
        /// <summary>
        /// Количество вопросов, на которые получен правильный ответ
        /// </summary>
        public virtual int RightAnswers
        {
            get { return rightAns; }
            protected set
            {
                if (value < 0 || value > QuestionsQuantity)
                {
                    rightAns = -1;
                    throw new ArgumentOutOfRangeException($"Недопустимое значение количества правильных ответов (от 0 до {questQuan})");
                }
                rightAns = value;
            }
        }
        private int _currentMark;
        /// <summary>
        /// Оценка за экзамен на текущий момент
        /// </summary>
        public int CurrentMark
        {
            get => _currentMark;
            set
            {
                if (value <= 0) _currentMark = 1;
                else _currentMark = value;
            }
        }
        /// <summary>
        /// Сдан ли экзамен
        /// </summary>
        public bool IsPassed { get; set; } = false;
        /// <summary>
        /// Проходной балл (по умолчанию 60% от общего количества вопросов с округлением до ближайшего целого числа)
        /// </summary>
        public virtual int PassingScore { get; set; }
        #endregion


        #region Constructors
        /// <summary>
        /// Создает экземпляр класса
        /// </summary>
        /// <param name="discipline">Название дисциплины</param>
        /// <param name="questionsQuantity">Общее количество вопросов</param>
        /// <param name="gradeScale">Максимальная оценка за экзамен</param>
        /// <param name="passingScore">Проходной балл</param>
        public Exam(string discipline, int questionsQuantity, 
            int gradeScale, int passingScore)
        {
            try
            {
                Discipline = discipline;
                PassingScore = passingScore;
                QuestionsQuantity = questionsQuantity;
                GradingScale = gradeScale;
                RightAnswers = 0;
            }
            catch (ArgumentOutOfRangeException)
            {
                string message = $"Введены неверные данные для создания экземпляра класса " +
                                $"экзамена по дисциплине {Discipline}\n" +
                                $"Экземпляр создается со значениями по умолчанию\n";
                Logger.NewLog(message);
                Discipline = discipline;                
                PassingScore = 36;
                QuestionsQuantity = 60;
                GradingScale = 60;
                RightAnswers = 0;
            }
        }
        /// <summary>
        /// Конструктор с датой проведения экзамена
        /// </summary>
        /// <param name="date">Дата проведения экзамена</param>
        protected Exam(DateTime date)
        {
            ExamDate = date;
        }

        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        public Exam() { }
        #endregion


        #region Methods
        /// <summary>
        /// Тип экзамена
        /// </summary>
        public string RetType
        {
            get
            {
                if (this is Test) return "Тест";
                else if (this is FinalExam) return "Итоговый экзамен";
                else if (this is FailPassExam) return "Зачет";
                else return "Контрольная работа";
            }
        }
        /// <summary>
        /// Выводит информацию об экзамене
        /// </summary>
        public virtual void DisplayInfo()
        {
            if (rightAns == -1) Print_UnsuccessfulAttemt();
            else
            {
                Print_Discipline();
                Print_Take();
                Print_Questions();
                Print_RightAns();
                Print_IsPassed();
                Console.WriteLine();
            }
        }

        #region Print methods
        /// <summary>
        /// Выводит сообщение о неудачной попытке
        /// </summary>
        protected virtual void Print_UnsuccessfulAttemt() => Console.WriteLine("Неудачная попытка пройти экзамен\n");
        /// <summary>
        /// Выводит сообщение о названии дисциплины
        /// </summary>
        protected virtual void Print_Discipline() => Console.WriteLine($"Экзамен по дисциплине: {Discipline}");
        /// <summary>
        /// Выводит сообщение о номере попытки
        /// </summary>
        protected virtual void Print_Take() => Console.WriteLine($"Попытка номер {Take}");
        /// <summary>
        /// Выводит сообщение об общем количестве вопросов
        /// </summary>
        protected virtual void Print_Questions() => Console.WriteLine($"Общее количество вопросов: {QuestionsQuantity}");
        /// <summary>
        /// Выводит сообщение о количестве правильных ответов
        /// </summary>
        protected virtual void Print_RightAns() => Console.WriteLine($"Из них правильно: {RightAnswers}");
        /// <summary>
        /// Выводит сообщение о проходном балле
        /// </summary>
        protected virtual void Print_PassingSc() => Console.WriteLine($"Проходной балл: {PassingScore}");
        /// <summary>
        /// Выводит сообщение о том, пройден ли экзамен
        /// </summary>
        protected virtual void Print_IsPassed()
        {
            Console.Write("Экзамен сдан: ");
            if (IsPassed) Console.WriteLine("Да");
            else Console.WriteLine("Нет");
        }
        #endregion

        /// <summary>
        /// Определяет итоговую оценку
        /// </summary>
        public abstract void CalculateMark();

        /// <summary>
        /// Проводит экзамен у одного человека (количество правильных ответов генерируется случайным образом)
        /// </summary>
        public abstract void TakeExam();

        int IComparable<Exam>.CompareTo(Exam other)
        {
            return Discipline.CompareTo(other.Discipline);
        }
        /// <summary>
        /// Сравнивает 2 объекта класса экзамен
        /// </summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns>Равны ли объекты</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Exam))
            {
                return false;
            }
            Exam ex = obj as Exam;
            return (Discipline == ex.Discipline
                && ExamDate == ex.ExamDate
                && RetType == ex.RetType
                && QuestionsQuantity == ex.QuestionsQuantity);
        }
        /// <summary>
        /// Приводит информацию об экзамене в строковом виде
        /// </summary>
        /// <returns>Строковое представление объекта класса</returns>
        public override string ToString()
        {
            return $"{ExamDate} : {RetType} по дисципине {Discipline} с {QuestionsQuantity} вопрос{(QuestionsQuantity % 10 == 1 ? "ом" : "ами")}";
        }
        /// <summary>
        /// Возвращает хэш-код объекта
        /// </summary>
        /// <returns>Хэш-код</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion


        #region Operators
        /// <summary>
        /// Сравнивает 2 экзамена по текущим оценкам
        /// </summary>
        /// <param name="e1">Экзамен 1</param>
        /// <param name="e2">Экзамен 2</param>
        /// <returns>Результат сравнения по оценкам</returns>
        public static bool operator >(Exam e1, Exam e2)
        {
            return e1.CurrentMark > e2.CurrentMark;
        }
        /// <summary>
        /// Сравнивает 2 экзамена по текущим оценкам
        /// </summary>
        /// <param name="e1">Экзамен 1</param>
        /// <param name="e2">Экзамен 2</param>
        /// <returns>Результат сравнения по оценкам</returns>
        public static bool operator <(Exam e1, Exam e2)
        {
            return e1.CurrentMark < e2.CurrentMark;
        }
        /// <summary>
        /// Складывает результаты 2х экзаменов
        /// </summary>
        /// <param name="e1">Экзамен 1</param>
        /// <param name="e2">Экзамен 2</param>
        /// <returns>Среднее арифметическое результатов за 2 экзамена</returns>
        public static double operator +(Exam e1, Exam e2)
        {
            return (double)(e1.CurrentMark + e2.CurrentMark) / 2;
        }
        #endregion
    }
}
