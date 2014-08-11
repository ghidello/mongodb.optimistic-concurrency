using MongoDB.Bson;
using OptimisticConcurrency.Integration.Tests.Base;
using OptimisticConcurrency.Integration.Tests.Model;

namespace OptimisticConcurrency.Integration.Tests
{
    public class ObjectWithObjectId : IntegrationTest<PersonWithObjectId, ObjectId>
    {
        protected override PersonWithObjectId GetInitialObject()
        {
            return new PersonWithObjectId(ObjectId.GenerateNewId(), 20);
        }
    }
}