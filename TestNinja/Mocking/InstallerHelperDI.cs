using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelperDI
    {
        private string _setupDestinationFile;
        private readonly IFileDownloader _fileDownloader;

        public InstallerHelperDI(IFileDownloader fileDownloader = null)
        {
            _fileDownloader = fileDownloader ?? new FileDownloader();
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _fileDownloader.DownloadFile(
                    string.Format("http://example.com/{0}/{1}",
                        customerName,
                        installerName),
                    _setupDestinationFile);

                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }
}