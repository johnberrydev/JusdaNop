using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Shipping.Jusda.Models
{
    public class JusdaShippingModel : BaseNopModel
    {
        #region Ctor

        public JusdaShippingModel()
        {
            CarrierServices = new List<string>();
            AvailableCarrierServices = new List<SelectListItem>();
            AvailableCustomerClassifications = new List<SelectListItem>();
            AvailablePickupTypes = new List<SelectListItem>();
            AvailablePackagingTypes = new List<SelectListItem>();
            AvaliablePackingTypes = new List<SelectListItem>();
            AvaliableWeightTypes = new List<SelectListItem>();
            AvaliableDimensionsTypes = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.AccountNumber")]
        public string AccountNumber { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.AccessKey")]
        public string AccessKey { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.Username")]
        public string Username { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.Password")]
        public string Password { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.UseSandbox")]
        public bool UseSandbox { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.AdditionalHandlingCharge")]
        public decimal AdditionalHandlingCharge { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.InsurePackage")]
        public bool InsurePackage { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.CustomerClassification")]
        public int CustomerClassification { get; set; }
        public IList<SelectListItem> AvailableCustomerClassifications { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.PickupType")]
        public int PickupType { get; set; }
        public IList<SelectListItem> AvailablePickupTypes { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.PackagingType")]
        public int PackagingType { get; set; }
        public IList<SelectListItem> AvailablePackagingTypes { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.AvailableCarrierServices")]
        public IList<SelectListItem> AvailableCarrierServices { get; set; }
        public IList<string> CarrierServices { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.SaturdayDeliveryEnabled")]
        public bool SaturdayDeliveryEnabled { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.PassDimensions")]
        public bool PassDimensions { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.PackingPackageVolume")]
        public int PackingPackageVolume { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.PackingType")]
        public int PackingType { get; set; }
        public IList<SelectListItem> AvaliablePackingTypes { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.Tracing")]
        public bool Tracing { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.WeightType")]
        public string WeightType { get; set; }
        public IList<SelectListItem> AvaliableWeightTypes { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.Jusda.Fields.DimensionsType")]
        public string DimensionsType { get; set; }
        public IList<SelectListItem> AvaliableDimensionsTypes { get; set; }

        #endregion
    }
}