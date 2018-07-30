using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            base.Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked grading requires at least 5 students to work.");
            }

            SetGradeMinimums();
            return DetermineLetterGrade(averageGrade);

        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            else
            {
                base.CalculateStatistics();
            }
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            else
            {
                base.CalculateStudentStatistics(name);
            }
        }

        private char DetermineLetterGrade(double averageGrade)
        {
            if (averageGrade >= minimumForA)
            {
                return 'A';
            }
            else if (averageGrade >= minimumForB)
            {
                return 'B';
            }
            else if (averageGrade >= minimumForC)
            {
                return 'C';
            }
            else if (averageGrade >= minimumForD)
            {
                return 'D';
            }
            else
            {
                return 'F';
            }
        }

        private void SetGradeMinimums()
        {
            int threshold = (int)Math.Ceiling(Students.Count * 0.2);
            List<double> grades = Students.OrderByDescending(e => e.AverageGrade)
                                    .Select(e => e.AverageGrade)
                                    .ToList();

            minimumForA = grades[threshold - 1];
            minimumForB = grades[(threshold * 2) - 1];
            minimumForC = grades[(threshold * 3) - 1];
            minimumForD = grades[(threshold * 4) - 1];
        }

        private double minimumForA;
        private double minimumForB;
        private double minimumForC;
        private double minimumForD;
    }
}
