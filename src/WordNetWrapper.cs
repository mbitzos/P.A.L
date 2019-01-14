/*
    The software includes the Syn.WordNet Nuget Package: Copyright (c) 2017 Synthetic Intelligence Network
 */

using System;
using System.Collections.Generic;
using System.IO;
using Syn.WordNet;

namespace AI {

    /// <summary>
    /// Handles intefacing with the WordNetEngine
    /// </summary>
    public class WordNetWrapper {
        WordNetEngine wordNet;
        public WordNetWrapper() {

            var directory =  "./resources/wordnet";
            wordNet = new WordNetEngine();

            // add all resources
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adj")), PartOfSpeech.Adjective);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adv")), PartOfSpeech.Adverb);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.noun")), PartOfSpeech.Noun);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.verb")), PartOfSpeech.Verb);

            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adj")), PartOfSpeech.Adjective);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adv")), PartOfSpeech.Adverb);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.noun")), PartOfSpeech.Noun);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.verb")), PartOfSpeech.Verb);

            wordNet.Load();
        }
        

        /// <summary>
        /// Gets all wordnet synonyms for a word
        /// </summary>
        /// <param name="word">The word to find synonyms for</param>
        /// <param name="limit">The max amount of synonyms to return</param>
        /// <returns>a list of unique synonyms for the word</returns>
        public string[] GetSynonyms(string word, int limit) {
            var synSetList = wordNet.GetSynSets(word);

            var words = new HashSet<string>();

            int counter = 0;
            
            // add each unique word to list
            foreach(SynSet set in synSetList) {
                foreach(string setWord in set.Words) {
                    
                    // dont add any these type of words (ex. test_test) (idk why these are in the wordnet...)
                    if (!setWord.Contains('_') && !setWord.Contains('-')) {
                        words.Add(setWord);
                        counter++;
                    }

                    // dont go over limit
                    if (counter>= limit) 
                        break;
                }

                // dont go over limit
                if (counter>= limit) 
                    break;
            }

            // null checks
            string[] result = new string[words.Count];
            words.CopyTo(result);
            
            return (result.Length != 0) ? result : new string[]{word};
        }
    }
}