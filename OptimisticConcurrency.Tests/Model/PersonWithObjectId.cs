using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace OptimisticConcurrency.Tests.Model
{
    public class PersonWithObjectId : BaseConcurrencyObject<ObjectId>
    {
        [BsonId]
        public override ObjectId Name { get; set; }

        public PersonWithObjectId(ObjectId name, int age)
        {
            Name = name;
            Age = age;
        }
    }
}