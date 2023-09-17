using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Interfaces.Data;

/// <summary>
/// Collection of animals.
/// </summary>
public interface IAnimals
{
    #region Inteface Members

    /// <summary>
    /// Mammals collection.
    /// </summary>
    public IMammals Mammals { get; set; }

    #endregion // Interface Members
}
