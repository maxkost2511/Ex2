using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Data.Mammals;

/// <summary>
/// Mammal base class.
/// </summary>
public class MammalBase : AnimalBase, IMammal
{
    #region IMammal Implementation 

    /// <inheritdoc/>   
    public MammalSpecies Species { get; set; }

    #endregion // IMammal Implementation

    #region Ctors

    /// <summary>
    /// Mammal base class constructor, with parameters.
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="age">Age</param>
    /// <param name="species">Species</param>
    public MammalBase(
        string name,
        int age,
        MammalSpecies species) : base(name, age)
    {
        Species = species;
    }

    /// <summary>
    /// Parameterless constructor.
    /// </summary>
    public MammalBase() : base()
    {
        Species = MammalSpecies.None;
    }

    #endregion // Ctors

    #region Public Methods

    /// <inheritdoc/>
    public override void MakeSound()
    {
        base.MakeSound();
        Console.WriteLine("My name is: {0} and I am making a mammal sound", Name);
    }

    /// <inheritdoc/>
    public override void Move()
    {
        base.Move();
        Console.WriteLine("My name is: {0} and I am moving like a mammal", Name);
    }

    /// <inheritdoc/>
    public override void Copy(IAnimal animal)
    {
        if (animal is IMammal am)
        {
            base.Copy(animal);
            Species = am.Species;
        }
    }

    #endregion // Public Methods
}
