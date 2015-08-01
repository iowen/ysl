using System;
using System.Collections.Generic;
namespace ysl_template.Models
{
	public interface IAccountRepository
	{
		List<Account> getAllAccounts();
		Account getAccount(int id);
		Account getAccount(string email);
		Account getAccount(string email, string pwd);
		int addAccount(Account account);
		bool updateAccount(int id, string email, string pwd);
		AccountInfoModel login(LoginModel model);
		bool emailAvailable(string email);
	}
}
