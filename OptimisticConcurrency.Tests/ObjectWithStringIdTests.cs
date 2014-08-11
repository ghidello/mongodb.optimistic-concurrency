using MongoDB.Bson;
using OptimisticConcurrency.Tests.Base;
using OptimisticConcurrency.Tests.Model;

namespace OptimisticConcurrency.Tests
{
    public class ObjectWithStringIdTests : IntegrationTest<PersonWithObjectId, ObjectId>
    {
        protected override PersonWithObjectId GetInitialObject()
        {
            return new PersonWithObjectId(ObjectId.GenerateNewId(), 20);
        }
    }
}