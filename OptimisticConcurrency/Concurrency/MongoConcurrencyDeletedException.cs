using System;
using MongoDB.Driver;

namespace Ghidello.MongoDB.OptimisticConcurrency.Concurrency
{
    public class MongoConcurrencyDeletedException : Exception
    {
        public IMongoQuery Query { get; set; }
        public int RequestedVersion { get; set; }

        public MongoConcurrencyDeletedException(IMongoQuery query, int requestedVersion)
            : base("The target object has already been deleted")
        {
            Query = query;
            RequestedVersion = requestedVersion;
        }

        public override string ToString()
        {
            const string message = "The requested object ({0}) has already been deleted: Requested version = {1}";
            return string.Format(message, Query, RequestedVersion);
        }
    }
}