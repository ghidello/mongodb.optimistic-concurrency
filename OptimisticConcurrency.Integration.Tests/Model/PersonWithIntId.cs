using MongoDB.Bson.Serialization.Attributes;

namespace OptimisticConcurrency.Integration.Tests.Model
{
    public class PersonWithIntId : BaseConcurrencyObject<int>
    {
        [BsonId]
        public override int Name { get; set; }

        public PersonWithIntId(int id, int age)
        {
            Name = id;
            Age = age;
        }
    }
}

namespace OptimisticConcurrency.Integration.Tests.Model
{
}