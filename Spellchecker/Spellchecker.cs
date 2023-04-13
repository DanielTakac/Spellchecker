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

            if (string.IsNullOrEmpty(word)) return true;

            string cleanedWord = Regex.Replace(word, "[^a-zA-Z]+", "").ToLower();
            
            if (cleanedWord.Length == 0) return true;

            return dictionary.Contains(cleanedWord);

        }

        public bool CheckText(string text, bool printResult = false, bool printSuggestions = true) {

            if (string.IsNullOrEmpty(text)) return true;

            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> badWords = new List<string>();

            for (int i = 0; i < words.Length; i++) {

                if (!CheckWord(words[i])) {

                    badWords.Add(words[i]);

                }

            }

            if (printResult) PrintCheckedText(words, badWords, printSuggestions);

            if (badWords.Count > 0) return false;

            return true;

        }

        public void PrintCheckedText(string[] words, List<string> badWords, bool printSuggestions = true) {

            if (words == null) return;

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

            if (!printSuggestions) return;

            for (int i = 0; i < badWords.Count; i++) {

                SuggestCorrection(badWords[i]);

                Console.WriteLine();

            }

        }

        private int GetHammingDistance(string word1, string word2) {

            if (string.IsNullOrEmpty(word1) || string.IsNullOrEmpty(word2)) return -1;

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

        public Dictionary<string, int> SuggestCorrection(string word, int maxDistance = 2, int maxSuggestions = 3) {

            if (string.IsNullOrEmpty(word)) return new Dictionary<string, int>();

            string cleanedWord = Regex.Replace(word, "[^a-zA-Z]+", "").ToLower();

            if (dictionary.Contains(cleanedWord)) {

                Console.WriteLine("Word is already correct");
                return new Dictionary<string, int>();

            }

            Dictionary<string, int> suggestions = new Dictionary<string, int>();

            for (int i = 0; i < dictionary.Length; i++) {

                if (dictionary[i].Length != cleanedWord.Length) continue;

                int distance = GetHammingDistance(cleanedWord, dictionary[i]);

                if (distance <= maxDistance && !suggestions.ContainsKey(dictionary[i].ToLower())) {

                    suggestions[dictionary[i].ToLower()] = distance;

                }

            }

            var sortedSuggestions = suggestions.OrderBy(x => x.Value).Take(maxSuggestions).ToDictionary(x => x.Key, x => x.Value);

            Console.WriteLine("-----------------");

            Console.WriteLine("Word: " + cleanedWord);

            foreach (var suggestion in sortedSuggestions) {

                Console.WriteLine(suggestion.Key + " - " + suggestion.Value);

            }

            if (sortedSuggestions.Count == 0) {

                Console.WriteLine("No suggestions found...");

            }

            Console.WriteLine("-----------------");

            return sortedSuggestions;

        }

    }

}
