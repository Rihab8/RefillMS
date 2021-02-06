using MailOrderPharmacy_RefillService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailOrderPharmacy_RefillService.Service
{
    public interface IRefillService
    {
         RefillOrder AddRefillStatus(Subscription subscription);
         RefillOrder ViewRefillStatus(int subscriptionId);
         List<RefillOrder> RefillDues(int subscriptionId, DateTime date);
         RefillOrderLineItem RequestAdhocRefill(int subscriptionId, int policyId, int memberId, string location);
    }
}
