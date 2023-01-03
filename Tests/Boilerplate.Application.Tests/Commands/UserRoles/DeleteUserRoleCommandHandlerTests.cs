using Boilerplate.Application.Commands.UserRoles;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Application.Tests.Commands.UserRoles;

public class DeleteUserRoleCommandHandlerTests
{
    private readonly Mock<IGenericRepository<UserRole>> _mockUserRoleRepository;
    private readonly DeleteUserRoleCommandHandler _userRoleHandler;

    public DeleteUserRoleCommandHandlerTests()
    {
        _mockUserRoleRepository = new Mock<IGenericRepository<UserRole>>();
        _userRoleHandler = new DeleteUserRoleCommandHandler(_mockUserRoleRepository.Object);
    }

    [Fact]
    public async Task Given_UserRole_When_DeleteAsyncWithGivenId_Then_BeDeleted()
    {
        //Arrange
        var mockUserRole = new UserRole
        {
            Id = 1,
            UserId = 1,
            RoleId = 1
        };
        _mockUserRoleRepository.Setup(s => s.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockUserRole);
        _mockUserRoleRepository.Setup(s => s.DeleteAsync(It.IsAny<UserRole>(), It.IsAny<CancellationToken>()));

        //Act
        await _userRoleHandler.Handle(new DeleteUserRoleCommand(1), default);

        //Assert
        _mockUserRoleRepository.VerifyAll();
    }

    [Fact]
    public async Task Given_UserRoleDelete_When_RecordDoesNotExist_Then_ThrowRecordNotFoundException()
    {
        //Arrange
        _mockUserRoleRepository.Setup(s => s.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserRole)null);

        //Act
        Task Result()
        {
            return _userRoleHandler.Handle(new DeleteUserRoleCommand(5), default);
        }

        //Act-Assert
        var exception = await Assert.ThrowsAsync<RecordNotFoundException>(Result);
        Assert.Equal("UserRole not found while deleting. UserRoleId: '5'", exception.Message);
        _mockUserRoleRepository.VerifyAll();
    }
}