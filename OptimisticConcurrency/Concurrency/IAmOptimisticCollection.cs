using Ghidello.MongoDB.OptimisticConcurrency.Utils;
using MongoDB.Driver;

namespace Ghidello.MongoDB.OptimisticConcurrency.Concurrency
{
    public interface IAmOptimisticCollection<T> : IFluentInterface where T : INeedOptimisticConcurrency
    {
        WriteConcernResult Update(T obj, WriteConcern concern = null);
        WriteConcernResult Remove(T obj, WriteConcern concern = null);
    }
}