using Moq;
using NUnit.Framework;
using Shouldly;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class VideoServiceDIConstructorInjectionTests
    {
        private Mock<IFileReader> _fileReader;
        private VideoServiceDIConstructorInjection _videoService;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _fileReader = new Mock<IFileReader>();
            _videoService = new VideoServiceDIConstructorInjection(_fileReader.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnsError()
        {
            // Arrange
            var service = new VideoServiceDIConstructorInjection(new MockFileReader());

            // Act
            var result = service.ReadVideoTitle();

            // Assert
            result.ShouldContain("error");
        }

        [Test]
        public void ReadVideoTitle_EmptyFileWithMoq_ReturnsError()
        {
            // Arrange
            _fileReader.Setup(x => x.Read("video.txt")).Returns("");

            // Act
            var result = _videoService.ReadVideoTitle();

            // Assert
            result.ShouldContain("error");
        }

    }
}