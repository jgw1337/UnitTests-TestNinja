using Moq;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class VideoServiceDIConstructorInjectionTests
    {
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _videoRepository;
        private VideoServiceDIConstructorInjection _videoService;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _fileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoServiceDIConstructorInjection(_fileReader.Object, _videoRepository.Object);
        }

        // Test with a Fake/Stub/Mocked object
        // ...relies on MockFileReader.cs
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


        // Test with Mocking Framework (e.g. Moq)
        // ...which does not rely on external fakes which might be hard-coded and inflexible
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

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosProcess_ReturnsEmptyString()
        {
            // Arrange
            _videoRepository.Setup(x => x.GetUnprocessedVideos()).Returns(new List<Video>());

            // Act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            // Assert
            result.ShouldBe("");
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_WithUnprocessedVideos_ReturnsCsvOfIds()
        {
            // Arrange
            _videoRepository.Setup(x => x.GetUnprocessedVideos())
                .Returns(new List<Video>
                {
                    new Video{ Id = 1 },
                    new Video{ Id = 2 },
                    new Video{ Id = 3 }
                });

            // Act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            // Assert
            result.ShouldBe("1,2,3");
        }

    }
}