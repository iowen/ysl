using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ysl_template.Models;

namespace ysl_template.Controllers
{
    public class EventController : Controller
    {
        //// GET: Event
        //public ActionResult Index()
        //{
        //    EventRepository eventRepository = new EventRepository(new yslDataContext());
        //    var events = eventRepository.getArtistEvents(1);
        //    ViewBag.events = events;
        //    return View();
        //}

        //// GET: Event
        //public ActionResult View(int id)
        //{
        //    var eRepo = new EventRepository(new yslDataContext());
        //    var _event = eRepo.getEvent(id);
        //    var events = eRepo.getArtistEvents(1);
        //    events.Remove(_event);
        //    ViewBag._event = _event;
        //    ViewBag.events = events;
        //    return View();
        //}

        public ActionResult Processupload( string meta, string uploads, string people)
        {
            string[] source = uploads.Split(new char[]
			{
				'~'
			});
            PhotoRepository photoRepository = new PhotoRepository(new yslDataContext());
            EventArtistRepository eventArtistRepository = new EventArtistRepository(new yslDataContext());
            EventRepository eventRepository = new EventRepository(new yslDataContext());
            ArtistRepository artistRepository = new ArtistRepository(new yslDataContext());
            Request.Cookies.Get("ysl");
            var text = source.Last<string>().Split(',').ToList();
            string text2 = text.First();
            text2 = text2.Replace("/temp", "");
            var useDefaultImage = text2.Trim().ToLower().Equals("none");
            string text3 = "";
            string destFileName =  "";
            string photoName = "";
            if (!useDefaultImage)
            {
                text3 = Server.MapPath(text.First());
                destFileName = text3.Replace("\\temp", "");
                photoName = text.Last();
            }
            int accountId = int.Parse(System.Web.HttpContext.Current.User.Identity.GetUserId()) ;

            var metaFields = meta.Split('~').ToList();
            try
            {
                if (!useDefaultImage)
                {
                    System.IO.File.Move(text3, destFileName);
                }
                Photo photo = new Photo
                {
                    AccountId = accountId,
                    Title = photoName,
                    Description = "",
                    Location = text2,
                    Uploaded = DateTime.Now,
                    Updated = DateTime.Now
                };
                int value = (useDefaultImage) ? GlobalVariables.DefaultEventImageId : photoRepository.addPhoto(photo);
                var ys = artistRepository.getArtist(1);
                Event ev = new Event();
                ev.AccountId = accountId;
                ev.Title = metaFields[0];
                ev.Start = DateTime.Parse(metaFields[1] + " " + metaFields[2]);
                ev.Ending = DateTime.Parse(metaFields[3] + " " + metaFields[4]);
                
                ev.Location = metaFields[5];
                ev.Description = metaFields[6];
                ev.Venue = metaFields[7];

                ev.PhotoId = value;
                ev.Created = DateTime.Now;
                var evId = eventRepository.addEvent(ev);
                EventArtist eva = new EventArtist() { ArtistId = ys.ArtistId, EventId = evId };
                eventArtistRepository.addEventArtist(eva);

              
            }
            catch
            {
            }
            return View();
        }
    }
}