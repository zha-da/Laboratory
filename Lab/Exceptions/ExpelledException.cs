using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
