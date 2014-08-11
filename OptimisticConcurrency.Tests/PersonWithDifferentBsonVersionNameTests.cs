using OptimisticConcurrency.Tests.Base;
using OptimisticConcurrency.Tests.Model;

namespace OptimisticConcurrency.Tests
{
    public class PersonWithDifferentBsonVersionNameTests : IntegrationTest<PersonWithDifferentBsonVersionName, string>
    {
        protected override PersonWithDifferentBsonVersionName GetInitialObject()
        {
            return new PersonWithDifferentBsonVersionName("DifferentBsonVersion", 20);
        }
    }
}