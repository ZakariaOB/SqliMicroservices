using System;

namespace RecordDemo
{
    // Basic record
    public record Person(string FirstName, string LastName, int Age);

    // Record with additional members
    public record Employee(string FirstName, string LastName, int Age, string Department) : Person(FirstName, LastName, Age)
    {
        public decimal Salary { get; set; }
        public void GiveRaise(decimal amount) => Salary += amount;
    }

    // Equivalent class for comparison
    public class PersonClass
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public int Age { get; init; }

        public PersonClass(string firstName, string lastName, int age)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
        }
    }

    // Record struct (C# 10+)
    public record struct Point(int X, int Y);
}