

namespace AI.Tags {

    /// <summary>
    /// Represents a markdown like tag in the speaker data base responses
    /// </summary>
    public abstract class Tag {

        // what label this tag is represented by (ex. <cen> </cen>)
        public string Label {get; internal set;}
        public bool Wrappable {get; internal set;}

        /// <summary>
        /// Creates a tag instance
        /// </summary>
        /// <param name="label">The label</param>
        /// <param name="wrappable">if the tag is wrappable (has to have an start and ending</param>
        protected Tag(string label, bool wrappable=false) {
            Label = label;
            Wrappable = wrappable;
        }

        /// <summary>
        /// The action to perform on the tag enter
        /// </summary>
        /// <param name="speaker">The speaker instance</param>
        /// <param name="text">The text of the tag</param>
        public abstract void StartAction(Speaker speaker, string text);

        /// <summary>
        /// The action to perform on the tag exit
        /// </summary>
        /// <param name="speaker">The speaker instance</param>
        /// <param name="text">The text of the tag</param>
        public abstract void EndAction(Speaker speaker, string text);

        // gets the value attribute of the tag
        protected string getTagValue(string tag) {
            const string ValueKeyWord= "val=";
            int valueIndex = tag.IndexOf(ValueKeyWord);
            
            // if no value return nothing
            if (valueIndex == -1)
                return "";
            
            valueIndex += ValueKeyWord.Length;
            string value = "";

            // create attribute value char by char
            for (int i = valueIndex; i < tag.Length; i++) {
                char letter = tag[i];

                //end of value string
                if (letter.Equals(' ') || letter.Equals('>') || letter.Equals('/')) {
                    return value;
                } else {

                    // build value
                    value += letter;
                }
            }
            return value;
        }
    }
}