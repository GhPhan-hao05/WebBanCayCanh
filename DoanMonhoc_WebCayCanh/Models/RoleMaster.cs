using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoanMonhoc_WebCayCanh.Models
{
	public class RoleMaster
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214: DoNotCallOverridableMethodsInConstructors")]
		public RoleMaster()
		{
			this.UserRolesMappings = new HashSet<UserRoleMapping>();
		}
		public int ID { get; set; }
		public string RoleName { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227: CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<UserRoleMapping> UserRolesMappings { get; set; }
	}
}