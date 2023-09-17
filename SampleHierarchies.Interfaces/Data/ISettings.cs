namespace SampleHierarchies.Interfaces.Data;
using Enums;

/// <summary>
/// Represents application settings that define the behavior and appearance of the application.
/// </summary>
public interface ISettings
{
    /// <summary>
    /// Gets or sets the colors associated with different screens in the application.
    /// </summary>
    Dictionary<Screens, ConsoleColor> ScreensColor { get; set; }
}