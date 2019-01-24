using System.Collections.Generic;
using System.Linq;
using System;
using AI.Functions;

namespace AI {

    /// <summary>
    /// Holds all string functions mapped to their real functions
    /// </summary>
    public class FunctionDatabase {
        public delegate bool function(string[] parameters=null, bool fromDatabase=false);

        Dictionary<string, function> functionMap;
        public FunctionDatabase() {
            functionMap = new Dictionary<string, function>() {
                {"get_name", (string[] parameters, bool fromDatabase) => {Program.PAL.Respond("My name is " + Program.PAL.name);return true;}}
            };
            var functions = new List<Function>() {
                new ChangeName()
            };

            // maps functions execute to their names
            foreach(Function function in functions)
                functionMap.Add(function.name, function.Execute);
        }   


        /// <summary>
        /// Executes the specified function, and passes the parameters to it
        /// </summary>
        /// <param name="function">The function name</param>
        /// <param name="parameters">The parameters</param>
        public bool Execute(string function, string[] parameters=null, bool fromDatabase=false) {
            if (!functionMap.ContainsKey(function)) {
                Program.PAL.Respond("Sorry, but the following: '" + function + "' is not a valid function.");
                return false;
            } else {

                // replace paramters with keys if they exist
                parameters = parameters.Select(parameter => parameter = Program.Properties.ReplaceTokens(parameter)).ToArray();

                // execute function
                return functionMap[function](parameters, fromDatabase);
            }
        }
    }
}