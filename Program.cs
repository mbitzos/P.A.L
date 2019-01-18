/*
    The software includes the Newtonsoft.Json NuGet Package: Copyright (c) 2007 James Newton-King
 */

using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace AI
{
    class Program
    {   

        static string context="";

        public static Config Config;
        public static LogConfig LogConfig;
        public static Logger Logger;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            initConfigs();

            var db = new Database();
            var interpreter = new Interpreter();
            var stopWatch = new Stopwatch();
            var speaker = new Speaker();

            Logger = new Logger();

            Console.WriteLine("***************************");
            Console.WriteLine("           P.A.L");
            Console.WriteLine("Personal Assistance Lexicon");
            Console.WriteLine("***************************");

            // continually get input
            try {
                while (true) {
                    Console.Write(">");
                    string input = Console.ReadLine();
                    if (input == "quit"){break;}
                    stopWatch.Start();

                    string interpretation = interpreter.ParseInput(input, context);

                    var result = db.GetResponse(interpretation);
                    speaker.Respond(result.Item1);
                    context = result.Item2;

                    stopWatch.Stop();

                    // log elasped time
                    Logger.Buffer.Time = stopWatch.ElapsedMilliseconds;
                    stopWatch.Reset();

                    // Log everything
                    Logger.LogBuffer();
                }
            } catch (Exception e) {
                Logger.LogError(e);
            }

        }

        // inits config settings
        static void initConfigs() {
            const string configFilePath = "./config/config.json";
            const string logConfigFilePath = "./config/log.config.json";

            // Set config settings
            string content = System.IO.File.ReadAllText(configFilePath);

            Config = JsonConvert.DeserializeObject<Config>(content);

            // Set log config settings
            content = System.IO.File.ReadAllText(logConfigFilePath);

            LogConfig = JsonConvert.DeserializeObject<LogConfig>(content);
            Console.WriteLine("Finished Setup Configuration.");
        }
    }

}
