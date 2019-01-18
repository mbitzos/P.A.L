using System.Collections.Generic;

namespace AI.Tags {

    /// <summary>
    /// Manages all available tags in system
    /// </summary>
    public class TagManager {
        List<Tag> tags;

        public TagManager() {
            tags = new List<Tag>() {
                new Censor(),
                new Delay(),
                new Speed(),
                new Ellipsis()
            };
        }


        /// <summary>
        /// Gets the tag associated with the text found between the <>
        /// </summary>
        /// <param name="tagContext">The tag text</param>
        /// <returns>The actual tag this represents</returns>
        public Tag GetTag(string tagContext) {
            foreach(Tag tag in tags) {
                if (tagContext.StartsWith(tag.Label))
                    return tag;
            }
            return null;
        }
    }
}