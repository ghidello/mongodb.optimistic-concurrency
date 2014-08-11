using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace OptimisticConcurrency.Integration.Tests.Model
{
    public abstract class BaseConcurrencyObject<T> : IProvideTestMethods<T>, IEquatable<BaseConcurrencyObject<T>>
    {
        public abstract T Name { get; set; }
        public int Age { get; set; }

        public virtual int Version { get; set; }
        public virtual IMongoQuery QueryById()
        {
            return Query.EQ("_id", BsonValue.Create(Name));
        }

        public bool Equals(BaseConcurrencyObject<T> other)
        {
            if (other == null) return false;
            if (other == this) return true;
            return other.Name.Equals(Name) && other.Age.Equals(Age) && other.Version.Equals(Version);
        }
    }
}