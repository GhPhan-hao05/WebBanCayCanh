using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoanMonhoc_WebCayCanh.Models
{
	public class OrderHeader
	{
		[Key]
		public int Id { get; set; }
		public DateTime? OrderDate { get; set ; }
		public double OrderTotal { get; set; }
		public string OrderStatus { get; set; }
		public string TenNguoiNhan { get; set; }
		public string DiaChiNhan { get; set; }
		public string SoDTNguoiNhan { get; set; }
		public int UserId { get; set; }
		
		[ForeignKey("UserId")]
		public User User { get; set; }
		public OrderHeader()
		{
			OrderDate = null; // Set the initial value to null
		}
	}
}