using System.Threading;

namespace AI.Tags {

    /// <summary>
    /// Writes ... with a short delay inbetween
    /// Allows for custom delay time
    /// </summary>
    public class Ellipsis:Tag {

        const int WriteSpeed = 300;
        public Ellipsis(): base("...", false) {

        }

        public override void StartAction(Speaker speaker, string text) {
           string value = getTagValue(text);

            // get value or default
            float delayTime = (!value.Equals("")) ? (int) (float.Parse(value)) : 0.5f; 

            // write ellipsis with delay inbetween
            Thread.Sleep((int) (delayTime * WriteSpeed));
            speaker.Write('.');
            Thread.Sleep((int) (delayTime * WriteSpeed));
            speaker.Write('.');
            Thread.Sleep((int) (delayTime * WriteSpeed));
            speaker.Write('.');
            Thread.Sleep((int) (delayTime * WriteSpeed));
        }

        public override void EndAction(Speaker speaker, string text) {

        }

    }
}