using Ghidello.MongoDB.OptimisticConcurrency.Concurrency;
using MongoDB.Bson;
using MongoDB.Driver;
using OptimisticConcurrency.Integration.Tests.Model;
using SharpTestsEx;
using Xunit;

namespace OptimisticConcurrency.Integration.Tests.Base
{
    public abstract class IntegrationTest<T, TU> : BaseIntegrationTest<T, TU> where T : BaseConcurrencyObject<TU>
    {
        public MongoCollection<T> TypedCollection { get; set; }
        public MongoCollection<BsonDocument> UnTypedCollection { get; set; }

        protected IntegrationTest()
        {
            TypedCollection = Database.GetCollection<T>(CollectionName);
            UnTypedCollection = Database.GetCollection(CollectionName);
        }

        [Fact]
        public void GivenAnUntypedCollectionAndAValidVersion_TheObjectCanBeUpdated()
        {
            var initialObject = SaveInitialObject(UnTypedCollection);

            initialObject.Age = 30;

            UpdateAndVerify(UnTypedCollection, initialObject, 1);

            initialObject.Age = 40;

            UpdateAndVerify(UnTypedCollection, initialObject, 2);
        }

        [Fact]
        public void GivenAnUntypedCollectionAndAnInvalidVersion_ConcurrencyUpdateThrows_MongoConcurrencyUpdatedException()
        {
            var initialObject = SaveInitialObject(UnTypedCollection);

            initialObject.Age = 30;
            initialObject.Version = 8;

            Executing.This(() => UnTypedCollection.Optimistic<T>().Update(initialObject))
                .Should().Throw<MongoConcurrencyUpdatedException>();
        }

        [Fact]
        public void GivenAnUntypedCollectionAndAnUnexistingId_ConcurrencyUpdateThrows_MongoConcurrencyDeletedException()
        {
            var initialObject = GetInitialObject();

            Executing.This(() => UnTypedCollection.Optimistic<T>().Update(initialObject))
                .Should().Throw<MongoConcurrencyDeletedException>();
        }

        [Fact]
        public void GivenATypedCollectionAndGivenAValidVersion_TheObjectCanBeUpdated()
        {
            var initialObject = SaveInitialObject(TypedCollection);

            initialObject.Age = 30;

            UpdateAndVerify(TypedCollection, initialObject, 1);

            initialObject.Age = 40;

            UpdateAndVerify(TypedCollection, initialObject, 2);
        }

        [Fact]
        public void GivenATntypedCollectionAndAnInvalidVersion_ConcurrencyUpdateThrows_MongoConcurrencyUpdatedException()
        {
            var initialObject = SaveInitialObject(TypedCollection);

            initialObject.Age = 30;
            initialObject.Version = 8;

            Executing.This(() => TypedCollection.Optimistic().Update(initialObject))
                .Should().Throw<MongoConcurrencyUpdatedException>();
        }

        [Fact]
        public void GivenATypedCollectionAndAnUnexistingId_ConcurrencyUpdateThrows_MongoConcurrencyDeletedException()
        {
            var initialObject = GetInitialObject();

            Executing.This(() => TypedCollection.Optimistic().Update(initialObject))
                .Should().Throw<MongoConcurrencyDeletedException>();
        }

        [Fact]
        public void GivenAnUntypedCollectionAndAValidVersion_TheObjectCanBeDeleted()
        {
            var initialObject = SaveInitialObject(UnTypedCollection);

            RemoveAndVerify(UnTypedCollection, initialObject);
        }

        [Fact]
        public void GivenAnUntypedCollectionAndAnInvalidVersion_TheObjectCanBeDeleted()
        {
            var initialObject = SaveInitialObject(UnTypedCollection);

            initialObject.Version = 32;

            Executing.This(() => TypedCollection.Optimistic().Remove(initialObject))
                .Should().Throw<MongoConcurrencyUpdatedException>();
        } 

        [Fact]
        public void GivenATypedCollectionAndAValidVersion_TheObjectCanBeDeleted()
        {
            var initialObject = SaveInitialObject(TypedCollection);

            RemoveAndVerify(TypedCollection, initialObject);
        }

        [Fact]
        public void GivenATntypedCollectionAndAnInvalidVersion_TheObjectCanBeDeleted()
        {
            var initialObject = SaveInitialObject(UnTypedCollection);

            initialObject.Version = 32;

            Executing.This(() => TypedCollection.Optimistic().Remove(initialObject))
                .Should().Throw<MongoConcurrencyUpdatedException>();
        } 
    }
}