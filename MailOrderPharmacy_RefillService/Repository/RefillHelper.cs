using MailOrderPharmacy_RefillService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailOrderPharmacy_RefillService.Repository
{
    public class RefillHelper
    {
        public static string SessionToken { get; set; }

        public static List<RefillOrder> refill = new List<RefillOrder>
        {
            new RefillOrder
            {
                RefillOrderId=1,
                SubscriptionId = 1,
                DrugId=1,
                DrugName = "Paracetamol",
                Quantity = 10,
                Payment = "Pending",
                RefillDate=new DateTime(2020,12,15),
                NextRefillDate=new DateTime(2021,01,15),
                PolicyId = 10,
                MemberId = 1

            },
            new RefillOrder
            {
                RefillOrderId=2,
                SubscriptionId = 2,
                DrugId=2,
                DrugName = "Saridon",
                Quantity = 20,
                Payment = "Clear",
                RefillDate=new DateTime(2021,01,07),
                NextRefillDate=new DateTime(2021,10,14),
                PolicyId = 11,
                MemberId = 2
            }
        };



        public static List<RefillOrderLineItem> item = new List<RefillOrderLineItem>
        {
            new RefillOrderLineItem
            {
                RefillOrderId=1,
                SubscriptionId = 1,
                DrugId = 1,
                DrugName = "Paracetamol",
                Quantity = 10,
                PolicyId = 10,
                MemberId = 1,
                MemberLocation="Delhi"

            }

        };
    }
}
