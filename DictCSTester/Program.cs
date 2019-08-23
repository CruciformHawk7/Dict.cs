using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictCS;

namespace DictCSTester {
    class Program {
        static void Main(string[]) {
            BasicDictionary cs = new BasicDictionary();
            var t = cs.myDictionary;
            Console.WriteLine("Size of t: {0}", t.Count);
            foreach (var item in t) {
                Console.Write("Key: {0}, Value: {{ ", item.Key);
                foreach (var listItem in item.Value) 
                    Console.Write("{0} ", listItem);
                Console.WriteLine("}");
            }
            Console.Read();
        }
    }
}
