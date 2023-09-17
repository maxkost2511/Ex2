using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SampleHierarchies.Data
{
    /// <summary>
    /// Represents a single line on the screen.
    /// </summary>
    public class ScreenLineEntry
    {
        /// <summary>
        /// Gets or sets the background color of the screen line.
        /// </summary>
        [JsonProperty("BgColor")]
        public ConsoleColor BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the foreground color of the screen line.
        /// </summary>
        [JsonProperty("FrColor")]
        public ConsoleColor ForegroundColor { get; set; }

        /// <summary>
        /// Gets or sets the text content of the screen line.
        /// </summary> 
        [JsonProperty("Text")]
        public string? Text { get; set; }
    }
}
