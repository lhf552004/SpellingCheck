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

 

The following is a suggestion as to how you might structure the solution using c# syntax. Feel free to use it or define your own solution.

 

class Misspelling

{

int TextPosition {get;set;}  //position within the original text of the misspelled word

string Word {get;set;}  //the misspelled word

string[] Suggestions {get;set;} //an array of suggested correct spellings

}

 

class SpellCheck

{

SpellCheck(string dictionaryFile);  //constructor is given the path to a dictionary text file

 

               Misspelling[]  CheckText(string text);  //given a string of multiple words, return an array of all misspelled words

               

               string[] SuggestCompletion(string text); //given a partially complete word, return an array of all suggested spellings

}