using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
	public class Item : BaseEntity
	{
		public string Name { get; set; }
		[Required]
		[Range(0, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
		public decimal Price { get; set; } = 0;
	}
}