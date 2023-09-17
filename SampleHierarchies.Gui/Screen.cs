using SampleHierarchies.Services;
using System;
using System.Collections.Generic;

namespace SampleHierarchies.Gui
{
    /// <summary>
    /// Abstract base class for a screen.
    /// </summary>
    public abstract class Screen
    {
        #region Fields

        protected readonly IScreenDefinitionService _screenDefinitionService;
        protected List<string> _history = new List<string>();
        private List<string> _msgHistory = new List<string>();

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Screen"/> class.
        /// </summary>
        /// <param name="screenDefinitionService">The screen definition service.</param>
        protected Screen(IScreenDefinitionService screenDefinitionService)
        {
            _screenDefinitionService = screenDefinitionService;
            ScreenDefinitionJson = "default.json"; // Set the default value
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the JSON file name for the screen definition.
        /// </summary>
        protected string ScreenDefinitionJson { get; set; }

        #endregion // Properties

        #region Public Methods

        /// <summary>
        /// Show the screen.
        /// </summary>
        public virtual void Show()
        {
            while (true)
            {
                Console.Clear();
                DisplayHistory();
                Console.WriteLine("Showing screen");
                Console.WriteLine("Use arrow keys to navigate, Enter to select, Esc to go back.");

                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        // Handle up arrow key
                        break;

                    case ConsoleKey.DownArrow:
                        // Handle down arrow key
                        break;

                    case ConsoleKey.LeftArrow:
                        // Handle left arrow key
                        break;

                    case ConsoleKey.RightArrow:
                        // Handle right arrow key
                        break;

                    case ConsoleKey.Enter:
                        // Handle Enter key
                        break;

                    case ConsoleKey.Escape:
                        // Handle Esc key
                        return;
                }

                DisplayMsgHistory();
            }
        }

        #endregion // Public Methods

        #region Private Methods

        private void DisplayHistory()
        {
            if (_history.Count > 0)
            {
                Console.WriteLine("History: " + string.Join(" -> ", _history));
                Console.WriteLine();
            }
        }

        private void DisplayMsgHistory()
        {
            if (_msgHistory.Count > 0)
            {
                Console.WriteLine(_msgHistory);
                Console.WriteLine();
            }
        }

        protected void AddToHistory(string item)
        {
            _history.Add(item);
        }

        #endregion // Private Methods
    }
}
