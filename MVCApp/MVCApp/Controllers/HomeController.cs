using Entities;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Controllers.Attributes;
using MVCApp.Controllers.Base;
using MVCApp.Controllers.Helpers;
using MVCApp.Models;
using System.Diagnostics;

namespace MVCApp.Controllers
{
    [AuthorizeByRoles("Admin", "User")]
    [Route("")]
    [ApiController]
    public class HomeController : BaseController
    {

        public HomeController() { }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
