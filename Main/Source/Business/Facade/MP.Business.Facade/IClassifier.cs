

namespace MP.Business.Facade
{
    using System.Collections.Generic;
    using System.IO;
    public interface IClassifier
    {


        void TeachPhrases(string cat, string[] phrases);



        void TeachCategory(string cat, TextReader tr);

        Dictionary<string, double> Classify(string[] words);
    }
}
