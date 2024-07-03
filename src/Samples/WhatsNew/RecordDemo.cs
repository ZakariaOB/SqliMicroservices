using System;

// Benefits of Records
// Immutability: Records are designed to be immutable by default.
// Value-based Equality: Records compare objects by value, not by reference.
// Concise Syntax: Records can be declared with less boilerplate code.
// When to Use Records
//  When you need to create immutable data objects.
//  When value-based equality is important.
//  For data transfer objects (DTOs) where immutability and equality are beneficial.

namespace RecordDemo
{
    public class PersonClass
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string? NationalId { get; set; }


        public PersonClass(string firstName, string lastName, string? nationalId = null)
        {
            FirstName = firstName;
            LastName = lastName;
            NationalId = nationalId;
        }

        
        public override bool Equals(object? obj)
        {
            if (obj is PersonClass person)
            {
                return
                    FirstName == person.FirstName
                 && LastName == person.LastName
                 && NationalId == person.NationalId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, NationalId);
        }
    }

    public record PersonRecord(string FirstName, string LastName);

    public class PersonComparer : IEqualityComparer<PersonClass>
    {
        public bool Equals(PersonClass? x, PersonClass? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;
            
            if (!string.IsNullOrWhiteSpace(x.NationalId) 
                && !string.IsNullOrWhiteSpace(y.NationalId))
            {
                return x.NationalId == y.NationalId;
            }

            return 
                x.FirstName == y.FirstName 
                && x.LastName == y.LastName;
        }

        public int GetHashCode(PersonClass obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.NationalId))
            {
                return obj.NationalId.GetHashCode();
            }
            return HashCode.Combine(obj.FirstName, obj.LastName);
        }
    }
}