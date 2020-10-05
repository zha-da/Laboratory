using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
