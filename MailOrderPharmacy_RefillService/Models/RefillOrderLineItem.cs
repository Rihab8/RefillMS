using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailOrderPharmacy_RefillService.Models
{
    public class RefillOrderLineItem
    {
        public int SubscriptionId { get; set; }

        public int RefillOrderId { get; set; }

        public int DrugId { get; set; }

        public string DrugName { get; set; }

        public int Quantity { get; set; }

        public int PolicyId { get; set; }

        public int MemberId { get; set; }

        public string MemberLocation { get; set; }
    }
}
