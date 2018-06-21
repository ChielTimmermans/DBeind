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

        public NoSQL()
        {
            startingComments();
            NoSQLHandler = new NoSQLHandler();
            inserting();
            selecting();
            updating();
            deleting();
        }

        public void inserting()
        {
            Console.WriteLine("INSERTING:");
            insertingRows(1);
            insertingRows(1000);
            insertingRows(100000);
            insertingRows(1000000);
            Console.WriteLine("\n-----------------------------------------------------------------------");
        }

        public void insertingRows(int count)
        {
            Console.Write(String.Format("\tAverage time {0,15} ROW:", count));
            List <TimeSpan> sourceList = new List<TimeSpan>();
            for (int idx = 0; idx < 10; idx++)
            {
                sourceList.Add(NoSQLHandler.createQueries(count));
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
                NoSQLHandler.emptyDB();
                NoSQLHandler.createQueries(count);
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
                NoSQLHandler.emptyDB();
                NoSQLHandler.createQueries(count);
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
    }


}
