using Moq;
using Samples.Patterns.Proxy;

namespace Samples.Test.Proxy
{
    public class ProxyTests
    {
        [Fact]
        public void TestProxyImageLoading()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            IImage image = new ProxyImage("image.jpg", mockLogger.Object);

            // Act
            image.Display();

            // Assert
            mockLogger.Verify(l => l.Log($"Loading image: image.jpg"), Times.Once);
        }

        [Fact]
        public void TestProxyImageCaching()
        {
            // Arrange
            var mockLogger = new Mock<ILogger>();
            IImage image = new ProxyImage("image.jpg", mockLogger.Object);

            // Act
            image.Display();
            image.Display();
            image.Display();
            image.Display();

            // Assert
            mockLogger.Verify(l => l.Log($"Loading image: image.jpg"), Times.Once);
            mockLogger.Verify(l => l.Log($"Displaying image: image.jpg"), Times.Exactly(4));
        }
    }
}
