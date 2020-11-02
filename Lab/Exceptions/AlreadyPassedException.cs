using System;

namespace Laboratory.Exams
{
    internal class AlreadyPassedException : Exception
    {
        public AlreadyPassedException()
            : base() { }

        public AlreadyPassedException(string message)
            : base(message) { }
    }
}
