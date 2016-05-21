using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Chatty_McChatface
{
    class Program
    {
        public const string Bot_Name = "Chatty";
        //public static Random randomGenerator = new Random(0);
        public static Dictionary<uint, string> DevUsers = new Dictionary<uint, string>();
        public static Dictionary<uint, string> Admins = new Dictionary<uint, string>();
        public static Dictionary<uint, string> ChatUsers = new Dictionary<uint, string>();
        public static Dictionary<string, List<string>> Responses = new Dictionary<string, List<string>>();

        // DEBUG
        public static List<string> test;
        // DEBUG

        static void Main(string[] args)
        {
            Console.Title = @"Chatty McChatface";

            RunStartupScript();

            // DEBUG
            test = Responses["Greetings"];
            Console.WriteLine(test.Count);
            // DEBUG

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
                System.Threading.Thread.Sleep(500);
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
            Random randomGenerator = new Random();
            return randomGenerator.Next(lower_bound, upper_Bound + 1);
        }

        public static void ProcessCommand(string command)
        {
            Regex cmdHello = new Regex("^(hi(ya)?|hey(a)?|hello|howdy|yo|oi)( " + Bot_Name + ")?[!?]*$", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex cmdSO_Query = new Regex(@"^[!]{2}[/](info)(.*)$", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            if(cmdHello.IsMatch(command))
            {
                List<string> text_out = Responses["Greetings"];
                int max = text_out.Count - 1;

                Console.WriteLine(text_out.ToArray()[RandBetween(0, max)]);
            }
            else if (cmdSO_Query.IsMatch(command))
            {
                string queryCmd = command.Substring(3);
                switch (queryCmd)
                {
                    case "info":
                        string val = QueryStackAPI("info?site=stackoverflow");

                        Console.WriteLine(val);
                        break;

                    default:
                        break;
                }

            }
            else
            {
                Console.WriteLine("Generic command processed");
            }
        }

        public static string QueryStackAPI(string command)
        {
            System.Net.HttpWebRequest request;

            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(@"https://api.stackexchange.com/2.2/" + command);
            request.AutomaticDecompression = System.Net.DecompressionMethods.GZip;

            string json;

            using (System.Net.WebResponse response = request.GetResponse())
            {
                using (System.IO.Stream stream = response.GetResponseStream())
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(stream))
                    {
                        json = reader.ReadToEnd();
                    }
                }
            }

            return json;


        }
        //public static async Task<string> QueryStackAPI(string command)
        //{

        //    HttpClient httpClient = new HttpClient();
        //    httpClient.DefaultRequestHeaders.Add("Accept-Enconding", "gzip");

        //    HttpResponseMessage response = await httpClient.GetAsync("https://api.stackexchange.com/2.2/" + command, HttpCompletionOption.ResponseContentRead);
        //    byte[] compressedResponse = await response.Content.ReadAsByteArrayAsync();

        //    int len = BitConverter.ToInt32(compressedResponse, 0);

        //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(compressedResponse, 0, compressedResponse.Length))
        //    {
        //        byte[] decompressedResponse = new byte[len];
        //        using (System.IO.Compression.GZipStream gz = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress))
        //        {
        //            gz.Read(decompressedResponse, 0, decompressedResponse.Length);

        //        }

        //        return Encoding.UTF8.GetString(decompressedResponse);
        //    }
            

        //}

    }
}
