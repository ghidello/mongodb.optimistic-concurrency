using MongoDB.Bson;
using OptimisticConcurrency.Integration.Tests.Base;
using OptimisticConcurrency.Integration.Tests.Model;

namespace OptimisticConcurrency.Integration.Tests
{
    public class ObjectWithStringAsObjectIdTests : IntegrationTest<PersonWithStringAsObjectId, string>
    {
        protected override PersonWithStringAsObjectId GetInitialObject()
        {
            return new PersonWithStringAsObjectId(ObjectId.GenerateNewId().ToString(), 20);
        }
    }
}