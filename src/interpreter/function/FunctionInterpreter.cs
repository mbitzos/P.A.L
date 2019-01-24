using System.Text.RegularExpressions;
using System.Linq;
using System;

namespace AI {

    /// <summary>
    /// Handles interpretation of user called-functions
    /// </summary>
    public class FunctionInterpreter {
        
        public FunctionDatabase functionDatabase;

        public FunctionInterpreter() {
            functionDatabase = new FunctionDatabase();
            Console.WriteLine("Finished Setting up Functional Database.");
        }

        /// <summary>
        /// if the input is a function call
        /// </summary>
        /// <param name="input">The input</param>
        public bool IsFunction(string input) {
            return input.StartsWith(":");
        }

        /// <summary>
        /// Interprets the input for function syntax
        /// </summary>
        /// <param name="input"></param>
        public void Interpret(string input) {
            input = input.Substring(1);

            // get all parameters (regex makes sure quoted parameters arent split)
            string[] split = Regex.Split(input, "(?<=^[^\"]*(?:\"[^\"]*\"[^\"]*)*) (?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            // first one is function
            string function = split[0];

            // rest are parameters
            string[] parameters = split.Skip(1).ToArray();

            functionDatabase.Execute(function, parameters);
        }
    }
}