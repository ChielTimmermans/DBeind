using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;

namespace DBeind
{
    class NoSQLHandler
    {
        MongoClient client;
        IMongoDatabase db;
        IMongoCollection<MongoDB.User> collection;
        FilterDefinition<MongoDB.User> filter;
        SortDefinition<MongoDB.User> sort;
        int lastCount;

        public NoSQLHandler()
        {
            client = new MongoClient("mongodb://localhost");
            db = client.GetDatabase("Databases2");
            collection = db.GetCollection<MongoDB.User>("user");

            filter = Builders<MongoDB.User>.Filter.Empty;
            sort = Builders<MongoDB.User>.Sort.Descending("_id");

            emptyDB();
        }

        public void emptyDB()
        {
            db.DropCollection("user");
        }

        public int countRows()
        {
            int temp = lastCount;
            lastCount = Convert.ToInt32(collection.Count(filter));
            return Convert.ToInt32(collection.Count(filter)) - temp;
        }

        public TimeSpan createQueries(List<MongoDB.User> users)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            foreach(MongoDB.User user in users)
            {
                collection.InsertOne(user);
            }

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        public TimeSpan selectQueries(int count)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            List<MongoDB.User> users = collection.AsQueryable().Select(x => new MongoDB.User{
                email = x.email,
                password = x.password,
                created_at = x.created_at,
                emailsActivated = x.emailsActivated,
                failedLoginAttempts = x.failedLoginAttempts
            }).Take(count).ToList();
            

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        public TimeSpan updateQueries(int count)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int idx = 0; idx < count; idx++)
            {
                FilterDefinition<MongoDB.User> filter = Builders<MongoDB.User>.Filter.Where(x => x.password == "xxxxxxx");
                UpdateDefinition<MongoDB.User> update = Builders<MongoDB.User>.Update.Set(x => x.password, "xxxx");
                UpdateResult result = collection.UpdateOneAsync(filter, update).Result;
            }

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }
        
        public TimeSpan deleteQueries(int count)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int idx = 0; idx < count; idx++)
            {
                collection.FindOneAndDelete(
                    Builders<MongoDB.User>.Filter.Eq("password", "xxxx"),
                    new FindOneAndDeleteOptions<MongoDB.User>
                    {
                        Sort = Builders<MongoDB.User>.Sort.Descending("_id")
                    }
                );
            }

            stopWatch.Stop();
            return stopWatch.Elapsed;
        }
    }
}
