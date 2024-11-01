using Contracts.Services;
using Entities;
using Entities.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace MVCApp.Controllers
{
    [Route("cargo")]
    [ApiController]
    public class CargoController : Controller
    {
        private readonly ICargoService _cargoService;

        public CargoController(ICargoService cargoService)
        {
            _cargoService = cargoService;
        }

        [HttpGet("", Name = "cargos")]
        [ResponseCache(CacheProfileName = "EntityCache")]
        public IActionResult Index([FromQuery] PaginationQueryParameters parameters)
        {
            var cargos = _cargoService.GetByPage(parameters);

            if (cargos == null || !cargos.Any())
                return NoContent();

            ViewBag.CurrentPage = cargos.MetaData.CurrentPage;
            ViewBag.PageSize = cargos.MetaData.PageSize;
            ViewBag.TotalSize = cargos.MetaData.TotalSize;

            ViewBag.HaveNext = cargos.MetaData.HaveNext;
            ViewBag.HavePrev = cargos.MetaData.HavePrev;

            return View(cargos);
        }
    }
}
