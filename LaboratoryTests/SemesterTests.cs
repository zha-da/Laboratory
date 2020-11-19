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
        public void GetFromFileTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteAllTest()
        {
            Console.WriteLine("Before~~~~~~~~~~~~~~~~~~~~~~~~~~");
            var sem = new Semester(new List<Exam>
            {
                new FinalExam(new DateTime(2020, 10, 31), "Математика", 2, 3, 3),
                new Test(new DateTime(2020, 11, 14), "История", "Правление Николая 2", 20, 12, 100),
                new Test(new DateTime(2020, 11, 15), "Биология", "Строение клетки", 20, 12, 100),
                new Test(new DateTime(2020, 11, 16), "География", "Южная Америка", 20, 12, 100),
                new Test(new DateTime(2020, 11, 17), "Математика", "Векторные пространства", 20, 12, 100)
            });
            foreach (var item in sem.Exams)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("After~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            sem.DeleteAll(typeof(Test));
            foreach (var item in sem.Exams)
            {
                Console.WriteLine(item.ToString());
            }
            //Assert.AreEqual<List<Exam>>(new List<Exam>
            //{
            //    new FinalExam(new DateTime(2020, 10, 31), "Математика", 2, 3, 3)
            //}, sem.Exams);
        }

        [TestMethod()]
        public void ReturnAllTest()
        {
            Assert.Fail();
        }
    }
}