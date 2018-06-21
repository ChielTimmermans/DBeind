using System;
using System.IO;

namespace DBeind
{
    class Program
    {
        static void Main(string[] args)
        {

            TextReader tIn = Console.In;
            String command = "";
            while (command != "exit")
            {
                Console.WriteLine("This is the benchmarkin tool to test. ADO.net, Entity Framework and NoSQl's crud operations time");
                Console.WriteLine("Testing every CRUD operations: 1, 1000, 10000, 1000000");
                Console.WriteLine("Typ the number which one you want to test");
                Console.WriteLine(".1 ADO.net");
                Console.WriteLine(".2 Entity Framework");
                Console.WriteLine(".3 NoSQl");

                command = tIn.ReadLine();

                if (command.Equals("1"))
                {
                    startADO();
                }
                else if (command.Equals("2"))
                {
                    startEF();
                }
                else if (command.Equals("3"))
                {
                    startNoSQL();
                }
                Console.WriteLine("Done benchmarking");
            }



        }

        static public void startADO()
        {
            ADO ado = new ADO();
        }

        static public void startEF()
        {
            EF ef = new EF();
        }

        static public void startNoSQL()
        {
            NoSQL noSQL = new NoSQL();
        }
    }
}
