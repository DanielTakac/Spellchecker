using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Spellchecker {

    class Spellchecker {

        private string[] dictionary;

        public Spellchecker() {

            this.dictionary = new string[0];

        }

        public Spellchecker(string[] dictionary) {

            this.dictionary = dictionary;

        }

        public void SetDictionary(string[] dictionary) {

            this.dictionary = dictionary;

        }

        public bool CheckWord(string word) {

            string cleanedWord = Regex.Replace(word, "[^a-zA-Z]+", "").ToLower();

            if (cleanedWord.Length == 0) return true;

            for (int i = 0; i < dictionary.Length; i++) {

                if (dictionary[i] == cleanedWord) {

                    return true;

                }

            }

            return false;

        }

        public bool CheckSentence(string sentence, bool printResult = false) {

            string[] words = sentence.Split(new char[] { });

            List<string> badWords = new List<string> { };

            for (int i = 0; i < words.Length; i++) {

                if (!CheckWord(words[i])) {

                    badWords.Add(words[i]);

                }

            }

            if (printResult) PrintCheckedSentence(words, badWords);

            if (badWords.Count > 0) return false;

            return true;

        }

        public void PrintCheckedSentence(string[] words, List<string> badWords) {

            for (int i = 0; i < words.Length; i++) {

                if (badWords.Contains(words[i])) {

                    Console.ForegroundColor = ConsoleColor.Red;

                } else {

                    Console.ForegroundColor = ConsoleColor.Green;

                }

                Console.Write(words[i] + " ");

            }

            Console.ResetColor();

            Console.WriteLine();

        }

    }

}
