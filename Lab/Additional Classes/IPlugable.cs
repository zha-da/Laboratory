using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratory.AdditionalClasses
{
    public interface IPlugable
    {
        string[] Print();
        void SortByString();
        void SortByInt();
    }
}
