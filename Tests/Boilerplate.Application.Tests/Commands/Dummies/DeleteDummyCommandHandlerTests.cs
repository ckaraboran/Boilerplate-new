using Boilerplate.Application.Commands.Dummies;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Application.Tests.Commands.Dummies;

public class DeleteDummyCommandHandlerTests
{
    private readonly DeleteDummyCommandHandler _dummyHandler;
    private readonly Mock<IGenericRepository<Dummy>> _mockDummyRepository;

    public DeleteDummyCommandHandlerTests()
    {
        _mockDummyRepository = new Mock<IGenericRepository<Dummy>>();
        _dummyHandler = new DeleteDummyCommandHandler(_mockDummyRepository.Object);
    }

    [Fact]
    public async Task Given_DummyDelete_When_RecordExist_Then_BeVerified()
    {
        //Arrange
        var mockDummy = new Dummy
        {
            Id = 1,
            Name = "Test"
        };
        _mockDummyRepository.Setup(s => s.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(mockDummy);
        _mockDummyRepository.Setup(s => s.DeleteAsync(It.IsAny<Dummy>()));

        //Act
        await _dummyHandler.Handle(new DeleteDummyCommand(1), default);

        //Assert
        _mockDummyRepository.VerifyAll();
    }

    [Fact]
    public async Task Given_DummyDelete_When_RecordDoesNotExist_Then_ThrowRecordNotFoundException()
    {
        //Arrange
        _mockDummyRepository.Setup(s => s.GetByIdAsync(It.IsAny<long>())).ReturnsAsync((Dummy)null);

        //Act
        Task Result()
        {
            return _dummyHandler.Handle(new DeleteDummyCommand(5), default);
        }

        //Act-Assert
        var exception = await Assert.ThrowsAsync<RecordNotFoundException>(Result);
        Assert.Equal("Dummy not found while deleting. DummyId: '5'", exception.Message);
        _mockDummyRepository.VerifyAll();
    }
}