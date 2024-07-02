namespace Samples.WhatsNew
{
    internal class PrimaryConstructors
    {
        // Basic usage
        public class Person(string name, int age)
        {
            public string Name { get; } = name;
            public int Age { get; } = age;
        }

        // With private fields
        public class Circle(double radius)
        {
            private readonly double _radius = radius;
            public double Diameter => 2 * _radius;
            public double Area => Math.PI * _radius * _radius;
        }

        // Combining with inheritance
        public class Animal(string species)
        {
            public string Species { get; } = species;
        }
        public class Dog(string name, int age) : Animal("Canine")
        {
            public string Name { get; } = name;
            public int Age { get; } = age;
        }

        // With optional parameters
        public class Configuration(
            string host, 
            int port = 8080, 
            bool useHttps = false)
        {
            public string Host { get; } = host;
            public int Port { get; } = port;
            public bool UseHttps { get; } = useHttps;
        }

        // Combining with init-only properties
        public class Product(
            string name, 
            decimal price)
        {
            public string Name { get; init; } = name;
            public decimal Price { get; init; } = price;
            public string Description { get; init; } = string.Empty;
        }

        private static void Use()
        {
            var person = new Person("Alice", 30);
            
            var circle = new Circle(5);
            
            var dog = new Dog("Buddy", 5);

            // Usage
            var config1 = new Configuration("example.com");
            var config2 = new Configuration("secure.example.com", 443, true);

            var product = new Product("Laptop", 999.99m) { Description = "High-performance laptop" };
        }
    }
}
