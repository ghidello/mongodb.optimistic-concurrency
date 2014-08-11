using System;
using MongoDB.Driver;

namespace Ghidello.MongoDB.OptimisticConcurrency.Concurrency
{
    public class MongoConcurrencyUpdatedException : Exception
    {
        public IMongoQuery Query { get; set; }
        public int RequestedVersion { get; set; }
        public int CurrentVersion { get; set; }

        public MongoConcurrencyUpdatedException(IMongoQuery query, int requestedVersion, int currentVersion)
            : base("The target object has already been modified")
        {
            Query = query;
            RequestedVersion = requestedVersion;
            CurrentVersion = currentVersion;
        }

        public override string ToString()
        {
            const string message = "The requested object ({0}) has already been modified: Requested version = {1} - Current version = {2}";
            return string.Format(message, Query, RequestedVersion, CurrentVersion);
        }
    }
}