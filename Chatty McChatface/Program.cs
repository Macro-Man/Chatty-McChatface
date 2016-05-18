using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatty_McChatface
{
    class Program
    {
        public const string Bot_Name = "Chatty";
        public static Random randomGenerator = new Random(0);
        public static Dictionary<uint, string> DevUsers = new Dictionary<uint, string>();
        public static Dictionary<uint, string> Admins = new Dictionary<uint, string>();
        public static Dictionary<uint, string> ChatUsers = new Dictionary<uint, string>();
        public static Dictionary<string, List<string>> Responses = new Dictionary<string, List<string>>();

        static void Main(string[] args)
        {

            RunStartupScript();

            await_read:
            string command = Console.ReadLine();

            if(command.ToUpper() == "EXIT")
            {
                Console.WriteLine("Are you sure you want me to quit? (Y/N)");
                string final_check = Console.ReadLine();
                if(final_check.Substring(0, 1).ToUpper() == "Y")
                {
                    return;
                }
                else
                {
                    goto await_read;
                }
            }
            else
            {
                ProcessCommand(command);
                goto await_read;
            }
        }

        public static void RunStartupScript()
        {
            Console.Write("Initializing");
            for (int i = 12; i < 15; i++)
            {
                System.Threading.Thread.Sleep(600);
                Console.Write(".");
            }

            InitializeVariables();

            Console.WriteLine("\nDev users added:");
            foreach(string user in DevUsers.Values)
            {
                Console.WriteLine("--| " + user);
            }

            Console.WriteLine("\nAdmin users added:");
            foreach(string user in Admins.Values)
            {
                Console.WriteLine("--| " + user);
            }
        }
        private static void InitializeVariables()
        {
            // Add dev users
            DevUsers.Add(4240221, "Macro Man");

            // Add admin users
            Admins.Add(4240221, "Macro Man");

            // Populate responses
            Responses.Add("Greetings",
                            new List<string>
                            {
                                "Hi!", "Hello!", "Hi there!", "Hey!", "Hey there!",
                                "Fancy seeing you here!", "Howdy!", "HEEEEeeeEEeeeyaaa!"
                            }
                        );


        }

        public static int RandBetween(int lower_bound, int upper_Bound)
        {
            return randomGenerator.Next(lower_bound, upper_Bound);
        }

        public static void ProcessCommand(string command)
        {
            Regex cmdHello = new Regex("^(hi(ya)?|hey(a)?|hello|howdy|yo|oi)( " + Bot_Name + ")?[!?]*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            
            if(cmdHello.IsMatch(command))
            {
                List<string> text_out = Responses["Greetings"];
                int max = text_out.Count - 1;

                Console.WriteLine(text_out.ToArray()[RandBetween(0, max)]);
            }
            else
            {
                Console.WriteLine("Generic command processed");
            }
        }

            
        
    }
}
