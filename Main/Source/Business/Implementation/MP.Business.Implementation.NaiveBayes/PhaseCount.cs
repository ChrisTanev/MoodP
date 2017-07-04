namespace MP.Business.Implementation.NaiveBayes
{
    public class PhraseCount
    {
        string m_RawPhrase;

        public PhraseCount(string rawPhrase)
        {
            m_RawPhrase = rawPhrase;
            _count = 0;
        }

        public string RawPhrase
        {
            get { return m_RawPhrase; }

        }

        int _count;
        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }
    }
}