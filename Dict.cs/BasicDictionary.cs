using System.IO;
using System;
using System.Collections.Generic;


namespace DictCS {
    /// <summary>
    /// The main namespace that will hold all the code related to this project
    /// </summary>
    public class BasicDictionary {

        /// DictFile stores the filename.
        private string DictFile;
        internal string[] AllWords;

        /// maps alphabetically sorted word to a list of words that are its anagram
        public Dictionary<string, List<string>> myDictionary { get; private set; }

        /// <summary>
        /// Constructor with optional filename arguement
        /// </summary>
        /// <param name="inputDictionaryFile">
        /// [Optional] Specify the file with the dictionary, default is Dictionary.txt
        /// </param>
        public BasicDictionary(string inputDictionaryFile = "Dictionary.txt") {
            DictFile = inputDictionaryFile;
            myDictionary = new Dictionary<string, List<string>>();
            readFile();
        }

        /// <summary>
        /// reads the file and generates the map
        /// </summary>
        private void readFile() {
            string textInFile = File.ReadAllText(DictFile);
            string[] seperators = { "\n", "\r", "\n\r" };
            AllWords = textInFile.Split(seperators, StringSplitOptions.None);
            // read the file, split it by words and store it in an array.
            foreach (var word in AllWords) {
                AddWord(word);
            }
        }

        /// <summary>
        /// accepts a word, puts it into the map
        /// </summary>
        internal void AddWord(string word) {
            // sort the letters in the word
            string sortedWord = SortText(word);
            // check if an anagram exists, if it does, 
            // put it in the list
            if (!myDictionary.ContainsKey(sortedWord)) {
                var newList = new List<string> { word };
                myDictionary.Add(sortedWord, newList);
            // if not, create a new mapping for the word
            } else {
                var valueWords = myDictionary[sortedWord];
                valueWords.Add(word);
                myDictionary[sortedWord] = valueWords;
            }
        }

                    /// <summary>
            /// sorts the letters of the text
            /// </summary>
            /// <returns>
            /// string with letters sorted lexically
            /// </returns>
        internal string SortText(string text) {
            // convert the text to character array
            var temp = text.ToCharArray();
            // perform bubble sort
            for (int i=0; i<text.Length; i++) {
                for (int j=0; j<text.Length-i-1; j++) {
                    if (temp[j]>temp[j+1]) {
                        var t = temp[j];
                        temp[j] = temp[j + 1];
                        temp[j + 1] = t;
                    }
                }
            }
            // return by converting it to a string
            return new string(temp);
        }

        /// <summary>
        /// checks if the word is in the dictionary
        /// </summary>
        /// <returns>
        /// Returns true if word is found in the dictionary.
        /// </returns>
        public bool IsWord(string word) {
            if (myDictionary.ContainsKey(SortText(word))) return true;
            else return false;
        }

        /// <summary>
        /// generates all the anagrams for a certain word
        /// </summary>
        /// <returns>
        /// a string array containing all the anagrams
        /// </returns>
        public string[] AnagramsOf(string word) {
            try {
                return myDictionary[SortText(word)].ToArray();
            } catch (Exception) {
                return null;
            }
        }
    }
}
