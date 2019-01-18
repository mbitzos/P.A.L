using System;
using System.Collections.Generic;
using System.IO;

namespace AI {

    /// <summary>
    /// Wrapper for SymSpell
    /// TODO: Add license
    /// </summary>
    public class SpellChecker {
        
        SymSpell spellChecker;

        const string CompiledDictionary = "./dictionaries/compiled/compiled.txt";

        public SpellChecker() {
            
            compileDictionary();

            // init spell checker
            const int InitialCapacity = 82765;
            const int MaxDistanceEditDictionary = 2;
            this.spellChecker = new SymSpell(InitialCapacity, MaxDistanceEditDictionary);

            
            //column of the term in the dictionary text file
            int termIndex = 0;
            
            //column of the term frequency in the dictionary text file
            int countIndex = 1; 

            if (!spellChecker.LoadDictionary(CompiledDictionary, termIndex, countIndex))
            {
                throw new FileNotFoundException("Dictionary Not found!");
            }
        }

        public string SpellCheck(string input) {
            // empty string
            if (input.Equals("")) {
                return input;
            }
            List<SymSpell.SuggestItem> suggestions;

            // check for mulit word spell check
            if (input.Split(" ").Length == 1) {

                // single word spell check
                //max edit distance per lookup (maxEditDistanceLookup<=maxEditDistanceDictionary)
                int maxEditDistanceLookup = 2;
                var suggestionVerbosity = SymSpell.Verbosity.Closest; 
                suggestions = spellChecker.Lookup(input, suggestionVerbosity, maxEditDistanceLookup);
            } else {

                // multi word spell check
                //max edit distance per lookup (per single word, not per whole input string)
                int maxEditDistanceLookup = 2; 
                suggestions = spellChecker.LookupCompound(input, maxEditDistanceLookup);
            }

            // return first suggestion if exists, else return back input
            return suggestions.Count != 0 ? suggestions[0].term : input;
        }

        // combines the game terms dictionary and english dictionary
        private void compileDictionary() {
            const string englishDictionary = "./dictionaries/english.txt";
            const string gameDictionary = "./dictionaries/game_terms.txt";

            // exceptions
            if (!File.Exists(gameDictionary)) throw new FileNotFoundException("Game term dictionary not found!");
            if (!File.Exists(englishDictionary)) throw new FileNotFoundException("English Dictionary not found!");

            // copies english dictionary to compiled
            File.Copy(englishDictionary,CompiledDictionary,true);


            // add all game terms to compiled
            using (StreamWriter stream = File.AppendText(CompiledDictionary)) 
            {
                string[] gameTerms = File.ReadAllLines(gameDictionary);
                foreach(string term in gameTerms) {

                    // add game term with highest frequency so that it is always chosen by spell checker
                    stream.WriteLine(term + " " + long.MaxValue);
                }
            }
        }
    }
}