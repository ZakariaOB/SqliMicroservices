namespace Samples.Patterns.Decorator
{
    // Another concrete decorator
    public class PepperoniDecorator : PizzaDecorator
    {
        public PepperoniDecorator(IPizza pizza) : base(pizza)
        {
        }

        public override string GetDescription()
        {
            return $"{base.GetDescription()}, pepperoni";
        }

        public override decimal GetCost()
        {
            return base.GetCost() + 2.00m;
        }
    }
}
