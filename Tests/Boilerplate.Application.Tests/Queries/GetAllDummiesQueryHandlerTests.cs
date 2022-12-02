using Boilerplate.Application.Queries;

namespace Boilerplate.Application.Tests.Queries;

public class GetAllDummiesQueryHandlerTests
{
    private readonly GetAllDummiesQueryHandler _dummyHandler;
    private readonly Mock<IGenericRepository<Domain.Entities.Dummy>> _mockDummyRepository;
    private readonly Mock<IMapper> _mockMapper;

    public GetAllDummiesQueryHandlerTests()
    {
        _mockDummyRepository = new Mock<IGenericRepository<Domain.Entities.Dummy>>();
        _mockMapper = new Mock<IMapper>();
        _dummyHandler = new GetAllDummiesQueryHandler(_mockDummyRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Dummy_GetAsync_ShouldReturnAllDummiesDto()
    {
        //Arrange
        var mockDummies = new List<Domain.Entities.Dummy>
        {
            new() { Id = 1, Name = "Test" },
            new() { Id = 1, Name = "Test2" }
        };
        var mockDummiesDto = new List<DummyDto>
        {
            new() { Id = 1, Name = "Test" },
            new() { Id = 1, Name = "Test2" }
        };
        _mockDummyRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(mockDummies);
        _mockMapper.Setup(m => m.Map<List<DummyDto>>(It.IsAny<List<Domain.Entities.Dummy>>())).Returns(mockDummiesDto);

        //Act
        var result = await _dummyHandler.Handle(new GetAllDummiesQuery(), default);

        //Assert
        Assert.Equal(result, mockDummiesDto);
        _mockDummyRepository.VerifyAll();
    }
}