

namespace AI.Functions {

    /// <summary>
    /// An abstraction of the functions available for the database/user
    /// </summary>
    public abstract class Function {

        public string name {get; internal set;}       

        public Function(string name) {
            this.name = name;
        } 

        /// <summary>
        /// The function to be called
        /// </summary>
        /// <param name="parameters">The parameters to pass to it</param>
        /// <param name="fromDatabase">If this execution came from a database or terminal</param>
        public abstract bool Execute(string[] parameters = null, bool fromDatabase=false);

    }
}