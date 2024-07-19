using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoanMonhoc_WebCayCanh.Models
{
	public class User
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214: DoNotCallOverridableMethodsInConstructors")]

		public User()
		{
			this.UserRolesMappings = new HashSet<UserRoleMapping>();

		}
		[Key]
		public int ID { get; set; }
		public string UserName { get; set; }
		public string UserPassword { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string Phone { get; set; }
		public string Role { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227: CollectionPropertiesShouldBeReadOnly")]
		public virtual IEnumerable<SelectListItem> RoleList { get; set; }
		public virtual ICollection<UserRoleMapping> UserRolesMappings { get; set; }
	}
}