using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Data.Mammals
{
    /// <summary>
    /// Class representing an Afalina (Tursiops truncatus).
    /// </summary>
    public class Afalina : MammalBase, IAfalina
    {
        #region Public Methods

        /// <inheritdoc/>
        public override void MakeSound()
        {
            Console.WriteLine("Afalina vocalization");
        }

        /// <inheritdoc/>
        public override void Move()
        {
            Console.WriteLine("Afalina is swimming");
        }

        /// <inheritdoc/>
        public override void Display()
        {
            Console.WriteLine($"Afalina: Name={Name}, Age={Age}, HasEcholocation={HasEcholocation}, SocialBehavior={SocialBehavior}, HasPlayfulBehavior={HasPlayfulBehavior}, BrainSize={BrainSize}, CanSwimAtHighSpeeds={CanSwimAtHighSpeeds}");
        }

        /// <inheritdoc/>
        public override void Copy(IAnimal animal)
        {
            if (animal is IAfalina afalina)
            {
                base.Copy(animal);
                HasEcholocation = afalina.HasEcholocation;
                SocialBehavior = afalina.SocialBehavior;
                HasPlayfulBehavior = afalina.HasPlayfulBehavior;
                BrainSize = afalina.BrainSize;
                CanSwimAtHighSpeeds = afalina.CanSwimAtHighSpeeds;
            }
        }

        #endregion // Public Methods

        #region Ctors And Properties

        /// <inheritdoc/>
        public bool HasEcholocation { get; set; }

        /// <inheritdoc/>
        public string SocialBehavior { get; set; }

        /// <inheritdoc/>
        public bool HasPlayfulBehavior { get; set; }

        /// <inheritdoc/>
        public int BrainSize { get; set; }

        /// <inheritdoc/>
        public bool CanSwimAtHighSpeeds { get; set; }

        /// <summary>
        /// Constructor to create an Afalina instance.
        /// </summary>
        /// <param name="name">Name of the Afalina.</param>
        /// <param name="age">Age of the Afalina.</param>
        /// <param name="echolocation">Whether the Afalina has echolocation ability.</param>
        /// <param name="socialBehavior">Description of social behavior.</param>
        /// <param name="playfulBehavior">Whether the Afalina exhibits playful behavior.</param>
        /// <param name="brainSize">Size of the Afalina's brain.</param>
        /// <param name="highSpeedSwimming">Whether the Afalina can swim at high speeds.</param>
        public Afalina(string name, int age, bool echolocation, string socialBehavior, bool playfulBehavior, int brainSize, bool highSpeedSwimming)
            : base(name, age, MammalSpecies.Afalina)
        {
            HasEcholocation = echolocation;
            SocialBehavior = socialBehavior;
            HasPlayfulBehavior = playfulBehavior;
            BrainSize = brainSize;
            CanSwimAtHighSpeeds = highSpeedSwimming;
        }

        #endregion // Ctors And Properties
    }
}