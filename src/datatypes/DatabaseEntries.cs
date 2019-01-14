using System.Collections.Generic;

namespace AI {
    
    /// <summary>
    /// Models an entry in the database
    /// </summary>
    [System.Serializable]
    public class DatabaseEntries {
        
        public List<DatabaseEntry> entries;

        public DatabaseEntries(List<DatabaseEntry> entries) {
            this.entries = entries;
        }

    }
}
