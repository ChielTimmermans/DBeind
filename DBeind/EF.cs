using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DBeind
{
    class EF 
    {
        EFHandler efHandler;

        public EF()
        {
            startingComments();
            efHandler = new EFHandler();

            inserting();
            selecting();
            updating();
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
            Console.Write("\tAverage time " + count + " ROW:");
            List<TimeSpan> sourceList = new List<TimeSpan>();
            for (int idx = 0; idx < 10; idx++)
            {
                sourceList.Add(efHandler.createQueries(count));
                int countedRows = efHandler.countRows();
                Console.SetCursorPosition(0, Console.CursorTop);
                TimeSpan ts = getAverage(sourceList);
                string avg = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.Write("\tAverage time " + count + " ROW:\t {0} ms \t\t Rows inserted: {1} \t\t DONE: {2}/10", avg, countedRows, sourceList.Count());
            }
            Console.WriteLine("\n");
        }

        public void selecting()
        {
            Console.WriteLine("Selecting:");
            selectingRows(1);
            selectingRows(1000);
            selectingRows(100000);
            selectingRows(1000000);
            Console.WriteLine("\n-----------------------------------------------------------------------");
        }

        public void selectingRows(int count)
        {
            Console.Write("\tAverage time " + count + " ROW:");
            List<TimeSpan> sourceList = new List<TimeSpan>();
            for (int idx = 0; idx < 10; idx++)
            {
                sourceList.Add(efHandler.selectQueries(count));
                Console.SetCursorPosition(0, Console.CursorTop);
                TimeSpan ts = getAverage(sourceList);
                string avg = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.Write("\tAverage time " + count + " ROW:\t {0} ms \t\t DONE: {1}/10", avg, sourceList.Count());
            }
            Console.WriteLine("\n");
        }

        public void updating()
        {
            Console.WriteLine("Updating:");
            updatingRows(1);
            updatingRows(1000);
            updatingRows(100000);
            updatingRows(1000000);
            Console.WriteLine("\n-----------------------------------------------------------------------");
        }

        public void updatingRows(int count)
        {
            Console.Write("\tAverage time " + count + " ROW:");
            List<TimeSpan> sourceList = new List<TimeSpan>();
            for (int idx = 0; idx < 10; idx++)
            {
                sourceList.Add(efHandler.updateQueries(count));
                Console.SetCursorPosition(0, Console.CursorTop);
                TimeSpan ts = getAverage(sourceList);
                string avg = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.Write("\tAverage time " + count + " ROW:\t {0} ms \t\t DONE: {1}/10", avg, sourceList.Count());
            }
            Console.WriteLine("\n");
        }

        public void deleting()
        {
            Console.WriteLine("Deleting:");
            deletingRows(1);
            deletingRows(1000);
            deletingRows(100000);
            deletingRows(1000000);
            Console.WriteLine("\n-----------------------------------------------------------------------");
        }

        public void deletingRows(int count)
        {
            Console.Write("\tAverage time " + count + " ROW:");
            List<TimeSpan> sourceList = new List<TimeSpan>();
            for (int idx = 0; idx < 10; idx++)
            {
                sourceList.Add(efHandler.deleteQueries(count));
                Console.SetCursorPosition(0, Console.CursorTop);
                TimeSpan ts = getAverage(sourceList);
                string avg = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.Write("\tAverage time " + count + " ROW:\t {0} ms \t\t DONE: {1}/10", avg, sourceList.Count());
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
            Console.WriteLine("Entity Framework om 1, 1000, 100000 en 1000000 crud operations te doen op de tabel users");
            Console.WriteLine("Elke operatie word 10 x uitgevoerd om zo een gemiddeld te krijgen\n");
            Console.WriteLine("");
            Console.WriteLine("Starting Entity Framework Benchmarking");
            Console.WriteLine("");
            Console.WriteLine("-----------------------------------------------------------------------");
        }
    }
}
