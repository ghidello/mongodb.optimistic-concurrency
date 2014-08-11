using System;
using MongoDB.Driver;
using OptimisticConcurrency.Tests.Model;
using OptimisticConcurrency.Tests.Properties;
using SharpTestsEx;

namespace OptimisticConcurrency.Tests.Base
{
    public abstract class BaseIntegrationTest<T, TU> : IDisposable
        where T : BaseConcurrencyObject<TU>
    {
        public const string CollectionName = "Persons";
        public string DatabaseName { get; set; }
        public MongoServer Server { get; set; }
        public MongoDatabase Database { get; set; }
        
        protected BaseIntegrationTest()
        {
            var tmpId = Guid.NewGuid().ToString().Replace("{", "").Replace("}", "").Replace("-", "");
            DatabaseName = string.Format("Ghidello_Concurrency_{0}", tmpId);
            Server = new MongoClient(Settings.Default.Db).GetServer();
            Database = Server.GetDatabase(DatabaseName);
        }

        protected abstract T GetInitialObject();

        protected T SaveInitialObject(MongoCollection collection)
        {
            var initialObject = GetInitialObject();
            collection.Save(initialObject);

            return VerifyUpdate(collection, initialObject, 0);
        }

        protected T UpdateAndVerify(MongoCollection<T> collection, T person, int expectedVersion)
        {
            collection.Optimistic().Update(person);

            return VerifyUpdate(collection, person, expectedVersion);
        }

        protected T RemoveAndVerify(MongoCollection<T> collection, T person)
        {
            collection.Optimistic().Remove(person);

            return VerifyDelete(collection, person);
        }

        protected T UpdateAndVerify(MongoCollection collection, T person, int expectedVersion)
        {
            collection.Optimistic<T>().Update(person);

            return VerifyUpdate(collection, person, expectedVersion);
        }

        protected T RemoveAndVerify(MongoCollection collection, T person)
        {
            collection.Optimistic<T>().Remove(person);

            return VerifyDelete(collection, person);
        }

        private T VerifyUpdate(MongoCollection collection, T obj, int expectedVersion)
        {
            var createdObject = collection.FindOneAs<T>(obj.QueryById());

            obj.Version.Should().Be.EqualTo(expectedVersion);

            createdObject.Should().Not.Be.Null();
            createdObject.Equals(obj).Should().Be.True();
            return createdObject;
        }

        private T VerifyDelete(MongoCollection collection, T obj)
        {
            var deletedObject = collection.FindOneAs<T>(obj.QueryById());
            deletedObject.Should().Be.Null();
            return deletedObject;
        }

        public void Dispose()
        {
            try
            {
                if (!string.IsNullOrEmpty(DatabaseName) && Server.DatabaseExists(DatabaseName))
                {
                    Server.DropDatabase(DatabaseName);
                }
            }
            finally
            {
                Database = null;
                Server = null;
            }
        }
    }
}