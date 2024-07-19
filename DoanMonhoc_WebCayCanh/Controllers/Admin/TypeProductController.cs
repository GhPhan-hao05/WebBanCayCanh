using DoanMonhoc_WebCayCanh.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoanMonhoc_WebCayCanh.Controllers.Admin
{
	[Authorize(Roles = "Admin")]
	public class TypeProductController : Controller
    {
		private readonly ShopDBContext context = new ShopDBContext();
		// GET: TypeProduct
		public ActionResult Index()
		{
			var listType = from type in context.TypeProducts select type;
			return View(listType.ToList());
		}
		public ActionResult CreateType()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateType(TypeProduct type)
		{
			if (ModelState.IsValid)
			{
				context.TypeProducts.Add(type);
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(type);
		}
		public ActionResult EditType(int id)
		{
			TypeProduct type = context.TypeProducts.FirstOrDefault(t=>t.IdType == id);
			return View(type);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult EditType(TypeProduct type)
		{
			if (ModelState.IsValid)
			{
				var old = context.TypeProducts.FirstOrDefault(t => t.IdType == type.IdType);
				old.TenLoai= type.TenLoai;
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(type);
		}
		public ActionResult DeleteType(int id)
		{
				var typedel = context.TypeProducts.FirstOrDefault(t => t.IdType == id);
			var prd = context.Products.FirstOrDefault(x => x.IdType == id);
			if (prd != null)
			{
				ViewBag.Exist = "Loại sản phẩm hiện đang có sản phẩm đang tồn tại, bạn không thể xóa ?";
			}
			return View(typedel);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]

		public ActionResult DeleteType(TypeProduct type)
		{
			var x = context.TypeProducts.FirstOrDefault(t=>t.IdType==type.IdType);
			context.TypeProducts.Remove(x);
			context.SaveChanges();
			return RedirectToAction("Index");
		}

	}
}