using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stemmer
{
    /// <summary>
    /// A rule for a stemmer.
    /// </summary>
    class StemRule
    {
        /// <summary>
        /// The ending of the word which indicates use of this rule.
        /// </summary>
        public string Ending { get; set; }

        /// <summary>
        /// The replacement for the ending if the rule is applied.
        /// </summary>
        public string Replacement { get; set; }

        /// <summary>
        /// Should the stemmer terminate the stemming process if (and once)
        /// this rule is applied.
        /// </summary>
        public bool Terminate { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ending">The ending to match against.</param>
        /// <param name="replacement">The replacement of the ending on application.</param>
        /// <param name="terminate">Terminate the stemming process on application.</param>
        public StemRule(string ending, string replacement, bool terminate)
        {
            this.Ending = ending;
            this.Replacement = replacement;
            this.Terminate = terminate;
        }
    }
}
