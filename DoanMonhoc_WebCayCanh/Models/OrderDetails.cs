using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoanMonhoc_WebCayCanh.Models
{
	public class OrderDetails
	{
		[Key]
		public int Id { get; set; }
		public int OrderId { get; set; }
		[ForeignKey("OrderId")]
		public OrderHeader OrderHeader { get; set; }
		public int ProductId { get; set; }
		[ForeignKey("ProductId")]
		public Product Product { get; set; }
		public int Count { get; set; }
	}
}
//order detail tuong duong voi thong tin nguoi mua + hang