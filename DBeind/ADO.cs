using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DBeind
{
    class ADO
    {
        public DatabaseHandler dbhandler;
        public ADO()
        {
            dbhandler = new DatabaseHandler("DESKTOP-OR069KP\\SQLEXPRESS", "DB", "root", "");
            startingComments();
            //startBenchmark();

            dbhandler.deleteAll();
            inserting();
            selecting();
            updating();
            deleting();
        }

        public void startingComments()
        {
            Console.WriteLine("ADO.net programma om 1, 1000, 100000 en 1000000 crud operations te doen op de tabel users");
            Console.WriteLine("Elke operatie word 10 x uitgevoerd om zo een gemiddeld te krijgen\n");
            Console.WriteLine("Database version: \n");
            dbhandler.getVersion();
            Console.WriteLine("");
            Console.WriteLine("Starting ADO.net Benchmarking");
            Console.WriteLine("");
            Console.WriteLine("-----------------------------------------------------------------------");
        }

        public void inserting()
        {
            Console.WriteLine("INSERTING:");
            insertingRows(1, 1);
            insertingRows(1000, 2);
            insertingRows(100000, 3);
            insertingRows(1000000, 4);
            Console.WriteLine("\n-----------------------------------------------------------------------");
        }

        public void insertingRows(int count, int id)
        {
            Console.Write("\tAverage time " + count + " ROW:");
            List<TimeSpan> sourceList = new List<TimeSpan>();
            for (int idx = 0; idx < 10; idx++)
            {
                sourceList.Add(dbhandler.createQueries(count, id));
                int countedRows = dbhandler.countRows();
                dbhandler.deleteQueries(count, id);
                Console.SetCursorPosition(0, Console.CursorTop);
                TimeSpan ts = getAverage(sourceList);
                string avg = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.Write("\tAverage time " + count + " ROW:\t {0} ms \t\t Rows inserted: {1} \t\t DONE: {2}/10", avg, countedRows, sourceList.Count());
            }
            Console.WriteLine("\n");
        }

        public void selecting()
        {
            Console.WriteLine("\nINSERTING TEST DATA");
            dbhandler.createQueries(1, 1);
            dbhandler.createQueries(1000, 2);
            dbhandler.createQueries(100000, 3);
            dbhandler.createQueries(1000000, 4);
            Console.WriteLine("INSERTING DONE");

            Console.WriteLine("SELECTING:");
            selectingRows(1, 1);
            selectingRows(1000, 2);
            selectingRows(100000, 3);
            selectingRows(1000000, 4);
            Console.WriteLine("\n-----------------------------------------------------------------------");
        }

        public void selectingRows(int count, int id)
        {

            Console.Write("\tAverage time " + count + " ROW");
            List<TimeSpan> sourceList = new List<TimeSpan>();
            for (int idx = 0; idx < 10; idx++)
            {
                sourceList.Add(dbhandler.selectQueries(count));
                Console.SetCursorPosition(0, Console.CursorTop);
                TimeSpan ts = getAverage(sourceList);
                string avg = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.Write("\tAverage time " + count + " ROW:\t {0} ms\t\t DONE: {1}/10", avg, sourceList.Count());
            }
            Console.WriteLine("\n");
        }

        public void updating()
        {

            Console.WriteLine("Updating:");
            updatingRows(1, 1);
            updatingRows(1000, 2);
            updatingRows(100000, 3);
            updatingRows(1000000, 4);
            Console.WriteLine("\n-----------------------------------------------------------------------");
        }

        public void updatingRows(int count, int id)
        {
            Console.Write("\tAverage time " + count + " ROW");
            List<TimeSpan> sourceList = new List<TimeSpan>();
            for (int idx = 0; idx < 10; idx++)
            {
                sourceList.Add(dbhandler.updateQueries(count, id));
                Console.SetCursorPosition(0, Console.CursorTop);
                TimeSpan ts = getAverage(sourceList);
                string avg = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.Write("\tAverage time " + count + " ROW:\t {0} ms\t\t DONE: {1}/10", avg, sourceList.Count());
            }
            Console.WriteLine("\n");
        }

        public void deleting()
        {
            Console.WriteLine("Updating:");
            deletingRows(1, 1);
            deletingRows(1000, 2);
            deletingRows(100000, 3);
            deletingRows(1000000, 4);
            Console.WriteLine("\n-----------------------------------------------------------------------");
        }

        public void deletingRows(int count, int id)
        {
            Console.Write("\tAverage time " + count + " ROW");
            List<TimeSpan> sourceList = new List<TimeSpan>();
            for (int idx = 0; idx < 10; idx++)
            {
                sourceList.Add(dbhandler.deleteQueries(count, id));
                if (idx < 9)
                {
                    dbhandler.createQueries(count, id);
                }
                Console.SetCursorPosition(0, Console.CursorTop);
                TimeSpan ts = getAverage(sourceList);
                string avg = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.Write("\tAverage time " + count + " ROW:\t {0} ms\t\t DONE: {1}/10", avg, sourceList.Count());
            }
            Console.WriteLine("\n");
        }

        public TimeSpan getAverage(List<TimeSpan> sourceList)
        {
            double doubleAverageTicks = sourceList.Average(timeSpan => timeSpan.Ticks);
            long longAverageTicks = Convert.ToInt64(doubleAverageTicks);

            return new TimeSpan(longAverageTicks);
        }

        public void startBenchmark()
        {

        }
    }
}
