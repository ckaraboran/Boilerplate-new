using System;
using System.Linq.Expressions;
using Boilerplate.Application.Commands.UserRoles;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Application.Tests.Commands.UserRoles;

public class CheckUserForRoleCommandHandlerTests
{
    private readonly Mock<IGenericRepository<Role>> _mockRoleRepository;
    private readonly Mock<IGenericRepository<User>> _mockUserRepository;
    private readonly Mock<IGenericRepository<UserRole>> _mockUserRoleRepository;
    private readonly CheckUserForRoleCommandHandler _openRoleHandler;

    public CheckUserForRoleCommandHandlerTests()
    {
        _mockUserRoleRepository = new Mock<IGenericRepository<UserRole>>();
        _mockUserRepository = new Mock<IGenericRepository<User>>();
        _mockRoleRepository = new Mock<IGenericRepository<Role>>();
        _mockUserRepository.Setup(s =>
                s.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new User { Name = "test" });
        _mockRoleRepository.Setup(s =>
                s.GetAsync(It.IsAny<Expression<Func<Role, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Role { Name = "test" });
        _openRoleHandler = new CheckUserForRoleCommandHandler(_mockUserRoleRepository.Object,
            _mockUserRepository.Object,
            _mockRoleRepository.Object);
    }

    [Fact]
    public async Task Given_CheckUserForRoleCommand_When_OpenRole_Then_ReturnsTrue()
    {
        //Arrange
        var newUserRole = new CheckUserForRoleCommand("test", "test");
        _mockUserRoleRepository.Setup(s =>
                s.GetAsync(It.IsAny<Expression<Func<UserRole, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new UserRole { RoleId = 1, UserId = 1 });
        //Act
        var result = await _openRoleHandler.Handle(newUserRole, default);

        //Assert
        Assert.True(result);
        _mockUserRoleRepository.VerifyAll();
        _mockUserRepository.VerifyAll();
    }

    [Fact]
    public async Task Given_CheckUserForRoleCommand_When_RoleNotFound_Then_ReturnsRoleNotFoundException()
    {
        //Arrange
        var newUserRole = new CheckUserForRoleCommand("test", "test");
        _mockRoleRepository.Setup(s =>
                s.GetAsync(It.IsAny<Expression<Func<Role, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Role)null);

        //Act
        Task Result()
        {
            return _openRoleHandler.Handle(newUserRole, default);
        }

        //Assert
        var exception = await Assert.ThrowsAsync<RecordNotFoundException>(Result);
        Assert.Equal($"Role not found. Role name: '{newUserRole.RoleName}'", exception.Message);
    }

    [Fact]
    public async Task Given_OpenRoleCommand_When_UserNotFound_Then_ReturnsUserNotFoundException()
    {
        //Arrange
        var newUserRole = new CheckUserForRoleCommand("test", "test");
        _mockUserRepository.Setup(s =>
                s.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((User)null);

        //Act
        Task Result()
        {
            return _openRoleHandler.Handle(newUserRole, default);
        }

        //Assert
        var exception = await Assert.ThrowsAsync<RecordNotFoundException>(Result);
        Assert.Equal($"User not found. Username: '{newUserRole.RoleName}'", exception.Message);
    }

    [Fact]
    public async Task Given_OpenRoleCommand_When_NotPermitted_Then_ThrowsNotPermittedException()
    {
        //Arrange
        var newUserRole = new CheckUserForRoleCommand("test", "test");
        _mockUserRoleRepository.Setup(s =>
                s.GetAsync(It.IsAny<Expression<Func<UserRole, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserRole)null);

        //Act
        var result = await _openRoleHandler.Handle(newUserRole, default);

        //Assert
        Assert.False(result);
        _mockUserRoleRepository.VerifyAll();
        _mockUserRepository.VerifyAll();
    }
}