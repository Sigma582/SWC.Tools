using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SWC.Tools.Common.Networking.Json.Entities
{
    [DataContract]
    public class PurchaseTracking
    {

        [DataMember(Name="totalSpend")]
        public double TotalSpend { get; set; }

        [DataMember(Name="totalPurchases")]
        public int TotalPurchases { get; set; }

//        [DataMember(Name="offerPurchases")]
//        public OfferPurchases offerPurchases { get; set; }

        [DataMember(Name="forcedGlobalCooldown")]
        public int ForcedGlobalCooldown { get; set; }

        [DataMember(Name="reservations")]
        public IList<object> Reservations { get; set; }
    }
}