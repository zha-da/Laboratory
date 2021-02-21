using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryMain
{
    [Serializable]
    public class UDirectory
    {
        public DateTime CreationTime { get; set; }
        public UDirectory[] SubDirectories { get; set; }
        public UFile[] Files { get; set; }
        public string Name { get; set; }

        public UDirectory(string path)
        {
            CreationTime = new DirectoryInfo(path).CreationTime;
            Name = path.Substring(path.LastIndexOf('\\') + 1);
            Files = GetFilesR(path).ToArray();
            SubDirectories = GetDirectoriesR(path).ToArray();
        }
        public UDirectory() { }

        private IEnumerable<UDirectory> GetDirectoriesR(string root)
        {
            foreach (var dir in Directory.GetDirectories(root))
            {
                var dirInfo = new DirectoryInfo(dir);
                var directory = new UDirectory
                {
                    CreationTime = dirInfo.CreationTime,
                    Name = dirInfo.Name,
                    Files = GetFilesR(dir).ToArray(),
                    SubDirectories = GetDirectoriesR(dir).ToArray()
                };
                yield return directory;
            }
        }

        private IEnumerable<UFile> GetFilesR(string dir)
        {
            foreach (var file in Directory.GetFiles(dir))
            {
                var fInfo = new FileInfo(file);

                yield return new UFile
                {
                    Data = File.ReadAllBytes(file),
                    Name = fInfo.Name,
                    ModificationTime = fInfo.LastWriteTime,
                    Size = fInfo.Length,
                    Attributes = fInfo.Attributes
                };
            }
        }
    }
}
