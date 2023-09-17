using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using System;
using System.IO;
using System.Text.Json;

namespace SampleHierarchies.Services;

/// <summary>
/// Service for managing application settings.
/// </summary>
public class SettingsService : ISettingsService
{
    #region ISettings Implementation

    /// <inheritdoc/>
    /// <summary>
    /// Read application settings from a JSON file.
    /// </summary>
    public ISettings? Read(string jsonPath)
    {
        try
        {
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"{jsonPath} not found");
                return null;
            }

            if (jsonPath is null)
            {
                throw new ArgumentNullException(nameof(jsonPath));
            }

            string? jsonSource = File.ReadAllText(jsonPath);
            var jsonContent = JsonSerializer.Deserialize<Settings>(jsonSource);

            if (jsonContent == null)
            {
                throw new ArgumentNullException(nameof(jsonContent));
            }

            return jsonContent;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Data reading was not successful: " + ex.Message);
            return new Settings();
        }
    }

    /// <inheritdoc/>
    /// <summary>
    /// Write application settings to a JSON file.
    /// </summary>
    public void Write(ISettings settings, string? jsonPath)
    {
        try
        {
            if (jsonPath is null)
            {
                throw new ArgumentNullException(nameof(jsonPath));
            }

            File.WriteAllText(jsonPath, JsonSerializer.Serialize(settings));
            Console.WriteLine($"Data saved by path: '{jsonPath}' successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Data saving was not successful: " + ex.Message);
        }
    }

    /// <summary>
    /// Loads the console color for a specific screen.
    /// </summary>
    public void GetConsoleColor(Screens screen)
    {
        try
        {
            ISettings? settings = Read("SavedColors.json");

            if (settings != null && settings.ScreensColor.TryGetValue(screen, out ConsoleColor consoleColor))
            {
                Console.ForegroundColor = consoleColor;
            }
            else
            {
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Console.ResetColor();
            Console.WriteLine("Data reading from JSON was not successful: " + ex.Message);
        }
    }

    /// <summary>
    /// Updates the console color for a specific screen.
    /// </summary>
    public void UpdateConsoleColor(Screens screen, ConsoleColor newConsoleColor)
    {
        ISettings? settings = Read("SavedColors.json");

        if (settings == null)
        {
            settings = new Settings();
        }

        settings.ScreensColor[screen] = newConsoleColor;
        Write(settings, "SavedColors.json");
    }

    #endregion // ISettings Implementation
}