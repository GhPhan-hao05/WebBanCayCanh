using DoanMonhoc_WebCayCanh.Models;
using DoanMonhoc_WebCayCanh.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace DoanMonhoc_WebCayCanh.Controllers.Customer
{
	[Authorize(Roles = "Customer")]
	public class CartController : Controller
    {
		
		private readonly ShopDBContext db = new ShopDBContext();

		public ShoppingCartVM ShoppingCartVM { get; set; }
		public int OrderTotal { get; set; }
		public ActionResult AddToCart(int id)
		{
			int idnguoidung = Convert.ToInt32(Request.Cookies["UserId"].Value);
			var productAdded = db.ShoppingCarts.FirstOrDefault(z => z.ProductId == id && z.UserId == idnguoidung);
			if (productAdded == null)
			{
				ShoppingCart cartObj = new ShoppingCart()
				{
					ProductId = id,
					Product = db.Products.FirstOrDefault(u => u.Id == id),
					Count = 1,
					UserId= idnguoidung
				};
				db.ShoppingCarts.Add(cartObj);
				db.SaveChanges();
			}
			else
			{
				productAdded.Count += 1;
				db.SaveChanges();
			}
			int cartcount = 0;
			var listItemCart = db.ShoppingCarts.Where(t => t.UserId == idnguoidung);
			foreach (ShoppingCart item in listItemCart)
			{
				cartcount += item.Count;
			}
			ViewBag.CartCount = cartcount;
			return RedirectToAction("Index");
		}
		public ActionResult Index()
		{
			int idnguoidung = Convert.ToInt32(Request.Cookies["UserId"].Value);
			var listItemCart = db.ShoppingCarts.Where(t => t.UserId == idnguoidung);

			ShoppingCartVM summaryCart = new ShoppingCartVM()
				{
					ListCart = listItemCart,
					OrderHeader = new OrderHeader()
				};
		
			foreach (var cart in listItemCart)
			{
				var prd=db.Products.FirstOrDefault(u => u.Id == cart.ProductId);
				summaryCart.OrderHeader.OrderTotal += (prd.Price * cart.Count);
			}
			int cartcount = 0;
			foreach (ShoppingCart item in listItemCart)
			{
				cartcount += item.Count;
			}
			ViewBag.count = cartcount;
			return View(summaryCart);
		}
		public ActionResult BuyFromCart()
		{
			int idnguoidung = Convert.ToInt32(Request.Cookies["UserId"].Value);
			OrderHeader orderheader = new OrderHeader() { };
			orderheader.UserId = idnguoidung;
			orderheader.OrderDate = System.DateTime.Now;
			orderheader.User= db.Users.FirstOrDefault(t=>t.ID== idnguoidung);
			orderheader.OrderStatus = SD.ChuaThanhToan;
			db.OrderHeaders.Add(orderheader);
			db.SaveChanges();
			ShoppingCartVM spcvm = new ShoppingCartVM()
			{
				ListCart = (from card in db.ShoppingCarts where card.UserId == idnguoidung select card).ToList(),
				OrderHeader = orderheader
			};
			foreach (var cart in spcvm.ListCart)
			{
				var prd = db.Products.FirstOrDefault(u => u.Id == cart.ProductId);
				//gán giá cho y để khi update giá cả thì giá ở những đơn hàng cũ không bị thay đổi
				double y = 0;
				y = prd.Price;
				spcvm.OrderHeader.OrderTotal += (y * cart.Count);
			}
			foreach (var cart in spcvm.ListCart)
			{
				OrderDetails orderDetail = new OrderDetails()
				{
					ProductId = cart.ProductId,
					OrderId = spcvm.OrderHeader.Id,
					Count = cart.Count
				};
				db.OrderDetailss.Add(orderDetail);
				db.SaveChanges();
			}
			return View(spcvm);
		}

		[HttpPost]
		public ActionResult BuyFromCart(ShoppingCartVM newspcvm, string paymentMethod)//truyen len phuong thưc tt và gia trị 0 hoặc 1 để biết ng dùng đã tt hay đợi trả sau
		{
			if(newspcvm.OrderHeader.TenNguoiNhan=="Tên"|| newspcvm.OrderHeader.SoDTNguoiNhan == "SDT"||newspcvm.OrderHeader.DiaChiNhan == "ĐC")
			{
				ViewBag.miss = "Vui lòng nhập đủ thông tin";
				return RedirectToAction("BuyFromCart");
			}
			else
			{
				int idnguoidung = Convert.ToInt32(Request.Cookies["UserId"].Value);
				newspcvm.ListCart = (from card in db.ShoppingCarts where card.UserId == idnguoidung select card).ToList();
				var old = db.OrderHeaders.FirstOrDefault(t => t.Id == newspcvm.OrderHeader.Id);
				old.TenNguoiNhan = newspcvm.OrderHeader.TenNguoiNhan;
				old.SoDTNguoiNhan = newspcvm.OrderHeader.SoDTNguoiNhan;
				old.DiaChiNhan = newspcvm.OrderHeader.DiaChiNhan;
				var sold = db.ShoppingCarts.Where(t => t.UserId == idnguoidung);
				db.ShoppingCarts.RemoveRange(sold);
				db.SaveChanges();
				if (paymentMethod == "By Card")
				{
					old.OrderStatus = SD.DaThanhToan;
					db.SaveChanges();
					return RedirectToAction("ThanhCong");
				}
				else
				{
					old.OrderStatus = SD.ChoVanChuyen;
					db.SaveChanges();
					return RedirectToAction("DoiShip");
				}
			}
			
			

		}
		public ActionResult BuyNow(int id)
		{
			int idnguoidung = Convert.ToInt32(Request.Cookies["UserId"].Value);
			OrderHeader orderheader = new OrderHeader() { };
			orderheader.UserId = idnguoidung;
			orderheader.User = db.Users.FirstOrDefault(t => t.ID == idnguoidung);
			orderheader.OrderDate = System.DateTime.Now;
			orderheader.OrderStatus = SD.ChuaThanhToan;

			//gán giá cho y để khi update giá cả thì giá ở những đơn hàng cũ không bị thay đổi
			var x = db.Products.FirstOrDefault(t => t.Id == id);
			double y = 0;
			y = x.Price;
			orderheader.OrderTotal = y;


			db.OrderHeaders.Add(orderheader);
			db.SaveChanges();
			OrderDetails BuyItem = new OrderDetails()
			{
				OrderId = orderheader.Id,
				OrderHeader = orderheader,
				ProductId= id,
				Product= db.Products.FirstOrDefault(t=>t.Id==id),
				Count=1
			};
			db.OrderDetailss.Add(BuyItem);
			db.SaveChanges();

			return View(BuyItem);
		}
		[HttpPost]
		
		public ActionResult BuyNow(OrderDetails BuyItem, string paymentMethod ,string soDTNguoiNhan, string tenNguoiNhan , string diaChiNhan)
		{
			if (tenNguoiNhan == "Tên" || soDTNguoiNhan == "SDT" || diaChiNhan == "ĐC")
			{
				ViewBag.miss = "Vui lòng nhập đủ thông tin";
				return RedirectToAction("BuyNow", new { id = BuyItem.ProductId });
			}
			else
			{
				var old = db.OrderHeaders.FirstOrDefault(t => t.Id == BuyItem.OrderId);
				old.TenNguoiNhan = tenNguoiNhan;
				old.DiaChiNhan = diaChiNhan;
				old.SoDTNguoiNhan = soDTNguoiNhan;

				if (paymentMethod == "By Card")
				{
					old.OrderStatus = SD.DaThanhToan;
					db.SaveChanges();
					return RedirectToAction("ThanhCong");
				}
				else
				{
					old.OrderStatus = SD.ChoVanChuyen;
					db.SaveChanges();
					return RedirectToAction("DoiShip");
				}
			}
		}
		public ActionResult Plus(int cartId)
		{
			var cart = db.ShoppingCarts.FirstOrDefault(x => x.Id == cartId);
			cart.Count += 1;
			db.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public ActionResult Minus(int cartId)
		{
			var cart = db.ShoppingCarts.FirstOrDefault(x => x.Id == cartId);
			if (cart.Count <= 1)
			{
				db.ShoppingCarts.Remove(cart);
				
			}
			else
			{
				cart.Count -= 1;
			}
			db.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		public ActionResult Remove(int cartId)
		{
			var cart = db.ShoppingCarts.FirstOrDefault(x => x.Id == cartId);
			db.ShoppingCarts.Remove(cart);
			db.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
		public ActionResult ThanhCong()
		{
			
			return View();
		}
		public ActionResult DoiShip()
		{
			return View();
		}



	}
}
//từ cart hoặc detail khi click summary/buynow hiển thị trang summary/buynow chứ thông tin header và sản phẩm
