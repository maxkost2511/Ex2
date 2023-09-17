using Newtonsoft.Json;
using SampleHierarchies.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Services
{
    /// <summary>
    /// Interface for working with ScreenDefinition.
    /// </summary>
    public interface IScreenDefinitionService
    {
        /// <summary>
        /// Read json and show lines
        /// </summary>
        /// <param name="jsonFileName">The path to the JSON file.</param>
        /// <param name="id">Line id</param>
        public string GetLineFromJson(string jsonFileName, int id, string argument = "");

        /// <summary>
        /// Loads a ScreenDefinition from a JSON file.
        /// </summary>
        /// <param name="jsonFileName">The path to the JSON file.</param>
        /// <returns>The loaded ScreenDefinition or null if loading fails.</returns>
        ScreenDefinition? Load(string jsonFileName);

        /// <summary>
        /// Saves a ScreenDefinition to a JSON file.
        /// </summary>
        /// <param name="screenDefinition">The ScreenDefinition to save.</param>
        /// <param name="jsonFileName">The path to the JSON file.</param>
        /// <returns>True if saving is successful, false otherwise.</returns>
        bool Save(ScreenDefinition screenDefinition, string jsonFileName);
    }


    /*
        IMPORTANT! I created a class not static, 
        because the task condition in point 5 says about creating an interface, 
        and it is impossible to create an interface to a static class 
        (I tried to do it crastically through abstract classes, but it didn't work).
     */

    /// <summary>
    /// Service for working with ScreenDefinition.
    /// </summary>
    public class ScreenDefinitionService : IScreenDefinitionService
    {
        public string GetLineFromJson(string jsonFileName, int id, string arg = "")
        {
            try
            {
                // Load screen definitions from a JSON file
                ScreenDefinition? screens = Load(jsonFileName);

                // Check if the loaded screen definitions are null
                if (screens == null)
                {
                    throw new NullReferenceException("Error: Screen definition is null.");
                }

                // Check if the provided 'id' is out of range
                if (id < 0 || id >= screens.LineEntries.Count)
                {
                    throw new IndexOutOfRangeException("Error: Invalid line id.");
                }

                // Set the console background and foreground colors based on screen definitions
                Console.BackgroundColor = screens.LineEntries[id].BackgroundColor;
                Console.ForegroundColor = screens.LineEntries[id].ForegroundColor;

                // Get the text from the screen definitions
                string? text = screens.LineEntries[id].Text;

                // Replace 'arg' in the text if 'arg' is provided
                if (!string.IsNullOrWhiteSpace(arg))
                {
                    text = text.Replace("arg", arg);
                }

                return text;
            }
            catch (NullReferenceException nre)
            {
                return nre.Message;
            }
            catch (IndexOutOfRangeException ioore)
            {
                return ioore.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public ScreenDefinition Load(string jsonFileName)
        {
            try
            {
                if (!File.Exists(jsonFileName))
                    throw new FileNotFoundException();

                string jsonContent = File.ReadAllText(jsonFileName);
                var screenDefinition = JsonConvert.DeserializeObject<ScreenDefinition>(jsonContent);

                if (screenDefinition == null)
                    throw new NullReferenceException();

                //Console.WriteLine("Successfully loaded screen definition with {0} line entries.", screenDefinition.LineEntries.Count);

                return screenDefinition;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to successfully load screen definition: " + ex.Message);
                return new ScreenDefinition();
            }
        }


        public bool Save(ScreenDefinition screenDefinition, string jsonFileName)
        {
            try
            {
                string json = JsonConvert.SerializeObject(screenDefinition, Formatting.Indented);
                File.WriteAllText(jsonFileName, json);
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., permission issues, etc.)
                Console.WriteLine($"Error saving ScreenDefinition: {ex.Message}");
                return false;
            }
        }
    }
}
