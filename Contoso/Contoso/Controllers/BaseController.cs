using ContosoDomain.Models;
using ContosoService.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contoso.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserManager<ApplicationUser> _userManager;

        public BaseController(IService _service)
        {
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ContosoDAL.ContosoDBContext.Create()));
        }

        protected void getUserIdentity(string userId)
        {
            if (userId != null)
            {
                if (_userManager.IsInRole(User.Identity.GetUserId(), "Admin")) ViewBag.IdentityRole = "Admin";
                else ViewBag.IdentityRole = "Member";
            }
            else
            {
                ViewBag.IdentityRole = "Guest";
            }
        }


    }
}