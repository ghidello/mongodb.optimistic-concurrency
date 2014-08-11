using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace OptimisticConcurrency.Tests.Model
{
    public class PersonWithStringAsObjectId : BaseConcurrencyObject<string>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public override string Name { get; set; }

        public PersonWithStringAsObjectId(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public override IMongoQuery QueryById()
        {
            return Query.EQ("_id", ObjectId.Parse(Name));
        }
    }
}