namespace Samples.WhatsNew
{
    public class CollectionExpressions
    {
        // Basic array initialization
        public int[] GetNumbers() => [1, 2, 3, 4, 5];

        // List initialization
        public List<string> GetFruits() => ["apple", "banana", "orange"];

        // Initializing a dictionary
        public Dictionary<string, int> GetAges() => new()
        {
            ["Alice"] = 30,
            ["Bob"] = 25,
            ["Charlie"] = 35
        };

        // Combining collections
        public int[] CombineArrays()
        {
            int[] first = [1, 2, 3];
            int[] second = [4, 5, 6];
            return [.. first, .. second];
        }

        // Using with custom types
        public record Person(string Name, int Age);

        public Person[] GetPeople() => [
            new("Alice", 30),
            new("Bob", 25),
            new("Charlie", 35)
        ];

        // As method parameters
        public void PrintNames(string[] names) 
            => Console.WriteLine(string.Join(", ", names));

        public void DemoPrintNames() => PrintNames(["John", "Jane", "Joe"]);

        // Mixing types (implicitly typed)
        public object[] GetMixedArray() => [1, "two", 3.0, true];

        // Creating a jagged array
        public int[][] GetMatrix() => [
            [1, 2, 3],
            [4, 5, 6],
            [7, 8, 9]
        ];
    }
}
