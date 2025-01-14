using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.DTOs
{
	public class PurchaseRequestDto
	{
		public Guid UserId { get; set; }
		public Guid ItemId { get; set; }
	}
}