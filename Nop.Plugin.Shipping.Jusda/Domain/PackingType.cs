using System;
using System.Collections.Generic;
using System.Text;

namespace Nop.Plugin.Shipping.Jusda.Domain
{
    public enum PackingType
    {
        /// <summary>
        /// Pack by dimensions
        /// </summary>
        PackByDimensions = 0,

        /// <summary>
        /// Pack by one item per package
        /// </summary>
        PackByOneItemPerPackage = 1,

        /// <summary>
        /// Pack by volume
        /// </summary>
        PackByVolume = 2
    }

}
