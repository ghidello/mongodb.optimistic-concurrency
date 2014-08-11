using MongoDB.Bson;
using OptimisticConcurrency.Tests.Base;
using OptimisticConcurrency.Tests.Model;

namespace OptimisticConcurrency.Tests
{
    public class ObjectWithStringAsObjectIdTests : IntegrationTest<PersonWithStringAsObjectId, string>
    {
        protected override PersonWithStringAsObjectId GetInitialObject()
        {
            return new PersonWithStringAsObjectId(ObjectId.GenerateNewId().ToString(), 20);
        }
    }
}