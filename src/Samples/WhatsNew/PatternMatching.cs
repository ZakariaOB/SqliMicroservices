using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Samples.WhatsNew
{
    public class PatternMatchingDemo
    {
        public void Demonstrate()
        {
            Console.WriteLine("1. Type Pattern:");
            TypePattern(new Circle(5));
            TypePattern(new Rectangle(4, 6));

            Console.WriteLine("\n2. Property Pattern:");
            PropertyPattern(new Person("Alice", 30));
            PropertyPattern(new Person("Bob", 17));

            Console.WriteLine("\n3. Tuple Pattern:");
            TuplePattern((5, 10));
            TuplePattern((0, 0));

            Console.WriteLine("\n4. Positional Pattern:");
            PositionalPattern(new Point(1, 1));
            PositionalPattern(new Point(0, 5));

            Console.WriteLine("\n5. List Pattern:");
            ListPattern(new[] { 1, 2, 3 });
            ListPattern(new[] { 1, 2, 3, 4, 5 });

            Console.WriteLine("\n6. Var Pattern:");
            VarPattern("Hello, World!");

            Console.WriteLine("\n7. Combination of Patterns:");
            CombinedPattern(new Rectangle(5, 5));
            CombinedPattern(new Circle(3));
            CombinedPattern("Not a shape");
        }

        // 1. Type Pattern
        private void TypePattern(object shape)
        {
            string result = shape switch
            {
                Circle c => $"Circle with radius {c.Radius}",
                Rectangle r => $"Rectangle with dimensions {r.Width}x{r.Height}",
                _ => "Unknown shape"
            };
            Console.WriteLine(result);
        }

        // 2. Property Pattern
        private void PropertyPattern(Person person)
        {
            string status = person switch
            {
                { Age: >= 18 } => "Adult",
                { Age: < 18 } => "Minor",
                _ => "Unknown"
            };
            Console.WriteLine($"{person.Name} is a {status}");
        }

        // 3. Tuple Pattern
        private void TuplePattern((int X, int Y) point)
        {
            string description = point switch
            {
                (0, 0) => "Origin",
                (var x, var y) when x == y => "On diagonal",
                _ => "Elsewhere"
            };
            Console.WriteLine($"Point {point} is {description}");
        }

        // 4. Positional Pattern
        private void PositionalPattern(Point point)
        {
            string description = point switch
            {
                (0, 0) => "At origin",
                (var x, var y) when x == y => "On diagonal",
                (_, > 0) => "Above x-axis",
                _ => "Elsewhere"
            };
            Console.WriteLine($"Point {point} is {description}");
        }

        // 5. List Pattern
        private void ListPattern(int[] numbers)
        {
            string result = numbers switch
            {
                [1, 2, 3] => "Exactly [1, 2, 3]",
                [1, 2, ..] => "Starts with 1, 2",
                [.., 4, 5] => "Ends with 4, 5",
                [1, .., 5] => "Starts with 1 and ends with 5",
                    _ => "Something else"
            };
            Console.WriteLine($"Array {string.Join(", ", numbers)} matches: {result}");
        }

        // 6. Var Pattern
        // The var pattern in C# is a bit unique compared to other pattern matching techniques.
        // Let's dive deeper into how it works and explore some examples.
        // The var pattern:
        // Always matches successfully
        // Introduces a new variable of the inferred type
        // Is particularly useful when you want to capture a value for further use without specifying its exact type
        private void VarPattern(object obj)
        {
            if (obj is var x)
            {
                Console.WriteLine($"Object is {x} and its type is {x.GetType().Name}");
            }
        }

        // 7. Combination of Patterns
        private void CombinedPattern(object obj)
        {
            string result = obj switch
            {
                Rectangle { Width: var w, Height: var h } when w == h => "Square",
                Circle { Radius: > 0 } c => $"Circle with area {Math.PI * c.Radius * c.Radius:F2}",
                IShape => "Some other shape",
                string s => $"A string of length {s.Length}",
                _ => "Unknown object"
            };
            Console.WriteLine(result);
        }
    }

    // Supporting classes
    public interface IShape { }
    public record Circle(double Radius) : IShape;
    public record Rectangle(double Width, double Height) : IShape;
    public record Point(int X, int Y);
    public record Person(string Name, int Age);
}
