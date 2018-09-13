using TestNinja.Mocking;

namespace TestNinja.UnitTests
{
    class MockFileReader : IFileReader
    {
        public string Read(string path)
        {
            return "";
        }
    }
}
