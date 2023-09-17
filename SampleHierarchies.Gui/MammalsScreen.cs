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
    /// Mammals main screen.
    /// </summary>
    public sealed class MammalsScreen : Screen
    {
        #region Properties And Ctor

        private const string MammalsScreenJsonPath = "mammalsScreen.json";

        private readonly DogsScreen _dogsScreen;
        private readonly ChimpanzeeScreen _chimpanzeeScreen;
        private readonly LionScreen _lionScreen;
        private readonly AfalinaScreen _afalinaScreen;
        private readonly SettingsService _settingsService;
        private List<string> _msgHistory = new List<string>();

        public MammalsScreen(
            IScreenDefinitionService screenDefinitionService,
            DogsScreen dogsScreen,
            ChimpanzeeScreen chimpanzeeScreen,
            LionScreen lionScreen,
            AfalinaScreen afalinaScreen,
            SettingsService settingsService)
            : base(screenDefinitionService)
        {
            _settingsService = settingsService;
            _dogsScreen = dogsScreen;
            _chimpanzeeScreen = chimpanzeeScreen;
            _lionScreen = lionScreen;
            _afalinaScreen = afalinaScreen;
            ScreenDefinitionJson = MammalsScreenJsonPath;
        }

        #endregion Properties And Ctor

        #region Public Methods

        public override void Show()
        {
            int selectedOption = 0;
            while (true)
            {
                _history.Clear();
                _history.Add(_screenDefinitionService.GetLineFromJson(MammalsScreenJsonPath, (int)MammalsScreenId.NavigateToMammalsScreen));
                Console.Clear();
                DisplayHistory();
                Console.WriteLine("Mammals Screen");
                Console.WriteLine("Use arrow keys to navigate, Enter to select, Esc to go back.");
                Console.WriteLine();

                for (int i = 0; i < 4; i++)
                {
                    string tmp = _screenDefinitionService.GetLineFromJson(MammalsScreenJsonPath, (int)MammalsScreenId.ShowDogs + i);
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
                        if (selectedOption < 4)
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
                    _dogsScreen.Show();
                    break;

                case 1:
                    _chimpanzeeScreen.Show();
                    break;

                case 2:
                    _lionScreen.Show();
                    break;

                case 3:
                    _afalinaScreen.Show();
                    break;

                case 4:
                    _msgHistory.Clear();
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

        #endregion // Private Methods
    }
}
