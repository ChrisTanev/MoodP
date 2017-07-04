using System;

namespace MP.Business.Implementation.NaiveBayes
{
    using System.Collections.Generic;
    using Facade;

    public class Classifier : IClassifier
    {
        public readonly SortedDictionary<string, ICategory> Categories;
        readonly ExcludedWords _excludedWords;

        public Classifier()
        {
            Categories = new SortedDictionary<string, ICategory>();
            _excludedWords = new ExcludedWords();
            _excludedWords.InitDefault();
        }

      
        int CountTotalWordsInCategories()
        {
            int total = 0;
            foreach (Category category in Categories.Values)
            {
                total += category.TotalWords;
            }
            return total;
        }

  
        public ICategory GetOrCreateCategory(string category)
        {
            ICategory c;
            if (!Categories.TryGetValue(category, out c))
            {
                c = new Category(category, _excludedWords);
                Categories.Add(category, c);
            }
            return c;
        }

        public void TeachPhrases(string category, string[] phrases)
        {
            GetOrCreateCategory(category).TeachPhrases(phrases);
        }

      
        public void TeachCategory(string category, System.IO.TextReader tr)
        {
            GetOrCreateCategory(category).TeachCategory(tr);
        }

    
        public Dictionary<string, double> Classify(string[] words)
        {
            var result = new Dictionary<string, double>();
            foreach (var category in Categories)
            {
                result.Add(category.Value.Name, 0.0);
            }

            var wordsInFile = new EnumerableCategory("", _excludedWords);
         wordsInFile.TeachPhrases(words);
            foreach (var word in wordsInFile)
            {
                var phrasesInAFile = word.Value;

                foreach (var category in Categories)
                {
                    ICategory categoryValue = category.Value;
                    int count = categoryValue.GetPhraseCount(phrasesInAFile.RawPhrase);
                    if (0 < count)
                    {
                        result[categoryValue.Name] += System.Math.Log((double) phrasesInAFile.Count/(double) categoryValue.TotalWords);
                    }
                    else
                    {
                        result[categoryValue.Name] += System.Math.Log(0.01/(double) categoryValue.TotalWords);
                    }

                }



                try
                {
                    foreach (var category in Categories)
                    {
                        ICategory categoryValue = category.Value;
                        result[categoryValue.Name] +=
                            System.Math.Log((double) categoryValue.TotalWords/
                                            (double) this.CountTotalWordsInCategories());
                    }
                }
                catch (Exception e)
                {


                }

              
            }
            return result;
        }
    }
}