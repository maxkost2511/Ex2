using SampleHierarchies.Data;
using SampleHierarchies.Enums.ScreenId;
using SampleHierarchies.Gui.Animals;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System;
using System.Collections.Generic;

namespace SampleHierarchies.Gui
{
    /// <summary>
    /// Application main screen.
    /// </summary>
    public sealed class MainScreen : Screen
    {
        #region Properties And Ctor

        private const string MainScreenJsonPath = "mainScreen.json";

        private readonly SettingsService _settingsService;
        private readonly SettingsScreen _settingsScreen;
        private readonly AnimalsScreen _animalsScreen;
        private List<string> _msgHistory = new List<string>();

        public MainScreen(
            IScreenDefinitionService screenDefinitionService,
            AnimalsScreen animalsScreen,
            SettingsService settingsService,
            SettingsScreen settingsScreen)
            : base(screenDefinitionService)
        {
            _settingsScreen = settingsScreen;
            _settingsService = settingsService;
            _animalsScreen = animalsScreen;
            ScreenDefinitionJson = MainScreenJsonPath;
        }

        #endregion Properties And Ctor

        #region Public Methods

        public override void Show()
        {
            int selectedOption = 0;
            while (true)
            {
                _history.Clear();
                _history.Add(_screenDefinitionService.GetLineFromJson(MainScreenJsonPath, (int)MainScreenId.ReturnToMainScreen));
                Console.Clear();
                DisplayHistory();
                Console.WriteLine("Main Screen");
                Console.WriteLine("Your available choices are:");
                Console.WriteLine("Use arrow keys to navigate, Enter to select, Esc to exit.");
                Console.WriteLine();

                for (int i = 0; i < 2; i++)
                {
                    string tmp = _screenDefinitionService.GetLineFromJson(MainScreenJsonPath, (int)MainScreenId.ShowAnimals + i);
                    if (i == selectedOption)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine(tmp);
                    Console.ResetColor();
                }
                Console.WriteLine();
                DisplayMsgHistory();

                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (selectedOption > 0)
                            selectedOption--;
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedOption < 1)
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

        #endregion // Public Methods

        #region Private Methods

        private void HandleOption(int selectedOption)
        {
            switch (selectedOption)
            {
                case 0:
                    _animalsScreen.Show();
                    break;

                case 1:
                    _settingsScreen.Show();
                    break;
            }
        }

        private void DisplayHistory()
        {
            Console.WriteLine("History: " + string.Join(" -> ", _history));
            Console.WriteLine();
        }

        private void DisplayMsgHistory()
        {
            if (_msgHistory.Count > 0)
            {
                Console.WriteLine(string.Join(Environment.NewLine, _msgHistory));
                Console.WriteLine();
            }
        }

        #endregion // Private Methods
    }
}
