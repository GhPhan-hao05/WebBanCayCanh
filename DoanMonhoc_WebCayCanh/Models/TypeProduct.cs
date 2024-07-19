using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DoanMonhoc_WebCayCanh.Models
{
	public class TypeProduct
	{
		[Key]
		[Required(ErrorMessage = "Phai Nhap Ma Loai cay")]
		[DisplayName("Ma Loai")]
		public int IdType { get; set; }

		[Required(ErrorMessage = "Phai Nhap Ten Loai Cay")]
		[DisplayName("Ten Loai")]
		public string TenLoai { get; set; }
		public virtual List<Product> Products { get; set; }
	}
}