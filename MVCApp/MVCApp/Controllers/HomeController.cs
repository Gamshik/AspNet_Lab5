using Contracts.Repositories;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Models;
using System.Diagnostics;

namespace MVCApp.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICargoRepository _cargoRepository;

        public HomeController(ILogger<HomeController> logger, ICargoRepository cargoRepository)
        {
            _logger = logger;
            _cargoRepository = cargoRepository;
        }

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
