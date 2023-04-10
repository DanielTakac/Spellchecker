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

        private int GetHammingDistance(string word1, string word2) {

            if (word1.Length != word2.Length) return -1;

            word1 = word1.ToLower();
            word2 = word2.ToLower();

            int distance = 0;

            for (int i = 0; i < word1.Length; i++) {

                if (word1[i] != word2[i]) {

                    distance++;

                }

            }

            return distance;

        }

        public void SuggestCorrection(string word) {

            if (dictionary.Contains(word)) {

                Console.WriteLine("Word is already correct");
                return;

            }

            Dictionary<string, int> suggestions = new Dictionary<string, int>();

            for (int i = 0; i < dictionary.Length; i++) {

                if (dictionary[i].Length != word.Length) continue;

                if (GetHammingDistance(word, dictionary[i]) <= 4 && !suggestions.Contains(dictionary[i].ToLower())) {

                    suggestions.Add(dictionary[i].ToLower());

                }

            }

            suggestions.Sort();

            Console.WriteLine("-----------------");

            Console.WriteLine("Word: " + word);

            for (int i = 0; i < suggestions.Count; i++) {

                Console.WriteLine(suggestions[i] + " - " + GetHammingDistance(word, suggestions[i]));

            }

            Console.WriteLine("-----------------");

        }

    }

}
