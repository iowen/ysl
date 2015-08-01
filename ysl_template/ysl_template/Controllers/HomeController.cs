using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ysl_template.Models;

namespace ysl_template.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AudioAlbumRepository audioAlbumRepository = new AudioAlbumRepository(new yslDataContext());
            MediaRepository mediaRepository = new MediaRepository(new yslDataContext());
            EventRepository eventRepository = new EventRepository(new yslDataContext());
            var events = eventRepository.getArtistEvents(1);
            AudioAlbumDataForJSON latestSingle = audioAlbumRepository.getLatestSingle(1);
            List<AudioAlbumData> audioAlbumsWithCover = audioAlbumRepository.getAudioAlbumsWithCover(0, 10);
            ViewBag.uploads = mediaRepository.GetRecentMedia();
            ViewBag.latestSingle = latestSingle;
            ViewBag.recent = audioAlbumsWithCover;
            ViewBag.events = events;
            return View("Index", "~/Views/Shared/_LayoutHome.cshtml");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}