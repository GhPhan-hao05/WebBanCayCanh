using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoanMonhoc_WebCayCanh.Models
{
	public class UserRoleMapping
	{
		public int ID { get; set; }
		public int UserID { get; set; }
		public int RoleID { get; set; }
		public virtual RoleMaster RoleMaster { get; set; }
		public virtual User User { get; set; }
	}
}