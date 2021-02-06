using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailOrderPharmacy_RefillService.Models
{
    public class DrugLocation
    {
        public int DrugId { get; set; }
        public string DrugName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int AvailableStock { get; set; }
        public string Location { get; set; }
    }
}
