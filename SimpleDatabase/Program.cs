using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDatabase
{
    public class Program
    {
        public interface IOutputWriter
        {
            void WriteLine(string s);
        }
        public class ConsoleOutputWriter : IOutputWriter
        {
            public void WriteLine(string s)
            {
                Console.WriteLine(s);
            }
        }


        static void Main(string[] args)
        {
            //if (args.Length == 1 && args[0] == "/?")
            //{
            //    Console.WriteLine("Connects to App Server using the Username and Password stored in the config file and runs provided test cases against Baseline test cases, storing results in provided output directory\n\n");
            //    Console.WriteLine("apitest.exe [TEST CASE FILE PATH] [OUTPUT DIRECTORY]");
            //    return 1;
            //}

            //else if (args.Length != 2)
            //{
            //    Console.WriteLine("Invalid number of command line arguments. Application syntax:\n\tapitest.exe [TEST CASE FILE PATH] [OUTPUT DIRECTORY]");
            //    return 1;
            //}

            //else
            Menu();

        }


        static void Menu()
        {
            string command = "";
            ConsoleOutputWriter writer = new ConsoleOutputWriter();
            Database db = new Database(writer);
            while (command != "end")
            {
                string[] line = Console.ReadLine().ToString().Split(' ');
                command = line[0].ToLower();
                try
                {
                    switch (command)
                    {
                        case "end":
                            break;
                        case "set":
                            db.Set(line[1], Int32.Parse(line[2]));
                            break;
                        case "get":
                            db.Get(line[1]);
                            break;
                        case "unset":
                            db.Unset(line[1]);
                            break;
                        case "numequalto":
                            db.NumEqualTo(Int32.Parse(line[1]));
                            break;
                        case "begin":
                            db.Begin();
                            break;
                        case "rollback":
                            db.Rollback();
                            break;
                        case "commit":
                            db.Commit();
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid command, please try again.");
                }
            }
        }
    }
}
