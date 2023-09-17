using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Interfaces.Data;

/// <summary>
/// Mammals collection.
/// </summary>
public interface IMammals
{
    #region Interface Members

    /// <summary>
    /// Dogs collection.
    /// </summary>
    List<IDog> Dogs { get; set; }

    /// <summary>
    /// Chimpanzee collection.
    /// </summary
    List<IChimpanzee> Chimpanzee { get; set; }

    /// <summary>
    /// Lion collection.
    /// </summary
    List<ILion> Lion { get; set; }

    /// <summary>
    /// Afalina collection.
    /// </summary
    List<IAfalina> Afalina { get; set; }

    #endregion // Interface Members
}
