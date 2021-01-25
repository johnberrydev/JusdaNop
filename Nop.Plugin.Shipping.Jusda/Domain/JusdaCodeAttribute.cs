using System;

namespace Nop.Plugin.Shipping.Jusda.Domain
{
    /// <summary>
    /// Represents custom attribute for UPS code
    /// </summary>
    public class JusdaCodeAttribute : Attribute
    {
        #region Ctor

        public JusdaCodeAttribute(string codeValue)
        {
            Code = codeValue;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a code value
        /// </summary>
        public string Code { get; }

        #endregion
    }
}