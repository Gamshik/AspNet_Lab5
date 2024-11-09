﻿using Contracts.Services;
using Entities.Models.DTOs;
using Entities.Models.DTOs.User;
using Entities.Pagination;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Controllers.Attributes;
using MVCApp.Controllers.Base;

namespace MVCApp.Controllers
{
    [AuthorizeByRoles("Admin")]
    [Route("users")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("", Name = "users")]
        [ResponseCache(CacheProfileName = "EntityCache")]
        public IActionResult Index([FromQuery] PaginationQueryParameters parameters)
        {
            var users = _userService.GetByPage<UserDto>(parameters);

            if (users == null || !users.Any())
                return NoContent();

            ViewBag.CurrentPage = users.MetaData.CurrentPage;
            ViewBag.PageSize = users.MetaData.PageSize;
            ViewBag.TotalSize = users.MetaData.TotalSize;

            ViewBag.HaveNext = users.MetaData.HaveNext;
            ViewBag.HavePrev = users.MetaData.HavePrev;

            ViewBag.ControllerName = "User";
            ViewBag.ViewActionName = "users";
            ViewBag.CreateActionName = "create-user-view";
            ViewBag.DeleteActionName = "delete-user";
            ViewBag.UpdateActionName = "update-user-view";

            return View(users);
        }
        [HttpGet("create", Name = "create-user-view")]
        public IActionResult CreateView() => View();
        [HttpPost("create", Name = "create-user")]
        public async Task<IActionResult> Create([FromForm] SettlementCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View("CreateView", dto);

            await _userService.CreateAsync<SettlementCreateDto, SettlementDto>(dto);
            return RedirectToAction("Index", new { page = 1, pageSize = 10 });
        }
        [HttpPost("delete-user", Name = "delete-user")]
        public async Task<IActionResult> Delete([FromForm] SettlementDeleteDto dto)
        {
            await _userService.DeleteByIdAsync(dto.Id);
            return RedirectToAction("Index", new { page = 1, pageSize = 10 });
        }
        [HttpGet("update", Name = "update-user-view")]
        public async Task<IActionResult> UpdateView([FromQuery] Guid id)
        {
            var settlement = await _userService.GetByIdAsync<SettlementUpdateDto>(id);
            return View(settlement);
        }
        [HttpPost("update", Name = "update-user")]
        public async Task<IActionResult> Update([FromForm] SettlementUpdateDto dto)
        {
            if (!ModelState.IsValid || string.IsNullOrEmpty(dto.Id.ToString()))
                return View("UpdateView", dto);

            await _userService.UpdateAsync<SettlementUpdateDto, SettlementDto>(dto);
            return RedirectToAction("Index", new { page = 1, pageSize = 10 });
        }
    }
}
