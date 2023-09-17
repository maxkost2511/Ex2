using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Data.Mammals
{
    /// <summary>
    /// Class representing a lion.
    /// </summary>
    public class Lion : MammalBase, ILion
    {
        #region Public Methods

        /// <inheritdoc/>
        public override void MakeSound()
        {
            Console.WriteLine("Lion roaring");
        }

        /// <inheritdoc/>
        public override void Move()
        {
            Console.WriteLine("Lion is moving");
        }

        /// <inheritdoc/>
        public override void Display()
        {
            Console.WriteLine($"Lion: Name={Name}, Age={Age}, IsTopPredator={IsTopPredator}, IsPackHunter={IsPackHunter}, ManeDescription={ManeDescription}, CanRoar={CanRoar}, DefendsTerritory={DefendsTerritory}");
        }

        /// <inheritdoc/>
        public override void Copy(IAnimal animal)
        {
            if (animal is ILion lion)
            {
                base.Copy(animal);
                IsTopPredator = lion.IsTopPredator;
                IsPackHunter = lion.IsPackHunter;
                ManeDescription = lion.ManeDescription;
                CanRoar = lion.CanRoar;
                DefendsTerritory = lion.DefendsTerritory;
            }
        }

        #endregion // Public Methods

        #region Ctors And Properties

        /// <inheritdoc/>
        public bool IsTopPredator { get; set; }

        /// <inheritdoc/>
        public bool IsPackHunter { get; set; }

        /// <inheritdoc/>
        public string ManeDescription { get; set; }

        /// <inheritdoc/>
        public bool CanRoar { get; set; }

        /// <inheritdoc/>
        public bool DefendsTerritory { get; set; }

        /// <summary>
        /// Constructor to create a lion instance.
        /// </summary>
        /// <param name="name">Name of the lion.</param>
        /// <param name="age">Age of the lion.</param>
        /// <param name="topPredator">Whether the lion is a top predator.</param>
        /// <param name="packHunter">Whether the lion is a pack hunter.</param>
        /// <param name="mane">Description of the lion's mane.</param>
        /// <param name="roaring">Whether the lion can roar.</param>
        /// <param name="territoryDefense">Whether the lion defends its territory.</param>
        public Lion(string name, int age, bool topPredator, bool packHunter, string mane, bool roaring, bool territoryDefense)
            : base(name, age, MammalSpecies.Lion)
        {
            IsTopPredator = topPredator;
            IsPackHunter = packHunter;
            ManeDescription = mane;
            CanRoar = roaring;
            DefendsTerritory = territoryDefense;
        }

        #endregion // Ctors And Properties
    }
}