using SampleHierarchies.Interfaces.Data;

namespace SampleHierarchies.Data;

/// <summary>
/// Animal base class with basic implementations.
/// </summary>
public abstract class AnimalBase : IAnimal, IBehaviour
{
    #region IAnimal Implementation

    /// <inheritdoc/>
    public string Name { get; set; }

    /// <inheritdoc/>
    public int Age { get; set; }

    /// <inheritdoc/>
    public virtual void Copy(IAnimal animal)
    {
        Name = animal.Name;
        Age = animal.Age;
    }

    #endregion // IAnimal Impementation

    #region IBehaviour Implementation

    /// <inheritdoc/>
    public virtual void MakeSound()
    {
        Console.WriteLine("My name is {0} and I make noise like an animal", Name);
    }

    /// <inheritdoc/>
    public virtual void Move()
    {
        Console.WriteLine("My name is {0} and I move like an animal", Name);
    }

    /// <inheritdoc/>
    public virtual void Display()
    {
        Console.WriteLine("My name is: {0} and I am: {1} years old", Name, Age);
    }

    #endregion // IBehaviour Implementation

    #region Ctors

    /// <summary>
    /// Ctor.
    /// </summary>
    public AnimalBase()
    {
        Name = string.Empty;
        Age = 0;
    }

    /// <summary>
    /// Constructor with parameters
    /// </summary>
    /// <param name="name">Name</param>
    /// <param name="age">Age</param>
    public AnimalBase(
        string name,
        int age)
    {
        Name = name;
        Age = age;
    }

    #endregion // Ctors
}
