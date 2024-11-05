using Contracts.Services;
using Entities;
using Entities.Models.DTOs;
using Entities.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
            var cargos = _cargoService.GetByPage<CargoDto>(parameters);

            if (cargos == null || !cargos.Any())
                return NoContent();

            ViewBag.CurrentPage = cargos.MetaData.CurrentPage;
            ViewBag.PageSize = cargos.MetaData.PageSize;
            ViewBag.TotalSize = cargos.MetaData.TotalSize;

            ViewBag.HaveNext = cargos.MetaData.HaveNext;
            ViewBag.HavePrev = cargos.MetaData.HavePrev;

            ViewBag.ControllerName = "Cargo";
            ViewBag.ViewActionName = "cargos";
            ViewBag.CreateActionName = "create-cargo-view";
            ViewBag.DeleteActionName = "delete-cargo";
            ViewBag.UpdateActionName = "update-cargo-view";


            return View(cargos);
        }
        [HttpGet("create", Name = "create-cargo-view")]
        public IActionResult CreateView() => View();
        [HttpPost("", Name = "create-cargo")]
        public async Task<IActionResult> Create([FromForm] CargoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View("CreateView", dto);

            await _cargoService.CreateAsync<CargoCreateDto, CargoDto>(dto);
            return RedirectToAction("Index", new { page = 1, pageSize = 10 });
        }
        [HttpPost("delete-cargo", Name = "delete-cargo")]
        public async Task<IActionResult> Delete([FromForm] CargoDeleteDto dto)
        {
            await _cargoService.DeleteByIdAsync(dto.Id);
            return RedirectToAction("Index", new { page = 1, pageSize = 10 });
        }
        [HttpGet("update", Name = "update-cargo-view")]
        public async Task<IActionResult> UpdateView([FromQuery] Guid id)
        {
            var cargo = await _cargoService.GetByIdAsync<CargoUpdateDto>(id);
            return View(cargo);
        }
        [HttpPost("update", Name = "update-cargo")]
        public async Task<IActionResult> Update([FromForm] CargoUpdateDto dto)
        {
            if (!ModelState.IsValid || dto.Id.ToString().IsNullOrEmpty())
                return View("UpdateView", dto);

            await _cargoService.UpdateAsync<CargoUpdateDto, CargoDto>(dto);
            return RedirectToAction("Index", new { page = 1, pageSize = 10 });
        }
    }
}
