using RecordDemo;
using System.Linq;

namespace Samples.Test.Records
{
    public class RecordsTests
    {
        [Fact]
        public void ClassEqualityTest()
        {
            var person1 = new PersonClass("John", "Doe");
            var person2 = new PersonClass("John", "Doe");

            Assert.Equal(person1, person2); // Will fail for class unless Equals is overridden
        }

        [Fact]
        public void RecordEqualityTest()
        {
            var person1 = new PersonRecord("John", "Doe");
            var person2 = new PersonRecord("John", "Doe");

            Assert.Equal(person1, person2); // Will pass for record
        }

        [Fact]
        public void RecordImmutabilityTest()
        {
            var person = new PersonRecord("John", "Doe");

            // Compilation error if trying to modify properties
            // person.FirstName = "Jane"; // Uncommenting this line will cause a compilation error

            person = person with { FirstName = "sqliUser" };

            Assert.Equal("sqliUser", person.FirstName);
        }

        [Fact]
        public void RecordDeconstructionTest()
        {
            var person = new PersonRecord("John", "Doe");
            var (firstName, lastName) = person;

            Assert.Equal("John", firstName);
            Assert.Equal("Doe", lastName);
        }

        [Fact]
        public void ClassDistinctTest()
        {
            var people = new List<PersonClass>
            {
                new PersonClass("John", "Doe"),
                new PersonClass("Jane", "Doe"),
                new PersonClass("John", "Doe") // Duplicate
            };

            var distinctPeople = people.Distinct(new PersonComparer()).ToList();

            Assert.Equal(2, distinctPeople.Count);

            // Using national Id
            people.ForEach(p => p.NationalId = "N1");
            var distinctPeople2 = people.Distinct(new PersonComparer()).ToList();
            Assert.Single(distinctPeople2);
        }

        [Fact]
        public void ClassDistinctTest_SameNationalId()
        {
            var people = new List<PersonClass>
            {
                new PersonClass("John", "Doe", "N"),
                new PersonClass("Jane", "Doe", "N"),
                new PersonClass("John", "Doe", "N") // Duplicate by FirstName and LastName
            };
            
            var distinctPeople = people.Distinct(new PersonComparer()).ToList();

            Assert.Single(distinctPeople); // Only one distinct person due to same NationalId
        }

        [Fact]
        public void RecordDistinctTest()
        {
            var people = new List<PersonRecord>
        {
            new PersonRecord("John", "Doe"),
            new PersonRecord("Jane", "Doe"),
            new PersonRecord("John", "Doe") // Duplicate
        };

            var distinctPeople = people.Distinct().ToList(); // No need for a comparer

            Assert.Equal(2, distinctPeople.Count);
        }
    }
}
