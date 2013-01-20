using System;

namespace FallenNova.Shared.ExtensionMethods
{
    public static class String
    {
        /// <summary>
        /// Return true/false depending on a case insensitive string comparison. 
        /// </summary>
        /// <param name="firstString">First string to compare.</param>
        /// <param name="secondString">Second string to compare.</param>
        /// <returns>True if the strings are equal, false otherwise.</returns>
        public static bool EqualsCaseInsensitive(
            this string firstString,
            string secondString)
        {
            return firstString.Equals(secondString, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Returns a version of the string which has the correct placement of the singular noun's apostrophe. 
        /// </summary>
        /// <param name="value">Value without the apostrophe.</param>
        /// <returns>Value with the apostrophe.</returns>
        public static string WithApostrophe(this string value)
        {
            return string.Concat(
                    value,
                    (!value.ToLower().EndsWith("s"))
                        ? "'s"
                        : "'");
        }
    }
}
