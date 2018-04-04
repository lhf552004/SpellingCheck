﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellingCheck
{
    /// <summary>
    /// Spell check class 
    /// function 1: could check misspelling words, and provide suggested correct words
    /// function 2: could give suggestion words for a typing word
    /// </summary>
    public class SpellCheck
    {
        #region Constructor
        /// <summary>
        /// constructor is given the path to a dictionary text file
        /// </summary>
        /// <param name="dictionaryFile"></param>
        public SpellCheck(string dictionaryFile)
        {

            readDic(dictionaryFile);
        }
        /// <summary>
        /// default constructor with default dictionary file
        /// the dictionary file should be at the same path with executable file
        /// </summary>
        public SpellCheck() : this("English.dictionary") { }
        #endregion
        #region Field
        private List<string> dictionary;
        #endregion

        #region private method
        /// <summary>
        /// The predicate for the misspelling checking
        /// </summary>
        /// <param name="dicWord"></param>
        /// <param name="toBeCheckedWord"></param>
        /// <param name="theMissSpelling"></param>
        /// <returns></returns>
        private bool isMissSpell(string dicWord, string toBeCheckedWord, ref Misspelling theMissSpelling)
        {
            if (dicWord == toBeCheckedWord)
            {
                //it is correct, so it's not need to suggestion
                return false;
            }

            //Condition1:
            //only some char is wrong but with the same width of string
            if (projectionCheck(dicWord, toBeCheckedWord, ref theMissSpelling))
            {
                return true;
            }
            //Confidtion2:
            //Maybe there is an additional char inserted into correct word
            if(similarityCheck(dicWord, toBeCheckedWord, ref theMissSpelling))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// To check the char at the same position
        /// </summary>
        /// <param name="dicWord"></param>
        /// <param name="toBeCheckedWord"></param>
        /// <returns></returns>
        private bool projectionCheck(string dicWord, string toBeCheckedWord, ref Misspelling theMissSpelling)
        {
            int failNum = 0;
            Dictionary<int, bool> theTable = new Dictionary<int, bool>();
            if (!string.IsNullOrEmpty(dicWord) && !string.IsNullOrEmpty(toBeCheckedWord) && dicWord.Length == toBeCheckedWord.Length)
            {
                for (int i = 0; i <= dicWord.Length - 1; i++)
                {
                    char left = dicWord[i];
                    char right = toBeCheckedWord[i];
                    if (left != right)
                    {
                        //the same postion char is different
                        theTable.Add(i, false);
                        failNum++;
                    }
                    else
                    {
                        theTable.Add(i, true);
                    }
                }
                if (failNum > 0)
                {
                    //It means there are some char are different
                    float failFloat = failNum;
                    float theLegth = dicWord.Length;
                    float index = failFloat / theLegth;
                    if (index <= 0.3)
                    {
                        //if index less than a value, it means it is simlar to the dic word
                        //Then suggest the dicWord to change
                        if (theMissSpelling == null)
                        {
                            theMissSpelling = new Misspelling();
                        }
                        //It will only store the postion with last matched string
                        //I think it's better to store every postion for evey matched string
                        theMissSpelling.TextPosition = theTable.Where(i => i.Value == false).FirstOrDefault().Key;

                        //indicate the dic word should be return
                        return true;
                    }
                }
            }
            return false;
        }
        int min(int a, int b, int c)
        {
            return Math.Min(a, Math.Min(b, c));
        }
        /// <summary>
        /// caculate the distance between the two string
        /// </summary>
        /// <param name="dicWord"></param>
        /// <param name="toBeCheckedWord"></param>
        /// <returns></returns>
        private int getDistance(string dicWord, string toBeCheckedWord)
        {

            int dicWordLength = dicWord.Length;
            int toBeCheckedWordLength = toBeCheckedWord.Length;
            int[,] matrix = new int[dicWordLength + 1, toBeCheckedWordLength + 1];
            for (int i = 0; i <= dicWordLength; i++)
                matrix[i, 0] = i;
            for (int j = 0; j <= toBeCheckedWordLength; j++)
                matrix[0, j] = j;
            for (int i = 1; i <= dicWordLength; i++)
            {
                for (int j = 1; j <= toBeCheckedWordLength; j++)
                {
                    if (dicWord[i - 1] != toBeCheckedWord[j - 1])
                    {

                        matrix[i, j] = min(1 + matrix[i - 1, j],  // deletion
                                        1 + matrix[i, j - 1],  // insertion
                                        1 + matrix[i - 1, j - 1] // replacement
                                      );
                    }
                    else
                        matrix[i, j] = matrix[i - 1, j - 1];
                }
            }
            return matrix[dicWordLength, toBeCheckedWordLength];
        }
        private int getPosition(string dicWord, string toBeCheckedWord)
        {
            bool isContained = false;
            int position = -1;
            int dicWordLength = dicWord.Length;
            int toBeCheckedWordLength = toBeCheckedWord.Length;
            for (int i = 0; i < dicWordLength; i++)
            {
                for (int j = i + 1; j < dicWordLength - i; j++)
                {
                    string part = dicWord.Substring(i, j);
                    if (toBeCheckedWord.Contains(part))
                    {
                        isContained = true;
                    }else if (isContained)
                    {
                        string matchedPart = part.Substring(0, part.Length - 1);
                        position = toBeCheckedWord.IndexOf(matchedPart);
                    }
                }
            }
            return position;
        }
        private bool similarityCheck(string dicWord, string toBeCheckedWord, ref Misspelling theMissSpelling)
        {
            if (getDistance(dicWord, toBeCheckedWord) <= 2)
            {
                if (theMissSpelling == null)
                {
                    theMissSpelling = new Misspelling();
                }
                theMissSpelling.TextPosition = getPosition(dicWord, toBeCheckedWord);
                return true;
            }
            return false;
        }

        /// <summary>
        /// The predicate for getting suggestion list
        /// </summary>
        /// <param name="dicWord"></param>
        /// <param name="toBeCheckedWord"></param>
        /// <returns></returns>
        private bool checkPossibleSpell(string dicWord, string toBeCheckedWord)
        {
            if (dicWord.Length >= toBeCheckedWord.Length && dicWord.Contains(toBeCheckedWord))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// load the dictionary file
        /// </summary>
        /// <param name="filePath"></param>
        private void readDic(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new Exception("Could not find the file.");
            }
            TextReader theDataFile = new StreamReader(filePath);
            string stringTotal = theDataFile.ReadToEnd();
            string[] stringRows = stringTotal.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            dictionary = new List<string>(stringRows);

        }
        #endregion


        /// <summary>
        /// Main function :
        /// given a string of multiple words, return an array of all misspelled words
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public Misspelling[] CheckText(string text)
        {
            //get the original word list from text
            string[] words = text.Split(new Char[] { '?', ':', ' ', '!', ';', '.', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
            List<Misspelling> theMissSpellingList = new List<Misspelling>();
            foreach (var word in words)
            {
                //remove space and tab
                string wordToBeCheck = word.Trim();
                //remove 's after a word
                if (wordToBeCheck.EndsWith("'s"))
                {
                    wordToBeCheck.Replace("'s", "");
                }

                Misspelling theMissSpelling = null;
                var existedWord = dictionary.Where(s => s == wordToBeCheck).FirstOrDefault();
                if (string.IsNullOrEmpty(existedWord))
                {
                    theMissSpelling = new Misspelling()
                    {
                        Word = wordToBeCheck,
                        TextPosition = -1
                    };
                    var suggestionWords = dictionary.Where(s =>
                    {
                        return isMissSpell(s, wordToBeCheck, ref theMissSpelling);
                    }).ToArray();
                    if (theMissSpelling != null)
                    {
                        theMissSpelling.Suggestions = suggestionWords;
                        theMissSpellingList.Add(theMissSpelling);
                    }
                }


            }
            return theMissSpellingList.ToArray();
        }

        /// <summary>
        /// Main function:
        /// given a partially complete word, return an array of all suggested spellings
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string[] SuggestCompletion(string text)
        {
            return dictionary.Where(s =>
            {
                return checkPossibleSpell(s, text);
            }).ToArray();
        }

    }
}
