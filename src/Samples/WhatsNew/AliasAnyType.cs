namespace Samples.WhatsNew
{
    using ComplexType = Dictionary<string, List<(int, string)>>;
    using NumberDictionary = Dictionary<string, int>;
    // Alias declarations
    using PointHere = (int X, int Y);
    using StringList = List<string>;

    public class AliasAnyType
    {
        // Using alias for a tuple
        public PointHere CreatePoint(int x, int y) => (x, y);

        // Using alias for a generic type
        public StringList GetNames() => ["Alice", "Bob", "Charlie"];

        // Using alias for a more complex generic type
        public NumberDictionary GetAges() => new()
        {
            ["Alice"] = 30,
            ["Bob"] = 25,
            ["Charlie"] = 35
        };

        // Using alias for a very complex type
        public ComplexType GetComplexData() => new()
        {
            ["Group1"] = [(1, "A"), (2, "B")],
            ["Group2"] = [(3, "C"), (4, "D")]
        };

        // Method using multiple aliases
        public void ProcessData(
            PointHere location, 
            StringList names, 
            NumberDictionary ages)
        {
            Console.WriteLine($"Location: ({location.X}, {location.Y})");
            Console.WriteLine($"Names: {string.Join(", ", names)}");
            foreach (var (name, age) in ages)
            {
                Console.WriteLine($"{name}: {age}");
            }
        }

        // Demonstrating usage
        public void Demonstrate()
        {
            var point = CreatePoint(10, 20);
            var names = GetNames();
            var ages = GetAges();
            var complexData = GetComplexData();

            ProcessData(point, names, ages);

            Console.WriteLine("Complex Data:");
            foreach (var (group, items) in complexData)
            {
                Console.WriteLine($"{group}: {string.Join(", ", items)}");
            }
        }
    }
}
