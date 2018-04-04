using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellingCheck
{
    public class SpellCheck

    {
        //constructor is given the path to a dictionary text file
        public SpellCheck(string dictionaryFile)
        {
             
            readDic(dictionaryFile);
        }
        private List<string> dictionary;
        private void readDic(string filePath)
        {
            TextReader theDataFile = new StreamReader(filePath);
            string stringTotal = theDataFile.ReadToEnd();
            string[] stringRows = stringTotal.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            dictionary = new List<string>(stringRows);

        }
        //given a string of multiple words, return an array of all misspelled words
        public Misspelling[] CheckText(string text)
        {
            string[] words = text.Split(new Char[] { '?', ':',' ', '!', ';' }, StringSplitOptions.RemoveEmptyEntries);
            List<Misspelling> theMissSpellingList = new List<Misspelling>();
            foreach (var word in words)
            {
                Misspelling theMissSpelling = null;
                //Func<string, bool> checkSpell = 
                var existedWords = dictionary.Where(s => 
                {
                    return isMissSpell(s, word,out theMissSpelling);
                }).ToArray();
                if(theMissSpelling!=null && existedWords.Length > 0)
                {
                    theMissSpelling.Suggestions = existedWords;
                    theMissSpellingList.Add(theMissSpelling);
                }
            }
            return theMissSpellingList.ToArray();
        }
         
        private bool isMissSpell(string dicWord, string toBeCheckedWord, out Misspelling theMissSpelling)
        {
            bool isTheWord = false;
            int targetLength = toBeCheckedWord.Length;
            theMissSpelling = null;
            
            for (int i =1; i<=dicWord.Length; i++)
            {
                string partialDicWord = dicWord.Substring(0, i);
                string partialToBeCheckedWord = string.Empty;
                if (targetLength>= i)
                {
                    partialToBeCheckedWord = toBeCheckedWord.Substring(0, i);
                    if (partialDicWord.Equals(partialToBeCheckedWord))
                    {
                        isTheWord = true;
                    }
                    else
                    {
                        if (isTheWord)
                        {
                            //have found the word in the library, but target word is miss-spelled
                            theMissSpelling = new Misspelling()
                            {
                                TextPosition = i,
                                Word = toBeCheckedWord,
                            };
                            return true;
                        }
                        else
                        {
                            //not the word
                            break;
                        }
                    }
                }
                else
                {
                    if (isTheWord)
                    {
                        //have found the word in the library, but target word is miss-spelled
                        //this situation like below
                        //word in library: Abcd
                        //target word is Abc
                        theMissSpelling = new Misspelling()
                        {
                            TextPosition = i,
                            Word = toBeCheckedWord,
                        };
                        return true;
                    }
                    else
                    {
                        //not the word
                        break;
                    }
                }
                
                 
            }
            return false;
        }

        private bool checkPossibleSpell(string dicWord, string toBeCheckedWord)
        {
            if (dicWord.Length>=toBeCheckedWord.Length && dicWord.StartsWith(toBeCheckedWord))
            {
                return true;
            }
            
            return false;
        }
        //given a partially complete word, return an array of all suggested spellings
        public string[] SuggestCompletion(string text)
        {
            return dictionary.Where(s =>
            {
                return checkPossibleSpell(s, text);
            }).ToArray();
        } 

    }
}
