using System;
using System.Linq.Expressions;
using Boilerplate.Application.Commands.Users;
using Boilerplate.Domain.Entities;
using Boilerplate.Infrastructure.Security;

namespace Boilerplate.Application.Tests.Commands.Users;

public class UpdateUserPasswordCommandHandlerTests
{
    private readonly Mock<IGenericRepository<User>> _mockUserRepository;
    private readonly UpdateUserPasswordCommandHandler _userHandler;

    public UpdateUserPasswordCommandHandlerTests()
    {
        _mockUserRepository = new Mock<IGenericRepository<User>>();
        _userHandler = new UpdateUserPasswordCommandHandler(_mockUserRepository.Object);
    }

    [Fact]
    public async Task Given_UpdateUserPassword_When_UpdateToNewOne_ThenBeOk()
    {
        //Arrange
        var oldPassword = "Test old password";
        var newPassword = "Test new password";
        var mockUser = new User
        {
            Id = 1,
            Username = "Test username",
            Name = "Test Name",
            Surname = "Test Surname"
        };
        mockUser.Password = ClayPasswordHasher.HashPassword(mockUser, oldPassword);
        _mockUserRepository.Setup(s =>
            s.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(mockUser);
        _mockUserRepository.Setup(s => s.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()));

        //Act
        await _userHandler.Handle(new UpdateUserPasswordCommand(mockUser.Username, oldPassword, newPassword), default);

        //Assert

        _mockUserRepository.VerifyAll();
    }

    [Fact]
    public async Task Given_UpdateUserPassword_When_OldPasswordWrong_ThenThrow()
    {
        //Arrange
        var oldPassword = "Test old password";
        var newPassword = "Test new password";
        var mockUser = new User
        {
            Id = 1,
            Username = "Test username",
            Name = "Test Name",
            Surname = "Test Surname"
        };
        mockUser.Password = ClayPasswordHasher.HashPassword(mockUser, oldPassword);
        _mockUserRepository.Setup(s =>
                s.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockUser);
        _mockUserRepository.Setup(s => s.UpdateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()));

        //Act

        Task Result()
        {
            return _userHandler.Handle(new
                UpdateUserPasswordCommand(mockUser.Username,
                    "Old wrong password", newPassword), default);
        }

        //Assert
        var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(Result);
        Assert.Equal("Old password is wrong.", exception.Message);
        _mockUserRepository.Verify(s =>
            s.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}