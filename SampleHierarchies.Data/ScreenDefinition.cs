using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data
{
    /// <summary>
    /// Represents a definition of a screen consisting of multiple line entries.
    /// </summary>
    public class ScreenDefinition
    {
        /// <summary>
        /// Gets or sets the list of screen line entries.
        /// </summary>
        public List<ScreenLineEntry> LineEntries { get; set; }

        /// <summary>
        /// Default constructor initializes the list of line entries.
        /// </summary>
        public ScreenDefinition()
        {
            LineEntries = new List<ScreenLineEntry>();
        }
    }
}
