using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OptimisticConcurrency.Integration.Tests.Model
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