﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            Exam mathExam = new Test("Математика", 50);
            mathExam.DisplayInfo();

            mathExam.TakeExam(28);
            mathExam.DisplayInfo();

            mathExam.TakeExam(48);
            mathExam.DisplayInfo();

            mathExam.TakeExam(50);
            mathExam.DisplayInfo();

            mathExam.TakeExam(43);
            mathExam.DisplayInfo();

            Exam bioEx = new Test("Биология", 12);
            bioEx.DisplayInfo();
            for (int i = 0; i < 2; i++)
            {
                bioEx.TakeExam();
                bioEx.DisplayInfo();
            }
            Console.ReadKey();
        }
    }
}
