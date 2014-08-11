using System;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Ghidello.MongoDB.OptimisticConcurrency.Concurrency
{
    public class OptimisticCollection<T> : IAmOptimisticCollection<T> where T : class, INeedOptimisticConcurrency
    {
        public MongoCollection Collection { get; set; }

        public OptimisticCollection(MongoCollection collection)
        {
            Collection = collection;
        }

        public WriteConcernResult Update(T obj, WriteConcern concern = null)
        {
            ConcurrencyParameters<T> parameters = null;

            try
            {
                parameters = new ConcurrencyParameters<T>(obj, true);

                var update = global::MongoDB.Driver.Builders.Update.Replace(parameters.Bson);
                var options = new MongoUpdateOptions { WriteConcern = concern };

                var result = Collection.Update(parameters.QueryWithVersion, update, options);

                return CheckConcurrencyResult(result, parameters);
            }
            catch (Exception)
            {
                if (parameters != null)
                {
                    obj.Version = parameters.RequestedVersion;
                }
                throw;
            }
        }

        public WriteConcernResult Remove(T obj, WriteConcern concern = null)
        {
            var parameters = new ConcurrencyParameters<T>(obj, false);

            var result = Collection.Remove(parameters.QueryWithVersion, RemoveFlags.Single, concern);

            return CheckConcurrencyResult(result, parameters);
        }

        private WriteConcernResult CheckConcurrencyResult(WriteConcernResult result, ConcurrencyParameters<T> parameters)
        {
            if (result.DocumentsAffected == 1) return result;

            var fields = Fields<T>.Include(x => x.Version);

            var existingObject = Collection.FindAs<T>(parameters.QueryById).SetFields(fields).FirstOrDefault();

            if (existingObject != null)
            {
                throw new MongoConcurrencyUpdatedException(parameters.QueryById, parameters.RequestedVersion, existingObject.Version);
            }

            if (parameters.IsUpdate)
            {
                throw new MongoConcurrencyDeletedException(parameters.QueryById, parameters.RequestedVersion);
            }

            return result;
        }
    }
}