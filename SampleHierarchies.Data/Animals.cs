using SampleHierarchies.Interfaces.Data;

namespace SampleHierarchies.Data;

/// <summary>
/// Animals collection.
/// </summary>
public class Animals : IAnimals
{
    #region IAnimals Implementation

    /// <inheritdoc/>
    public IMammals Mammals { get; set; }

    #endregion // IAnimals Implementation

    #region Ctors

    /// <summary>
    /// Default ctor.
    /// </summary>
    public Animals()
    {
        Mammals = new Mammals.Mammals();
    }

    #endregion // Ctors
}
