namespace Samples.Patterns.Decorator
{
    // Concrete decorator
    public class CheeseDecorator(IPizza pizza) : PizzaDecorator(pizza)
    {
        public override string GetDescription()
        {
            return $"{base.GetDescription()}, extra cheese";
        }

        public override decimal GetCost()
        {
            return base.GetCost() + 1.50m;
        }
    }
}
