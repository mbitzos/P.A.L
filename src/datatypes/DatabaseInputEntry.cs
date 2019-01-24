
using System.ComponentModel;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AI {
    
    /// <summary>
    /// Models an input entry in the database
    /// </summary>
    [System.Serializable]
    public class DatabaseInputEntry  {
        
        // requests that possible to retrieve this entry
        public List<string> requests;

        // what response to write back on function success
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string success;

        // what response to write back on function failure
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string failure;


        // what function to call (optional)
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string function;

        // what parameters to pass to the function (optional)
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string[] parameters;

        // if this response signifys if user input is needed
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public DatabaseInputEntry[] input;
    }
}