using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stemmer
{
    /// <summary>
    /// An indexer for text. Find's all root forms within the
    /// text and maintains a lookup of those root forms against
    /// their frequency.
    /// </summary>
    class TextIndexer
    {
        /// <summary>
        /// A dictionary used for storing root forms and 
        /// their frequencies within the text.
        /// </summary>
        private Dictionary<string, int> _stemFrequencies = new Dictionary<string, int>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">The text to create the indexer with.</param>
        public TextIndexer(string text)
        {
            text = text.Replace(".", "");

            // evaluate the text
            Stemmer stemmer = Stemmer.CreateStandardStemmer();
            foreach (string word in text.Split(new char[] {' '}))
            {
                // add the root form to the dictionary
                string stem = stemmer.findRootForm(word);
                if (_stemFrequencies.ContainsKey(stem))
                {
                    _stemFrequencies[stem] = _stemFrequencies[stem] + 1;
                }
                else
                {
                    _stemFrequencies[stem] = 1;
                }
            }
        }

        /// <summary>
        /// Get the frequency of a root form word within the text.
        /// </summary>
        /// <param name="word">The word in root form.</param>
        /// <returns>The number of occurences of the root form.</returns>
        public int Frequency(string word)
        {
            if (_stemFrequencies.ContainsKey(word))
                return _stemFrequencies[word];
            else
                return 0;
        }
    }
}
