using Mongo2Go;
using MongoDB.Driver;

namespace ProductCatalog.Tests.Integration
{
    public class MongoDbFixture : IDisposable
    {
        private readonly MongoDbRunner _runner;

        public MongoDbFixture()
        {
            _runner = MongoDbRunner.Start();
            Client = new MongoClient(_runner.ConnectionString);
            Database = Client.GetDatabase("TestDatabase");
        }

        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }

        public void Dispose()
        {
            _runner.Dispose();
        }
    }

}
