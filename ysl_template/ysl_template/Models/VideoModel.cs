using System;
using System.ComponentModel.DataAnnotations;
namespace ysl_template.Models
{
	public class VideoModel
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
	}
}
