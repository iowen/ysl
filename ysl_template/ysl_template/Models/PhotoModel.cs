using System;
using System.ComponentModel.DataAnnotations;
namespace ysl_template.Models
{
	public class PhotoModel
	{
		[Display(Name = "Title"), Required]
		public string Title
		{
			get;
			set;
		}
		[Display(Name = "Description")]
		public string Description
		{
			get;
			set;
		}
		public string Location
		{
			get;
			set;
		}
		public int PhotoId
		{
			get;
			set;
		}
	}
}
