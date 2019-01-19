namespace AI
{

    // helper functions for string extensions
    public static class StringExtensions {

        // help to capitilize first letter of string
        public static string ToUpperFirst(this string word) {
            return char.ToUpper(word[0]) + word.Substring(1);
        }

        // help to lowercase first letter of string
        public static string ToLowerFirst(this string word) {
            return char.ToLower(word[0]) + word.Substring(1);
        }
    }    
}