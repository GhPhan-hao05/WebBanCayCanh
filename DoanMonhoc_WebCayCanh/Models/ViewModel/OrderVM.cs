using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoanMonhoc_WebCayCanh.Models.ViewModel
{
	public class OrderVM
	{
		public List<Product> Products { get; set; }
		public OrderHeader Orderheaders { get; set; }
	}
}