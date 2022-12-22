using System;
using System.Linq.Expressions;
using Boilerplate.Application.Commands.Dummies;
using Boilerplate.Application.Mappings;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Application.Tests.Commands.Dummies;

public class UpdateDummyCommandHandlerTests
{
    private readonly UpdateDummyCommandHandler _dummyHandler;
    private readonly Mock<IGenericRepository<Dummy>> _mockDummyRepository;

    public UpdateDummyCommandHandlerTests()
    {
        _mockDummyRepository = new Mock<IGenericRepository<Dummy>>();
        var myProfile = new AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        var mapper = new Mapper(configuration);
        _dummyHandler = new UpdateDummyCommandHandler(_mockDummyRepository.Object, mapper);
    }

    [Fact]
    public async Task Given_DummyPut_When_WithGivenUpdateDummyRequest_Then_ReturnCreateDummyDto()
    {
        //Arrange
        var newDummy = new Dummy
        {
            Id = 1,
            Name = "New Dummy"
        };
        var mockDummyUpdateCommand = new UpdateDummyCommand(newDummy.Id, newDummy.Name);

        var oldDummy = new Dummy
        {
            Id = 1,
            Name = "Old Dummy"
        };

        _mockDummyRepository.Setup(s => s.GetByIdAsync(It.IsAny<long>())).ReturnsAsync(oldDummy);
        _mockDummyRepository.Setup(s => s.UpdateAsync(It.IsAny<Dummy>())).ReturnsAsync(newDummy);

        //Act
        var result = await _dummyHandler.Handle(mockDummyUpdateCommand, default);

        //Assert
        Assert.Equal(result.Id, newDummy.Id);
        Assert.Equal(result.Name, newDummy.Name);
        _mockDummyRepository.VerifyAll();
    }

    [Fact]
    public async Task Given_DummyPut_When_AnotherDummyWithSameNameExists_Then_RecordAlreadyExistsException()
    {
        //Arrange
        var sameName = "New Dummy";
        var newDummy = new Dummy
        {
            Id = 1,
            Name = sameName
        };
        var newAnotherDummy = new Dummy
        {
            Id = 2,
            Name = sameName
        };
        var mockDummyUpdateCommand = new UpdateDummyCommand(newDummy.Id, newDummy.Name);

        var oldDummy = new Dummy
        {
            Id = 1,
            Name = "Old Dummy"
        };

        _mockDummyRepository.Setup(s => s.GetByIdAsync(newDummy.Id)).ReturnsAsync(oldDummy);
        _mockDummyRepository.Setup(s => s.GetAsync(It.IsAny<Expression<Func<Dummy, bool>>>()))
            .ReturnsAsync(newAnotherDummy);
        _mockDummyRepository.Setup(s => s.UpdateAsync(It.IsAny<Dummy>())).ReturnsAsync(newDummy);

        Task Result()
        {
            return _dummyHandler.Handle(mockDummyUpdateCommand, default);
        }

        //Assert
        var exception = await Assert.ThrowsAsync<RecordAlreadyExistsException>(Result);
        Assert.Equal($"There is a dummy with the same name: '{newAnotherDummy.Name}'", exception.Message);
        _mockDummyRepository.Verify(s => s.UpdateAsync(It.IsAny<Dummy>()), Times.Never);
    }

    [Fact]
    public async Task Given_DummyPut_When_RecordDoesNotExist_Then_ThrowRecordNotFoundException()
    {
        //Arrange
        var mockUpdateDummyCommand = new UpdateDummyCommand(1, "Test");
        _mockDummyRepository.Setup(s => s.GetByIdAsync(mockUpdateDummyCommand.Id))
            .ReturnsAsync((Dummy)null);

        //Act
        Task Result()
        {
            return _dummyHandler.Handle(mockUpdateDummyCommand, default);
        }

        //Assert
        var exception = await Assert.ThrowsAsync<RecordNotFoundException>(Result);
        Assert.Equal("Dummy not found. DummyId: '1'", exception.Message);
        _mockDummyRepository.VerifyAll();
    }
}