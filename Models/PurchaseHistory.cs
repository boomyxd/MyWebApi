using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
    public class PurchaseHistory : BaseEntity
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid ItemId { get; set; }
        public virtual Item Item { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}