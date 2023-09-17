namespace SampleHierarchies.Interfaces.Data.Mammals
{
    /// <summary>
    /// Interface depicting an Afalina (Tursiops truncatus).
    /// </summary>
    public interface IAfalina : IMammal
    {
        #region Interface Members
        /// <summary>
        /// Indicates if the Afalina has echolocation ability.
        /// </summary>
        bool HasEcholocation { get; set; }

        /// <summary>
        /// Description of the Afalina's social behavior.
        /// </summary>
        string SocialBehavior { get; set; }

        /// <summary>
        /// Indicates if the Afalina exhibits playful behavior.
        /// </summary>
        bool HasPlayfulBehavior { get; set; }

        /// <summary>
        /// Size of the Afalina's brain.
        /// </summary>
        int BrainSize { get; set; }

        /// <summary>
        /// Indicates if the Afalina can swim at high speeds.
        /// </summary>
        bool CanSwimAtHighSpeeds { get; set; }
        #endregion // Interface Members
    }
}