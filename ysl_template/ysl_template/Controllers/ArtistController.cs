using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ysl_template.Models;

namespace ysl_template.Controllers
{
    public class ArtistController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Processupload(string meta, string uploads)
        {
            string[] array = uploads.Split(new char[]
			{
				';'
			});
            string[] array2 = meta.Split(new char[]
			{
				'~'
			});
            ArtistRepository artistRepository = new ArtistRepository(new yslDataContext());
            Request.Cookies.Get("ysl");
            int accountId = 2;
            DateTime.Now.ToString("MMMM dd, yyyy");
            MemberRepository memberRepository = new MemberRepository(new yslDataContext());
            ArtistMemberRepository artistMemberRepository = new ArtistMemberRepository(new yslDataContext());
            IPhotoRepository photoRepository = new PhotoRepository(new yslDataContext());
            string text = array2[1];
            text = text.Replace("/temp", "");
            string text2 = Server.MapPath(array2[1]);
            string destFileName = text2.Replace("\\temp", "");
            int value = 0;
            try
            {
                System.IO.File.Move(text2, destFileName);
                value = photoRepository.addPhoto(new Photo
                {
                    AccountId = accountId,
                    Location = text,
                    Title = "",
                    Description = ""
                });
            }
            catch
            {
            }
            Artist artist = new Artist
            {
                Name = array2[0],
                PhotoId = new int?(value),
                Bio = array2[2]
            };
            int artistId = artistRepository.addArtist(artist);
            string[] array3 = array;
            for (int i = 0; i < array3.Length; i++)
            {
                string text3 = array3[i];
                if (!string.IsNullOrWhiteSpace(text3))
                {
                    try
                    {
                        Member member = memberRepository.getMember(text3.Trim());
                        ArtistMember member2 = new ArtistMember
                        {
                            ArtistId = artistId,
                            MemberId = member.MemberId
                        };
                        artistMemberRepository.addArtistMember(member2);
                    }
                    catch
                    {
                    }
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Get(int id)
        {
            ArtistRepository artistRepository = new ArtistRepository(new yslDataContext());
            var artist = artistRepository.ConvertToModel(artistRepository.getArtist(id));
            JsonResult jsonResult = new JsonResult();
            jsonResult.Data = artist;
            var result = jsonResult;
            return result;
        }
    }
}