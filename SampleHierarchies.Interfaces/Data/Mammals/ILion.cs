namespace SampleHierarchies.Interfaces.Data.Mammals
{
    /// <summary>
    /// Interface depicting a lion.
    /// </summary>
    public interface ILion : IMammal
    {
        #region Interface Members
        /// <summary>
        /// Indicates if the lion is a top predator.
        /// </summary>
        bool IsTopPredator { get; set; }

        /// <summary>
        /// Indicates if the lion is a pack hunter.
        /// </summary>
        bool IsPackHunter { get; set; }

        /// <summary>
        /// Description of the lion's mane.
        /// </summary>
        string ManeDescription { get; set; }

        /// <summary>
        /// Indicates if the lion can roar to mark territory.
        /// </summary>
        bool CanRoar { get; set; }

        /// <summary>
        /// Indicates if the lion defends its territory.
        /// </summary>
        bool DefendsTerritory { get; set; }
        #endregion // Interface Members
    }
}