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

        public static PAL PAL;
        
        public static Properties Properties = new Properties() {{
            "name", "P.A.L"
        }};

        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            initConfigs();

            var db = new Database();
            var interpreter = new Interpreter();
            var stopWatch = new Stopwatch();
            var functionInterpreter = new FunctionInterpreter();

            Logger = new Logger();

            PAL = new PAL();
            

            DatabaseInputEntry[] inputEntryTree= null;

            Console.WriteLine("***************************");
            Console.WriteLine("           P.A.L");
            Console.WriteLine("Personal Assistance Lexicon");
            Console.WriteLine("***************************");

            // continually get input
            try {
                while (true) {
                    Console.Write(">");
                    string input = Console.ReadLine();

                    // set input var
                    Properties["input"] = input;

                    if (input == "quit"){break;}

                    stopWatch.Start();

                    // if we are asking for user input specifically
                    if (inputEntryTree != null) {
                        var inputEntry = db.GetInputResponse(inputEntryTree, input);
                        string response = "";

                        // if there is a function, execute it (more likely case)
                        if (inputEntry.function != null) {

                            // check for success/failure of function executed
                            bool success = functionInterpreter.functionDatabase.Execute(inputEntry.function, inputEntry.parameters, true);
                            if (success)
                                response = inputEntry.success;
                            else { 
                                
                                // if failure response isnt specified, just say success anyways
                                response = (inputEntry.failure != null) ? inputEntry.failure : inputEntry.success;
                            }
                        } else {

                            // if no function, just respond with a successful input if it exists
                            response = (inputEntry.success != null) ? inputEntry.success : "";
                        }

                        // respond if there is one
                        PAL.Respond(response);

                        // check for more inputs
                        inputEntryTree = inputEntry.input;
                    } else {

                        if (functionInterpreter.IsFunction(input)) {

                            // if function is calling function
                            functionInterpreter.Interpret(input);

                            
                        } else {

                            // regular input
                            string interpretation = interpreter.ParseInput(input, context);

                            // get best response from database
                            var result = db.GetResponse(interpretation);
                            
                            // response will be set to null if not found
                            string response = (result != null) ? result.GetResponse : null;
                            PAL.Respond(response);

                            // increase visits
                            result.visits++;

                            context = (result != null) ? result.context : "";

                            // if has a function, perform it with its parameters
                            if (result != null)
                                if (result.function != null) {
                                    bool success = functionInterpreter.functionDatabase.Execute(result.function, result.parameters, true);
                                }

                            // if this result is asking for input, check for it next
                            inputEntryTree = (result != null) ? result.input: null;
                        }
                    }
                    stopWatch.Stop();

                    // log elasped time
                    Logger.Buffer.Time = stopWatch.ElapsedMilliseconds;
                    stopWatch.Reset();

                    // Log everything
                    Logger.LogBuffer();
                }
            } catch (Exception e) {
                Console.WriteLine("Something seems to have gone wrong, please see logs.");
                Console.WriteLine("Terminating...");
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
