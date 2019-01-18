using System.Threading;

namespace AI.Tags {

    /// <summary>
    /// Creates a pause in text writing
    /// </summary>
    public class Delay:Tag {

        public Delay(): base("d", false) {

        }

        public override void StartAction(Speaker speaker, string text) {
            int delayTime= (int) (float.Parse(getTagValue(text)) * 1000);
            Thread.Sleep(delayTime);
        }

        public override void EndAction(Speaker speaker, string text) {

        }

    }
}