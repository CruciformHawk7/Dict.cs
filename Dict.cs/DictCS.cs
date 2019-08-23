using System.IO;
using System;
using System.Collections.Generic;

namespace DictCS {
    public class Dict {
        private string dictFile;
        public Dictionary<string, List<string>> myDictionary { get; set; }

        public Dict(string inputDictonaryFile = "Dictionary.txt") {
            dictFile = inputDictonaryFile;
            myDictionary = readFile();
        }

        private Dictionary<string, List<string>> readFile() {
            Dictionary<string, List<string>> dictionaryVals = new Dictionary<string, List<string>>();
            string textInFile = File.ReadAllText(dictFile);
            string[] seperators = { "\n", "\r", "\n\r" };
            string[] words = textInFile.Split(seperators, StringSplitOptions.None);
            foreach (var word in words) {
                string sortedWord = sortText(word);
                if (!dictionaryVals.ContainsKey(sortedWord)) {
                    var newList = new List<string>();
                    newList.Add(word);
                    dictionaryVals.Add(sortedWord, newList);
                } else {
                    var valueWords = dictionaryVals[sortedWord];
                    valueWords.Add(word);
                    dictionaryVals[sortedWord] = valueWords;
                }
            }
            return dictionaryVals;
        }

        private string sortText(string text) {
            var temp = text.ToCharArray();
            for (int i=0; i<text.Length; i++) {
                for (int j=0; j<text.Length-i-1; j++) {
                    if (temp[j]>temp[j+1]) {
                        var t = temp[j];
                        temp[j] = temp[j + 1];
                        temp[j + 1] = t;
                    }
                }
            }
            return new string(temp);
        }
    }
}
