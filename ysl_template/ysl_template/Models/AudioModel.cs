using System;
using System.ComponentModel.DataAnnotations;
namespace ysl_template.Models
{
	public class AudioModel
	{
		[Display(Name = "Title")]
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
		public int AudioId
		{
			get;
			set;
		}
	}
}
