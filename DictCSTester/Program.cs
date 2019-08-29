using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictCS;

namespace DictCSTester {
    class Program {
        static BasicDictionary BD;
        static void Main(string[] args) {
            BD = new BasicDictionary();
            while (true) {
                //foreach(var v in BD.myDictionary)
                //{
                //    Console.Write(v.Key);
                //    foreach(var p in v.Value)
                //    {
                //        Console.Write(" {0}", p);
                //    }
                //    Console.WriteLine();
                //}
                Console.Write("> ");
                string Command = Console.ReadLine();
                string[] separator = { " " };
                string[] wordsInCmd = Command.Split(separator, StringSplitOptions.None);
                switch(wordsInCmd[0].ToLower())
                {
                    case "isword":
                        Console.WriteLine(BD.IsWord(wordsInCmd[1].ToLower()));
                        break;
                    case "anagramof":
                        var op = (BD.AnagramsOf(wordsInCmd[1].ToLower()));
                        foreach (var p in op) Console.Write("{0} ", p);
                        Console.WriteLine();
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
