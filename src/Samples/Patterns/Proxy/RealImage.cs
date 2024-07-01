namespace Samples.Patterns.Proxy
{
    // Real component
    public class RealImage : IImage
    {
        private readonly string _fileName;
        private readonly ILogger _logger;

        public RealImage(string fileName, ILogger logger)
        {
            _fileName = fileName;
            _logger = logger;
            LoadImageFromDisk();
        }

        private void LoadImageFromDisk()
        {
            _logger.Log($"Loading image: {_fileName}");
            // Simulate loading image from disk
            // In a real application, this would load from actual disk storage
        }

        public void Display()
        {
            _logger.Log($"Displaying image: {_fileName}");
        }
    }
}
