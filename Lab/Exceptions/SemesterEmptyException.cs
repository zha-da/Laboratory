using System;

namespace Laboratory.Exams.Exceptions
{
    class SemesterEmptyException : Exception
    {
        public SemesterEmptyException()
            : base() { }

        public SemesterEmptyException(string message)
            : base(message) { }
    }
}
