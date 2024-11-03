using Contracts.Services;
using Entities.Models.DTOs;
using Entities.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace MVCApp.Controllers
{
    [Route("settlements")]
    [ApiController]
    public class SettlementController : Controller
    {
        private readonly ISettlementService _settlementService;

        public SettlementController(ISettlementService settlementService)
        {
            _settlementService = settlementService;
        }

        [HttpGet("", Name = "settlements")]
        [ResponseCache(CacheProfileName = "EntityCache")]
        public IActionResult Index([FromQuery] PaginationQueryParameters parameters)
        {
            var settlements = _settlementService.GetByPage<SettlementDto>(parameters);

            if (settlements == null || !settlements.Any())
                return NoContent();

            ViewBag.CurrentPage = settlements.MetaData.CurrentPage;
            ViewBag.PageSize = settlements.MetaData.PageSize;
            ViewBag.TotalSize = settlements.MetaData.TotalSize;

            ViewBag.HaveNext = settlements.MetaData.HaveNext;
            ViewBag.HavePrev = settlements.MetaData.HavePrev;

            ViewBag.ControllerName = "Settlement";
            ViewBag.ViewActionName = "settlements";
            ViewBag.CreateActionName = "create-settlement-view";
            ViewBag.DeleteActionName = "delete-settlement";
            ViewBag.UpdateActionName = "UpdateView";

            return View(settlements);
        }
        [HttpGet("create", Name = "create-settlement-view")]
        public IActionResult CreateView() => View();
        [HttpPost("", Name = "create-settlement")]
        public async Task<IActionResult> Create([FromForm] SettlementCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View("CreateView", dto);

            await _settlementService.CreateAsync<SettlementCreateDto, SettlementDto>(dto);
            return RedirectToAction("Index", new { page = 1, pageSize = 10 });
        }
        [HttpPost("delete-settlement", Name = "delete-settlement")]
        public async Task<IActionResult> Delete([FromForm] SettlementDeleteDto dto)
        {
            await _settlementService.DeleteByIdAsync(dto.Id);
            return RedirectToAction("Index", new { page = 1, pageSize = 10 });
        }
    }
}
