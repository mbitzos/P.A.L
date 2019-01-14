using System.ComponentModel;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AI {
    
    /// <summary>
    /// Models an entry in the database
    /// </summary>
    [System.Serializable]
    public class DatabaseEntry {
        
        public List<string> requests;
        public string response;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string context;

        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public DatabaseEntry[] responseTree;

        [JsonIgnore]
        public DatabaseEntry[] parents;
    }
}