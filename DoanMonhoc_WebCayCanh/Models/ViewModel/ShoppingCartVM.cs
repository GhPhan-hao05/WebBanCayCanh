using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoanMonhoc_WebCayCanh.Models.ViewModel
{
	public class ShoppingCartVM
	{
		public IEnumerable<ShoppingCart> ListCart { get; set; }

		public OrderHeader OrderHeader { get; set; }
	}
}