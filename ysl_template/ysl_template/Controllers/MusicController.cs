using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using ysl_template.Models;

namespace ysl_template.Controllers
{
    public class MusicController : Controller
    {
        // GET: Music
        public ActionResult Index()
        {
            var audioAlbumRepository = new AudioAlbumRepository(new yslDataContext());
            var audioAlbumsWithCover = audioAlbumRepository.getAudioAlbumsWithCover(0, 100);
            ViewBag.audios = audioAlbumsWithCover;
            return View();
        }

        public ActionResult Listen(int aid)
        {
            var audioAlbumRepository = new AudioAlbumRepository(new yslDataContext());
            var audioAlbum = audioAlbumRepository.getAudioAlbum(aid);
            //var obj = (
            //            from a in audioAlbum.AudioAlbumItems
            //            where a.Audio.AudioId > 0
            //            select a into o
            //            select new
            //            {
            //                title = o.Audio.Title,
            //                mp3 = "http://guerilladevteam.com"+o.Audio.Location
            //            }).ToList();
            //JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            ViewBag.albumTitle = audioAlbum.Title;
            ViewBag.albumDesc = audioAlbum.Description;
            ViewBag.albumPhoto = audioAlbum.Photo;
            ViewBag.albumId = audioAlbum.AudioAlbumId;
            ViewBag.albumDownloadLink = audioAlbum.DownloadLink;
        //    ViewBag.albumAudio = javaScriptSerializer.Serialize(obj);
            ViewBag.artistName = audioAlbum.Artist.Name;
            return View();
        }
        public ActionResult GetMusic(int aid)
        {
            var audioAlbumRepository = new AudioAlbumRepository(new yslDataContext());
            var audioAlbum = audioAlbumRepository.getAudioAlbum(aid);
            var obj = (
                        from a in audioAlbum.AudioAlbumItems
                        where a.Audio.AudioId > 0
                        select a into o
                        select new
                        {
                            title = o.Audio.Title,
                            mp3 = "http://guerilladevteam.com" + o.Audio.Location,
                            free = "true"
                        }).ToList();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            JsonResult jsonResult = new JsonResult();

            jsonResult.Data = javaScriptSerializer.Serialize(obj);
            return jsonResult;
        }

        public string UpdateArtist(int aId, string Name)
        {
            AudioAlbumRepository audioAlbumRepository = new AudioAlbumRepository(new yslDataContext());
            AudioAlbum audioAlbum = audioAlbumRepository.getAudioAlbum(aId);
            ArtistRepository artistRepository = new ArtistRepository(new yslDataContext());
            Artist artist = artistRepository.getArtist(Name);
            audioAlbum.ArtistId = new int?(artist.ArtistId);
            audioAlbumRepository.updateAudioAlbum(audioAlbum);
            return "true";
        }
        public ActionResult Processupload(string uploads)
        {
            string[] array = uploads.Split(new char[]
			{
				';'
			});
            new AudioRepository(new yslDataContext());
            Request.Cookies.Get("ysl");
            int account = 1;
            string title = DateTime.Now.ToString("MMMM dd, yyyy");
            IAudioAlbumRepository audioAlbumRepository = new AudioAlbumRepository(new yslDataContext());
            new AudioAlbumItemRepository(new yslDataContext());
            AudioAlbum audioAlbum;
            if (audioAlbumRepository.AccountAudioAlbumExists(account, title))
            {
                audioAlbum = audioAlbumRepository.GetAccountAudioAlbumByTitle(account, title);
            }
            else
            {
                AudioAlbum a = new AudioAlbum
                {
                    Created = DateTime.Now,
                    Description = "",
                    ArtistId = new int?(1),
                    Title = DateTime.Now.ToString("MMMM dd, yyyy"),
                    PhotoId = new int?(2),
                    IsSingle = false,
                    IsFeatured = false
                };
                int audioAlbumId = audioAlbumRepository.addAudioAlbum(a);
                audioAlbum = audioAlbumRepository.getAudioAlbum(audioAlbumId);
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
                        Audio audio = new Audio
                        {
                            AccountId = 5,
                            Title = array2[1],
                            Description = "",
                            Location = text
                        };
                        EntitySet<AudioAlbumItem> arg_213_0 = audioAlbum.AudioAlbumItems;
                        AudioAlbumItem audioAlbumItem = new AudioAlbumItem();
                        audioAlbumItem.Audio = audio;
                        audioAlbumItem.Created = DateTime.Now;
                        audioAlbumItem.AudioAlbumId = audioAlbum.AudioAlbumId;
                        audioAlbumItem.Track = Math.Min(audioAlbum.AudioAlbumItems.Count + 1, audioAlbum.AudioAlbumItems.AsEnumerable<AudioAlbumItem>().Max((AudioAlbumItem s) => s.Track) + 1);
                        arg_213_0.Add(audioAlbumItem);
                        audioAlbumRepository.updateAudioAlbum(audioAlbum);
                    }
                    catch
                    {
                    }
                }
            }
            return base.RedirectToAction("ViewAlbum", new RouteValueDictionary(new
            {
                controller = "AudioController",
                action = "ViewAlbum",
                aid = audioAlbum.AudioAlbumId
            }));
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
            new AudioRepository(new yslDataContext());
            Request.Cookies.Get("ysl");
            int value = 1;
            DateTime.Now.ToString("MMMM dd, yyyy");
            IAudioAlbumRepository audioAlbumRepository = new AudioAlbumRepository(new yslDataContext());
            new AudioAlbumItemRepository(new yslDataContext());
            IPhotoRepository photoRepository = new PhotoRepository(new yslDataContext());
            string text = array2[2];
            text = text.Replace("/temp", "");
            string text2 = HostingEnvironment.MapPath(array2[2]);
            string destFileName = text2.Replace("\\temp", "");
            int value2 = 0;
            try
            {
             System.IO.File.Move(text2, destFileName);
                value2 = photoRepository.addPhoto(new Photo
                {
                    AccountId = 5,
                    Location = text,
                    Title = "",
                    Description = ""
                });
            }
            catch
            {
            }
            AudioAlbum audioAlbum = new AudioAlbum
            {
                ArtistId = new int?(value),
                Title = array2[0],
                Description = array2[1],
                PhotoId = new int?(value2),
                IsSingle = false,
                IsFeatured = bool.Parse(array2[3])
            };
            int audioAlbumId = audioAlbumRepository.addAudioAlbum(audioAlbum);
            AudioAlbum audioAlbum2 = audioAlbumRepository.getAudioAlbum(audioAlbumId);
            string[] array3 = array;
            for (int i = 0; i < array3.Length; i++)
            {
                string text3 = array3[i];
                if (!string.IsNullOrWhiteSpace(text3))
                {
                    string[] array4 = text3.Split(new char[]
					{
						','
					});
                    text = array4[0];
                    text = text.Replace("/temp", "");
                    text2 = HostingEnvironment.MapPath(array4[0]);
                    destFileName = text2.Replace("\\temp", "");
                    try
                    {
                   System.IO.File.Move(text2, destFileName);
                        Audio audio = new Audio
                        {
                            AccountId = 5,
                            Title = array4[1],
                            Description = "",
                            Location = text
                        };
                        audioAlbum2.AudioAlbumItems.Add(new AudioAlbumItem
                        {
                            Audio = audio,
                            Track = audioAlbum.AudioAlbumItems.Count + 1,
                            AudioAlbum = audioAlbum
                        });
                    }
                    catch
                    {
                    }
                }
            }
            audioAlbumRepository.updateAudioAlbum(audioAlbum2);
            return base.RedirectToAction("Index");
        }
        public ActionResult Processsingle(string meta, string uploads)
        {
            string[] array = uploads.Split(new char[]
			{
				';'
			});
            string[] array2 = meta.Split(new char[]
			{
				'~'
			});
            new AudioRepository(new yslDataContext());
            base.Request.Cookies.Get("ysl");
            int value = 1;
            DateTime.Now.ToString("MMMM dd, yyyy");
            IAudioAlbumRepository audioAlbumRepository = new AudioAlbumRepository(new yslDataContext());
            new AudioAlbumItemRepository(new yslDataContext());
            IPhotoRepository photoRepository = new PhotoRepository(new yslDataContext());
            string text = array2[2];
            text = text.Replace("/temp", "");
            string text2 = HostingEnvironment.MapPath(array2[2]);
            string destFileName = text2.Replace("\\temp", "");
            int value2 = 0;
            try
            {
             System.IO.File.Move(text2, destFileName);
                value2 = photoRepository.addPhoto(new Photo
                {
                    AccountId = 5,
                    Location = text,
                    Title = "",
                    Description = ""
                });
            }
            catch
            {
            }
            AudioAlbum audioAlbum = new AudioAlbum
            {
                ArtistId = new int?(value),
                Title = array2[0],
                Description = array2[1],
                PhotoId = new int?(value2),
                IsSingle = true,
                IsFeatured = bool.Parse(array2[3])

            };
            int audioAlbumId = audioAlbumRepository.addAudioAlbum(audioAlbum);
            AudioAlbum audioAlbum2 = audioAlbumRepository.getAudioAlbum(audioAlbumId);
            string[] array3 = array;
            for (int i = 0; i < array3.Length; i++)
            {
                string text3 = array3[i];
                if (!string.IsNullOrWhiteSpace(text3))
                {
                    string[] array4 = text3.Split(new char[]
					{
						','
					});
                    text = array4[0];
                    text = text.Replace("/temp", "");
                    text2 = HostingEnvironment.MapPath(array4[0]);
                    destFileName = text2.Replace("\\temp", "");
                    var dLink = text;
                    if (!array2[4].Trim().Equals("autoGen"))
                        dLink = array2[4].Trim();
                    try
                    {
                     System.IO.File.Move(text2, destFileName);
                        Audio audio = new Audio
                        {
                            AccountId = 5,
                            Title = array2[0],
                            Description = array2[1],
                            Location = text
                        };
                        audioAlbum2.AudioAlbumItems.Add(new AudioAlbumItem
                        {
                            Audio = audio,
                            Track = audioAlbum.AudioAlbumItems.Count + 1,
                            AudioAlbum = audioAlbum,
                            DownloadLink = dLink
                        });
                    }
                    catch
                    {
                    }
                }
            }
            audioAlbumRepository.updateAudioAlbum(audioAlbum2);
            return base.RedirectToAction("Index");
        }
        [Authorize(Roles = "Member, Administrator")]
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Get(int id)
        {
            AudioAlbumRepository audioAlbumRepository = new AudioAlbumRepository(new yslDataContext());
            AudioAlbumDataForJSON audioAlbumDataForJSON = audioAlbumRepository.getAudioAlbumDataForJSON(id);
            ActionResult result;
            try
            {
                JsonResult jsonResult = new JsonResult();
                jsonResult.Data =audioAlbumDataForJSON;
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
        [HttpPost]
        public ActionResult UpdateTracks(int aId, string TrackList)
        {
            AudioRepository audioRepository = new AudioRepository(new yslDataContext());
            AudioAlbumItemRepository audioAlbumItemRepository = new AudioAlbumItemRepository(new yslDataContext());
            try
            {
                List<AudioAlbumItem> allAudioAlbumItemsForAlbum = audioAlbumItemRepository.getAllAudioAlbumItemsForAlbum(aId);
                ListOfTracks listOfTracks = JsonConvert.DeserializeObject<ListOfTracks>(TrackList);
                using (List<AudioAlbumItem>.Enumerator enumerator = allAudioAlbumItemsForAlbum.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        AudioAlbumItem item = enumerator.Current;
                        try
                        {
                            Tracks tracks = listOfTracks.tracks.Single((Tracks a) => a.audioAlbumItemId == item.AudioAlbumItemId);
                            item.Track = tracks.Number;
                            Audio audio = item.Audio;
                            audio.Title = tracks.Title;
                            audioAlbumItemRepository.updateAudioAlbumItem(item);
                            audioRepository.updateAudio(audio);
                        }
                        catch (Exception)
                        {
                            audioAlbumItemRepository.deleteAudioAlbumItem(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            JsonResult jsonResult = new JsonResult();
            jsonResult.Data ="hereeee";
            return jsonResult;
        }
        public ActionResult UpdateAlbumTitle(int aId, string Text)
        {
            AudioAlbumRepository audioAlbumRepository = new AudioAlbumRepository(new yslDataContext());
            AudioAlbum audioAlbum = audioAlbumRepository.getAudioAlbum(aId);
            audioAlbum.Title = Text;
            audioAlbumRepository.updateAudioAlbum(audioAlbum);
            JsonResult jsonResult = new JsonResult();
            jsonResult.Data ="success";
            return jsonResult;
        }
        public ActionResult HandleTrackUpload(int aId)
        {
            HttpPostedFileBase httpPostedFileBase = base.Request.Files["Filedata"];
            JsonResult jsonResult = new JsonResult();
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(httpPostedFileBase.FileName);
            string path = MediaNameGen.GetRandomMediaName() + Path.GetExtension(httpPostedFileBase.FileName);
            if (Path.GetExtension(httpPostedFileBase.FileName).ToLower().Equals(".mp3"))
            {
                string text = Path.Combine(HostingEnvironment.MapPath("~/wMedia/Audio/Uploads"), path);
                httpPostedFileBase.SaveAs(text);
                AudioRepository audioRepository = new AudioRepository(new yslDataContext());
                AudioAlbumItemRepository audioAlbumItemRepository = new AudioAlbumItemRepository(new yslDataContext());
                AudioAlbumRepository audioAlbumRepository = new AudioAlbumRepository(new yslDataContext());
                int audioId = audioRepository.addAudio(new Audio
                {
                    AccountId = 1,
                    Description = "",
                    Title = fileNameWithoutExtension,
                    Location = text
                });
                AudioAlbum audioAlbum = audioAlbumRepository.getAudioAlbum(aId);
                int num = audioAlbumItemRepository.addAudioAlbumItem(new AudioAlbumItem
                {
                    AudioId = audioId,
                    AudioAlbumId = aId,
                    Track = audioAlbum.AudioAlbumItems.Count<AudioAlbumItem>() + 1
                });
                string[,] array = new string[1, 3];
                int num2 = 0;
                array[num2, 0] = Url.Content(text);
                array[num2, 1] = fileNameWithoutExtension;
                array[num2, 2] = num.ToString();
                jsonResult.Data =array;
            }
            return jsonResult;
        }
        public ActionResult HandleUpload()
        {
            HttpPostedFileBase httpPostedFileBase = base.Request.Files["Filedata"];
            JsonResult jsonResult = new JsonResult();
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(httpPostedFileBase.FileName);
            string path = MediaNameGen.GetRandomMediaName() + Path.GetExtension(httpPostedFileBase.FileName);
            if (Path.GetExtension(httpPostedFileBase.FileName).ToLower().Equals(".mp3"))
            {
                string text = Path.Combine(HostingEnvironment.MapPath("~/wMedia/Audio/Uploads/temp"), path);
                httpPostedFileBase.SaveAs(text);
                string[,] array = new string[1, 2];
                int num = 0;
                array[num, 0] = Url.Content(text);
                array[num, 1] = fileNameWithoutExtension;
                jsonResult.Data=array;
            }
            else
            {
                if (Path.GetExtension(httpPostedFileBase.FileName).ToLower().Equals(".png") || Path.GetExtension(httpPostedFileBase.FileName).ToLower().Equals(".jpg") || Path.GetExtension(httpPostedFileBase.FileName).ToLower().Equals(".jpeg"))
                {
                    string text2 = Path.Combine(HostingEnvironment.MapPath("~/wMedia/Photo/Uploads/temp"), path);
                    httpPostedFileBase.SaveAs(text2);
                    string[,] array2 = new string[1, 2];
                    int num2 = 0;
                    array2[num2, 0] = Url.Content(text2);
                    array2[num2, 1] = fileNameWithoutExtension;
                    jsonResult.Data=array2;
                }
            }
            return jsonResult;
        }
    }
}