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
        Random rnd = new Random();
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

        public TimeSpan createQueries(int count)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            List<MongoDB.User> users = new List<MongoDB.User>();
            List<MongoDB.Billing> billings = new List<MongoDB.Billing>();
            List<MongoDB.PasswordForgetToken> passwordForgetTokens = new List<MongoDB.PasswordForgetToken>();
            List<MongoDB.Userprofile> userprofiles = new List<MongoDB.Userprofile>();

            for (int idx = 0; idx < rnd.Next(1, 3); idx++)
            {
                int[] price = new int[] {799, 1099, 1399 };
                MongoDB.Billing billing = new MongoDB.Billing()
                {
                    price = price[rnd.Next(1, 3)],
                    TIMESTAMP = DateTime.UtcNow,
                    paid = (rnd.Next(0,1)  == 1) ? DateTime.Now : DateTime.MinValue
                };
                billings.Add(billing);
            }

            for (int idx = 0; idx < rnd.Next(0, 2); idx++)
            {
                MongoDB.PasswordForgetToken passwordForgetToken = new MongoDB.PasswordForgetToken()
                {
                    email = "user@hotmail.com",
                    hash = RandomString(),
                    created_at = DateTime.Now,
                };
                passwordForgetTokens.Add(passwordForgetToken);
            }

            for (int idx = 0; idx < rnd.Next(1, 4); idx++)
            {
                MongoDB.Userprofile userprofile = new MongoDB.Userprofile()
                {
                    name = "user",
                    avatarUrl = "randomurl",
                    dob = DateTime.Now,
                    subtitlesOn = (rnd.Next(0, 1) == 1) ? true : false,
                    created_at = DateTime.Now,
                };
                userprofiles.Add(userprofile);
            }

            for (int idx = 0; idx < count; idx++)
            {
                MongoDB.User user = new MongoDB.User
                {
                    email = "user@hotmail.com",
                    password = "xxxxxxx",
                    created_at = DateTime.UtcNow,
                    emailsActivated = 0,
                    failedLoginAttempts = 0,
                    billings = billings,
                    passwordForgetTokens = passwordForgetTokens,
                    userprofiles = userprofiles,
                };
                users.Add(user);
            }

            collection.InsertMany(users);

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
            
            FilterDefinition<MongoDB.User> filter = Builders<MongoDB.User>.Filter.Empty;
            UpdateDefinition<MongoDB.User> update = Builders<MongoDB.User>.Update.Set(a => a.password, "yyyyyyyy");
            collection.UpdateManyAsync(filter, update);
            
            stopWatch.Stop();
            return stopWatch.Elapsed;
        }
        
        public TimeSpan deleteQueries(int count)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            collection.DeleteMany(Builders<MongoDB.User>.Filter.Empty);
            
            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        public string RandomString()
        {
            int length = 20;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
