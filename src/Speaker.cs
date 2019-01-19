using System;
using System.Collections.Generic;
using System.Threading;
using AI.Tags;

namespace AI {

    /// <summary>
    /// Responsible for writing the AI's response to the console in a human like fashion
    /// </summary>
    public class Speaker {
        const char EscapeChar = '`';
        int charPerSecond = 20;
    
        // how long the thread should sleep before writing next char
        int charWriteSpeed {get {return (int) (charPerSecond * currentCharPerSecondMod);}}

        // keeps track of all mods so we can have layered speeds not reverting to original
        public Stack<float> charPerSecondMods = new Stack<float>();

        // returns the current char speed mod
        float currentCharPerSecondMod { get { return (charPerSecondMods.Count != 0) ? charPerSecondMods.Peek() : 1f; }}

        public bool censoring = false;

        Random randomGenerator = new Random();

        TagManager tagManager = new TagManager();

        NullResponder nullResponder = new NullResponder();

        public Speaker() {
            
        }

        /// <summary>
        /// Response back to the user
        /// </summary>
        /// <param name="response">The response to write to console</param>
        public void Respond(string response) {

            // response again but with new null response constructed
            if (response == null) 
                response = nullResponder.BuildResponse();

            string currentTag = "";
            bool inTag = false;
            bool inClosingTag = false;
            bool escaping = false;
            for(int i = 0; i < response.Length; i++) {
                char letter = response[i];

                // escape current char
                if (escaping) {
                        Write(letter);
                        escaping = false;
                        continue;
                }

                // escape char
                if (letter.Equals(EscapeChar)) {
                    escaping = true;
                    continue;
                }

                if (letter.Equals('<')) {

                    // start recording tag
                    inTag= true;
                    
                    // if recording in the closing tag
                    if (i != response.Length - 1 && response[i+1].Equals('/')) {
                        inClosingTag = true;
                    }

                } else if (letter.Equals('>') && inTag) {

                    // either perform open or close tag action
                    if (!inClosingTag) {
                        performTagAction(currentTag);
                    } else {
                        performCloseTagAction(currentTag.Substring(1,currentTag.Length -1));
                    }

                    // reset
                    inTag = false;
                    inClosingTag = false;
                    currentTag = "";
                } else if (inTag) {

                    // keep track of string inside < >
                    currentTag += letter;
                } else {
                    Write(letter);
                }

            }

            // exception
            if (inTag) {
                throw new Exception("Response did not have ending '>'.");
            }

            // new line
            Console.WriteLine();
        }

        // performs the opening tags action
        private void performTagAction(string tag) {
            tagManager.GetTag(tag).StartAction(this, tag);
        }
        
        // performs the closing operation of a tag if it exists
        private void performCloseTagAction(string tag) {
            tagManager.GetTag(tag).EndAction(this, tag);
        }

     

        // Wrapper for censoring writing chars to console
        public void Write(char letter) {
            if (censoring) {
                
                // range of ASCII random chars
                const int ASCIIStart = 33;
                const int ASCIIEnd = 126;

                // how many more chars we can write
                const int MaxCharWrite = 2;
                
                // create 1 or 2 random chars
                int loops = (randomGenerator.Next(MaxCharWrite)+1);
                for (int i = 0;i <loops; i++)
                    _write((char) randomGenerator.Next(ASCIIStart,ASCIIEnd));
            } else {
                _write(letter);
            }
        }
        
        // actually writes to console
        private void _write (char letter) {
            Console.Write(letter);
            Thread.Sleep(charWriteSpeed);
        }
    }
}