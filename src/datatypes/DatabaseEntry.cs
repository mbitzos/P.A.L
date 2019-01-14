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
        public string context {get; set;}

        public DatabaseEntry(List<string> requests, string response, string context) {
           this.requests = requests;
           this.response = response;
           this.context = context;
        }
    }
}