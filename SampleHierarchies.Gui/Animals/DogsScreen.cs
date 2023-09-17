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
    /// Dogs main screen.
    /// </summary>
    public class DogsScreen : Screen
    {
        #region Properties And Ctor

        private const string DogsScreenJsonPath = "dogsScreen.json";

        private readonly IDataService _dataService;
        private readonly SettingsService _settingsService;
        private List<string> _msgHistory = new List<string>();

        public DogsScreen(IScreenDefinitionService screenDefinitionService, IDataService dataService, SettingsService settingsService)
            : base(screenDefinitionService)
        {
            _dataService = dataService;
            _settingsService = settingsService;
            ScreenDefinitionJson = DogsScreenJsonPath;
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
                _history.Add(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.NavigateToDogsScreen));
                DisplayHistory();
                Console.WriteLine("Dogs Screen");
                Console.WriteLine("Use arrow keys to navigate, Enter to select, Esc to go back.");

                for (int i = 0; i < 4; i++)
                {
                    string tmp = _screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.ListAllDogs + i);
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
                    ListDogs();
                    break;

                case 1:
                    AddDog();
                    break;

                case 2:
                    DeleteDog();
                    break;

                case 3:
                    EditDogMain();
                    break;

                case 4:
                    _msgHistory.Clear();
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.ReturnToParentMenu));
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

        private void ListDogs()
        {
            _msgHistory.Clear();
            if (_dataService?.Animals?.Mammals?.Dogs is not null &&
                _dataService.Animals.Mammals.Dogs.Count > 0)
            {
                Console.WriteLine(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.DisplayListOfDogs));
                int i = 1;
                foreach (Dog dog in _dataService.Animals.Mammals.Dogs)
                {
                    Console.WriteLine(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.DisplayDogNumber, i.ToString()));
                    dog.Display();
                    i++;
                }
            }
            else
            {
                Console.WriteLine(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.DisplayEmptyListOfDogs));
            }
            Console.ReadLine();
        }

        private void AddDog()
        {
            _msgHistory.Clear();
            try
            {
                Dog dog = AddEditDog();
                _dataService?.Animals?.Mammals?.Dogs?.Add(dog);
                _msgHistory.Add(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.DogAddedToTheList, dog.Name));
            }
            catch (Exception ex)
            {
                _msgHistory.Add($"Error: {ex.Message}");
            }
        }

        private void DeleteDog()
        {
            _msgHistory.Clear();
            try
            {
                _msgHistory.Add(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.PromptDogNameForDeletion));
                DisplayMsgHistory();
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (dog is not null)
                {
                    _dataService?.Animals?.Mammals?.Dogs?.Remove(dog);
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.DogDeletedFromTheList, dog.Name));
                }
                else
                {
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.DogNotFound));
                }
            }
            catch (Exception ex)
            {
                _msgHistory.Add($"Error: {ex.Message}");
            }
        }

        private void EditDogMain()
        {
            _msgHistory.Clear();
            try
            {
                _msgHistory.Add(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.PromptDogNameForEdit));
                DisplayMsgHistory();
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                    ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
                if (dog is not null)
                {
                    Dog dogEdited = AddEditDog();
                    dog.Copy(dogEdited);
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.DisplayDogAfterEdit));
                    dog.Display();
                }
                else
                {
                    _msgHistory.Add(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.DogNotFound));
                }
            }
            catch (Exception ex)
            {
                _msgHistory.Add($"Error: {ex.Message}");
            }
            Console.ReadLine();
        }

        private Dog AddEditDog()
        {
            string? name = ReadInput(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.PromptDogName));
            int age = ReadIntInput(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.PromptDogAge));
            string? breed = ReadInput(_screenDefinitionService.GetLineFromJson(DogsScreenJsonPath, (int)DogsScreenId.PromptDogBreed));

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Dog dog = new Dog(name, age, breed);

            return dog;
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

        #endregion // Private Methods
    }
}
