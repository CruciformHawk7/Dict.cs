using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictCS;

namespace DictCSTester {
    class Program {
        static BasicDictionary BD2;
        static Dict BD;

        static void ShowHelp() {
            Console.WriteLine("Commands:");

            Console.WriteLine("\tisWord <word>");
            Console.WriteLine("\t\t-lets you check if the word is in the dictionary.");

            Console.WriteLine("\tanagramOf <word>");
            Console.WriteLine("\t\t-lists all the anagrams of the given word.");

            Console.WriteLine("\thelp");
            Console.WriteLine("\t\t-shows you this help message.");
        }

        static void ShowDictionary() {
            foreach(var v in BD.myDictionary){
                Console.Write(v.Key);
                foreach(var p in v.Value)  {
                    Console.Write(" {0}", p);
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args) {
            BD2 = new BasicDictionary();
            BD = new Dict();
            while (true) {
                Console.Write("> ");
                string Command = Console.ReadLine();
                string[] separator = { " " };
                string[] wordsInCmd = Command.Split(separator, StringSplitOptions.None);
                switch(wordsInCmd[0].ToLower())
                {
                    case "isword":
                        bool isword = BD.IsWord(wordsInCmd[1].ToLower());
                        if (!isword) BD.SuggestedWords(wordsInCmd[1]);
                        Console.WriteLine(isword);
                        break;
                    case "anagramof":
                        var op = (BD.AnagramsOf(wordsInCmd[1].ToLower()));
                        foreach (var p in op) Console.Write("{0} ", p);
                        Console.WriteLine();
                        break;
                    case "help":
                        ShowHelp();
                        break;
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Invalid Command!");
                        break;
                }
            }
        }
    }
}
