using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoanMonhoc_WebCayCanh.Models
{
	public class ShopDBContext : DbContext
	{
		public ShopDBContext() { }
		public  DbSet<Product> Products { get; set; }
		public  DbSet<TypeProduct> TypeProducts { get; set; }

		public  DbSet<RoleMaster> RoleMasters { get; set; }
		public  DbSet<UserRoleMapping> UserRoleMappings { get; set; }
		public  DbSet<User> Users { get; set; }
		public DbSet<OrderDetails> OrderDetailss { get; set; }
		public  DbSet<OrderHeader> OrderHeaders { get; set; }
		public  DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<Feedback> Feedbacks { get; set; }
	}
}