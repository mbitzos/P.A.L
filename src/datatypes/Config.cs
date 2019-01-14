using Newtonsoft.Json;
using System.ComponentModel;

namespace AI {

    /// <summary>
    /// Models All configuration for program read from json file
    /// </summary>
    [System.Serializable]
    public class Config {
        // minimum similarity to even been considered similar 

        [DefaultValue(0.75)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public double SimilarityThreshold {get; set;}

        // the max similarity for a result to skip synonyms 
        [DefaultValue(0.9)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public double MaxSimilarityThreshold {get; set;}

        // the minimum similarity to even continue to synonyms
        [DefaultValue(0.25)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public double MinSimilarityThreshold {get; set;}

        // how many synonyms we are allowed to hold for each word
        [DefaultValue(10)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public int MaxSynonyms {get; set;}
    }
}