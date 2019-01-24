using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AI {

    /// <summary>
    /// Handles all the key properties of PAL and the interactable environment
    /// Think of these as system environment variables
    /// </summary>
    /// <typeparam name="string"></typeparam>
    /// <typeparam name="string"></typeparam>
    public class Properties: Dictionary<string,string>{

        
        /// <summary>
        /// Replaces all key tokens with the correct value of the key
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string ReplaceTokens(string input) {

            // match all tokens (ex. $token$)
            string regex = @"\$(\S*)\$";
            var matches = Regex.Matches(input, regex);

            // find all words that match key tokens and replaces it with its real value
            foreach(var match in matches) {
                string matchString = match.ToString();

                // remove starting and ending '$'   
                string matchKey = matchString.Substring(1, matchString.Length - 2);
                if (this.ContainsKey(matchKey)) {
                    input = input.Replace(matchString, this[matchKey]);
                } else {

                    // if not found stop, this is a breaking operation
                    throw new KeyNotFoundException(matchKey + " does not exist as a property key.");
                }
            }
            return input;
        }
    }   

}