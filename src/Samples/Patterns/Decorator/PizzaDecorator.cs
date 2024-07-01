namespace Samples.Patterns.Decorator
{
    // Decorator abstract class
    public abstract class PizzaDecorator(IPizza pizza) : IPizza
    {
        protected IPizza _pizza = pizza;

        public virtual string GetDescription()
        {
            return _pizza.GetDescription();
        }

        public virtual decimal GetCost()
        {
            return _pizza.GetCost();
        }
    }

}
