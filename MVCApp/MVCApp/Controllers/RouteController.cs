using Contracts.Services;
using Entities.Models.DTOs;
using Entities.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVCApp.Controllers
{
    [Route("route")]
    [ApiController]
    public class RouteController : Controller
    {
        private readonly IRouteService _routeService;
        private readonly ISettlementService _settlementService;

        public RouteController(IRouteService routeService, ISettlementService settlementService)
        {
            _routeService = routeService;
            _settlementService = settlementService;
        }

        [HttpGet("", Name = "routes")]
        [ResponseCache(CacheProfileName = "EntityCache")]
        public IActionResult Index([FromQuery] PaginationQueryParameters parameters)
        {
            var routes = _routeService.GetByPage<RouteDto>(parameters);

            if (routes == null || !routes.Any())
                return NoContent();

            ViewBag.CurrentPage = routes.MetaData.CurrentPage;
            ViewBag.PageSize = routes.MetaData.PageSize;
            ViewBag.TotalSize = routes.MetaData.TotalSize;

            ViewBag.HaveNext = routes.MetaData.HaveNext;
            ViewBag.HavePrev = routes.MetaData.HavePrev;

            ViewBag.ControllerName = "Route";
            ViewBag.ViewActionName = "routes";
            ViewBag.CreateActionName = "create-route-view";
            ViewBag.DeleteActionName = "delete-route";
            ViewBag.UpdateActionName = "UpdateView";

            return View(routes);
        }
        [HttpGet("create", Name = "create-route-view")]
        public IActionResult CreateView()
        {
            var settlements = _settlementService.GetAll<SettlementDto>();
            Console.WriteLine(settlements);
            ViewBag.Settlements = new SelectList(settlements, "Id", "Title");
            return View();
        }
        [HttpPost("", Name = "create-route")]
        public async Task<IActionResult> Create([FromForm] RouteCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                var settlements = _settlementService.GetAll<SettlementDto>();
                ViewBag.Settlements = new SelectList(settlements, "Id", "Title");
                return View(dto);
            }

            var validationResult = dto.ValidateSettlements();
            if (validationResult != ValidationResult.Success)
            {
                ModelState.AddModelError("", validationResult.ErrorMessage!);
                var settlements = _settlementService.GetAll<SettlementDto>();
                ViewBag.Settlements = new SelectList(settlements, "Id", "Title");
                return View("CreateView", dto);
            }


            await _routeService.CreateAsync<RouteCreateDto, RouteDto>(dto);
            return RedirectToAction("Index", new { page = 1, pageSize = 10 });
        }
        [HttpPost("delete-route", Name = "delete-route")]
        public async Task<IActionResult> Delete([FromForm] RouteDeleteDto dto)
        {
            await _routeService.DeleteByIdAsync(dto.Id);
            return RedirectToAction("Index", new { page = 1, pageSize = 10 });
        }
    }
}
