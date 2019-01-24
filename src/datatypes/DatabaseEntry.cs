using System;
using System.ComponentModel;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AI {
    
    /// <summary>
    /// Models an entry in the database
    /// </summary>
    [System.Serializable]
    public class DatabaseEntry {
        
        // requests that possible to retrieve this entry
        public string[] requests;

        // what response to write back
        [JsonProperty]
        string[] response = null;

        // returns the response
        public string GetResponse { get { return response[Math.Min(visits, response.Length - 1)]; } }

        // what function to call (optional)
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string function;

        // what parameters to pass to the function (optional)
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string[] parameters;

        // what context this entry is in
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string context;

        // response tree available for this entry
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public DatabaseEntry[] responseTree;

        // if this response signifys if user input is needed
        [DefaultValue(null)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public DatabaseInputEntry[] input;

        [JsonIgnore]
        public DatabaseEntry[] parents;

        [JsonIgnore]
        public int visits = 0;

    }
}