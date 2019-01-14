/*
    This software includes the VaderSharp NuGet Package: Copyright (c) 2017 Jordan Andrews
*/


using System;
using System.Text.RegularExpressions;
using System.IO;
using VaderSharp;

// TODO: move all words from txt file to in class data struct (i.e array of words)
namespace AI {

    /// <summary>
    /// Handles all user input parsing
    /// </summary>
    public class Interpreter {
        SpellChecker spellChecker;

        // const data structs
        static string[] pronouns = {"he", "she", "they", "his", "her", "they", "it", "that"};
        static char[] punctuations  = {',', '.', ';', '?', '!', '\'', '"', ')', '('};
        static string[] fillerWords = {"the", "a", "are", "is"};
        
        public Interpreter() {
            spellChecker = new SpellChecker();
            Console.WriteLine("Finished Interpreter Setup.");
        }

        /// <summary>
        /// Gets user input
        /// </summary>
        /// <param name="input">The input to parse</param>
        /// <param name="context">The context to use</param>
        /// <returns>Returns the parsed user input</returns>
        public string ParseInput(string input, string context) {
            
            Program.Logger.Buffer.Input = input;
            input = punctuationClean(input);

            input = spellChecker.SpellCheck(input);

            // analyze sentiment before cleaning
            sentimentAnalyze(input);            

            input = clean(input);
            input = parseContext(input, context);
            return input;   
        }

        /// <summary>
        /// Removes all punctuation
        /// </summary>
        /// <param name="input">The input to clean</param>
        /// <returns>The input with no punctuation</returns>
        public static string punctuationClean(string input) {
            
            // todo make this more expansive
            foreach(char punctuation in punctuations) {
                input = input.Replace(punctuation,' ');
            }
            return input;
        }

        /// <summary>
        /// Cleans the input for lowercase and filler words
        /// </summary>
        /// <param name="input">The input to clean</param>
        /// <returns>The new cleaned input</returns>
        public static string clean(string input) {
            
            // lowercase
            input = input.ToLower();

            // remove all filler words
            foreach(string fillerWord in fillerWords) {

                // replace whole word
                input = replaceWholeWord(input, fillerWord, " ");
            }

            // replace all whitespaces with single one to prevent extra padding
            input = Regex.Replace(input, @"\s+", " ");
            return input;
        }

        // analyzes the sentiment
        private void sentimentAnalyze(string input) {
            SentimentIntensityAnalyzer analyzer = new SentimentIntensityAnalyzer();

            var results = analyzer.PolarityScores(input);

            Program.Logger.Buffer.Sentiment = results.Compound;
        }

        // replace context with words
        private string parseContext(string input, string context) {

            // if no context return
            if (context == null || context.Equals(""))
                return input;

            // replace all
            foreach(string pronoun in pronouns) {
                input = replaceWholeWord(input, pronoun, context);
            }
            return input;
        }

        /// <summary>
        /// replace whole word
        /// </summary>
        /// <param name="input">The string to perform the replace on</param>
        /// <param name="word">The word to replace</param>
        /// <param name="replace">The word to switch in</param>
        /// <returns>The new string with the word replaced with replace</returns>
        public static string replaceWholeWord(string input,string word, string replace) {
            string pattern = @"\b" + word + @"\b";
            return Regex.Replace(input, pattern, replace);
        }

    }

}