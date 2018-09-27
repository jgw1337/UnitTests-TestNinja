using NUnit.Framework;
using Shouldly;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class VideoServiceWithDependencyInjectionParameterInjectionTests
    {
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnsError()
        {
            // Arrange
            var service = new VideoServiceWithDependencyInjectionParameterInjection
            {
                FileReader = new MockFileReader()
            };

            // Act
            var result = service.ReadVideoTitle();

            // Assert
            result.ShouldContain("error");
        }
    }
}
