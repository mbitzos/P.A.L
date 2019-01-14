using System.Collections.Generic;
using System;

namespace  AI
{

    /// <summary>
    /// Models a line in the log
    /// </summary>
    public class LogObject: object {
        public string Response;

        public double Sentiment;

        public string DatabaseRequest;

        public double Similarity;

        public string Input;

        public float Time;

        public LogObject() {

        }

        /// <summary>
        /// Returns a nice formatted log line
        /// </summary>
        public override string ToString() {
            var props = new List<string>();
            props.Add("Input: " + "\"" + Input + "\"");
            if (Program.LogConfig.ShowSentiment)
                props.Add("Sentiment: " + Math.Round(Sentiment, 3));

            if (Program.LogConfig.ShowDatabaseRequestUsed)
                props.Add("Database Request Used: " + "\"" + DatabaseRequest + "\"");

            if (Program.LogConfig.ShowSimilarityScore)
                props.Add("Similarity: " + Math.Round(Similarity,3));

            props.Add("Response: " + "\"" + Response + "\"");

            if (Program.LogConfig.ShowTimeElapsed)
                props.Add("Time Elasped: " + Time +"ms");

            return string.Join(" | ", props.ToArray());
        }
    } 
}