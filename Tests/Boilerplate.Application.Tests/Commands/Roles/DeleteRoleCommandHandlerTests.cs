using Boilerplate.Application.Commands.Roles;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Enums;

namespace Boilerplate.Application.Tests.Commands.Roles;

public class DeleteRoleCommandHandlerTests
{
    private readonly Mock<IGenericRepository<Role>> _mockRoleRepository;
    private readonly DeleteRoleCommandHandler _roleHandler;

    public DeleteRoleCommandHandlerTests()
    {
        _mockRoleRepository = new Mock<IGenericRepository<Role>>();
        _roleHandler = new DeleteRoleCommandHandler(_mockRoleRepository.Object);
    }

    [Fact]
    public async Task Given_RoleDelete_When_WithGivenId_Then_BeDeleted()
    {
        //Arrange
        var mockRole = new Role
        {
            Id = 1,
            Name = "Test"
        };
        _mockRoleRepository.Setup(s => s.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockRole);
        _mockRoleRepository.Setup(s => s.DeleteAsync(It.IsAny<Role>(), It.IsAny<CancellationToken>()));

        //Act
        await _roleHandler.Handle(new DeleteRoleCommand(1), default);

        //Assert
        _mockRoleRepository.VerifyAll();
    }

    [Fact]
    public async Task Given_RoleDelete_When_PredefinedRole_Then_ThrowRecordCannotBeDeletedException()
    {
        //Arrange
        var mockRole = new Role
        {
            Id = 1,
            Name = KnownRoles.Manager.ToString()
        };
        _mockRoleRepository.Setup(s => s.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockRole);
        _mockRoleRepository.Setup(s => s.DeleteAsync(It.IsAny<Role>(), It.IsAny<CancellationToken>()));

        //Act
        Task Result()
        {
            return _roleHandler.Handle(new DeleteRoleCommand(mockRole.Id), default);
        }

        //Act-Assert
        var exception = await Assert.ThrowsAsync<RecordCannotBeDeletedException>(Result);
        Assert.Equal($"Predefined role cannot be deleted: Role name: '{mockRole.Name}'", exception.Message);
        //verify mock role repository delete async not called
        _mockRoleRepository.Verify(s => s.DeleteAsync(It.IsAny<Role>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Given_RoleDelete_When_RecordDoesNotExist_Then_ThrowRecordNotFoundException()
    {
        //Arrange
        _mockRoleRepository.Setup(s => s.GetByIdAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Role)null);

        //Act
        Task Result()
        {
            return _roleHandler.Handle(new DeleteRoleCommand(5), default);
        }

        //Act-Assert
        var exception = await Assert.ThrowsAsync<RecordNotFoundException>(Result);
        Assert.Equal("Role not found while deleting. RoleId: '5'", exception.Message);
        _mockRoleRepository.VerifyAll();
    }
}