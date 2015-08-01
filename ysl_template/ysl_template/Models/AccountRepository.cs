using System;
using System.Collections.Generic;
using System.Linq;
namespace ysl_template.Models
{
	public class AccountRepository : IAccountRepository
	{
		private yslDataContext db;
		public AccountRepository(yslDataContext context)
		{
			this.db = context;
		}
		public List<Account> getAllAccounts()
		{
			return this.db.Accounts.ToList<Account>();
		}
		public Account getAccount(int id)
		{
			Account result;
			try
			{
				Account account = this.db.Accounts.Single((Account a) => a.AccountId == id);
				result = account;
			}
			catch
			{
				result = new Account
				{
					AccountId = -1
				};
			}
			return result;
		}
		public Account getAccount(string email)
		{
			Account result;
			try
			{
				Account account = this.db.Accounts.Single((Account a) => a.Email == email);
				result = account;
			}
			catch
			{
				result = new Account
				{
					AccountId = -1
				};
			}
			return result;
		}
		public Account getAccount(string email, string pwd)
		{
			Account result;
			try
			{
				Account account = this.db.Accounts.Single((Account a) => a.Email == email && a.Password == pwd);
				result = account;
			}
			catch
			{
				result = new Account
				{
					AccountId = -1
				};
			}
			return result;
		}
		public int addAccount(Account account)
		{
			this.db.Accounts.InsertOnSubmit(account);
			this.db.SubmitChanges();
			return account.AccountId;
		}
		public bool updateAccount(int id, string email, string pwd)
		{
			Account account = this.getAccount(id);
			if (account.AccountId > 0)
			{
				account.Email = email;
				account.Password = pwd;
				this.db.SubmitChanges();
				return true;
			}
			return false;
		}
		public AccountInfoModel login(LoginModel model)
		{
			AccountInfoModel accountInfoModel = new AccountInfoModel();
			AccountInfoModel result;
			try
			{
				Account account = this.db.Accounts.Single((Account a) => a.Email == model.Email && a.Password == model.Password);
				if (account.AccountId > 0)
				{
					accountInfoModel.account = account;
				}
				else
				{
					accountInfoModel.account = new Account
					{
						AccountId = -1
					};
				}
				result = accountInfoModel;
			}
			catch (Exception ex)
			{
				Console.Write(ex.Message);
				accountInfoModel.account = new Account
				{
					AccountId = -1
				};
				result = accountInfoModel;
			}
			return result;
		}
		public bool emailAvailable(string email)
		{
			return (
				from a in this.db.Accounts
				where a.Email == email
				select a).Any<Account>();
		}
	}
}
