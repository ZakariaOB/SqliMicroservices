using Samples.Patterns.Decorator;

namespace Samples.Test.Decorator
{
    public class DecoratorTest
    {
        [Fact]
        public void TestPlainPizza()
        {
            // Arrange
            IPizza plainPizza = new PlainPizza();

            // Act
            var description = plainPizza.GetDescription();
            var cost = plainPizza.GetCost();

            // Assert
            Assert.Equal("Plain pizza", description);
            Assert.Equal(5.00m, cost);
        }

        [Fact]
        public void TestCheeseDecorator()
        {
            // Arrange
            IPizza plainPizza = new PlainPizza();
            IPizza cheesePizza = new CheeseDecorator(plainPizza);

            // Act
            var description = cheesePizza.GetDescription();
            var cost = cheesePizza.GetCost();

            // Assert
            Assert.Equal("Plain pizza, extra cheese", description);
            Assert.Equal(6.50m, cost);
        }

        [Fact]
        public void TestPepperoniDecorator()
        {
            // Arrange
            IPizza plainPizza = new PlainPizza();
            IPizza pepperoniPizza = new PepperoniDecorator(plainPizza);

            // Act
            var description = pepperoniPizza.GetDescription();
            var cost = pepperoniPizza.GetCost();

            // Assert
            Assert.Equal("Plain pizza, pepperoni", description);
            Assert.Equal(7.00m, cost);
        }

        [Fact]
        public void TestCheeseAndPepperoniDecorator()
        {
            // Arrange
            IPizza plainPizza = new PlainPizza();
            IPizza cheesePepperoniPizza = new PepperoniDecorator(new CheeseDecorator(plainPizza));

            // Act
            var description = cheesePepperoniPizza.GetDescription();
            var cost = cheesePepperoniPizza.GetCost();

            // Assert
            Assert.Equal("Plain pizza, extra cheese, pepperoni", description);
            Assert.Equal(8.50m, cost);
        }
    }
}
