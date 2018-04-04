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
            SpellCheck checker = new SpellCheck();
            DateTime beforDT = System.DateTime.Now;
            var missSpellingList = checker.CheckText("Abraan seam ceuching");
            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            Console.WriteLine("CheckText Total cost time: {0}ms.", ts.TotalMilliseconds);
            foreach (var missSpelling in missSpellingList)
            {
                Console.WriteLine("missSpellingword {0}, postion: {1}", missSpelling.Word, missSpelling.TextPosition);
                Console.WriteLine("Suggestion list:");
                foreach (var suggestion in missSpelling.Suggestions)
                {
                    Console.Write(suggestion);
                    Console.Write(" ");
                }
                Console.WriteLine("\n------");
            }
            Console.WriteLine("Ab get the suggestion list:");
            var suggestedList = checker.SuggestCompletion("Ab");
            foreach (var suggestion in suggestedList)
            {
                Console.Write(suggestion);
                Console.Write(" ");
            }
            Console.WriteLine("\n------");
            Console.ReadLine();
        }
    }
}
