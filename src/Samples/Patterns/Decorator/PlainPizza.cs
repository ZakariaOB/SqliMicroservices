namespace Samples.Patterns.Decorator
{
    public class PlainPizza : IPizza
    {
        public string GetDescription()
        {
            return "Plain pizza";
        }

        public decimal GetCost()
        {
            return 5.00m;
        }
    }
}
