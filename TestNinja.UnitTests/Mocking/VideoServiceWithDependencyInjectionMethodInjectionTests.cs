using NUnit.Framework;
using Shouldly;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class VideoServiceWithDependencyInjectionMethodInjectionTests
    {
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnsError()
        {
            // Arrange
            var service = new VideoServiceWithDependencyInjectionMethodInjection();

            // Act
            var result = service.ReadVideoTitle(new MockFileReader());

            // Assert
            result.ShouldContain("error");
        }
    }
}
