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
    /// Afalina main screen.
    /// </summary>
    public sealed class AfalinaScreen : Screen
    {
        #region Properties And Ctor

        private const string AfalinaScreenJsonPath = "afalinaScreen.json";

        private readonly IDataService _dataService;
        private readonly SettingsService _settingsService;
        private List<string> _msgHistory = new List<string>();

        public AfalinaScreen(IScreenDefinitionService screenDefinitionService, IDataService dataService, SettingsService settingsService)
            : base(screenDefinitionService)
        {
            _dataService = dataService;
            _settingsService = settingsService;
            ScreenDefinitionJson = AfalinaScreenJsonPath;
        }

        #endregion Properties And Ctor

        #region Public Methods

        public override void Show()
        {
            int selectedOption = 0;
            while (true)
            {
                Console.Clear();
                Console.ResetColor();
                _history.Clear();
                _history.Add(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.NavigateToAfalinaScreen));
                DisplayHistory();
                Console.WriteLine("Afalina Screen");
                Console.WriteLine("Use arrow keys to navigate, Enter to select, Esc to go back.");
                Console.WriteLine();

                for (int i = 0; i < 4; i++)
                {
                    string tmp = _screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.ListAllAfalina + i);
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
                    ListAfalina();
                    break;

                case 1:
                    AddAfalina();
                    break;

                case 2:
                    DeleteAfalina();
                    break;

                case 3:
                    EditAfalinaMain();
                    break;

                case 4:
                    _msgHistory.Clear();
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.ReturnToParentMenu));
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

        private void ListAfalina()
        {
            _msgHistory.Clear();
            if (_dataService?.Animals?.Mammals?.Afalina is not null &&
                _dataService.Animals.Mammals.Afalina.Count > 0)
            {
                Console.WriteLine(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.DisplayListOfAfalina));
                int i = 1;
                foreach (Afalina afalina in _dataService.Animals.Mammals.Afalina)
                {
                    Console.WriteLine(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.DisplayAfalinaNumber, i.ToString()));
                    afalina.Display();
                    i++;
                }
            }
            else
            {
                Console.WriteLine(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.DisplayEmptyListOfAfalina));
            }
            Console.ReadLine();
        }

        private void AddAfalina()
        {
            _msgHistory.Clear();
            try
            {
                Afalina afalina = AddEditAfalina();
                _dataService?.Animals?.Mammals?.Afalina?.Add(afalina);
                _msgHistory.Add(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.AfalinaAddedToTheList, afalina.Name));
            }
            catch (Exception ex)
            {
                _msgHistory.Add($"Error: {ex.Message}");
            }
        }

        private void DeleteAfalina()
        {
            _msgHistory.Clear();
            try
            {
                _msgHistory.Add(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.PromptAfalinaNameForDeletion));
                DisplayMsgHistory();
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Afalina? afalina = (Afalina?)(_dataService?.Animals?.Mammals?.Afalina
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (afalina is not null)
                {
                    _dataService?.Animals?.Mammals?.Afalina?.Remove(afalina);
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.AfalinaDeletedFromTheList, afalina.Name));
                }
                else
                {
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.AfalinaNotFound));
                }
            }
            catch (Exception ex)
            {
                _msgHistory.Add($"Error: {ex.Message}");
            }
        }

        private void EditAfalinaMain()
        {
            _msgHistory.Clear();
            try
            {
                _msgHistory.Add(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.PromptAfalinaNameForEdit));
                DisplayMsgHistory();
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Afalina? afalina = (Afalina?)(_dataService?.Animals?.Mammals?.Afalina
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (afalina is not null)
                {
                    Afalina afalinaEdited = AddEditAfalina();
                    afalina.Copy(afalinaEdited);
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.DisplayAfalinaAfterEdit));
                    afalina.Display();
                }
                else
                {
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.AfalinaNotFound));
                }
            }
            catch (Exception ex)
            {
                _msgHistory.Add($"Error: {ex.Message}");
            }
        }

        private Afalina AddEditAfalina()
        {
            string? name = ReadInput(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.PromptAfalinaName));
            int age = ReadIntInput(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.PromptAfalinaAge));
            bool echolocation = ReadBoolInput(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.PromptAfalinaEcholocation));
            string? socialBehavior = ReadInput(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.PromptAfalinaSocialBehavior));
            bool playfulBehavior = ReadBoolInput(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.PromptAfalinaPlayfulBehavior));
            int brainSize = ReadIntInput(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.PromptAfalinaBrainSize));
            bool highSpeedSwimming = ReadBoolInput(_screenDefinitionService.GetLineFromJson(AfalinaScreenJsonPath, (int)AfalinaScreenId.PromptAfalinaHighSpeedSwimming));

            Afalina afalina = new Afalina(name, age, echolocation, socialBehavior, playfulBehavior, brainSize, highSpeedSwimming);
            return afalina;
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
