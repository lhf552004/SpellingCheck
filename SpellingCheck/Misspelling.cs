using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellingCheck
{
    /// <summary>
    /// Misspelling hold the wrong spelled word and the position which is wrong
    /// and store the suggestion list
    /// </summary>
    public class Misspelling
    {
        /// <summary>
        /// Postion in the word
        /// </summary>
        public int TextPosition { get; set; }
        /// <summary>
        ///the misspelled word
        /// </summary>
        public string Word { get; set; }
        /// <summary>
        ///an array of suggested correct spellings
        /// </summary>
        public string[] Suggestions { get; set; } 

    }
    /// <summary>
    /// Suggestion Item which store the specific suggestion word and the relevated wrong postion
    /// </summary>
    public class Suggestion
    {
        /// <summary>
        /// 
        /// </summary>
        public int TextPosition { get; set; }
        public string SuggestedString { get; set; }
    }
}
