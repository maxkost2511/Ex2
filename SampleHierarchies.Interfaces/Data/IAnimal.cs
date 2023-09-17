namespace SampleHierarchies.Interfaces.Data;

/// <summary>
/// Interface describing an animal.
/// </summary>
public interface IAnimal
{
    #region Interface Members

    /// <summary>
    /// Gets or sets the name of the animal.
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// Gets or sets the age of the animal.
    /// </summary>
    int Age { get; set; }

    #endregion // Interface Members

    #region Interface Methods

    /// <summary>
    /// Copies data from another animal.
    /// </summary>
    /// <param name="animal">Animal to copy data from</param>
    void Copy(IAnimal animal);

    #endregion // Interface Methods
}
