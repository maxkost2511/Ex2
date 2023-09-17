using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;

namespace SampleHierarchies.Interfaces.Services;

/// <summary>
/// Represents a service for managing application settings.
/// </summary>
public interface ISettingsService
{
    #region Interface Members

    /// <summary>
    /// Read settings.
    /// </summary>
    /// <param name="jsonPath">Json path</param>
    /// <returns></returns>
    ISettings? Read(string jsonPath);

    /// <summary>
    /// Write settings.
    /// </summary>
    /// <param name="settings">Settings to written</param>
    /// <param name="jsonPath">Json path</param>
    void Write(ISettings settings, string jsonPath);

    /// <summary>
    /// Retrieves the console color for a specific screen.
    /// </summary>
    /// <param name="screen">The screen for which to retrieve the console color.</param>
    void GetConsoleColor(Screens screen);

    /// <summary>
    /// Updates the console color for a specific screen.
    /// </summary>
    /// <param name="screen">The screen for which to update the console color.</param>
    /// <param name="consoleColor">The new console color for the screen.</param>
    void UpdateConsoleColor(Screens screen, ConsoleColor consoleColor);

    #endregion // Interface Members
}
