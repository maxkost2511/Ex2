using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;


namespace SampleHierarchies.Data
{
    /// <summary>
    /// Represents the settings for the application.
    /// </summary>
    public class Settings : ISettings
    {
        private static readonly Settings instance = new Settings();

        /// <summary>
        /// Gets the singleton instance of the <see cref="Settings"/> class.
        /// </summary>
        public static Settings Instance => instance;

        /// <summary>
        /// Gets or sets the mapping of screen types to console colors.
        /// </summary>
        public Dictionary<Screens, ConsoleColor> ScreensColor { get; set; } = new Dictionary<Screens, ConsoleColor>();
    }
}
