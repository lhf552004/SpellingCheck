### Requirements:

·         Find misspelled words (using the provided dictionary file)

·         Provide correct spellings for misspelled words

·         Case sensitivity aware

·         Provide suggestions for spelling of incomplete words (autocomplete)

·         Provide enough documentation so that we can use it

 

### Rules:

1)      Submit after 24 hours, prioritize requirement to provide as complete a solution as possible in time given. We expect that this task will take longer than 3 hours to complete. Make sure to prioritize appropriately.

2)      Use C#

3)      Must be written from scratch, no use of existing spell check libraries. We are trying to see how you approach the problem space. Using somebody else’s code will invalidate the purpose of the exercise.

 

### How to Use it:
Create a checker, with the dictionary file:

	SpellCheck checker = new SpellCheck("c:\\English.dictionary");

Check the text with mutiple words:
	var missSpellingList = checker.CheckText("Abren seam ceuching");

Get the suggestion list of a word:

	 var suggestedList = checker.SuggestCompletion("Ab");

complete example:
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