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
    /// Chimpanzee main screen.
    /// </summary>
    public class ChimpanzeeScreen : Screen
    {
        #region Properties And Ctor

        private const string ChimpanzeeScreenJsonPath = "chimpanzeeScreen.json";

        private readonly IDataService _dataService;
        private readonly SettingsService _settingsService;
        private List<string> _msgHistory = new List<string>();

        public ChimpanzeeScreen(IScreenDefinitionService screenDefinitionService, IDataService dataService, SettingsService settingsService)
            : base(screenDefinitionService)
        {
            _dataService = dataService;
            _settingsService = settingsService;
            ScreenDefinitionJson = ChimpanzeeScreenJsonPath;
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
                _history.Add(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.NavigateToChimpanzeeScreen));
                DisplayHistory();
                Console.WriteLine("Chimpanzee Screen");
                Console.WriteLine("Use arrow keys to navigate, Enter to select, Esc to go back.");
                Console.WriteLine();

                for (int i = 0; i < 4; i++)
                {
                    string tmp = _screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.ListAllChimpanzee + i);
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
                        _history.Add("Mammals => ");
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
                    ListChimpanzees();
                    break;

                case 1:
                    AddChimpanzee();
                    break;

                case 2:
                    DeleteChimpanzee();
                    break;

                case 3:
                    EditChimpanzeeMain();
                    break;

                case 4:
                    _msgHistory.Clear();
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.ReturnToParentMenu));
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

        private void ListChimpanzees()
        {
            _msgHistory.Clear();
            if (_dataService?.Animals?.Mammals?.Chimpanzee is not null &&
                _dataService.Animals.Mammals.Chimpanzee.Count > 0)
            {
                Console.WriteLine(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.DisplayListOfChimpanzee));
                int i = 1;
                foreach (Chimpanzee chimpanzee in _dataService.Animals.Mammals.Chimpanzee)
                {
                    Console.WriteLine(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.DisplayChimpanzeeNumber, i.ToString()));
                    chimpanzee.Display();
                    i++;
                }
            }
            else
            {
                Console.WriteLine(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.DisplayEmptyListOfChimpanzee));
            }
            Console.ReadLine();
        }

        private void AddChimpanzee()
        {
            _msgHistory.Clear();
            try
            {
                Chimpanzee chimpanzee = AddEditChimpanzee();
                _dataService?.Animals?.Mammals?.Chimpanzee?.Add(chimpanzee);
                _msgHistory.Add(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.ChimpanzeeAddedToTheList, chimpanzee.Name));
            }
            catch (Exception ex)
            {
                _msgHistory.Add($"Error: {ex.Message}");
            }
        }

        private void DeleteChimpanzee()
        {
            _msgHistory.Clear();
            try
            {
                _msgHistory.Add(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.PromptChimpanzeeNameForDeletion));
                DisplayMsgHistory();
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Chimpanzee? chimpanzee = (Chimpanzee?)(_dataService?.Animals?.Mammals?.Chimpanzee
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (chimpanzee is not null)
                {
                    _dataService?.Animals?.Mammals?.Chimpanzee?.Remove(chimpanzee);
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.ChimpanzeeDeletedFromTheList, chimpanzee.Name));
                }
                else
                {
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.ChimpanzeeNotFound));
                }
            }
            catch (Exception ex)
            {
                _msgHistory.Add($"Error: {ex.Message}");
            }
        }

        private void EditChimpanzeeMain()
        {
            _msgHistory.Clear();
            try
            {
                _msgHistory.Add(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.PromptChimpanzeeNameForEdit));
                DisplayMsgHistory();
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Chimpanzee? chimpanzee = (Chimpanzee?)(_dataService?.Animals?.Mammals?.Chimpanzee
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (chimpanzee is not null)
                {
                    Chimpanzee chimpanzeeEdited = AddEditChimpanzee();
                    chimpanzee.Copy(chimpanzeeEdited);
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.DisplayChimpanzeeAfterEdit));
                    chimpanzee.Display();
                }
                else
                {
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.ChimpanzeeNotFound));
                }
            }
            catch (Exception ex)
            {
                _msgHistory.Add($"Error: {ex.Message}");
            }
        }

        private Chimpanzee AddEditChimpanzee()
        {
            string? name = ReadInput(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.PromptChimpanzeeName));
            int age = ReadIntInput(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.PromptChimpanzeeAge));
            bool thumbs = ReadBoolInput(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.PromptChimpanzeeThumbs));
            string? behavior = ReadInput(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.PromptChimpanzeeBehavior));
            bool tools = ReadBoolInput(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.PromptChimpanzeeTools));
            int intelligence = ReadIntInput(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.PromptChimpanzeeIntelligence));
            string? diet = ReadInput(_screenDefinitionService.GetLineFromJson(ChimpanzeeScreenJsonPath, (int)ChimpanzeeScreenId.PromptChimpanzeeDiet));

            Chimpanzee chimpanzee = new Chimpanzee(name, age, thumbs, behavior, tools, intelligence, diet);
            return chimpanzee;
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
                throw new FormatException($"Invalid input for {prompt}");
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
