namespace Samples.Patterns.Proxy
{
    // Proxy
    public class ProxyImage : IImage
    {
        private readonly string _fileName;
        private RealImage _realImage;
        private readonly ILogger _logger;

        public ProxyImage(string fileName, ILogger logger)
        {
            _fileName = fileName;
            _logger = logger;
        }

        public void Display()
        {
            if (_realImage == null)
            {
                _realImage = new RealImage(_fileName, _logger);
            }
            _realImage.Display();
        }
    }
}
