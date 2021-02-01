using System;
using System.Collections.Generic;
using System.Linq;
using Nop.Core;
using Nop.Plugin.Shipping.Jusda.Domain;
using Nop.Plugin.Shipping.Jusda.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.Shipping;
using Nop.Services.Shipping.Tracking;

namespace Nop.Plugin.Shipping.Jusda
{
    /// <summary>
    /// Represents UPS computation method
    /// </summary>
    public class JusdaComputationMethod : BasePlugin, IShippingRateComputationMethod
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly JusdaService _jusdaService;

        #endregion

        #region Ctor

        public JusdaComputationMethod(ILocalizationService localizationService,
            ISettingService settingService,
            IWebHelper webHelper,
            JusdaService jusdaService)
        {
            _localizationService = localizationService;
            _settingService = settingService;
            _webHelper = webHelper;
            _jusdaService = jusdaService;
        }

        #endregion

        #region Methods

        /// <summary>
        ///  Gets available shipping options
        /// </summary>
        /// <param name="getShippingOptionRequest">A request for getting shipping options</param>
        /// <returns>Represents a response of getting shipping rate options</returns>
        public GetShippingOptionResponse GetShippingOptions(GetShippingOptionRequest getShippingOptionRequest)
        {
            if (getShippingOptionRequest == null)
                throw new ArgumentNullException(nameof(getShippingOptionRequest));

            if (!getShippingOptionRequest.Items?.Any() ?? true)
                return new GetShippingOptionResponse { Errors = new[] { "No shipment items" } };

            if (getShippingOptionRequest.ShippingAddress?.CountryId == null)
                return new GetShippingOptionResponse { Errors = new[] { "Shipping address is not set" } };

            return _jusdaService.GetRates(getShippingOptionRequest).Result; //hack for interface :(
        }

        /// <summary>
        /// Gets fixed shipping rate (if shipping rate computation method allows it and the rate can be calculated before checkout).
        /// </summary>
        /// <param name="getShippingOptionRequest">A request for getting shipping options</param>
        /// <returns>Fixed shipping rate; or null in case there's no fixed shipping rate</returns>
        public decimal? GetFixedRate(GetShippingOptionRequest getShippingOptionRequest)
        {
            return null;
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/JusdaShipping/Configure";
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //settings
            _settingService.SaveSetting(new JusdaSettings
            {
                UseSandbox = true,
                //CustomerClassification = CustomerClassification.StandardListRates,
                //PickupType = PickupType.OneTimePickup,
                //PackagingType = PackagingType.ExpressBox,
                //PackingPackageVolume = 5184,
                //PackingType = PackingType.PackByDimensions,
                //PassDimensions = true,
                WeightType = "LB",
                DimensionsType = "IN"
            });

            //locales
            _localizationService.AddPluginLocaleResource(new Dictionary<string, string>
            {
                ["Enums.Nop.Plugin.Shipping.Jusda.PackingType.PackByDimensions"] = "Pack by dimensions",
                ["Enums.Nop.Plugin.Shipping.Jusda.PackingType.PackByOneItemPerPackage"] = "Pack by one item per package",
                ["Enums.Nop.Plugin.Shipping.Jusda.PackingType.PackByVolume"] = "Pack by volume",
                ["Plugins.Shipping.Jusda.Fields.AccessKey"] = "Access Key",
                ["Plugins.Shipping.Jusda.Fields.AccessKey.Hint"] = "Specify UPS access key.",
                ["Plugins.Shipping.Jusda.Fields.AccountNumber"] = "Account number",
                ["Plugins.Shipping.Jusda.Fields.AccountNumber.Hint"] = "Specify UPS account number (required to get negotiated rates).",
                ["Plugins.Shipping.Jusda.Fields.AdditionalHandlingCharge"] = "Additional handling charge",
                ["Plugins.Shipping.Jusda.Fields.AdditionalHandlingCharge.Hint"] = "Enter additional handling fee to charge your customers.",
                ["Plugins.Shipping.Jusda.Fields.AvailableCarrierServices"] = "Carrier Services",
                ["Plugins.Shipping.Jusda.Fields.AvailableCarrierServices.Hint"] = "Select the services you want to offer to customers.",
                ["Plugins.Shipping.Jusda.Fields.CustomerClassification"] = "UPS Customer Classification",
                ["Plugins.Shipping.Jusda.Fields.CustomerClassification.Hint"] = "Choose customer classification.",
                ["Plugins.Shipping.Jusda.Fields.DimensionsType"] = "Dimensions type",
                ["Plugins.Shipping.Jusda.Fields.DimensionsType.Hint"] = "Choose dimensions type (inches or centimeters).",
                ["Plugins.Shipping.Jusda.Fields.InsurePackage"] = "Insure package",
                ["Plugins.Shipping.Jusda.Fields.InsurePackage.Hint"] = "Check to insure packages.",
                ["Plugins.Shipping.Jusda.Fields.PackagingType"] = "UPS Packaging Type",
                ["Plugins.Shipping.Jusda.Fields.PackagingType.Hint"] = "Choose UPS packaging type.",
                ["Plugins.Shipping.Jusda.Fields.PackingPackageVolume"] = "Package volume",
                ["Plugins.Shipping.Jusda.Fields.PackingPackageVolume.Hint"] = "Enter your package volume.",
                ["Plugins.Shipping.Jusda.Fields.PackingType"] = "Packing type",
                ["Plugins.Shipping.Jusda.Fields.PackingType.Hint"] = "Choose preferred packing type.",
                ["Plugins.Shipping.Jusda.Fields.PassDimensions"] = "Pass dimensions",
                ["Plugins.Shipping.Jusda.Fields.PassDimensions.Hint"] = "Check if you want to pass package dimensions when requesting rates.",
                ["Plugins.Shipping.Jusda.Fields.Password"] = "Password",
                ["Plugins.Shipping.Jusda.Fields.Password.Hint"] = "Specify UPS password.",
                ["Plugins.Shipping.Jusda.Fields.PickupType"] = "UPS Pickup Type",
                ["Plugins.Shipping.Jusda.Fields.PickupType.Hint"] = "Choose UPS pickup type.",
                ["Plugins.Shipping.Jusda.Fields.SaturdayDeliveryEnabled"] = "Saturday Delivery enabled",
                ["Plugins.Shipping.Jusda.Fields.SaturdayDeliveryEnabled.Hint"] = "Check to get rates for Saturday Delivery options.",
                ["Plugins.Shipping.Jusda.Fields.Tracing"] = "Tracing",
                ["Plugins.Shipping.Jusda.Fields.Tracing.Hint"] = "Check if you want to record plugin tracing in System Log. Warning: The entire request and response XML will be logged (including AccessKey/Username,Password). Do not leave this enabled in a production environment.",
                ["Plugins.Shipping.Jusda.Fields.Username"] = "Username",
                ["Plugins.Shipping.Jusda.Fields.Username.Hint"] = "Specify UPS username.",
                ["Plugins.Shipping.Jusda.Fields.UseSandbox"] = "Use sandbox",
                ["Plugins.Shipping.Jusda.Fields.UseSandbox.Hint"] = "Check to use sandbox (testing environment).",
                ["Plugins.Shipping.Jusda.Fields.WeightType"] = "Weight type",
                ["Plugins.Shipping.Jusda.Fields.WeightType.Hint"] = "Choose the weight type (pounds or kilograms).",
                ["Plugins.Shipping.Jusda.Tracker.Arrived"] = "Arrived",
                ["Plugins.Shipping.Jusda.Tracker.Booked"] = "Booked",
                ["Plugins.Shipping.Jusda.Tracker.Delivered"] = "Delivered",
                ["Plugins.Shipping.Jusda.Tracker.Departed"] = "Departed",
                ["Plugins.Shipping.Jusda.Tracker.ExportScanned"] = "Export scanned",
                ["Plugins.Shipping.Jusda.Tracker.NotDelivered"] = "Not delivered",
                ["Plugins.Shipping.Jusda.Tracker.OriginScanned"] = "Origin scanned",
                ["Plugins.Shipping.Jusda.Tracker.Pickup"] = "Pickup"
            });

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<JusdaSettings>();

            //locales
            _localizationService.DeletePluginLocaleResources("Enums.Nop.Plugin.Shipping.UPS");
            _localizationService.DeletePluginLocaleResources("Plugins.Shipping.UPS");

            base.Uninstall();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a shipping rate computation method type
        /// </summary>
        public ShippingRateComputationMethodType ShippingRateComputationMethodType => ShippingRateComputationMethodType.Realtime;

        /// <summary>
        /// Gets a shipment tracker
        /// </summary>
        public IShipmentTracker ShipmentTracker => new JusdaShipmentTracker(_jusdaService);

        #endregion
    }
}