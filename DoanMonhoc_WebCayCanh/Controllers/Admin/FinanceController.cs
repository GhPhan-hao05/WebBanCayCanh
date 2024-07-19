using DoanMonhoc_WebCayCanh.Models;
using DoanMonhoc_WebCayCanh.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoanMonhoc_WebCayCanh.Controllers.Admin
{
	[Authorize(Roles = "Admin")]
	public class FinanceController : Controller
    {
		//finance se luu nhieu ordertails
		private readonly ShopDBContext db = new ShopDBContext();                                                                                                                                                                                                                                                                                                                                                                                                     
		// GET: Finance
		public ActionResult DoanhThu()
        {
			double total = 0;
            var allpurchased = from all in db.OrderHeaders where all.OrderStatus == SD.DaThanhToan || all.OrderStatus == SD.ChoVanChuyen select all;
			foreach (var item in allpurchased)
			{
				
				total += item.OrderTotal;
			}
			ViewBag.TotalDoanhThu= total;	
			return View();
        }

	
		public ActionResult DonHang()
		{
			var allpurchase = from all in db.OrderHeaders select all;
			return View(allpurchase.ToList());
		}
		public ActionResult Details(int id)
		{
			var detailorder = db.OrderDetailss.Where(t=>t.OrderId==id);
			return View(detailorder.ToList());
		}
		public ActionResult Delete(int id)
		{
			var detailorder = db.OrderDetailss.Where(t => t.OrderId == id);
			db.OrderDetailss.RemoveRange(detailorder);
			db.SaveChanges();
			var ordhead = db.OrderHeaders.FirstOrDefault(t => t.Id == id);
			db.OrderHeaders.Remove(ordhead);
			db.SaveChanges();
			return RedirectToAction("DonHang");
		}

	}
}