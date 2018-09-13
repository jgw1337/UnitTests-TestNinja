using System.IO;

namespace TestNinja.Mocking
{
    class FileReader : IFileReader
    {
        public string Read(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
