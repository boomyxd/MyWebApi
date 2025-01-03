using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
    public class PurchaseHistory
    {
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}