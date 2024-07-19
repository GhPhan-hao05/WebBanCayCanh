using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DoanMonhoc_WebCayCanh.Models
{
	public class Product
	{
		[Key]
		[Required(ErrorMessage = "Phai Nhap Id Cay")]
		[DisplayName("Ma Cay")]
		public int Id { get; set; }
		[Required(ErrorMessage = "Phai Nhap Ten Cay")]
		[DisplayName("Ten Cay")]
		public string NameProduct { get; set; }
		[Required(ErrorMessage = "Phai Nhap Mo Ta Cho Cay")]
		[DisplayName("Mo Ta Ngan")]
		public string ShortDescribe { get; set; }
		[Required(ErrorMessage = "Phai Nhap Tinh Trang")]
		[DisplayName("Tinh Trang")]
		public string Status { get; set; }
		[Required(ErrorMessage = "Phai Nhap Don Gia ")]
		[DisplayName("Don Gia")]
		public double Price { get; set; }
		public string Image { get; set; }
		[NotMapped]
		public HttpPostedFileBase FileImage { get; set; }

		[Required(ErrorMessage = "Phai Nhap Nguon Goc ")]
		[DisplayName("Nguon Goc")]
		public string Source { get; set; }
		[Required(ErrorMessage = "Phai Nhap Loai Cay")]
		[DisplayName("Chon Loai Cay")]
		public int IdType { get; set; }
		[ForeignKey("IdType")]
		public virtual TypeProduct TypeProduct { get; set; }
		public virtual IEnumerable<SelectListItem> RoleList { get; set; }

	}
}