using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stemmer
{
    class Program
    {
        static void Main(string[] args)
        {
            // We want a text indexer which will allow
            // easy access to all roots within the text.
            string text = @"Friends are friendlier friendlies that are friendly and classify the friendly classification class. Flowery flowers flow through following the flower flows";
            TextIndexer indexer = new TextIndexer(text);

            // tests
            Test("following", indexer);
            Test("flow", indexer);
            Test("classification", indexer);
            Test("class", indexer);
            Test("flower", indexer);
            Test("friend", indexer);
            Test("friendly", indexer);
            Test("classes", indexer);

            // finish
            Console.Read();
        }

        /// <summary>
        /// Test a word 
        /// </summary>
        /// <param name="word">The word to check.</param>
        /// <param name="indexer">The indexer for the text.</param>
        static void Test(string word, TextIndexer indexer)
        {
            // find the root form of the word
            Stemmer stemmer = Stemmer.CreateStandardStemmer();
            word = stemmer.findRootForm(word);

            // check the frequency of the root form within the indexer (text)
            Console.WriteLine("frequency(\"{0}\") = {1}", word, indexer.Frequency(word)); 
        }
    }
}
