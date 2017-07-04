

namespace MP.Business.Implementation.NaiveBayes
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using MP.Business.Facade;
    using System.IO;
    public class Category : ICategory
    {
        protected SortedDictionary<string, PhraseCount> phrases;
        int _totalWords;
        readonly string _name;
        readonly ExcludedWords _excludedWords;

        public Category(string category, ExcludedWords excludedWords)
        {
            phrases = new SortedDictionary<string, PhraseCount>();
            _excludedWords = excludedWords;
            _name = category;
        }
        public string Name => _name;

        /// <value>
        /// Occurences</value>
        public int TotalWords
        {
            get { return _totalWords; }
       
        }

     

        /// <summary>
        /// Gets a Count for Phrase or 0 if not present<\summary>
        public int GetPhraseCount(string phrase)
        {
            PhraseCount phraseCount;
            if (phrases.TryGetValue(phrase, out phraseCount))
                return phraseCount.Count;
            else
                return 0;
        }

        /// <summary>
        /// Reset all trained data<\summary>
        public void Reset()
        {
            _totalWords = 0;
            phrases.Clear();
        }

       SortedDictionary<string, PhraseCount> Phrases
        {
            get { return phrases; }

        }
        
        public void TeachCategory(TextReader reader)
        {
       
            Regex regex = new Regex(@"(\w+)\W*", RegexOptions.Compiled);
            string line;
            while (null != (line = reader.ReadLine()))
            {
                Match m = regex.Match(line);
                while (m.Success)
                {
                    string word = m.Groups[1].Value;
                    TeachPhrase(word);
                    m = m.NextMatch();
                }
            }
        }


        public void TeachPhrases(string[] words)
        {
            foreach (string word in words)
            {
                TeachPhrase(word);
            }
        }

        public void TeachPhrase(string rawPhrase)
        {
            if ((null != _excludedWords) && (_excludedWords.IsExcluded(rawPhrase)))
                return;

            PhraseCount phraseCount;
            string phrase = DePhrase(rawPhrase);
            if (!phrases.TryGetValue(phrase, out phraseCount))
            {
                phraseCount = new PhraseCount(rawPhrase);
                phrases.Add(phrase, phraseCount);
            }
            phraseCount.Count++;
            _totalWords++;
        }

        static Regex _phraseRegEx = new Regex(@"\W", RegexOptions.Compiled);

  
        public static bool CheckIsPhrase(string s)
        {
            return _phraseRegEx.IsMatch(s);
        }

      
        public static string DePhrase(string s)
        {
            return _phraseRegEx.Replace(s, @"");
        }
    }
}