using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
