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
    /// Animals main screen.
    /// </summary>
    public sealed class AnimalsScreen : Screen
    {
        #region Properties And Ctor

        private const string AnimalScreenJsonPath = "animalScreen.json";

        private readonly IDataService _dataService;
        private readonly MammalsScreen _mammalsScreen;
        private readonly SettingsService _settingsService;
        private List<string> _msgHistory = new List<string>();

        public AnimalsScreen(IScreenDefinitionService screenDefinitionService, IDataService dataService, MammalsScreen mammalsScreen, SettingsService settingsService)
            : base(screenDefinitionService)
        {
            _dataService = dataService;
            _mammalsScreen = mammalsScreen;
            _settingsService = settingsService;
            ScreenDefinitionJson = AnimalScreenJsonPath;
        }

        #endregion Properties And Ctor

        #region Public Methods

        public override void Show()
        {
            int selectedOption = 0;
            while (true)
            {
                _history.Clear();
                _history.Add(_screenDefinitionService.GetLineFromJson(AnimalScreenJsonPath, (int)AnimalScreenId.NavigateToAnimalsScreen));
                Console.Clear();
                DisplayHistory();
                Console.WriteLine("Animals Screen");
                Console.WriteLine("Use arrow keys to navigate, Enter to select, Esc to go back.");
                Console.WriteLine();

                for (int i = 0; i < 3; i++)
                {
                    string tmp = _screenDefinitionService.GetLineFromJson(AnimalScreenJsonPath, (int)AnimalScreenId.ShowAnimals + i);
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
                        if (selectedOption < 2)
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
                    _mammalsScreen.Show();
                    break;

                case 1:
                    SaveToFile();
                    break;

                case 2:
                    ReadFromFile();
                    break;

                case 3:
                    _msgHistory.Clear();
                    _msgHistory.Add("Going back to the parent menu.");
                    return;
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

        private void SaveToFile()
        {
            _msgHistory.Clear();
            try
            {
                Console.Write("Save data to file: ");
                string? fileName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    throw new ArgumentNullException(nameof(fileName));
                }
                _dataService.Write(fileName);
                _msgHistory.Add($"Data saved to '{fileName}' successfully.");
            }
            catch (Exception ex)
            {
                _msgHistory.Add($"Error: {ex.Message}");
            }
        }

        private void ReadFromFile()
        {
            _msgHistory.Clear();
            try
            {
                Console.Write("Read data from file: ");
                string? fileName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    throw new ArgumentNullException(nameof(fileName));
                }
                _dataService.Read(fileName);
                _msgHistory.Add($"Data read from '{fileName}' successfully.");
            }
            catch (Exception ex)
            {
                _msgHistory.Add($"Error: {ex.Message}");
            }
        }

        #endregion // Private Methods
    }
}
