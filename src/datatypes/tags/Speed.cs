
namespace AI.Tags {

    /// <summary>
    /// Changes the speed of writing
    /// NOTE: this can be open for an entire text block to set the write speed for the entire text 
    /// </summary>
    public class Speed:Tag {

        public Speed(): base("s", false) {

        }

        public override void StartAction(Speaker speaker, string text) {
            // add new mod
            speaker.charPerSecondMods.Push(1f/float.Parse(getTagValue(text)));
        }

        public override void EndAction(Speaker speaker, string text) {
            speaker.charPerSecondMods.Pop();
        }

    }
}