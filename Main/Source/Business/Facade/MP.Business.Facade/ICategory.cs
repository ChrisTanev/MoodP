

namespace MP.Business.Facade
{
    using System.IO;
    public interface ICategory
    {
        string Name { get; }
    

        void Reset();


        int GetPhraseCount(string phrase);

       
        void TeachCategory(TextReader reader);
      

        void TeachPhrase(string rawPhrase);


        void TeachPhrases(string[] words);
  

        int TotalWords { get; }
    }
}
