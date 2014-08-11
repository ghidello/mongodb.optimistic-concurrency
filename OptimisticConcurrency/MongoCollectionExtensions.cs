using Ghidello.MongoDB.OptimisticConcurrency;
using Ghidello.MongoDB.OptimisticConcurrency.Concurrency;

// ReSharper disable CheckNamespace
namespace MongoDB.Driver
// ReSharper restore CheckNamespace
{
    public static class MongoCollectionExtensions
    {
        public static IAmOptimisticCollection<T> Optimistic<T>(this MongoCollection collection) where T : class, INeedOptimisticConcurrency
        {
            return new OptimisticCollection<T>(collection);
        }

        public static IAmOptimisticCollection<T> Optimistic<T>(this MongoCollection<T> collection) where T : class, INeedOptimisticConcurrency
        {
            return new OptimisticCollection<T>(collection);
        }
    }
}