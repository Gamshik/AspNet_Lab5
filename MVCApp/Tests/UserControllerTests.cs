using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using MVCApp.Controllers;
using Contracts.Services;
using Entities.Models.DTOs.User;
using Entities.Pagination;
using System;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models.DTOs;

public class UserControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockAuthService = new Mock<IAuthService>();
        _controller = new UserController(_mockUserService.Object, _mockAuthService.Object);
    }

    [Fact]
    public void Index_ShouldReturnNoContent_WhenNoUsersFound()
    {
        // Arrange:
        var users = new PagedList<UserDto>(
            new List<UserDto> {},
            1, 1, 10
        );

        _mockUserService.Setup(service => service.GetByPage<UserDto>(It.IsAny<PaginationQueryParameters>()))
                        .Returns(users);

        // Act: Call the controller action
        var result = _controller.Index(new PaginationQueryParameters());

        // Assert: Ensure that the result is NoContent
        var noContentResult = Assert.IsType<NoContentResult>(result);
        Assert.Equal(204, noContentResult.StatusCode);
    }

    [Fact]
    public async Task Create_ShouldRedirectToIndex_WhenModelIsValid()
    {
        // Arrange: Create a valid DTO
        var dto = new UserRegistrationDto
        {
            FirstName = "Gleb",
            LastName = "Kosharov",
            UserName = "Gamshik",
            Email = "gamshik@example.com",
            Password = "password123"
        };

        _mockAuthService.Setup(service => service.RegisterAsync(It.IsAny<UserRegistrationDto>(), It.IsAny<string[]>()))
                        .ReturnsAsync(true);

        // Act: Call the Create method
        var result = await _controller.Create(dto);

        // Assert: Ensure the result is a redirect to Index
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
    }

    [Fact]
    public async Task Create_ShouldReturnView_WhenModelStateIsInvalid()
    {
        // Arrange
        var dto = new UserRegistrationDto
        {
            FirstName = "Gleb",
            LastName = "Kosharov",
            UserName = "Gamshik",
            Email = "gamshik@example.com",
            Password = ""
        };

        _controller.ModelState.AddModelError("Password", "Password is required.");

        // Act
        var result = await _controller.Create(dto);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("CreateView", viewResult.ViewName);
        Assert.Equal(dto, viewResult.Model);
    }

    [Fact]
    public async Task Delete_ShouldRedirectToIndex_WhenUserIsDeleted()
    {
        // Arrange
        var dto = new UserDeleteDto { Id = Guid.NewGuid() };

        _mockUserService.Setup(service => service.DeleteByIdAsync(It.IsAny<Guid>()))
                        .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(dto);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        Assert.Equal(1, redirectToActionResult.RouteValues["page"]);
        Assert.Equal(10, redirectToActionResult.RouteValues["pageSize"]);
    }

    [Fact]
    public async Task Update_ShouldReturnView_WhenModelStateIsInvalid()
    {
        // Arrange
        var dto = new UserUpdateDto
        {
            Id = Guid.NewGuid(),
            FirstName = "Gleb",
            LastName = "Kosharov",
            UserName = "Gamshik",
            Email = "gamshik@example.com",
            SecurityStamp = ""
        };

        _controller.ModelState.AddModelError("SecurityStamp", "Security Stamp is required.");

        // Act: Call the Update method
        var result = await _controller.Update(dto);

        // Assert: Ensure the result is a View with the same model
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("UpdateView", viewResult.ViewName);
        Assert.Equal(dto, viewResult.Model);
    }

    [Fact]
    public async Task Update_ShouldRedirectToIndex_WhenModelIsValid()
    {
        // Arrange: Create a valid DTO
        var dto = new UserUpdateDto
        {
            Id = Guid.NewGuid(),
            FirstName = "Gleb",
            LastName = "Kosharov",
            UserName = "Gamshik",
            Email = "gamshik@example.com",
            SecurityStamp = "security_stamp"
        };

        _mockUserService.Setup(service => service.UpdateAsync<UserUpdateDto, UserDto>(It.IsAny<UserUpdateDto>()))
                        .ReturnsAsync(new UserDto
                        {
                            Id = dto.Id,
                            FirstName = dto.FirstName,
                            LastName = dto.LastName,
                            UserName = dto.UserName,
                            Email = dto.Email
                        });

        // Act: Call the Update method
        var result = await _controller.Update(dto);

        // Assert: Ensure the result is a redirect to Index
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        Assert.Equal(1, redirectToActionResult.RouteValues["page"]);
        Assert.Equal(10, redirectToActionResult.RouteValues["pageSize"]);
    }
}