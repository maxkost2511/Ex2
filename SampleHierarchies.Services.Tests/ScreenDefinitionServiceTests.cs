using System;
using SampleHierarchies.Services;
using SampleHierarchies.Data;

namespace SampleHierarchies.Services.Tests
{
    public class ScreenDefinitionServiceTests
    {
        [Fact]
        public void Load_ValidJsonFile_ReturnsScreenDefinition()
        {
            // Arrange
            IScreenDefinitionService screenDefinitionService = new ScreenDefinitionService();
            string jsonFileName = "valid.json";

            // Act
            ScreenDefinition? screenDefinition = screenDefinitionService.Load(jsonFileName);

            // Assert
            Assert.NotNull(screenDefinition);
            Console.WriteLine("Load_ValidJsonFile_ReturnsScreenDefinition test passed."); // Display success message
        }

        [Fact]
        public void Save_ValidScreenDefinition_SavesToFile()
        {
            // Arrange
            IScreenDefinitionService screenDefinitionService = new ScreenDefinitionService();
            string jsonFileName = "test.json";
            ScreenDefinition screenDefinition = new ScreenDefinition(); // Create an instance of ScreenDefinition

            // Act
            bool result = screenDefinitionService.Save(screenDefinition, jsonFileName);

            // Assert
            Assert.True(result);
            Console.WriteLine("Save_ValidScreenDefinition_SavesToFile test passed."); // Display success message
        }

        [Fact]
        public void Load_InvalidJsonFile_ReturnsNull()
        {
            // Arrange
            IScreenDefinitionService screenDefinitionService = new ScreenDefinitionService();
            string jsonFileName = "nonexistent.json"; // A non-existent JSON file

            // Act
            ScreenDefinition? screenDefinition = screenDefinitionService.Load(jsonFileName);

            // Assert
            Assert.Null(screenDefinition);
            Console.WriteLine("Load_InvalidJsonFile_ReturnsNull test passed."); // Display success message
        }
    }
}
