using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjection.Services.Membership
{
    public class MembershipAdvert : IMembershipAdvert
    {
        public MembershipAdvert(decimal offerPrice, decimal discount)
        {
            OfferPrice = offerPrice;
            Saving = discount;
        }

        public decimal OfferPrice { get; }

        public decimal Saving { get; }

    }
}
