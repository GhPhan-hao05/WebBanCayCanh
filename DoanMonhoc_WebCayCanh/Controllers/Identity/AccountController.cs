using DoanMonhoc_WebCayCanh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DoanMonhoc_WebCayCanh.Controllers.Identity
{
    public class AccountController : Controller
    {
		private readonly ShopDBContext context = new ShopDBContext();
		// GET: Account
		
		public ActionResult Login()
		{
			return View();
		}
		public ActionResult XemRole()
		{
			var x = from all in context.RoleMasters select all;
			return View(x.ToList());
		}
		public ActionResult CreateRole()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateRole(RoleMaster role)
		{
			if (ModelState.IsValid)
			{
				context.RoleMasters.Add(role);
				context.SaveChanges();
				return RedirectToAction("Login");
			}
			return View(role);
		}

		[HttpPost]
		public ActionResult Login(UserModel model)
		{
				bool IsValidUser = context.Users.Any(user => user.UserName.ToLower() == model.UserName.ToLower() && user.UserPassword == model.UserPassword);
				var currentUser = context.Users.FirstOrDefault(user => user.UserName.ToLower() == model.UserName.ToLower() && user.UserPassword == model.UserPassword);
				if (IsValidUser)
				{
					FormsAuthentication.SetAuthCookie(model.UserName, false);
					HttpCookie cookie = new HttpCookie("UserId", currentUser.ID.ToString());
					Response.Cookies.Add(cookie);
				if (currentUser.Role == "Admin")
				{
					return RedirectToAction("Index", "Product");
				}
				else
					return RedirectToAction("Portal", "Home");
			}
				ModelState.AddModelError("", "invalid Username or Password");
				return View();	
		}

		public ActionResult Signup()
		{
			User newUser = new User(){};

			newUser.RoleList = context.RoleMasters.ToList().Select(
			i => new SelectListItem
			{
				Text = i.RoleName,
				Value = i.RoleName
			});


			return View(newUser);
		}
		[HttpPost]
		public ActionResult Signup(User model)
		{
			bool isduplicate = context.Users.Any(user => user.UserName == model.UserName);

			if(!isduplicate)
			{
				UserRoleMapping temp = new UserRoleMapping();
				context.Users.Add(model);
				context.SaveChanges();
				var tempRole = context.RoleMasters.FirstOrDefault(t => t.RoleName == model.Role);
				temp.RoleID = tempRole.ID;
				temp.UserID = model.ID;
				context.UserRoleMappings.Add(temp);
				context.SaveChanges();
				return RedirectToAction("Login");
			}
			else
			{
				ViewBag.dup = "Tên người dùng đã tồn tại";
				return View(model);
			}
		}

		public ActionResult Logout()
		{
			FormsAuthentication.SignOut();
			return RedirectToAction("Login");

		}
	}
}