namespace TestNinja.Mocking
{
    class FileDownloader : IFileDownloader
    {
        public void DownloadFile(string url, string destination)
        {
            var client = new FileDownloader();
            client.DownloadFile(url, destination);
        }
    }
}
