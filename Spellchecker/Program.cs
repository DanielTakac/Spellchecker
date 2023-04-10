using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Spellchecker {
    
    class Program {

        static void Main(string[] args) {

            string[] dictionary = File.ReadAllLines(@"D:\100k.txt");    

            var checker = new Spellchecker(dictionary);

            string word = "charecter";

            string sentence = "This ise a sentence with charecters, like 123 amd %$#!";

            checker.CheckSentence(sentence, true);

            checker.SuggestCorrection(word);

            Console.ReadKey();

        }
    
    }

}
