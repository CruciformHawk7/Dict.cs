using System;
using System.Linq;
using System.Collections.Generic;

namespace DictCS {
    /// <summary>
    /// this class extends BasicDictionary to add AI functionality
    /// </summary>
    public class Dict : BasicDictionary {

        private string LearntWordsFileName { get; set; }

        private string WordWeightFileName { get; set; }

        static internal Dictionary<char, char[]> Neighbours;
        // Neighbours holds a path to a probable mistyped letter

        internal Dictionary<char, List<string>> AllWord;

        static internal int WordLearningFrequency = 3;
        // determines how frequently a word has to be used to be "learnt"
        private Dictionary<string, int> LearntWords { get; set; }
        // Holds a list of LearntWords

        /// <summary>
        /// this is the constructor for Dict. Initialise local variables and prepare system
        /// </summary>
        /// <param name="InputFileName">
        /// Path to the text file with words.
        /// Default: Dictionary.txt
        /// ! This file is required, else the program will PURPOSELY throw FileNotFound Exception
        /// </param>
        /// <param name="LearntWordsFileName">
        /// Path to open and store LearntWords.
        /// Default: LearntWords.txt
        /// </param>
        /// <param name="WordWeightFileName">
        /// Path to load and store "weights" of different words.
        /// Default: Weights.txt
        /// </param>
        public Dict(string InputFileName = "Dictionary.txt", 
                    string LearntWordsFileName = "LearntWords.txt", 
                    string WordWeightFileName = "Weights.txt") 
            : base(InputFileName) {
            this.LearntWordsFileName = LearntWordsFileName;
            this.WordWeightFileName = WordWeightFileName;
            AllWord = new Dictionary<char, List<string>>();
            Neighbours = new Dictionary<char, char[]>();
            LearntWords = new Dictionary<string, int>();
            FillAllWords();
            GenerateNeighbours();
        }

        /// <summary>
        /// Extension of IsWord from BasicDictionary: See <see cref="BasicDictionary.IsWord(string)"/>
        /// </summary>
        /// <param name="word">The string word contains the word to be looked up in the loaded dictionary file.</param>
        /// <returns>Returns true if the word exists; false if it doesn't exist</returns>
        public new bool IsWord(string word) {
            bool result = base.IsWord(word);
            if (!result) {
                bool newWord = LearnWord(word);
                return newWord;
            }
            return result;
        }

        public string[] SuggestWord(string partWord) {
            if (partWord == null || partWord == "") return null;
            else if (partWord.Length == 1) {
                return AllWord[partWord[0]].ToArray();
            } else {
                var temp = AllWord[partWord[0]];
                var op = temp.Where(pw => pw.Contains(partWord));
                return op.ToArray();
            }
        }

        internal void FillAllWords() {
            foreach(string word in base.AllWords) {
                if (word == "") continue;
                if (AllWord.ContainsKey(word[0])) {
                    var temp = AllWord[word[0]];
                    temp.Add(word);
                    AllWord[word[0]] = temp;
                } else {
                    List<string> temp = new List<string>();
                    temp.Add(word);
                    AllWord.Add(word[0], temp);
                }
            }
        }

        /// <summary>
        /// Accepts a word, checks for possible errors.
        /// ! This function requires that you ensure that word doesn't exist
        /// </summary>
        /// <param name="word">Look this erranous word up for suggestions</param>
        /// <returns>A list of suggestive words</returns>
        // TODO: Weighting the words, alter the list contents to a word that is more likely erranous.
        public List<string> SuggestedWords(string word) {
            int i;
            List<string> op = new List<string>();
            for (i = 0; i<word.Length; i++) {
                foreach (var neighbour in Neighbours[word[i]]) {
                    string text = ReplaceCharAt(i, neighbour, word);
                    if (base.IsWord(text)) op.Add(text);
                }
            }
            Console.Write("Suggested Words: ");
            ShowList(op);
            return op;
        }

        /// <summary>
        /// A simple function that displays all the elements of a List(string)
        /// </summary>
        /// <param name="ip">Input List to print</param>
        public static void ShowList(List<string> ip) {
            foreach(var item in ip) {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
        }

        private static string ReplaceCharAt(int location, char replacement, string word) {
            char[] arr = word.ToCharArray();
            arr[location] = replacement;
            return new string(arr);
        }

        private bool LearnWord(string word) {
            if (LearntWords.ContainsKey(word)) {
                if (LearntWords[word] > WordLearningFrequency) {
                    Console.WriteLine("Learnt new word: {0}", word);
                    base.AddWord(word);
                    return true;
                } else {
                    LearntWords[word] += 1;
                }
            } else {
                //new word was used once.
                LearntWords.Add(word, 1);
            }
            return false;
        }


        internal void GenerateNeighbours() {
            char[] temp;
            temp = new char[] { 'q', 'w', 's', 'z', 'x'};
            Neighbours.Add('a', temp);
            temp = new char[] { 'g', 'v', 'n', 'h'};
            Neighbours.Add('b', temp);
            temp = new char[] { 'x', 'd', 'f', 'v' };
            Neighbours.Add('c', temp);
            temp = new char[] { 'w', 'e', 'r', 'f', 'c', 'x', 's' };
            Neighbours.Add('d', temp);
            temp = new char[] { 'w', 'r', 'f', 'd', 's' };
            Neighbours.Add('e', temp);
            temp = new char[] { 'e', 'r', 't', 'g', 'v', 'c', 'd' };
            Neighbours.Add('f', temp);
            temp = new char[] { 'r', 't', 'y', 'h', 'b', 'v', 'f' };
            Neighbours.Add('g', temp);
            temp = new char[] { 't', 'y', 'u', 'j', 'n', 'b', 'g' };
            Neighbours.Add('h', temp);
            temp = new char[] { 'u', 'o', 'k', 'l', 'j' };
            Neighbours.Add('i', temp);
            temp = new char[] { 'u', 'i', 'k', 'm', 'n', 'h' };
            Neighbours.Add('j', temp);
            temp = new char[] { 'u', 'i', 'o', 'l', 'm', 'j' };
            Neighbours.Add('k', temp);
            temp = new char[] { 'o', 'p', 'k' };
            Neighbours.Add('l', temp);
            temp = new char[] { 'j', 'k', 'l', 'n' };
            Neighbours.Add('m', temp);
            temp = new char[] { 'm', 'j', 'h', 'b' };
            Neighbours.Add('n', temp);
            temp = new char[] { 'p', 'i', 'k', 'l' };
            Neighbours.Add('o', temp);
            temp = new char[] { 'o', 'l' };
            Neighbours.Add('p', temp);
            temp = new char[] { 'w', 's', 'a' };
            Neighbours.Add('q', temp);
            temp = new char[] { 'e', 't', 'f', 'd', 'g' };
            Neighbours.Add('r', temp);
            temp = new char[] { 'w', 'a', 'd', 'q', 'e', 'x', 'z' };
            Neighbours.Add('s', temp);
            temp = new char[] { 'r', 'y', 'g', 'f', 'h' };
            Neighbours.Add('t', temp);
            temp = new char[] { 'y', 'i', 'j', 'h', 'k' };
            Neighbours.Add('u', temp);
            temp = new char[] { 'f', 'g', 'c', 'b' };
            Neighbours.Add('v', temp);
            temp = new char[] { 'q', 'e', 's', 'd', 'a' };
            Neighbours.Add('w', temp);
            temp = new char[] { 'z', 'a', 's', 'd', 'c' };
            Neighbours.Add('x', temp);
            temp = new char[] { 't', 'u', 'h', 'g', 'j' };
            Neighbours.Add('y', temp);
            temp = new char[] { 'a', 's', 'x' };
            Neighbours.Add('z', temp);
        }
    }
}