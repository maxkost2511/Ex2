using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums.ScreenId;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleHierarchies.Gui.Animals
{
    /// <summary>
    /// Mammals main screen.
    /// </summary>
    public class LionScreen : Screen
    {
        #region Properties And Ctor

        private const string LionScreenJsonPath = "lionScreen.json";

        private readonly IDataService _dataService;
        private readonly SettingsService _settingsService;
        private List<string> _msgHistory = new List<string>();

        public LionScreen(IScreenDefinitionService screenDefinitionService, IDataService dataService, SettingsService settingsService)
            : base(screenDefinitionService)
        {
            _dataService = dataService;
            _settingsService = settingsService;
            ScreenDefinitionJson = LionScreenJsonPath;
        }

        #endregion Properties And Ctor

        #region Public Methods

        /// <summary>
        /// Show the screen.
        /// </summary>
        public override void Show()
        {
            int selectedOption = 0;

            while (true)
            {
                Console.Clear();
                Console.ResetColor();
                _history.Clear();
                _history.Add(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.NavigateToLionsScreen));
                DisplayHistory();
                Console.WriteLine("Lion Screen");
                Console.WriteLine("Use arrow keys to navigate, Enter to select, Esc to go back.");

                for (int i = 0; i < 4; i++)
                {
                    string tmp = _screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.ListAllLions + i);
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
                        if (selectedOption < 3)
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
                    ListLion();
                    break;

                case 1:
                    AddLion();
                    break;

                case 2:
                    DeleteLion();
                    break;

                case 3:
                    EditLionMain();
                    break;

                case 4:
                    _msgHistory.Clear();
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.ReturnToParentMenu));
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

        private void ListLion()
        {
            _msgHistory.Clear();
            if (_dataService?.Animals?.Mammals?.Lion is not null &&
                _dataService.Animals.Mammals.Lion.Count > 0)
            {
                Console.WriteLine(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.DisplayListOfLions));
                int i = 1;
                foreach (Lion lion in _dataService.Animals.Mammals.Lion)
                {
                    Console.WriteLine(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.DisplayLionNumber, i.ToString()));
                    lion.Display();
                    i++;
                }
            }
            else
            {
                Console.WriteLine(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.DisplayEmptyListOfLions));
            }
            Console.ReadLine();
        }

        private void AddLion()
        {
            _msgHistory.Clear();
            try
            {
                Lion lion = AddEditLion();
                _dataService?.Animals?.Mammals?.Lion?.Add(lion);
                _msgHistory.Add(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.LionAddedToTheList, lion.Name));
            }
            catch (Exception ex)
            {
                _msgHistory.Add($"Error: {ex.Message}");
            }
        }

        private void DeleteLion()
        {
            _msgHistory.Clear();
            try
            {
                _msgHistory.Add(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.PromptLionNameForDeletion));
                DisplayMsgHistory();
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Lion? lion = (Lion?)(_dataService?.Animals?.Mammals?.Lion
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (lion is not null)
                {
                    _dataService?.Animals?.Mammals?.Lion?.Remove(lion);
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.LionDeletedFromTheList, lion.Name));
                }
                else
                {
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.LionNotFound));
                }
            }
            catch (Exception ex)
            {
                _msgHistory.Add($"Error: {ex.Message}");
            }
        }

        private void EditLionMain()
        {
            _msgHistory.Clear();
            try
            {
                _msgHistory.Add(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.PromptLionNameForEdit));
                DisplayMsgHistory();
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Lion? lion = (Lion?)(_dataService?.Animals?.Mammals?.Lion
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (lion is not null)
                {
                    Lion lionEdited = AddEditLion();
                    lion.Copy(lionEdited);
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.DisplayLionAfterEdit));
                    lion.Display();
                }
                else
                {
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.LionNotFound));
                }
            }
            catch (Exception ex)
            {
                _msgHistory.Add($"Error: {ex.Message}");
            }
        }

        private Lion AddEditLion()
        {
            string? name = ReadInput(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.PromptLionName));
            int age = ReadIntInput(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.PromptLionAge));
            bool predator = ReadBoolInput(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.PromptLionTopPredator));
            bool packHunter = ReadBoolInput(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.PromptLionPackHunter));
            string? mane = ReadInput(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.PromptLionMane));
            bool roaring = ReadBoolInput(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.PromptLionRoaring));
            bool territoryDefense = ReadBoolInput(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.PromptLionTerritoryDefense));

            Lion lion = new Lion(name, age, predator, packHunter, mane, roaring, territoryDefense);
            return lion;
        }

        private string? ReadInput(string prompt)
        {
            Console.Write(prompt + " ");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException(prompt);
            }
            return input;
        }

        private int ReadIntInput(string prompt)
        {
            string? input = ReadInput(prompt);
            if (!int.TryParse(input, out int result))
            {
                throw new FormatException(_screenDefinitionService.GetLineFromJson(LionScreenJsonPath, (int)LionScreenId.InvalidInput));
            }
            return result;
        }

        private bool ReadBoolInput(string prompt)
        {
            string? input = ReadInput(prompt);
            return input.ToLower() == "yes";
        }

        #endregion // Private Methods
    }
}
