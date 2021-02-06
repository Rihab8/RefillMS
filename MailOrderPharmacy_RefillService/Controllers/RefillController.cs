using MailOrderPharmacy_RefillService.Models;
using MailOrderPharmacy_RefillService.Repository;
using MailOrderPharmacy_RefillService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MailOrderPharmacy_RefillService.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RefillController : ControllerBase
    {
         readonly IRefillService _refillService;

        public RefillController(IRefillService refillService)
        {

            _refillService = refillService;
        }

        //This method returns the refillstatus searched by subscriptionId
        [HttpGet("{subscriptionId}")]
        public IActionResult GetRefillStatus(int subscriptionId)
        {
            if (subscriptionId > 0)
            {
                var details = _refillService.ViewRefillStatus(subscriptionId);
                if (details != null)
                    return Ok(details);
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult AddRefillStatus(Subscription subscription)
        {
            if (subscription!=null)
            {
                var details = _refillService.AddRefillStatus(subscription);
                if (details != null)
                    return Ok(details);
            }
            return BadRequest();
        }

        //This method will communicate with the subscription microservice to check the frequency and then return the subscriptionId along with the dates.
        [HttpGet]
        public IActionResult GetRefillDueAsOfDate(int subscriptionId, DateTime date)
        {
            RefillHelper.SessionToken = this.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);
            if (subscriptionId > 0 && date != null)
            {
                var details = _refillService.RefillDues(subscriptionId, date);
                if (details != null)
                    return Ok(details);
            }
            return BadRequest();
        }


        //This method will communicate with the drugs microservice to check for the location then return the refill details.
        [HttpGet("{subscriptionId}/{policyId}/{memberId}/{location}")]
        public IActionResult AdhocRefill(int subscriptionId, int policyId, int memberId, string location)
        {
            RefillHelper.SessionToken = this.HttpContext.Request.Headers["Authorization"].ToString().Substring(7);

            if (subscriptionId > 0 && policyId > 0 && memberId > 0 && location != null)
            {
                var details = _refillService.RequestAdhocRefill(subscriptionId, policyId, memberId, location);
                if (details != null)
                    return Ok(details);
            }
            return BadRequest();
            
        }
       
    }
}
