using OptimisticConcurrency.Integration.Tests.Base;
using OptimisticConcurrency.Integration.Tests.Model;

namespace OptimisticConcurrency.Integration.Tests
{
    public class ObjectWithDifferentBsonVersionNameTests : IntegrationTest<PersonWithDifferentBsonVersionName, string>
    {
        protected override PersonWithDifferentBsonVersionName GetInitialObject()
        {
            return new PersonWithDifferentBsonVersionName("DifferentBsonVersion", 20);
        }
    }
}