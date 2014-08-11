using Ghidello.MongoDB.OptimisticConcurrency;
using MongoDB.Driver;

namespace OptimisticConcurrency.Tests.Model
{
    public interface IProvideTestMethods<T> : INeedOptimisticConcurrency
    {
        T Name { get; set; }
        int Age { get; set; }
        IMongoQuery QueryById();
    }
}