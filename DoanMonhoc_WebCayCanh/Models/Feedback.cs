using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoanMonhoc_WebCayCanh.Models
{
	public class Feedback
	{
		[Key]
		public int Id { get; set; }
		public string NoiDung { get; set; }
		[Required]
		public int IdSanPham { get; set; }
		[ForeignKey("IdSanPham")]
		public Product Product { get; set; }
		public int IdUser { get; set; }
		[ForeignKey("IdUser")]
		public User User { get; set; }
	}
}