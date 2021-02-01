using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Plugin.Shipping.Jusda.Domain;
using Nop.Plugin.Shipping.Jusda.Models;
//using Nop.Plugin.Shipping.UPS.Services;
using Nop.Services;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.Shipping.Jusda.Controllers
{
    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class JusdaShippingController : BasePluginController
    {
        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IMeasureService _measureService;
        private readonly INotificationService _notificationService;
        private readonly IPermissionService _permissionService;
        private readonly ISettingService _settingService;
        //private readonly UPSService _upsService;
        private readonly JusdaSettings _jusdaSettings;

        #endregion

        #region Ctor

        public JusdaShippingController(ILocalizationService localizationService,
            IMeasureService measureService,
            INotificationService notificationService,
            IPermissionService permissionService,
            ISettingService settingService,
            //UPSService upsService,
            JusdaSettings jusdaSettings
            )
        {
            _localizationService = localizationService;
            _measureService = measureService;
            _notificationService = notificationService;
            _permissionService = permissionService;
            _settingService = settingService;
            //_upsService = upsService;
            _jusdaSettings = jusdaSettings;
        }

        #endregion

        #region Methods

        public IActionResult Configure()
        {
            //whether user has the authority to manage configuration
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            //prepare common model
            var model = new JusdaShippingModel
            {
                AccountNumber = _jusdaSettings.AccountNumber,
                AccessKey = _jusdaSettings.AccessKey,
                //Username = _jusdaSettings.Username,
                //Password = _jusdaSettings.Password,
                UseSandbox = _jusdaSettings.UseSandbox,
                //AdditionalHandlingCharge = _jusdaSettings.AdditionalHandlingCharge,
                //InsurePackage = _jusdaSettings.InsurePackage,
                //CustomerClassification = (int)_jusdaSettings.CustomerClassification,
                //PickupType = (int)_jusdaSettings.PickupType,
                //PackagingType = (int)_jusdaSettings.PackagingType,
                //SaturdayDeliveryEnabled = _jusdaSettings.SaturdayDeliveryEnabled,
                //PassDimensions = _jusdaSettings.PassDimensions,
                //PackingPackageVolume = _jusdaSettings.PackingPackageVolume,
                //PackingType = (int)_jusdaSettings.PackingType,
                //Tracing = _jusdaSettings.Tracing,
                WeightType = _jusdaSettings.WeightType,
                DimensionsType = _jusdaSettings.DimensionsType
            };

            //prepare offered delivery services
            var servicesCodes = _jusdaSettings.CarrierServicesOffered.Split(':', StringSplitOptions.RemoveEmptyEntries)
                .Select(idValue => idValue.Trim('[', ']')).ToList();

            //prepare available options
            //model.AvailableCustomerClassifications = CustomerClassification.DailyRates.ToSelectList(false)
            //    .Select(item => new SelectListItem(item.Text, item.Value)).ToList();
            //model.AvailablePickupTypes = PickupType.DailyPickup.ToSelectList(false)
            //    .Select(item => new SelectListItem(item.Text, item.Value)).ToList();
            //model.AvailablePackagingTypes = PackagingType.CustomerSuppliedPackage.ToSelectList(false)
            //    .Select(item => new SelectListItem(item.Text?.TrimStart('_'), item.Value)).ToList();
            //model.AvaliablePackingTypes = PackingType.PackByDimensions.ToSelectList(false)
            //    .Select(item => new SelectListItem(item.Text, item.Value)).ToList();
            //model.AvailableCarrierServices = DeliveryService.Standard.ToSelectList(false).Select(item =>
            //{
            //    var serviceCode = _upsService.GetUpsCode((DeliveryService)int.Parse(item.Value));
            //    return new SelectListItem($"UPS {item.Text?.TrimStart('_')}", serviceCode, servicesCodes.Contains(serviceCode));
            //}).ToList();
            model.AvailableCarrierServices = new List<SelectListItem>() {
                new SelectListItem() { Value = "UPS Next Day Air" },
                new SelectListItem() { Value = "UPS 2Nd Day Air" },
                new SelectListItem() { Value = "UPS Next Day Air Early" },
                new SelectListItem() { Value = "UPS Next Day Air Saver" },
                new SelectListItem() { Value = "UPS 2Nd Day Air A.M." },
                new SelectListItem() { Value = "UPS 3 Day Select" },
                new SelectListItem() { Value = "UPS Ground" },
                new SelectListItem() { Value = "FedEx Priority Overnight" },
                new SelectListItem() { Value = "FedEx First Overnight" },
                new SelectListItem() { Value = "FedEx Standard Overnight" },
                new SelectListItem() { Value = "FedEx 2Day" },
                new SelectListItem() { Value = "FedEx 2Day A.M." },
                new SelectListItem() { Value = "FedEx Ground" },
                new SelectListItem() { Value = "FedEx Express Saver" }
            };
            model.AvailableCarrierServices.ToList().ForEach(item => {
                item.Selected = servicesCodes.Contains(item.Value);
            });
            model.AvaliableWeightTypes = new List<SelectListItem> { new SelectListItem("LB", "LB"), new SelectListItem("KG", "KG") };
            model.AvaliableDimensionsTypes = new List<SelectListItem> { new SelectListItem("IN", "IN"), new SelectListItem("CM", "CM") };

            //check measures
            //var weightSystemName = _jusdaSettings.WeightType switch { "LBS" => "lb", "KGS" => "kg", _ => null };
            //if (_measureService.GetMeasureWeightBySystemKeyword(weightSystemName) == null)
            //    _notificationService.ErrorNotification($"Could not load '{weightSystemName}' <a href=\"{Url.Action("List", "Measure")}\" target=\"_blank\">measure weight</a>", false);

            //var dimensionSystemName = _jusdaSettings.DimensionsType switch { "IN" => "inches", "CM" => "centimeters", _ => null };
            //if (_measureService.GetMeasureDimensionBySystemKeyword(dimensionSystemName) == null)
            //    _notificationService.ErrorNotification($"Could not load '{dimensionSystemName}' <a href=\"{Url.Action("List", "Measure")}\" target=\"_blank\">measure dimension</a>", false);

            return View("~/Plugins/Shipping.Jusda/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(JusdaShippingModel model)
        {
            //whether user has the authority to manage configuration
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageShippingSettings))
                return AccessDeniedView();

            if (!ModelState.IsValid)
                return Configure();

            //save settings
            _jusdaSettings.AccountNumber = model.AccountNumber;
            _jusdaSettings.AccessKey = model.AccessKey;
            //_jusdaSettings.Username = model.Username;
            //_jusdaSettings.Password = model.Password;
            _jusdaSettings.UseSandbox = model.UseSandbox;
            //_jusdaSettings.AdditionalHandlingCharge = model.AdditionalHandlingCharge;
            //_jusdaSettings.CustomerClassification = (CustomerClassification)model.CustomerClassification;
            //_jusdaSettings.PickupType = (PickupType)model.PickupType;
            //_jusdaSettings.PackagingType = (PackagingType)model.PackagingType;
            //_jusdaSettings.InsurePackage = model.InsurePackage;
            //_jusdaSettings.SaturdayDeliveryEnabled = model.SaturdayDeliveryEnabled;
            //_jusdaSettings.PassDimensions = model.PassDimensions;
            //_jusdaSettings.PackingPackageVolume = model.PackingPackageVolume;
            //_jusdaSettings.PackingType = (PackingType)model.PackingType;
            //_jusdaSettings.Tracing = model.Tracing;
            _jusdaSettings.WeightType = model.WeightType;
            _jusdaSettings.DimensionsType = model.DimensionsType;

            //use default services if no one is selected 
            if (!model.CarrierServices.Any())
            {
                model.CarrierServices = new List<string>
                {
                    "UPS Next Day Air",
                    "UPS Ground",
                    "FedEx Ground",
                    "FedEx Priority Overnight",
                    "FedEx First Overnight",
                    "FedEx Standard Overnight",
                    //_upsService.GetUpsCode(DeliveryService.Ground),
                    //_upsService.GetUpsCode(DeliveryService.WorldwideExpedited),
                    //_upsService.GetUpsCode(DeliveryService.Standard),
                    //_upsService.GetUpsCode(DeliveryService._3DaySelect)
                };
            }
            _jusdaSettings.CarrierServicesOffered = string.Join(':', model.CarrierServices.Select(service => $"[{service}]"));

            _settingService.SaveSetting(_jusdaSettings);

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        #endregion
    }
}