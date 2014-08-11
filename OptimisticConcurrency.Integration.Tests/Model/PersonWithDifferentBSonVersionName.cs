using MongoDB.Bson.Serialization.Attributes;

namespace OptimisticConcurrency.Integration.Tests.Model
{
    public class PersonWithDifferentBsonVersionName : BaseConcurrencyObject<string>
    {
        [BsonId]
        public override string Name { get; set; }

        [BsonElement("DifferentFieldName")]
        public override int Version { get; set; }

        public PersonWithDifferentBsonVersionName(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}