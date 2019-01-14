/*
    The software includes the Newtonsoft.Json NuGet Package: Copyright (c) 2007 James Newton-King
 */


using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using F23.StringSimilarity;

namespace AI {
    
    /// <summary>
    /// Initializes database of responses
    /// Reads from file and creates data structure
    /// that is able to be interfaced with
    /// </summary>
    public class Database {

        WordNetWrapper wordNetWrapper;
        DatabaseEntries entries;
        Cosine cosine;
        const int CharSequence = 2;

        public Database() {
            wordNetWrapper = new WordNetWrapper();
            cosine = new Cosine(CharSequence);

            string filePath = "./database.json";
            string content = System.IO.File.ReadAllText(filePath);

            entries = JsonConvert.DeserializeObject<DatabaseEntries>(content);
            Console.WriteLine("Finished Database Setup.");
        }


        /// <summary>
        /// Gets the response from the database based on an input
        /// </summary>
        /// <param name="input">The request</param>
        /// <returns>The best response from the database or a default unknown answer (ex.static "Sorry, I dont quite understand")</returns>
        public Tuple<string,string> GetResponse(string input) {
            var bestResultTuple = getBestEntry(input);
            DatabaseEntry bestResult = bestResultTuple.Item1;
            double bestCosineSimilarity = bestResultTuple.Item2;
            if (bestCosineSimilarity < Program.Config.MaxSimilarityThreshold && bestCosineSimilarity > Program.Config.MinSimilarityThreshold )
                bestResult = trySynonyms(input, bestResult, bestCosineSimilarity);

            string response = (bestResult != null) ? bestResult.response : "Sorry, I dont quite understand.";
            string context = (bestResult != null) ? bestResult.context : "";

            // logging
            Program.Logger.Buffer.Similarity = bestCosineSimilarity;
            Program.Logger.Buffer.DatabaseRequest = (bestResult != null) ? String.Join(',', bestResult.requests.ToArray()) : "n/a";
            Program.Logger.Buffer.Response = response;

            return new Tuple<string,string>(response, context);
        }

        // gets the best response for the input
        private Tuple<DatabaseEntry, double> getBestEntry(string input) {
            DatabaseEntry bestResult = null;
            double bestCosineSimilarity= 0f;

            foreach(DatabaseEntry entry in entries.entries) {
                foreach(string request in entry.requests) {

                    // cleans database request to match with input cleaning
                    string cleanRequest = request;
                    cleanRequest = Interpreter.punctuationClean(cleanRequest);
                    cleanRequest = Interpreter.clean(cleanRequest);

                    double similarity = cosine.Similarity(input, cleanRequest);
                    if (similarity > bestCosineSimilarity && similarity > Program.Config.SimilarityThreshold) {
                        bestCosineSimilarity = similarity;
                        bestResult = entry;
                    }
                }
            }
            return new Tuple<DatabaseEntry, double>(bestResult,bestCosineSimilarity);
        }


        // tries searching database with synonyms
        private DatabaseEntry trySynonyms(string input, DatabaseEntry previousBest, double previousBestCosineSimilarity) {
            DatabaseEntry bestResult = previousBest;
            string[] words = input.Split(" ");

            // creates 2d array of synonyms for each word
            var synonyms = new string[words.Length][];
            for(int i =0;i < words.Length; i++) {
                synonyms[i] = wordNetWrapper.GetSynonyms(words[i], Program.Config.MaxSynonyms);
            }

            // tries to find better result using synonyms
            tryToFindBetterSynonym(synonyms,0 ,"",ref bestResult,ref previousBestCosineSimilarity);

            return bestResult;
        }

        // try to find better synonym
        // recursively builds all possible combinations of synonyms
        // sets the best result with its score
        private void tryToFindBetterSynonym(string[][] synonyms,  int wordIndex, string word, ref DatabaseEntry previousBest, ref double previousBestCosineSimilarity) {

            // if no more words return empty
            if (wordIndex >= synonyms.GetLength(0)) {

                // get best entry
                var result = getBestEntry(word);
                
                // if better set new best
                if (result.Item2 > previousBestCosineSimilarity) {
                    previousBest = result.Item1;
                    previousBestCosineSimilarity = result.Item2;
                }
            } else {

                // next word
                int nextWordIndex= wordIndex + 1;
                for (int i = 0;i < synonyms[wordIndex].Length; i++) {

                    // append new word
                    string newWord = word + ((wordIndex != 0 ?  " " : "") + synonyms[wordIndex][i]);
                    tryToFindBetterSynonym(synonyms, nextWordIndex, newWord,ref previousBest,ref previousBestCosineSimilarity);
                }
            }
        }
    }
}