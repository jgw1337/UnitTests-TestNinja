using Moq;
using NUnit.Framework;
using Shouldly;
using System.Net;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class InstallerHelperWithDependencyInjectionTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelperWithDependencyInjection _installerHelper;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelperWithDependencyInjection(_fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_DownloadFails_ReturnsFalse()
        {
            // Arrange
            _fileDownloader
                .Setup(x => x.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();

            // Act
            var result = _installerHelper.DownloadInstaller("customer", "installer");

            // Assert
            result.ShouldBeFalse();
        }

        [Test]
        public void DownloadInstaller_DownloadSucceeds_ReturnsTrue()
        {
            // Arrange

            // Act
            var result = _installerHelper.DownloadInstaller("customer", "installer");

            // Assert
            result.ShouldBeTrue();
        }

    }
}
