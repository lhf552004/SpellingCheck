using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellingCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            SpellCheck checker = new SpellCheck("English.dictionary");
            var missSpellingList = checker.CheckText("Abren seam ceuching");
            foreach(var missSpelling in missSpellingList)
            {
                Console.WriteLine("missSpellingword {0}, postion: {1}", missSpelling.Word, missSpelling.TextPosition);
                Console.WriteLine("Suggestion list:");
                foreach (var suggestion in missSpelling.Suggestions)
                {
                    Console.Write(suggestion);
                    Console.Write(" ");
                }
                Console.WriteLine("------");
            }
            Console.WriteLine("Ab get the suggestion list:");
            var suggestedList = checker.SuggestCompletion("Ab");
            foreach (var suggestion in suggestedList)
            {
                Console.Write(suggestion);
            }
            Console.ReadLine();
        }
    }
}
