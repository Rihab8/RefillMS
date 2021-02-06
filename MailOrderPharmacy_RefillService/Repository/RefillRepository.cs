using MailOrderPharmacy_RefillService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MailOrderPharmacy_RefillService.Repository;
using System.Net.Http.Headers;

namespace MailOrderPharmacy_RefillService.Repository
{
    public class RefillRepository : IRefillRepository
    {
        
        public RefillOrder ViewRefillStatus(int subscriptionId)
        {
            var drug = RefillHelper.refill.FirstOrDefault(x => x.SubscriptionId == subscriptionId);
            if (drug == null)
                return null;
            return drug;

        }

        public RefillOrder AddRefillStatus(Subscription subscription)
        {
            Random random = new Random();
            RefillOrder refillOrder = new RefillOrder()
            {
                RefillOrderId = random.Next(9,50),
                SubscriptionId = subscription.SubscriptionId,
                DrugId = subscription.DrugId,
                DrugName = subscription.DrugName,
                Quantity = random.Next(3,10),
                Payment = "Pending",
                RefillDate = DateTime.Today,              
                PolicyId = random.Next(10,1000),
                MemberId = subscription.MemberId
            };
            if (subscription.RefillOccurrence == "Monthly") refillOrder.NextRefillDate = refillOrder.RefillDate.AddMonths(1);
            else refillOrder.NextRefillDate = refillOrder.RefillDate.AddDays(7);

            if (refillOrder == null) return null;
            RefillHelper.refill.Add(refillOrder);
            return refillOrder;

        }

        public List<RefillOrder> RefillDues(int subscriptionId, DateTime date)
        {
            var refilldue = RefillHelper.refill.FirstOrDefault(x => x.SubscriptionId == subscriptionId && x.Payment == "Pending" && x.RefillDate == date);
           
            if (refilldue == null) return null;
            string frequency = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RefillHelper.SessionToken);

                client.BaseAddress = new Uri("https://localhost:5002/api/");//Target Web Api

                var responseTask = client.GetAsync("Subscribe/GetSubscriptionDetails/" + refilldue.SubscriptionId);

                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;

                    var subscription = JsonConvert.DeserializeObject<Subscription>(data);

                    frequency = subscription.RefillOccurrence;

                    List<RefillOrder> refillDetails = new List<RefillOrder>();
                    refillDetails = Occurrence(subscriptionId, frequency, date);
                    return refillDetails;
                }
                else
                {
                    return null;
                }
            }
        }

        //Check for the frequency
        public List<RefillOrder> Occurrence(int subscriptionId,string frequency,DateTime date)
        {
            List<RefillOrder> details = new List<RefillOrder>();

            if (string.Equals(frequency, "Monthly"))
            {
               
                    RefillOrder refills = new RefillOrder();
                    var refill = RefillHelper.refill.SingleOrDefault(s => s.SubscriptionId == subscriptionId);
                    refills.RefillOrderId = refill.RefillOrderId;
                    refills.DrugId = refill.DrugId;
                    refills.DrugName = refill.DrugName;
                    refills.Quantity = refill.Quantity;
                    refills.Payment = refill.Payment;
                    refills.PolicyId= refill.PolicyId;
                    refills.MemberId= refill.MemberId;
                    refills.SubscriptionId = subscriptionId;
                    date = date.AddMonths(1);
                    refills.RefillDate = date;
                    refills.NextRefillDate = date.AddMonths(1);
                    details.Add(refills);
                   

                
            }

            else if (string.Equals(frequency, "Weekly"))
            {
                RefillOrder refills = new RefillOrder();
                var refill = RefillHelper.refill.SingleOrDefault(s => s.SubscriptionId == subscriptionId);
                refills.RefillOrderId = refill.RefillOrderId;
                refills.DrugId = refill.DrugId;
                refills.DrugName = refill.DrugName;
                refills.Quantity = refill.Quantity;
                refills.Payment = refill.Payment;
                refills.PolicyId = refill.PolicyId;
                refills.MemberId = refill.MemberId;
                refills.SubscriptionId = subscriptionId;
                date = date.AddDays(7);
                refills.RefillDate = date;
                refills.NextRefillDate = date.AddDays(7);
                details.Add(refills);
            }

            else
            {
               
                RefillOrder refills = new RefillOrder();
                var refill = RefillHelper.refill.SingleOrDefault(s => s.SubscriptionId == subscriptionId);
                refills.RefillOrderId = refill.RefillOrderId;
                refills.DrugId = refill.DrugId;
                refills.DrugName = refill.DrugName;
                refills.Quantity = refill.Quantity;
                refills.Payment = refill.Payment;
                refills.PolicyId = refill.PolicyId;
                refills.MemberId = refill.MemberId;
                refills.SubscriptionId = subscriptionId;
                date = date.AddYears(1);
                refills.RefillDate = date;
                refills.NextRefillDate = date.AddYears(1);
                details.Add(refills);
             




            }
            return details;
        }


        public RefillOrderLineItem RequestAdhocRefill(int subscriptionId, int policyId, int memberId, string location) 
        {

            RefillOrderLineItem detail = RefillHelper.item.FirstOrDefault(x => x.MemberId == memberId);
            if (detail == null) return null;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", RefillHelper.SessionToken);
                client.BaseAddress = new Uri("https://localhost:5009/api/");//Target Web Api

                var responseTask = client.GetAsync("DrugsApi/SearchDrugsById/" + detail.DrugId);

                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    Drug drug = JsonConvert.DeserializeObject<Drug>(data);

                    if (drug.DrugLocation.Location == location)
                    {
                        detail.MemberLocation = location;
                        return (detail);
                    }
                        
                    return (null);
                }
                return null;
            }
        }

    }
}
