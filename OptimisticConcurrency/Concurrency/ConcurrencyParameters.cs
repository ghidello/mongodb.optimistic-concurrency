using System;
using Ghidello.MongoDB.OptimisticConcurrency.Utils;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Ghidello.MongoDB.OptimisticConcurrency.Concurrency
{
    internal class ConcurrencyParameters<T> where T : INeedOptimisticConcurrency
    {
        public bool IsUpdate { get; set; }
        public BsonValue Id { get; set; }
        public int RequestedVersion { get; set; }

        public IMongoQuery QueryWithVersion { get; set; }
        public IMongoQuery QueryById { get; set; }
        public BsonDocument Bson { get; set; }

        public ConcurrencyParameters(T obj, bool isUpdate)
        {
            Guard.NotNull(() => obj, obj);

            IsUpdate = isUpdate;
            RequestedVersion = obj.Version;

            if (isUpdate)
            {
                obj.Version++;
            }

            Bson = obj.ToBsonDocument();

            Id = Bson["_id"];

            if (Id.IsNullOrEmpty())
                throw new InvalidOperationException("Optimistic Update can only be used with documents that have an Id.");

            if (isUpdate)
            {
                Bson.Remove("_id");
            }

            QueryById = Query.EQ("_id", Id);
            QueryWithVersion = Query.And(QueryById, Query<T>.EQ(x => x.Version, RequestedVersion));
        }
    }
}