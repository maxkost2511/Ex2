namespace SampleHierarchies.Interfaces.Data.Mammals
{
    /// <summary>
    /// Interface depicting a chimpanzee.
    /// </summary>
    public interface IChimpanzee : IMammal
    {
        #region Interface Members
        /// <summary>
        /// Indicates if the chimpanzee has opposable thumbs.
        /// </summary>
        bool HasOpposableThumbs { get; set; }

        /// <summary>
        /// Description of the chimpanzee's complex social behavior.
        /// </summary>
        string SocialBehavior { get; set; }

        /// <summary>
        /// Indicates if the chimpanzee uses tools.
        /// </summary>
        bool UsesTools { get; set; }

        /// <summary>
        /// Level of intelligence of the chimpanzee.
        /// </summary>
        int IntelligenceLevel { get; set; }

        /// <summary>
        /// Description of the chimpanzee's flexible diet.
        /// </summary>
        string Diet { get; set; }
        #endregion // Interface Members
    }
}