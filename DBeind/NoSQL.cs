using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBeind
{
    class NoSQL
    {
        NoSQLHandler NoSQLHandler;

        Random rnd = new Random();
        List<MongoDB.User> users1; 
        List<MongoDB.User> users2; 
        List<MongoDB.User> users3; 
        List<MongoDB.User> users4;
        public NoSQL()
        {
            startingComments();
            NoSQLHandler = new NoSQLHandler();
            initialize();
            inserting();
            selecting();
            updating();
            deleting();
        }
        public void initialize()
        {
            users1 = setUser(1);
            users2 = setUser(1000);
            users3 = setUser(100000);
            users4 = setUser(1000000);
        }

        public List<MongoDB.User> setUser(int count)
        {
            List<MongoDB.User> users = new List<MongoDB.User>();
            for(int idx2 = 0; idx2 < count; idx2++)
            {
                List<MongoDB.Billing> billings = new List<MongoDB.Billing>();
                List<MongoDB.PasswordForgetToken> passwordForgetTokens = new List<MongoDB.PasswordForgetToken>();
                List<MongoDB.Userprofile> userprofiles = new List<MongoDB.Userprofile>();

                for (int idx = 0; idx < rnd.Next(1, 3); idx++)
                {
                    int[] price = new int[] { 799, 1099, 1399 };
                    MongoDB.Billing billing = new MongoDB.Billing()
                    {
                        price = price[rnd.Next(1, 3)],
                        TIMESTAMP = DateTime.UtcNow,
                        paid = (rnd.Next(0, 1) == 1) ? DateTime.Now : DateTime.MinValue
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

                users.Add(new MongoDB.User {
                    email = "user@hotmail.com",
                    password = "xxxxxxx",
                    created_at = DateTime.UtcNow,
                    emailsActivated = 0,
                    failedLoginAttempts = 0,
                    billings = billings,
                    passwordForgetTokens = passwordForgetTokens,
                    userprofiles = userprofiles,
                });
            
            }
            return users;
        }

        public void inserting()
        {
            Console.WriteLine("INSERTING:");
            insertingRows(users1, 1);
            insertingRows(users2, 1000);
            insertingRows(users3, 100000);
            insertingRows(users4, 1000000);
            Console.WriteLine("\n-----------------------------------------------------------------------");
        }

        public void insertingRows(List<MongoDB.User> users, int count)
        {
            Console.Write(String.Format("\tAverage time {0,15} ROW:", count));
            List <TimeSpan> sourceList = new List<TimeSpan>();
            for (int idx = 0; idx < 10; idx++)
            {
                sourceList.Add(NoSQLHandler.createQueries(users));
                int countedRows = NoSQLHandler.countRows();
                Console.SetCursorPosition(0, Console.CursorTop);
                TimeSpan ts = getAverage(sourceList);
                string avg = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.Write(String.Format("\tAverage time {0,15} ROW: {1} ms {2,10} Rows inserted: {3,10}  {4,10} DONE: {5}/10", count, avg, "", countedRows, "", sourceList.Count()));
            }
            Console.WriteLine("\n");
        }

        public void selecting()
        {
            Console.WriteLine("SELECTING:");
            selectingRows(1);
            selectingRows(1000);
            selectingRows(100000);
            selectingRows(1000000);
            Console.WriteLine("\n-----------------------------------------------------------------------");
        }

        public void selectingRows(int count)
        {
            Console.Write(String.Format("\tAverage time {0,15} ROW:", count));
            List<TimeSpan> sourceList = new List<TimeSpan>();
            for (int idx = 0; idx < 10; idx++)
            {
                sourceList.Add(NoSQLHandler.selectQueries(count));
                Console.SetCursorPosition(0, Console.CursorTop);
                TimeSpan ts = getAverage(sourceList);
                string avg = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.Write(String.Format("\tAverage time {0,15} ROW: {1} ms {2,10} DONE: {3}/10", count, avg, "", sourceList.Count()));

            }
            Console.WriteLine("\n");
        }

        public void updating()
        {
            Console.WriteLine("UPDATING:");
            updatingRows(1);
            updatingRows(1000);
            updatingRows(100000);
            updatingRows(1000000);
            Console.WriteLine("\n-----------------------------------------------------------------------");
        }

        public void updatingRows(int count)
        {
            Console.Write(String.Format("\tAverage time {0,15} ROW:", count));
            List<TimeSpan> sourceList = new List<TimeSpan>();
            for (int idx = 0; idx < 10; idx++)
            {
                sourceList.Add(NoSQLHandler.updateQueries(count));
                Console.SetCursorPosition(0, Console.CursorTop);
                TimeSpan ts = getAverage(sourceList);
                string avg = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.Write(String.Format("\tAverage time {0,15} ROW: {1} ms {2,10} DONE: {3}/10", count, avg, "", sourceList.Count()));
            }
            Console.WriteLine("\n");
        }

        public void deleting()
        {
            Console.WriteLine("UPDATING:");
            deletingRows(1);
            deletingRows(1000);
            deletingRows(100000);
            deletingRows(1000000);
            Console.WriteLine("\n-----------------------------------------------------------------------");
        }

        public void deletingRows(int count)
        {
            Console.Write(String.Format("\tAverage time {0,15} ROW:", count));
            List<TimeSpan> sourceList = new List<TimeSpan>();
            for (int idx = 0; idx < 10; idx++)
            {
                sourceList.Add(NoSQLHandler.deleteQueries(count));
                Console.SetCursorPosition(0, Console.CursorTop);
                TimeSpan ts = getAverage(sourceList);
                string avg = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.Write(String.Format("\tAverage time {0,15} ROW: {1} ms {2,10} DONE: {3}/10", count, avg, "", sourceList.Count()));
            }
            Console.WriteLine("\n");
        }

        public TimeSpan getAverage(List<TimeSpan> sourceList)
        {
            double doubleAverageTicks = sourceList.Average(timeSpan => timeSpan.Ticks);
            long longAverageTicks = Convert.ToInt64(doubleAverageTicks);

            return new TimeSpan(longAverageTicks);
        }

        public void startingComments()
        {
            Console.WriteLine("MongoDB om 1, 1000, 100000 en 1000000 crud operations te doen op de tabel users");
            Console.WriteLine("Elke operatie word 10 x uitgevoerd om zo een gemiddeld te krijgen\n");
            Console.WriteLine("");
            Console.WriteLine("Starting MongoDB Benchmarking");
            Console.WriteLine("");
            Console.WriteLine("-----------------------------------------------------------------------");
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
