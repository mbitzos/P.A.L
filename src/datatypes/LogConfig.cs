using Newtonsoft.Json;
using System.ComponentModel;

namespace AI {

    /// <summary>
    /// Models All Log configuration read from json file
    /// </summary>
    [System.Serializable]
    public class LogConfig {

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool ShowSentiment;
        
        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool ShowSimilarityScore;

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool ShowDatabaseRequestUsed;

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool ShowTimeElapsed;
    }
}