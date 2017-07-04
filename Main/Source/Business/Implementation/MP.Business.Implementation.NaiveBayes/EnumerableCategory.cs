



namespace MP.Business.Implementation.NaiveBayes
{
    using System.Collections;
    using System.Collections.Generic;
    class EnumerableCategory : Category, IEnumerable<KeyValuePair<string, PhraseCount>>
    {
        public EnumerableCategory(string category, ExcludedWords excludedWords) : base(category, excludedWords)
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<KeyValuePair<string, PhraseCount>> GetEnumerator()
        {
            return phrases.GetEnumerator();
        }

    }
}
