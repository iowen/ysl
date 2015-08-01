using System;
using System.ComponentModel.DataAnnotations;
namespace ysl_template.Models
{
	public class RegisterModel
	{
		[DataType(DataType.EmailAddress), Display(Name = "Email"), Required]
		public string Email
		{
			get;
			set;
		}
		[DataType(DataType.Password), Display(Name = "Password"), Required, StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		public string Password
		{
			get;
			set;
		}
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match."), DataType(DataType.Password), Display(Name = "Confirm password")]
		public string ConfirmPassword
		{
			get;
			set;
		}
	}
}
