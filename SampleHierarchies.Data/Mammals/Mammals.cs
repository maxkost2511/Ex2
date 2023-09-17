using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Data.Mammals;

/// <summary>
/// Mammals collection.
/// </summary>
public class Mammals : IMammals
{
    #region IMammals Implementation

    /// <inheritdoc/>
    public List<IDog> Dogs { get; set; }
    /// <inheritdoc/>
    public List<IChimpanzee> Chimpanzee { get; set; }
    /// <inheritdoc/>
    public List<ILion> Lion { get; set; }
    /// <inheritdoc/>
    public List<IAfalina> Afalina { get; set; }

    #endregion // IMammals Implementation

    #region Ctors

    /// <summary>
    /// Default ctor.
    /// </summary>
    public Mammals()
    {
        Dogs = new List<IDog>();
        Chimpanzee = new List<IChimpanzee>();
        Lion = new List<ILion>();
        Afalina = new List<IAfalina>();
    }

    #endregion // Ctors
}
