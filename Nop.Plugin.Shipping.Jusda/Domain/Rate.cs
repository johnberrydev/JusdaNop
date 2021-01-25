using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Nop.Plugin.Shipping.Jusda.Domain
{
    public class RateRequest
    {
        //[JsonIgnore]
        //public Guid RateRequestId { get; set; }

        /// <summary>
        /// Custom reference the requesting party can provide. Will be returned in the Response.
        /// </summary>
        [StringLength(100)]
        public String RateRequestIdentifier { get; set; }

        [StringLength(100)]
        public string RequestParty { get; set; }

        //[JsonIgnore]
        //public DateTime RequestDate { get; set; }

        /// <summary>
        /// Acceptable values: Parcel, LTL, ALL
        /// </summary>
        public string Mode { get; set; }

        //[JsonIgnore]
        //public Guid? ShipperOrganizationId { get; set; }

        //[JsonIgnore]
        //public Organization ShipperOrganization { get; set; }

        //[JsonIgnore]
        //public Guid? ShipperAddressId { get; set; }

        //[JsonIgnore]
        //public Address ShipperAddress { get; set; }

        [StringLength(100)]
        public string ShipperName { get; set; }

        [StringLength(100)]
        public string ShipperAddress1 { get; set; }

        [StringLength(100)]
        public string ShipperAddress2 { get; set; }

        [StringLength(100)]
        public string ShipperCity { get; set; }

        [StringLength(100)]
        public string ShipperStateProvince { get; set; }

        [StringLength(100)]
        public string ShipperPostalCode { get; set; }

        /// <summary>
        /// Two digit ISO Country code like US or CN
        /// </summary>
        [StringLength(2)]
        public string ShipperCountry { get; set; }

        [StringLength(100)]
        public string ShipToName { get; set; }

        [StringLength(100)]
        public string ShipToAddress1 { get; set; }

        [StringLength(100)]
        public string ShipToAddress2 { get; set; }

        [StringLength(100)]
        public string ShipToCity { get; set; }

        [StringLength(100)]
        public string ShipToStateProvince { get; set; }

        /// <summary>
        /// Two digit ISO Country code like US or CN
        /// </summary>
        [StringLength(100)]
        public string ShipToPostalCode { get; set; }

        [StringLength(2)]
        public string ShipToCountry { get; set; }

        public int? Pieces { get; set; }
        public Decimal Weight { get; set; }

        [StringLength(2)]
        public string WeightUnit { get; set; }

        public Decimal? Length { get; set; }
        public Decimal? Width { get; set; }
        public Decimal? Height { get; set; }

        [StringLength(20)]
        public string TextDims { get; set; }

        [StringLength(2)]
        public string DimUnit { get; set; }

        public DateTime? ShipDateTime { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(16,4)")]
        public decimal? DeclaredValue { get; set; }

        [StringLength(3)]
        public string DeclaredValueCurrencyCode { get; set; }

        [StringLength(100)]
        public string ShipperReferenceNo { get; set; }

        public decimal? FreightClass { get; set; }

        [StringLength(100)]
        public string Nmfc { get; set; }
        public bool Hazardous { get; set; }

        //[JsonIgnore]
        //public string ServiceLevel { get; set; }

        [StringLength(100)]
        public string CarrierName { get; set; }

        public bool OriginResidential { get; set; }
        public bool DestinationResidential { get; set; }
        public bool OriginWeekend { get; set; }
        public bool DestinationWeekend { get; set; }

        public decimal? ShippingCost { get; set; }

        //[JsonIgnore]
        //public bool Processed { get; set; }
        [NotMapped]
        public string[] Carriers { get; set; }

        //public Guid? DomainId { get; set; }
        //public Domain Domain { get; set; }

        [JsonProperty("items")]
        public List<RateRequestItem> RateRequestItems { get; set; }

        public List<RateRequestCarrier> RateRequestCarriers { get; set; }

        //[JsonIgnore]
        ////Default environment to production
        //public Guid? EnvironmentId { get; set; } = Guid.Parse("ECB20A9E-ADE4-415B-A27C-C58B4A756A4A");
        //[JsonIgnore]
        //public Environment Environment { get; set; }

    }

    public class RateRequestItem
    {
        [JsonIgnore]
        public Guid RateRequestItemId { get; set; }
        [JsonIgnore]
        public Guid RateRequestId { get; set; }

        [StringLength(100)]
        public string Description { get; set; }
        public int Pieces { get; set; }
        public string PackType { get; set; }
        public decimal? Weight { get; set; }
        [StringLength(2)]
        public string WeightUnit { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }

        [StringLength(2)]
        public string DimUnit { get; set; }
        public decimal? FreightClass { get; set; }
        [StringLength(100)]
        public string Nmfc { get; set; }
        public bool Hazardous { get; set; }

    }

    public class RateRequestCarrier
    {
        [JsonIgnore]
        public Guid RateRequestCarrierId { get; set; }
        [JsonIgnore]
        public Guid RateRequestId { get; set; }
        [StringLength(100)]
        public string Carrier { get; set; }
    }
    public class RateResponse
    {
        public Guid RateResponseId { get; set; }

        public Guid RateRequestId { get; set; }

        [JsonIgnore]
        public RateRequest RateRequest { get; set; }

        /// <summary>
        /// Custom reference provided by the requesting party in the Rate Request
        /// </summary>
        [NotMapped]
        public String RateRequestIdentifier { get; set; }

        public DateTime ResponseDate { get; set; } = DateTime.UtcNow;

        [StringLength(100)]
        public string QuoteNo { get; set; }

        [StringLength(1000)]
        public string ResponseMessage { get; set; }

        [StringLength(100)]
        public string Mode { get; set; }

        [StringLength(100)]
        public string Provider { get; set; }

        [StringLength(100)]
        public string CarrierName { get; set; }

        [NotMapped]
        [StringLength(400)]
        public string CarrierLogoUrl { get; set; }

        [StringLength(100)]
        public string Service { get; set; }

        public decimal BillingWeight { get; set; }

        [StringLength(2)]
        public string BillingWeightUnit { get; set; }

        public decimal TransportationCharge { get; set; }

        public decimal AccessorialCharge { get; set; }

        public decimal TotalCharge { get; set; }

        public decimal DiscountedCharge { get; set; }

        public int TransitDays { get; set; }

        public DateTime? EstDeliveryDate { get; set; }

        [StringLength(3)]
        public string CurrencyCode { get; set; }

        public bool BookingSupported { get; set; }
        [StringLength(400)]
        public string BookingUrl { get; set; }

        //public Guid? DomainId { get; set; }
        //public Domain Domain { get; set; }

        public List<RateResponseCharge> Charges { get; set; }
        public List<RateResponseMessage> Messages { get; set; }

    }

    public class RateResponseCharge
    {
        [JsonIgnore]
        public Guid RateResponseChargeId { get; set; }

        [JsonIgnore]
        public Guid RateResponseId { get; set; }
        [JsonIgnore]
        public RateResponse RateResponse { get; set; }

        [StringLength(100)]
        public string ChargeType { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public decimal Rate { get; set; }
        public decimal Amount { get; set; }

        [StringLength(3)]
        public string CurrencyCode { get; set; }
        public decimal? Weight { get; set; }
    }

    public class RateResponseMessage
    {
        [JsonIgnore]
        public Guid RateResponseMessageId { get; set; }

        [JsonIgnore]
        public Guid RateResponseId { get; set; }
        [JsonIgnore]
        public RateResponse RateResponse { get; set; }

        [StringLength(100)]
        public string Code { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(100)]
        public string Severity { get; set; }
    }

    //public class RateMarkup
    //{
    //    public Guid RateMarkupId { get; set; }
    //    //public Guid? DomainId { get; set; }
    //    //public Domain Domain { get; set; }
    //    public Guid ModeId { get; set; }
    //    public Mode Mode { get; set; }

    //    public Guid? ContainerModeId { get; set; }
    //    public ContainerMode ContainerMode { get; set; }
    //    public decimal? MarkupPercent { get; set; }
    //    public decimal? AddOn { get; set; }

    //    [ForeignKey("Person")]
    //    public Guid? SalesRepId { get; set; }
    //    public Person SalesRep { get; set; }

    //    [ForeignKey("Person")]
    //    public Guid? HandlingAgentId { get; set; }
    //    public Person HandlingAgent { get; set; }

    //    [StringLength(400)]
    //    public string NotificationEmail { get; set; }

    //    public List<RateMarkupCarrier> RateMarkupCarriers { get; set; }

    //    [JsonIgnore]
    //    public DateTime EntryDate { get; set; } = DateTime.UtcNow;

    //    [JsonIgnore]
    //    public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
    //}

    //public class RateMarkupCarrier
    //{
    //    public Guid RateMarkupCarrierId { get; set; }

    //    public Guid RateMarkupId { get; set; }
    //    public RateMarkup RateMarkup { get; set; }

    //    public Guid? CarrierId { get; set; }
    //    public Organization Carrier { get; set; }

    //    [StringLength(200)]
    //    public string CarrierAccountNo { get; set; }

    //    [JsonIgnore]
    //    public DateTime EntryDate { get; set; } = DateTime.UtcNow;

    //    [JsonIgnore]
    //    public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
    //}
}
