using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Models
{
	public class Login
	{
		public Guid UserId { get; set; }
		public DateTime LoginTime { get; set; }
		public string IpAddress { get; set; }
	}
}