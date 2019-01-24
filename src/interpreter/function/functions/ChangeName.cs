
namespace AI.Functions {

    /// <summary>
    /// Function for changing PAL's name
    /// </summary>
    public class ChangeName: Function {

        public ChangeName() : base("change_name") {

        } 

        /// <summary>
        /// Changes PALs name
        /// </summary>
        /// <param name="parameters">[0] The new name</param>
        /// <returns></returns>
        public override bool Execute(string[] parameters, bool fromDatabase = false) {
            if (parameters == null || parameters.Length < 1) {
                
            }
            string newName = parameters[0];
            string oldName = Program.PAL.name;
            Program.PAL.ChangeName(newName);

            // if called from terminal
            if (!fromDatabase)
                Program.PAL.Respond("Name has been changed from: " + oldName + " to: " + newName);
            return true;
        }
    }
}