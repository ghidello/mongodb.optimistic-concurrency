using OptimisticConcurrency.Integration.Tests.Base;
using OptimisticConcurrency.Integration.Tests.Model;

namespace OptimisticConcurrency.Integration.Tests
{
    public class PersonWithIntIdTests : IntegrationTest<PersonWithIntId, int>
    {
        protected override PersonWithIntId GetInitialObject()
        {
            return new PersonWithIntId(34, 20);
        }
    }
}