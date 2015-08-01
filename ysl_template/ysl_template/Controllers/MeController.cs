using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ysl_template.Models;

namespace ysl_template.Controllers
{
    public class MeController : Controller
    {
        // GET: Me
        public ActionResult Index()
        {
            EventRepository eventRepository = new EventRepository(new yslDataContext());
            var events = eventRepository.getArtistEvents(1);
            ViewBag.events = events;
            return View();
        }
    }
}