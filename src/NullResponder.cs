using System;
using System.Linq;

namespace AI {

    /// <summary>
    /// Handles the AI's response when no database response is found
    /// </summary>
    public class NullResponder {

        string[] particles = {
            "sorry",
            "my apologizes",
            "unfortuntely",
            "oops",
            "my bad"
        };
        string [] bodies = {
            "I can't seem to find anything related to this",
            "There doesn't seem to be anything on this",
            "I dont know anything about that",
            "Theres nothing in my databases about this"
        };

        int[] bodiesOccurancy;
        int[] particlesOccurancy;

        Random randomizer = new Random();

        string previousBody ="";
        string previousParticle ="";

        public NullResponder() {
            bodiesOccurancy = new int[bodies.Length];
            particlesOccurancy = new int[particles.Length];
        }

        /// <summary>
        /// Constructs a randomized null response
        /// </summary>
        /// <returns></returns>
        public string BuildResponse() {
            return _buildResponse();
        }
        
        // actually constructs the response
        private string _buildResponse() {
            
            // get available
            string[] availableParticles = getAvailable(particles, particlesOccurancy, ref previousParticle);
            string[] availableBodies = getAvailable(bodies, bodiesOccurancy, ref previousBody);

            // get random particle from available list
            string particle = getRandom(availableParticles);

            // get random body from available list
            string body = getRandom(availableBodies);

            // mark this as already selected
            bodiesOccurancy[Array.IndexOf(bodies, body)] = 1;

            string response;

            // if there is a pre or post or neither in the sentence
            int randomResult = randomizer.Next(3);

            if (randomResult == 0) {

                // pre
                response = particle.ToUpperFirst() + ", " + body.ToLowerFirst();

                // mark this as already selected
                particlesOccurancy[Array.IndexOf(particles, particle)] = 1;
            } else if (randomResult == 1) {

                // post
                body = body.ToUpperFirst();
                
                // format post to come after
                response = body + ", " + particle.ToLowerFirst();

                // mark this as already selected
                particlesOccurancy[Array.IndexOf(particles, particle)] = 1;
            } else {

                // neither
                response = body.ToUpperFirst();
            }

            // always add period
            return response + ".";
        }

        // returns a random string or empty if out of bounds
        private string getRandom(string[] array) {
            int randomIndex = randomizer.Next(array.Length);
            return array[randomIndex];
        }

        // gets the available entries in array with scores
        // changes scores if no more are left and sets previous
        private string[] getAvailable(string[] original, int[] scores, ref string previous) {
            string previousCopy = previous;

            // find all that havent been picked (score == 1) and arent the previously used
            string[] available = original.Where(p => (scores[Array.IndexOf(original, p)] == 0 && p != previousCopy)).ToArray();

            // if we cant find any, reset scores
            if (available.Length == 0) {
                for(int i =0; i< scores.Length;i++)
                    scores[i] = 0;
                available = getAvailable(original, scores, ref previous);
            } else if (available.Length == 1){

                // if only one left, set this to be the one that is being used last
                previous = available[0];
            }
            return available;
        }
    }
}