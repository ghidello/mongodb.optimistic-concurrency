using MongoDB.Bson.Serialization.Attributes;

namespace OptimisticConcurrency.Integration.Tests.Model
{
    public class PersonWithString : BaseConcurrencyObject<string>
    {
        [BsonId]
        public override string Name { get; set; }

        public PersonWithString(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}