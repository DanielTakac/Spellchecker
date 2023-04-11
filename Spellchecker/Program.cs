using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Spellchecker {
    
    class Program {

        static void Main(string[] args) {

            string[] dictionary = File.ReadAllLines(@".\100k.txt");    

            var checker = new Spellchecker(dictionary);

            string sentence = "This ise a sentence with charecters, like 123 amd %$#!";

            string text = "The qick brown fox jumps over the lazy dog. Th dog is not very happy about it. He would like to jum over the fence and chase the fox. But he's too lazy.";

            checker.CheckText(sentence, true);

            Console.ReadKey();

        }
    
    }

}
