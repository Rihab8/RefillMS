using MailOrderPharmacy_RefillService.Models;
using MailOrderPharmacy_RefillService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailOrderPharmacy_RefillService.Service
{
    public class RefillService : IRefillService
    {
        private readonly IRefillRepository _refillRepository;

        public RefillService(IRefillRepository refillRepository)
        {
            _refillRepository = refillRepository;
        }
        public RefillOrder AddRefillStatus(Subscription subscription)
        {
            return _refillRepository.AddRefillStatus(subscription);
        }

        public List<RefillOrder> RefillDues(int subscriptionId, DateTime date)
        {
            return _refillRepository.RefillDues(subscriptionId, date);
        }

        public RefillOrderLineItem RequestAdhocRefill(int subscriptionId, int policyId, int memberId, string location)
        {
            return _refillRepository.RequestAdhocRefill(subscriptionId, policyId, memberId, location);
        }

        public RefillOrder ViewRefillStatus(int subscriptionId)
        {
            return _refillRepository.ViewRefillStatus(subscriptionId);
        }
    }
}
