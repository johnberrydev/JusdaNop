﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using Nop.Plugin.Shipping.Jusda.Services;
using Nop.Services.Shipping.Tracking;

namespace Nop.Plugin.Shipping.Jusda
{
    /// <summary>
    /// Represents the USP shipment tracker
    /// </summary>
    public class JusdaShipmentTracker : IShipmentTracker
    {
        #region Fields

        private readonly JusdaService _jusdaService;

        #endregion

        #region Ctor

        public JusdaShipmentTracker(JusdaService upsService)
        {
            _jusdaService = upsService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets if the current tracker can track the tracking number.
        /// </summary>
        /// <param name="trackingNumber">The tracking number to track.</param>
        /// <returns>True if the tracker can track, otherwise false.</returns>
        public virtual bool IsMatch(string trackingNumber)
        {
            if (string.IsNullOrEmpty(trackingNumber))
                return false;

            //details on https://www.ups.com/us/en/tracking/help/tracking/tnh.page
            return Regex.IsMatch(trackingNumber, "^1Z[A-Z0-9]{16}$", RegexOptions.IgnoreCase) ||
                Regex.IsMatch(trackingNumber, "^\\d{9}$", RegexOptions.IgnoreCase) ||
                Regex.IsMatch(trackingNumber, "^T\\d{10}$", RegexOptions.IgnoreCase) ||
                Regex.IsMatch(trackingNumber, "^\\d{12}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Gets an URL for a page to show tracking info (third party tracking page).
        /// </summary>
        /// <param name="trackingNumber">The tracking number to track.</param>
        /// <returns>URL of a tracking page.</returns>
        public virtual string GetUrl(string trackingNumber)
        {
            return $"https://www.ups.com/track?&tracknum={trackingNumber}";
        }

        /// <summary>
        /// Gets all events for a tracking number.
        /// </summary>
        /// <param name="trackingNumber">The tracking number to track</param>
        /// <returns>List of Shipment Events.</returns>
        public virtual IList<ShipmentStatusEvent> GetShipmentEvents(string trackingNumber)
        {
            var result = new List<ShipmentStatusEvent>();

            if (string.IsNullOrEmpty(trackingNumber))
                return result;

            result.AddRange(_jusdaService.GetShipmentEvents(trackingNumber));

            return result;
        }

        #endregion
    }
}