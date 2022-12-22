using Boilerplate.Application.Commands.Dummies;
using Boilerplate.Application.Mappings;
using Boilerplate.Domain.Entities;

namespace Boilerplate.Application.Tests.Commands.Dummies;

public class CreateDummyCommandHandlerTests
{
    private readonly CreateDummyCommandHandler _doorHandler;
    private readonly Mock<IGenericRepository<Dummy>> _mockDummyRepository;

    public CreateDummyCommandHandlerTests()
    {
        _mockDummyRepository = new Mock<IGenericRepository<Dummy>>();
        var myProfile = new AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        var mapper = new Mapper(configuration);
        _doorHandler = new CreateDummyCommandHandler(_mockDummyRepository.Object, mapper);
    }

    [Fact]
    public async Task Given_DummyCreate_When_WithGivenCreateDummyCommand_Then_ReturnCreateDummyDto()
    {
        //Arrange
        var mockDummy = new Dummy
        {
            Id = 1,
            Name = "Test"
        };
        _mockDummyRepository.Setup(s => s.AddAsync(It.IsAny<Dummy>())).ReturnsAsync(mockDummy);

        //Act
        var result = await _doorHandler.Handle(new CreateDummyCommand(mockDummy.Name), default);

        //Assert
        Assert.Equal(result.Id, mockDummy.Id);
        Assert.Equal(result.Name, mockDummy.Name);

        _mockDummyRepository.VerifyAll();
    }

    [Fact]
    public async Task
        Given_DummyPost_When_WithGivenCreateDummyRequest_Then_ThrowExistingRecordException_IfRecordExists()
    {
        //Arrange
        var mockCreateDummyCommand = new CreateDummyCommand("Test");
        var mockDummy = new Dummy
        {
            Id = 1,
            Name = "Test"
        };
        _mockDummyRepository.Setup(s => s.GetAsync(p => p.Name == mockCreateDummyCommand.Name)).ReturnsAsync(mockDummy);

        //Act
        Task Result()
        {
            return _doorHandler.Handle(mockCreateDummyCommand, default);
        }

        //Assert
        var exception = await Assert.ThrowsAsync<RecordAlreadyExistsException>(Result);
        Assert.Equal("There is a dummy with the same name: 'Test'", exception.Message);
        _mockDummyRepository.VerifyAll();
    }
}