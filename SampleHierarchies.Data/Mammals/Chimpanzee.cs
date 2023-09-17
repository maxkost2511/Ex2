using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Data.Mammals
{
    /// <summary>
    /// Class representing a chimpanzee.
    /// </summary>
    public class Chimpanzee : MammalBase, IChimpanzee
    {
        #region Public Methods

        /// <inheritdoc/>
        public override void MakeSound()
        {
            Console.WriteLine("Chimpanzee vocalization");
        }

        /// <inheritdoc/>
        public override void Move()
        {
            Console.WriteLine("Chimpanzee is moving");
        }

        /// <inheritdoc/>
        public override void Display()
        {
            Console.WriteLine($"Chimpanzee: Name={Name}, Age={Age}, OpposableThumbs={HasOpposableThumbs}, SocialBehavior={SocialBehavior}, UsesTools={UsesTools}, IntelligenceLevel={IntelligenceLevel}, Diet={Diet}");
        }

        /// <inheritdoc/>
        public override void Copy(IAnimal animal)
        {
            if (animal is IChimpanzee chimpanzee)
            {
                base.Copy(animal);
                HasOpposableThumbs = chimpanzee.HasOpposableThumbs;
                SocialBehavior = chimpanzee.SocialBehavior;
                UsesTools = chimpanzee.UsesTools;
                IntelligenceLevel = chimpanzee.IntelligenceLevel;
                Diet = chimpanzee.Diet;
            }
        }

        #endregion // Public Methods

        #region Ctors And Properties

        /// <inheritdoc/>
        public bool HasOpposableThumbs { get; set; }

        /// <inheritdoc/>
        public string SocialBehavior { get; set; }

        /// <inheritdoc/>
        public bool UsesTools { get; set; }

        /// <inheritdoc/>
        public int IntelligenceLevel { get; set; }

        /// <inheritdoc/>
        public string Diet { get; set; }

        /// <summary>
        /// Constructor to create a chimpanzee instance.
        /// </summary>
        /// <param name="name">Name of the chimpanzee.</param>
        /// <param name="age">Age of the chimpanzee.</param>
        /// <param name="thumbs">Whether the chimpanzee has opposable thumbs.</param>
        /// <param name="behavior">Description of social behavior.</param>
        /// <param name="tools">Whether the chimpanzee uses tools.</param>
        /// <param name="intelligence">Level of intelligence.</param>
        /// <param name="diet">Description of the diet.</param>
        public Chimpanzee(string name, int age, bool thumbs, string behavior, bool tools, int intelligence, string diet)
            : base(name, age, MammalSpecies.Chimpanzee)
        {
            HasOpposableThumbs = thumbs;
            SocialBehavior = behavior;
            UsesTools = tools;
            IntelligenceLevel = intelligence;
            Diet = diet;
        }

        #endregion // Ctors And Properties
    }
}