namespace MP.Business.Implementation.NaiveBayes
{

    using System.Collections.Generic;
    public class ExcludedWords
    {

        static string[] enu_most_common =
        {
            "the",
            "to",
            "and",
            "a",
            "an",
            "in",
            "is",
            "it",
            "you",
            "that",
            "was",
            "for",
            "on",
            "are",
            "with",
            "as",
            "be",
            "been",
            "at",
            "one",
            "have",
            "this",
            "what",
            "which",
        };

        Dictionary<string, int> m_Dict;

        public ExcludedWords()
        {
            m_Dict = new Dictionary<string, int>();
        }

        public void InitDefault()
        {
            Init(enu_most_common);
        }

        public void Init(string[] excluded)
        {
            m_Dict.Clear();
            for (int i = 0; i < excluded.Length; i++)
            {
                m_Dict.Add(excluded[i], i);
            }
        }

  
        public bool IsExcluded(string word)
        {
            return m_Dict.ContainsKey(word);
        }
    }
}