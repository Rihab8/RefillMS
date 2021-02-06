using MailOrderPharmacy_RefillService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailOrderPharmacy_RefillService.Repository
{
    public interface IRefillRepository
    {

        public RefillOrder AddRefillStatus(Subscription subscription);
        public RefillOrder ViewRefillStatus(int subscriptionId);
        public List<RefillOrder> RefillDues(int subscriptionId, DateTime date);
        public RefillOrderLineItem RequestAdhocRefill(int subscriptionId,int policyId,int memberId,string location);
    }
}
