using System;

namespace Laboratory.Exams
{
    internal class UnsuccessfulAttemtException : Exception
    {
        public UnsuccessfulAttemtException()
            : base() { }

        public UnsuccessfulAttemtException(string message)
            : base(message) { }
    }
}
