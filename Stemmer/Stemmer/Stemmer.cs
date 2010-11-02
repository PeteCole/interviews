using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stemmer
{
    /// <summary>
    /// A stemmer for reducing words to their stem. This uses a simple suffix
    /// stripping algorithm.
    /// </summary>
    class Stemmer
    {
        /// <summary>
        /// The rule store for the stemmer.
        /// </summary>
        /// <remarks>
        /// This is a list purely to allow a simple implementation of
        /// a suffix stripping stemmer. It would be best to replace this
        /// with a tree structure as in the Paice/Husk stemmer implementation.
        /// </remarks>
        List<StemRule> _rules = new List<StemRule>();

        /// <summary>
        /// Add a rule to the rules collection for the stemmer.
        /// </summary>
        /// <param name="rule">The rule to add.</param>
        public void AddRule(StemRule rule)
        {
            _rules.Add(rule);
        }

        /// <summary>
        /// Find the root form of a word.
        /// </summary>
        /// <param name="word">The word to find the root form of.</param>
        /// <returns>The root form. This may be the original word.</returns>
        public string findRootForm(string word)
        {
            string stem = word.ToLower();
            bool findingStem = true;
            while (findingStem)
            {
                // find the next rule
                StemRule ruleToApply = null;
                foreach (StemRule rule in _rules)
                {
                    if (stem.EndsWith(rule.Ending))
                        if (CanApplyRule(stem, rule))
                        {
                            ruleToApply = rule;
                            break;
                        }
                }

                // have we found a rule to apply? If not, we've found the stem
                if (ruleToApply == null)
                    break;

                // apply the rule
                stem = ApplyRule(stem, ruleToApply);

                // terminate if the rule requires termination
                findingStem =!ruleToApply.Terminate;
            }
            return stem;
        }

        /// <summary>
        /// Apply a rule to a stem to create a new stem.
        /// </summary>
        /// <param name="stem">The stem to apply the rule to.</param>
        /// <param name="rule">The rule to apply to the stem.</param>
        /// <returns>A new stem.</returns>
        private string ApplyRule(string stem, StemRule rule)
        {
            return stem.Substring(0, stem.Length - rule.Ending.Length) + rule.Replacement;
        }

        /// <summary>
        /// Check if a rule can be applied to a stem.
        /// </summary>
        /// <remarks>
        /// The application of a rule to a stem may not always result
        /// in a stem that makes sense linguistically. This check is 
        /// an attempt to reduce the error in the stemming process.
        /// </remarks>
        /// <param name="stem">The stem to check application against.</param>
        /// <param name="rule">The rule to check.</param>
        /// <returns>True if the rule can be applied, false otherwise.</returns>
        private bool CanApplyRule(string stem, StemRule rule)
        {
            string newStem = ApplyRule(stem, rule);
            return WordContainsVowel(newStem);
        }

        /// <summary>
        /// Check if a word contains a vowel.
        /// </summary>
        /// <param name="word">The word to check.</param>
        /// <returns>True if the word contains a vowel, false otherwise.</returns>
        private bool WordContainsVowel(string word)
        {
            foreach (char c in word.ToLower())
            {
                if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Create an instance of a stemmer and include some standard rules.
        /// </summary>
        /// <returns>A new stemmer.</returns>
        public static Stemmer CreateStandardStemmer()
        {
            Stemmer s = new Stemmer();
            s.AddRule(new StemRule("ss", "ss", true));
            s.AddRule(new StemRule("s", "", false));
            s.AddRule(new StemRule("ly", "", false));
            s.AddRule(new StemRule("y", "", false));
            s.AddRule(new StemRule("lie", "", false));
            s.AddRule(new StemRule("e", "", false));
            return s;
        }
    }
}
