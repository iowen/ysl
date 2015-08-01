using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ysl_template.Models;

namespace ysl_template.Controllers
{
    [Authorize]
    public class AdministratorController : Controller
    {
                private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdministratorController()
        {
        }

        public AdministratorController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Administrator
        public ActionResult Index()
        {
            return View("Index", "~/Views/Shared/_LayoutAdmin.cshtml");
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
                //
                // GET: /Account/Login
                [AllowAnonymous]
                public ActionResult Login(string returnUrl)
                {
                    ViewBag.ReturnUrl = returnUrl;
                    return View("Login", "~/Views/Shared/_LayoutBlank.cshtml");
                }
                public ActionResult LogOff()
                {
                    SignInManager.AuthenticationManager.SignOut();
                    return RedirectToAction("Index", "Home");
                }
                //
                // POST: /Account/Login
                [HttpPost]
                [AllowAnonymous]
                [ValidateAntiForgeryToken]
                public async Task<ActionResult> Login(LoginModel model, string returnUrl)
                {
                    if (!ModelState.IsValid)
                    {
                        return View(model);
                    }

                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, change to shouldLockout: true
                    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, false, shouldLockout: false);
                    switch (result)
                    {
                        case SignInStatus.Success:
                            return RedirectToLocal(returnUrl);
                        case SignInStatus.LockedOut:
                            return View("Lockout");
                        case SignInStatus.RequiresVerification:
                            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false});
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("", "Invalid login attempt.");
                            return View("Login", "~/Views/Shared/_LayoutBlank.cshtml", model);
                    }
                }

                public ActionResult Music()
		{
			IAudioAlbumRepository audioAlbumRepository = new AudioAlbumRepository(new yslDataContext());
			ArtistRepository artistRepository = new ArtistRepository(new yslDataContext());
			List<Artist> allArtist = artistRepository.getAllArtist();
			List<AudioAlbumData> audioAlbumsWithCover = audioAlbumRepository.getAudioAlbumsWithCover(0, 100);
			ViewBag.audios = audioAlbumsWithCover;
			ViewBag.artist = allArtist;
			return View("Music", "~/Views/Shared/_LayoutAdmin.cshtml");
		}

                public ActionResult Artist()
		{
			ArtistRepository artistRepository = new ArtistRepository(new yslDataContext());
			MemberRepository memberRepository = new MemberRepository(new yslDataContext());
			List<Artist> allArtist = artistRepository.getAllArtist();
			List<Member> allMember = memberRepository.getAllMember();
			ViewBag.artist = allArtist;
			ViewBag.members =  allMember;
			return View("Artist", "~/Views/Shared/_LayoutAdmin.cshtml");
		}
                public ActionResult Member()
		{
			MemberRepository memberRepository = new MemberRepository(new yslDataContext());
			List<Member> allMember = memberRepository.getAllMember();
			ViewBag.members = allMember;
			return View("Member", "~/Views/Shared/_LayoutAdmin.cshtml");
		}
                public ActionResult Photos()
		{
			IPhotoAlbumRepository photoAlbumRepository = new PhotoAlbumRepository(new yslDataContext());
			List<PhotoAlbumModel> photoAlbumsWithPhotos = photoAlbumRepository.getPhotoAlbumsWithPhotos();
			ViewBag.photos = photoAlbumsWithPhotos;
			return View("Photos", "~/Views/Shared/_LayoutAdmin.cshtml");
		}
                public ActionResult Events()
		{
			EventRepository eventRepository = new EventRepository(new yslDataContext());
			List<EventData> recentEventsWithData = eventRepository.getRecentEventsWithData();
			ArtistRepository artistRepository = new ArtistRepository(new yslDataContext());
			List<Artist> allArtist = artistRepository.getAllArtist();
			ViewBag.artists =  allArtist.ToList<Artist>();
			ViewBag.events = recentEventsWithData;
			return View("Events", "~/Views/Shared/_LayoutAdmin.cshtml");
		}

    }
}