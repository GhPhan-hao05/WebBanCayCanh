using DoanMonhoc_WebCayCanh.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoanMonhoc_WebCayCanh.Controllers.Admin
{
	[Authorize(Roles = "Admin")]
	public class ProductController : Controller
	{
		// GET: Product
		private readonly ShopDBContext db = new ShopDBContext();
		public ActionResult Index()
		{
			var listProduct= from prd in db.Products select prd;
			return View(listProduct.ToList());
		}

		public ActionResult AddProduct()
		{
			
			
			return View();
		}
		[HttpPost]
		public ActionResult AddProduct(Product product, string Type)
		{
			if (ModelState.IsValid)
			{
				int idtype = Convert.ToInt32(Type);
				product.IdType = idtype;
					
					string FileName = Path.GetFileNameWithoutExtension(product.FileImage.FileName);
					string FileExtension = Path.GetExtension(product.FileImage.FileName);
					product.Image = FileName.Trim() + FileExtension;
					string _path = Path.Combine(Server.MapPath("~/Content/Images/"), product.Image);
					product.FileImage.SaveAs(_path);
					db.Products.Add(product);
					db.SaveChanges();
					return RedirectToAction("Index");
				

			}
			return View(product);
		}
		public ActionResult EditProduct(int id)
		{
			Product product = db.Products.FirstOrDefault(x => x.Id == id);
			return View(product);
		}
		[HttpPost]
		public ActionResult EditProduct(Product newproduct, string Type)
		{
			if (ModelState.IsValid)
			{
				int idtype = Convert.ToInt32(Type);
		
				Product old = db.Products.FirstOrDefault(v => v.Id == newproduct.Id);
					old.NameProduct = newproduct.NameProduct;
					old.ShortDescribe = newproduct.ShortDescribe;
					old.Status = newproduct.Status;
					old.Source = newproduct.Source;
					old.Price = newproduct.Price;
					old.IdType = idtype;

					string FileName = Path.GetFileNameWithoutExtension(newproduct.FileImage.FileName);
					string FileExtension = Path.GetExtension(newproduct.FileImage.FileName);
					newproduct.Image = FileName.Trim() + FileExtension;
					string _path = Path.Combine(Server.MapPath("~/Content/Images/"), newproduct.Image);
					newproduct.FileImage.SaveAs(_path);
					old.Image = newproduct.Image;
					old.FileImage = newproduct.FileImage;
					db.SaveChanges();
					return RedirectToAction("Index");
			}
			return View(newproduct);
		}
		public ActionResult DeleteProduct(int id)
		{
			var q = db.ShoppingCarts.Any(t=>t.ProductId==id);
			var w = db.ShoppingCarts.Any(z=> z.ProductId == id);
			if (!q && !w)
			{
				Product product = db.Products.FirstOrDefault(x => x.Id == id);
				db.Products.Remove(product);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			else
			{
				ViewBag.TonTai = "Sản phẩm đang trong giỏ hàng và trong các hóa đơn, nếu muốn tiếp tục xóa thì phải xóa các Giỏ hàng và hóa đơn có liên quan";
				return View();
			}
			
		}
	}
}