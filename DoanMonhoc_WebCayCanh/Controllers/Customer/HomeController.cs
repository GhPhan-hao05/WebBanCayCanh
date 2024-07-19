using DoanMonhoc_WebCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoanMonhoc_WebCayCanh.Controllers
{
	public class HomeController : Controller
	{
		private readonly ShopDBContext db = new ShopDBContext();

		public ActionResult Index()
		{
			var allprd = from all in db.Products select all;
			return View(allprd.ToList());
		}
		public ActionResult Details(int id)
		{
			var detail = db.Products.FirstOrDefault(p => p.Id == id);
			return View(detail);
		}

		public ActionResult Detailsx(int id)
		{
			var detail = db.Products.Where(p => p.IdType == id);
			ViewBag.Nhom= db.TypeProducts.FirstOrDefault(p => p.IdType == id).TenLoai;
			return View(detail.ToList());
		}
		[Authorize(Roles = "Customer")]
		public ActionResult DonHang()
		{
			int idnguoidung = Convert.ToInt32(Request.Cookies["UserId"].Value);
			var allpurchase = db.OrderHeaders.Where(t=>t.UserId==idnguoidung);
			return View(allpurchase.ToList());
		}

		public ActionResult Portal()
		{
			

			return View();
		}
		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
		[Authorize(Roles = "Customer")]
		public ActionResult FeedBack(string noidung, int nguoidung, int idsanpham)
		{
			Feedback fb = new Feedback();
			fb.IdSanPham = idsanpham;
			fb.IdUser = nguoidung;
			fb.NoiDung = noidung;
			db.Feedbacks.Add(fb);
			db.SaveChanges();
			return RedirectToAction("Details", new { id = idsanpham });
		}
	}
}