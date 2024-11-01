using Contracts.Services;
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
            var settlements = _settlementService.GetByPage(parameters);

            if (settlements == null || !settlements.Any())
                return NoContent();

            ViewBag.CurrentPage = settlements.MetaData.CurrentPage;
            ViewBag.PageSize = settlements.MetaData.PageSize;
            ViewBag.TotalSize = settlements.MetaData.TotalSize;

            ViewBag.HaveNext = settlements.MetaData.HaveNext;
            ViewBag.HavePrev = settlements.MetaData.HavePrev;

            return View(settlements);
        }
    }
}
