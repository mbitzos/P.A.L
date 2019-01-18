
namespace AI.Tags {

    /// <summary>
    /// Censors out a piece of text
    /// </summary>
    public class Censor:Tag {

        public Censor(): base("cen", true) {

        }

        public override void StartAction(Speaker speaker, string text) {
            speaker.censoring = true;
        }

        public override void EndAction(Speaker speaker, string text) {
            speaker.censoring = false;
        }
    }
}