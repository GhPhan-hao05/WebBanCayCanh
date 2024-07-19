using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoanMonhoc_WebCayCanh.Models
{
	public class ShoppingCart
	{
		[Key]
		public int Id { get; set; }
		public int ProductId { get; set; }
		[ForeignKey("ProductId")]
		public Product Product { get; set; }
		
		public int Count { get; set; }

		public int UserId { get; set; }
		[ForeignKey("UserId")]

		public virtual User User { get; set; }

	}
}