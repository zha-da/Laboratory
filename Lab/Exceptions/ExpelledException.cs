using System;

namespace Laboratory.Exams
{
    internal class ExpelledException : Exception
    {
        public ExpelledException()
            : base() { }

        public ExpelledException(string message)
            : base(message) { }
    }
}
