using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Diagnostics;

namespace DBeind
{
    class EFHandler : DbContext
    {
        public EFHandler() : base()
        {
            Database.SetInitializer(
                new DropCreateDatabaseAlways<EFHandler>()
            );
        }
        public DbSet<User> users { get; set; }

        public int countRows()
        {
            int amount = 0;
            using (var connection = new EFHandler())
            {
                amount = connection.users.Count();
            }
            return amount;
        }

        public TimeSpan createQueries(int count)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            using (var connection = new EFHandler())
            {
                User user = new User() { email = "chiel1997@hotmail.com", created_at = DateTime.Now };
                for (int idx = 0; idx < count; idx++)
                {
                    connection.users.Add(user);
                       
                    connection.SaveChanges();
                    
                }
            }
            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        public TimeSpan selectQueries(int count)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            using (var connection = new EFHandler())
            {
                List<User> users = connection.users.Take(count).ToList();
            }
            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        public TimeSpan updateQueries(int count)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            using (var connection = new EFHandler())
            {
                List<User> users = connection.users.Take(count).ToList();
                foreach(User user in users)
                {
                    user.failedLoginAttempts = 1;
                }
                connection.SaveChanges();
            }
            stopWatch.Stop();
            return stopWatch.Elapsed;
        }

        public TimeSpan deleteQueries(int count)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            using (var connection = new EFHandler())
            {
                List<User> users = connection.users.Take(count).ToList();
                foreach (User user in users)
                {
                    connection.users.Remove(user);
                }
                connection.SaveChanges();
            }
            stopWatch.Stop();
            return stopWatch.Elapsed;
        }
    }
}
