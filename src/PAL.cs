using System;
using System.Collections.Generic;

namespace AI {

    /// <summary>
    /// Models the stateful information for P.A.L
    /// </summary>
    public class PAL {

        static Speaker Speaker;
        public string name {get; internal set;}


        public PAL() {
            name = "P.A.L";
            Speaker = new Speaker();
            Console.WriteLine("Finished setting up PAL core.");
            Program.Properties["pal.name"] = "P`.A`.L";
        }


        /// <summary>
        /// Wrapper for Speaker.Respond, Response back to the user
        /// </summary>
        /// <param name="response">The response to write to console</param>
        public void Respond(string response) {
            Speaker.Respond(response);
        }

        /// <summary>
        /// Changes PALS name
        /// </summary>
        /// <param name="newName">The new name to assign to him</param>
        /// <returns>If the name change was successful</returns>
        public bool ChangeName(string newName) {
            name= newName;
            Program.Properties["pal.name"] = newName;
            return true;
        }


    }
}