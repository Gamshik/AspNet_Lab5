using Contracts.Services;
using Entities.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace MVCApp.Controllers
{
    [Route("route")]
    [ApiController]
    public class RouteController : Controller
    {
        private readonly IRouteService _routeService;

        public RouteController(IRouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet("", Name = "routes")]
        [ResponseCache(CacheProfileName = "EntityCache")]
        public IActionResult Index([FromQuery] PaginationQueryParameters parameters)
        {
            var routes = _routeService.GetByPage(parameters);

            if (routes == null || !routes.Any())
                return NoContent();

            ViewBag.CurrentPage = routes.MetaData.CurrentPage;
            ViewBag.PageSize = routes.MetaData.PageSize;
            ViewBag.TotalSize = routes.MetaData.TotalSize;

            ViewBag.HaveNext = routes.MetaData.HaveNext;
            ViewBag.HavePrev = routes.MetaData.HavePrev;

            return View(routes);
        }
    }
}
