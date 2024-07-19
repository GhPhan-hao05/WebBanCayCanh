using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoanMonhoc_WebCayCanh.Models.ViewModel
{
	public class OrderManagerVM
	{
		public int orderid { get; set; }
		public double total { get; set; }
		public string status { get; set; }
	}
}