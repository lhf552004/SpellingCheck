using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellingCheck
{
    public class Misspelling

    {
        public int TextPosition { get; set; }
        public string Word { get; set; }  //the misspelled word

        public string[] Suggestions { get; set; } //an array of suggested correct spellings

    }
    public class Suggestion
    {
        public int TextPosition { get; set; }
        public string SuggestedString { get; set; }
    }
}
