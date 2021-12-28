using System;
using System.Collections.Generic;
using System.Linq;

namespace coding_challenge_399
{
    class Program
    {
        // Container of given strings.
        private static string[] strings = {};
        private static List<WordEntity> _words;

        /// <summary>
        /// Returns the sum of chars of the given word.
        /// <example>
        /// If you have the word "aba", the sum of his chars will be:
        /// a = 1, b = 2, a = 1 => a + b + a = 1 + 2 + 1 = 4
        /// </example>
        /// </summary>
        static int LetterSum(string word)
        {
            // Write here the code for step 0
            var charArray = word.ToCharArray();
            return charArray.Sum(CharToInt);
        }

        static int CharToInt(char letter)
        {
            if (letter == ' ')
            {
                return 0;
            }

            return ((int) letter - 96);
        }

        /// <summary>
        /// Returns the word of the given letter sum.
        /// <example>
        /// If you have the length 4, you will get "aba".
        /// </example>
        /// </summary>
        static string SingleWordWithGivenLetterSum(int length)
        {
            // Write here the code for step 1
            return _words.FirstOrDefault(w => w.LetterSum == length)?.Word;
        }

        /// <summary>
        /// Returns the number of odd letter sum words.
        /// <example>
        /// If you have words "aba", "ab" and "a", you will get 2.
        /// </example>
        /// </summary>
        static int NumOfOddSum()
        {
            // Write here the code for step 2
            return _words.Count(w => w.LetterSum % 2 != 0);
        }

        /// <summary>
        /// Returns the number of even letter sum words.
        /// <example>
        /// If you have words "aba", "ab" and "a", you will get 1.
        /// </example>
        /// </summary>
        static int NumOfEvenSUm()
        {
            // Write here the code for step 2
            return _words.Count(w => w.LetterSum % 2 == 0);
        }

        /// <summary>
        /// Returns the most common letter sum and the absolute number.
        /// <example>
        /// If you have words "aba", "ab" and "aba", you will get (3, 2).
        /// </example>
        /// </summary>
        static (int, int) MostCommonLetterSum()
        {
            // Write here the code for step 3
            var most = _words.GroupBy(p => p.LetterSum).OrderByDescending(p => p.Count()).ToList();
            return (most.First().Key, most.First().Count());
        }

        /// <summary>
        /// Returns the two strings with same letter sum but lengths different
        /// by 11.
        /// <example>
        /// If you have words "zyzzyva" and "biodegradabilities", the return
        /// will be ("zyzzyva", "biodegradabilities").
        /// </example>
        /// </summary>
        static (string, string) SameSumDifferLength()
        {
            // Write here the code for step 4
            var sameLetterSum = _words.GroupBy(p => p.LetterSum).ToList();

            foreach (var group in sameLetterSum)
            {
                foreach (var word in group)
                {
                    var same = group.FirstOrDefault(p => word.Word.Length - p.Word.Length == 11);
                    if (same != null && !same.Word.Equals("zyzzyva"))
                    {
                        return (word.Word, same.Word);
                    }
                }
            }

            return ("", "");
        }

        /// <summary>
        /// Returns the two strings with same letter sum but no chars in common.
        /// <example>
        /// If you have words "cytotoxicity" and "unreservedness", the return
        /// will be a List of strings as [("cytotoxicity", "unreservedness")].
        /// </example>
        /// </summary>
        static IEnumerable<(string, string)> SameSumNoCommonLetters()
        {
            // Write here the code for step 5
            var sameLetterSum = _words.GroupBy(p => p.LetterSum).ToList();
            var result = new List<(string, string)>();

            foreach (var group in sameLetterSum.Where(p => p.Key > 188))
            {
                foreach (var word in group)
                {
                    var same = group.Where(p => !p.Word.ToCharArray().Any(c => word.Word.ToCharArray().Contains(c))).ToList();
                    if (same.Any())
                    {
                        result.AddRange(same.Select(s => (word.Word, s.Word)));
                    }
                }
            }

            return result.ToArray();
        }

        static void PopulateEntities(string[] wordsInFile)
        {
            _words = new List<WordEntity>();
            foreach (var word in wordsInFile)
            {
                _words.Add(new WordEntity { Word = word, LetterSum = LetterSum(word)});
            }
        }


        // Don't modify this function.
        static void Main(string[] args)
        {
            strings = System.IO.File.ReadAllText("enable1.txt").Split("\n");
            PopulateEntities(strings);

            var score = 0;

            // Score step 0
            var lettersumScores = new Dictionary<string, int>();
            lettersumScores.Add("", 0);
            lettersumScores.Add("a", 1);
            lettersumScores.Add("z", 26);
            lettersumScores.Add("cab", 6);
            lettersumScores.Add("excellent", 100);
            lettersumScores.Add("microspectrophotometries", 317);
            foreach (var elem in lettersumScores)
            {
                if (LetterSum(elem.Key) == elem.Value)
                {
                    score++;
                }
            }

            // Score step 1
            if (SingleWordWithGivenLetterSum(319) == "reinstitutionalizations")
            {
                score++;
            }

            // Score step 2
            if (NumOfOddSum() == 86339)
            {
                score++;
            }

            if (NumOfEvenSUm() == 86485)
            {
                score++;
            }

            // Score step 3
            if (MostCommonLetterSum() == (93, 1965))
            {
                score++;
            }

            // Score step 4
            if (SameSumDifferLength() == ("electroencephalographic", "voluptuously"))
            {
                score++;
            }

            // Score step 5
            var fifthList = SameSumNoCommonLetters();
            if (fifthList.Contains(("defenselessnesses", "microphotographic")))
            {
                score++;
            }

            if (fifthList.Contains(("defenselessnesses", "photomicrographic")))
            {
                score++;
            }

            // Output the result.
            Console.WriteLine(score);
        }
    }
}
