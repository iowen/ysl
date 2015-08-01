using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ysl_template.Models;
using System.Web.Hosting;

namespace ysl_template.Controllers
{
    public class PhotoController : Controller
    {
        // GET: Photo
        public ActionResult Index()
        {
            PhotoAlbumRepository photoAlbumRepository = new PhotoAlbumRepository(new yslDataContext());
           var a  = photoAlbumRepository.getPhotoAlbumsWithPhotos().Take(10);
           ViewBag.photos = a;
            return View();
        }

        public ActionResult ViewAlbum(int aid)
        {
            var photoAlbumRepository = new PhotoAlbumRepository(new yslDataContext());
            var photoAlbum = photoAlbumRepository.getPhotoAlbum(aid);
            var photosForAlbum = photoAlbumRepository.GetPhotosForAlbum(aid);
            ViewBag.photos = photosForAlbum;
            ViewBag.album = photoAlbum;
             return View();
        }
        public ActionResult Processupload(string uploads)
        {
            string[] array = uploads.Split(new char[]
			{
				';'
			});
            new PhotoRepository(new yslDataContext());
            Request.Cookies.Get("ysl");
            int num = int.Parse(System.Web.HttpContext.Current.User.Identity.GetUserId());
            string title = DateTime.Now.ToString("MMMM dd, yyyy");
            IPhotoAlbumRepository photoAlbumRepository = new PhotoAlbumRepository(new yslDataContext());
            PhotoAlbum photoAlbum;
            if (photoAlbumRepository.AccountPhotoAlbumExists(num, title))
            {
                photoAlbum = photoAlbumRepository.GetAccountPhotoAlbumByTitle(num, title);
            }
            else
            {
                int photoAlbumId = photoAlbumRepository.addPhotoAlbum(new PhotoAlbum
                {
                    AccountId = num,
                    Title = title,
                    Description = ""
                });
                photoAlbum = photoAlbumRepository.getPhotoAlbum(photoAlbumId);
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(array[i]))
                {
                    string[] array2 = array[i].Split(new char[]
					{
						','
					});
                    string text = array2[0];
                    text = text.Replace("/temp", "");
                    string text2 = HostingEnvironment.MapPath(array2[0]);
                    string destFileName = text2.Replace("\\temp", "");
                    try
                    {
                        System.IO.File.Move(text2, destFileName);
                        Photo photo = new Photo
                        {
                            AccountId = num,
                            Title = array2[1],
                            Description = "",
                            Location = text
                        };
                        photoAlbum.PhotoAlbumItems.Add(new PhotoAlbumItem
                        {
                            Photo = photo,
                            Created = DateTime.Now,
                            PhotoAlbumId = photoAlbum.PhotoAlbumId
                        });
                        photoAlbumRepository.updatePhotoAlbum(photoAlbum);
                    }
                    catch
                    {
                    }
                }
            }
            return View();
        }
        public ActionResult Processalbum(string meta, string uploads)
        {
            string[] array = uploads.Split(new char[]
			{
				';'
			});
            string[] array2 = meta.Split(new char[]
			{
				'~'
			});
            IPhotoAlbumRepository photoAlbumRepository = new PhotoAlbumRepository(new yslDataContext());
            Request.Cookies.Get("ysl");
            int num = 5;
            PhotoAlbum photoAlbum;
            if (photoAlbumRepository.AccountPhotoAlbumExists(num, array2[0]))
            {
                photoAlbum = photoAlbumRepository.GetAccountPhotoAlbumByTitle(num, array2[0]);
            }
            else
            {
                PhotoAlbum album = new PhotoAlbum
                {
                    AccountId = num,
                    Title = array2[0],
                    Description = array2[1]
                };
                int photoAlbumId = photoAlbumRepository.addPhotoAlbum(album);
                photoAlbum = photoAlbumRepository.getPhotoAlbum(photoAlbumId);
            }
            for (int i = 0; i < array.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(array[i]))
                {
                    string[] array3 = array[i].Split(new char[]
					{
						','
					});
                    string text = array3[0];
                    text = text.Replace("/temp", "");
                    string text2 = HostingEnvironment.MapPath(array3[0]);
                    string destFileName = text2.Replace("\\temp", "");
                    try
                    {
                        System.IO.File.Move(text2, destFileName);
                        Photo photo = new Photo
                        {
                            AccountId = num,
                            Title = array3[1],
                            Description = "",
                            Location = text
                        };
                        photoAlbum.PhotoAlbumItems.Add(new PhotoAlbumItem
                        {
                            Photo = photo,
                            Created = DateTime.Now,
                            PhotoAlbumId = photoAlbum.PhotoAlbumId
                        });
                        photoAlbumRepository.updatePhotoAlbum(photoAlbum);
                    }
                    catch
                    {
                    }
                }
            }
            return View();
        }
        [Authorize(Roles = "Member, Administrator")]
        public ActionResult Upload()
        {
            return base.View();
        }
        public ActionResult HandleUpload(HttpPostedFileBase FileData)
        {
            JsonResult jsonResult = new JsonResult();
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileData.FileName);
            string path = MediaNameGen.GetRandomMediaName() + Path.GetExtension(FileData.FileName);
            string text = Path.Combine(HostingEnvironment.MapPath("~/wMedia/Photo/Uploads/temp"), path);
            FileData.SaveAs(text);
            string[,] array = new string[1, 2];
            int num = 0;
            array[num, 0] = Url.Content(text);
            array[num, 1] = fileNameWithoutExtension;
            jsonResult.Data = array;
            return jsonResult;
        }
        [HttpPost]
        public ActionResult Get(int id)
        {
            PhotoAlbumRepository photoAlbumRepository = new PhotoAlbumRepository(new yslDataContext());
            PhotoAlbumData photoAlbumDataForJSON = photoAlbumRepository.getPhotoAlbumDataForJSON(id);
            ActionResult result;
            try
            {
                JsonResult jsonResult = new JsonResult();
                jsonResult.Data=photoAlbumDataForJSON;
                result = jsonResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                JsonResult jsonResult2 = new JsonResult();
                jsonResult2.Data ="";
                result = jsonResult2;
            }
            return result;
        }
    }
}