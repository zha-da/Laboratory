using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryMain
{
    [Serializable]
    public class UFile
    {
        private long _size;

        public string Name { get; set; }
        public long Size
        {
            get { return _size; }
            set { _size = value; }
        }
        public DateTime ModificationTime { get; set; }
        public System.IO.FileAttributes Attributes { get; set; }
        public byte[] Data { get; set; }

        public UFile() { }
    }
}
