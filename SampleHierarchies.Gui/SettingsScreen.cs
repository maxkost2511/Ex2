using SampleHierarchies.Enums;
using SampleHierarchies.Enums.ScreenId;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System;

namespace SampleHierarchies.Gui
{
    /// <summary>
    /// Represents the settings screen of the application.
    /// </summary>
    public sealed class SettingsScreen : Screen
    {
        private readonly ISettingsService _settingsService;

        public SettingsScreen(IScreenDefinitionService screenDefinitionService, ISettingsService settingsService)
            : base(screenDefinitionService)
        {
            _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
            ScreenDefinitionJson = "settingsScreen.json";
        }

        public override void Show()
        {
            int selectedOption = 0;
            while (true)
            {
                _history.Clear();
                _history.Add(_screenDefinitionService.GetLineFromJson("settingsScreen.json", (int)SettingsScreenId.NavigationToSettingsScreen));
                Console.Clear();
                DisplayHistory();
                Console.WriteLine("Settings Screen");
                Console.WriteLine("Use arrow keys to navigate, Enter to select, Esc to exit.");
                Console.WriteLine();

                for (int i = 0; i < 8; i++)
                {
                    string tmp = _screenDefinitionService.GetLineFromJson("settingsScreen.json", (int)SettingsScreenId.MainScreen + i);
                    if (i == selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine(tmp);
                    Console.ResetColor();
                }
                Console.WriteLine();

                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedOption > 0)
                            selectedOption--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedOption < 7)
                            selectedOption++;
                        break;

                    case ConsoleKey.Enter:
                        HandleOption(selectedOption);
                        break;

                    case ConsoleKey.Escape:
                        return;
                }
                Console.ResetColor();
            }
        }

        private void DisplayHistory()
        {
            Console.WriteLine("History: " + string.Join(" -> ", _history));
            Console.WriteLine();
        }

        private void HandleOption(int selectedOption)
        {

            // Validate user choice
            try
            {

                if (selectedOption >= 0 && selectedOption <= 7)
                {
                    GetConsoleColor((Screens)selectedOption);
                }
                else
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid choice. Try again.");
            }
        }

        private void GetConsoleColor(Screens screen)
        {
            Console.Clear();
            Console.WriteLine($"Changing Display Color for {screen}");
            Console.WriteLine();

            try
            {
                Console.Write("Enter a new color for the display: ");
                string colorAsString = Console.ReadLine();

                if (colorAsString is null)
                {
                    throw new ArgumentNullException();
                }

                if (Enum.TryParse(colorAsString, out ConsoleColor newScreenColor))
                {
                    _settingsService.UpdateConsoleColor(screen, newScreenColor);
                    Console.WriteLine($"Display color for {screen} updated to {newScreenColor}");
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input. Try again.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
