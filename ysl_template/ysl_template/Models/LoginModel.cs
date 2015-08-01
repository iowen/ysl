using System;
using System.ComponentModel.DataAnnotations;
namespace ysl_template.Models
{
	public class LoginModel
	{
		[Display(Name = "Email"), Required]
		public string Email
		{
			get;
			set;
		}
		[DataType(DataType.Password), Display(Name = "Password"), Required]
		public string Password
		{
			get;
			set;
		}
	}
}
