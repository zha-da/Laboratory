using Microsoft.VisualStudio.TestTools.UnitTesting;
using Laboratory.Exams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.Exams.Tests
{
    [TestClass()]
    public class SemesterTests
    {
        [TestMethod()]
        public void EnumTest()
        {
            var sem = new Semester(new List<Exam>
            {
                new FinalExam(new DateTime(2020, 10, 31), "Математика", 2, 3, 3),
                new Test(new DateTime(2020, 11, 14), "История", "Правление Николая 2", 20, 12, 100),
                new FailPassExam(new DateTime(2020, 11, 15), "Биология", 20, 12, 3),
                new Test(new DateTime(2020, 11, 16), "География", "Южная Америка", 20, 12, 100),
                new Control(new DateTime(2020, 11, 17), "Математика", 20, 12)
            });
            foreach (var item in sem)
            {
                Console.WriteLine(item.ToString());
            }
        }
        [TestMethod()]
        public void GetFromFileTest()
        {
            List<Exam> read = Semester.GetFromFile("C:/Users/Pepe/Visual Studio Projects/Laba/LaboratoryMainApp/bin/Debug/exams.TXT");
            Semester sem1 = new Semester(read);
            read = new List<Exam>()
            {
                new Test(new DateTime(2020, 12, 20), "История", "Николай 2", 30, 18, 100),
                new Test(new DateTime(2020, 12, 18), "География", "Страны мира", 20, 15, 100),
                new FailPassExam(new DateTime(2020, 12, 26), "Алгебра", 10, 6 , 3)
            };
            Semester sem2 = new Semester(read);

            foreach (var item in sem1.Exams)
            {
                Console.WriteLine(item.ToString());
            }
            foreach (var item in sem2.Exams)
            {
                Console.WriteLine(item.ToString());
            }
            Assert.AreEqual(sem1, sem2);
        }

        [TestMethod()]
        public void DeleteAllTest()
        {
            Console.WriteLine("Before~~~~~~~~~~~~~~~~~~~~~~~~~~");
            var sem = new Semester(new List<Exam>
            {
                new FinalExam(new DateTime(2020, 10, 31), "Математика", 2, 3, 3),
                new Test(new DateTime(2020, 11, 14), "История", "Правление Николая 2", 20, 12, 100),
                new FailPassExam(new DateTime(2020, 11, 15), "Биология", 20, 12, 3),
                new Test(new DateTime(2020, 11, 16), "География", "Южная Америка", 20, 12, 100),
                new Control(new DateTime(2020, 11, 17), "Математика", 20, 12)
            });
            var res1 = new Semester(new List<Exam>
            {
                sem.Exams[1],
                sem.Exams[2],
                sem.Exams[3],
                sem.Exams[4]
            });
            var res2 = new Semester(new List<Exam>
            {
                sem.Exams[2],
                sem.Exams[4]
            });
            var res3 = new Semester(new List<Exam>
            {
                sem.Exams[2]
            });


            sem.WriteExams();
            Console.WriteLine("After~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            sem.DeleteAll(typeof(FinalExam));
            sem.WriteExams();
            Assert.AreEqual(sem, res1);

            Console.WriteLine("After~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            sem.DeleteAll(typeof(Test));
            sem.WriteExams();
            Assert.AreEqual(sem, res2);

            Console.WriteLine("After~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            sem.DeleteAll(typeof(Control));
            sem.WriteExams();
            Assert.AreEqual(sem, res3);
        }

        //[TestMethod()]
        //public void ReturnAllTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void RemoveAllTest()
        {
            var sem = new Semester(new List<Exam>
            {
                new FinalExam(new DateTime(2020, 10, 31), "Геометрия", 2, 3, 3),
                new Test(new DateTime(2020, 11, 14), "История", "Правление Николая 2", 20, 12, 100),
                new FailPassExam(new DateTime(2020, 11, 15), "Биология", 20, 12, 3),
                new Test(new DateTime(2020, 11, 16), "География", "Южная Америка", 20, 12, 100),
                new Control(new DateTime(2020, 11, 17), "Алгебра", 20, 12)
            });

            Semester res = new Semester(new List<Exam>
            {
                new FinalExam(new DateTime(2020, 10, 31), "Геометрия", 2, 3, 3),
                new Test(new DateTime(2020, 11, 14), "История", "Правление Николая 2", 20, 12, 100),
                new FailPassExam(new DateTime(2020, 11, 15), "Биология", 20, 12, 3)
            });
            sem.RemoveAll(x => x.ExamDate > new DateTime(2020, 11, 15));
            foreach (var item in sem)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~");

            res.WriteExams();
            Assert.AreEqual(res, sem);
        }

        [TestMethod()]
        public void FindAllTest()
        {
            var sem = new Semester(new List<Exam>
            {
                new FinalExam(new DateTime(2020, 10, 31), "Геометрия", 2, 3, 3),
                new Test(new DateTime(2020, 11, 14), "История", "Правление Николая 2", 20, 12, 100),
                new FailPassExam(new DateTime(2020, 11, 15), "Биология", 20, 12, 3),
                new Test(new DateTime(2020, 11, 16), "География", "Южная Америка", 20, 12, 100),
                new Control(new DateTime(2020, 11, 17), "Алгебра", 20, 12)
            });

            Semester res = new Semester(new List<Exam>
            {
                new FinalExam(new DateTime(2020, 10, 31), "Геометрия", 2, 3, 3),
                new Test(new DateTime(2020, 11, 14), "История", "Правление Николая 2", 20, 12, 100),
                new FailPassExam(new DateTime(2020, 11, 15), "Биология", 20, 12, 3)
            });
            foreach (var item in sem.FindAll(x => x.ExamDate < new DateTime(2020, 11, 16)))
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("~~~~~~~~~~~~~~~~~~");
            res.WriteExams();
            Assert.AreEqual(res, sem.FindAll(x => x.ExamDate < new DateTime(2020, 11, 16)));
        }
    }
}