using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ysl_template.Models;

namespace ysl_template.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            EventRepository eventRepository = new EventRepository(new yslDataContext());
           // var events = eventRepository.getArtistEvents(1);
          //  ViewBag.events = events;
            return View();
        }

        [HttpPost]
        public ActionResult Send(string name, string email, string tel, string message)
        {
            JsonResult jsonResult = new JsonResult();
            jsonResult.Data = "success";
            var result = jsonResult;
            return result;
        }
    }
}