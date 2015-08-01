using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ysl_template.Models;

namespace ysl_template.Controllers
{
    public class MemberController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ProcessAddition(string meta)
        {
            string[] source = meta.Split(new char[]
			{
				'~'
			});
            PhotoRepository photoRepository = new PhotoRepository(new yslDataContext());
            AccountRepository accountRepository = new AccountRepository(new yslDataContext());
            MemberRepository memberRepository = new MemberRepository(new yslDataContext());
            Request.Cookies.Get("ysl");
            string text = source.Last<string>();
            string text2 = text;
            text2 = text2.Replace("/temp", "");
            string text3 = Server.MapPath(text);
            string destFileName = text3.Replace("\\temp", "");
            try
            {
                System.IO.File.Move(text3, destFileName);
                Account account = new Account
                {
                    FirstName = source.First<string>(),
                    LastName = source.ElementAt(1),
                    Email = source.ElementAt(3),
                    Password = "password"
                };
                int accountId = accountRepository.addAccount(account);
                Photo photo = new Photo
                {
                    AccountId = accountId,
                    Title = "",
                    Description = "",
                    Location = text2
                };
                int value = photoRepository.addPhoto(photo);
                memberRepository.addMember(new Member
                {
                    AccountId = accountId,
                    Name = source.ElementAt(2),
                    PhotoId = new int?(value)
                });
            }
            catch
            {
            }
            return View();
        }
        [HttpPost]
        public ActionResult Get(int id)
        {
            MemberRepository memberRepository = new MemberRepository(new yslDataContext());
            var member = memberRepository.ConvertToModel(memberRepository.getMember(id));
            JsonResult jsonResult = new JsonResult();
            jsonResult.Data = member;
            var result = jsonResult;
            return result;
        }
    }
}