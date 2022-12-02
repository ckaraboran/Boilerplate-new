using Boilerplate.Application.Queries;

namespace Boilerplate.Application.Tests.Queries;

public class GetDummyQueryHandlerTests
{
    private readonly GetDummyQueryHandler _dummyHandler;
    private readonly Mock<IGenericRepository<Domain.Entities.Dummy>> _mockDummyRepository;
    private readonly Mock<IMapper> _mockMapper;

    public GetDummyQueryHandlerTests()
    {
        _mockDummyRepository = new Mock<IGenericRepository<Domain.Entities.Dummy>>();
        _mockMapper = new Mock<IMapper>();
        _dummyHandler = new GetDummyQueryHandler(_mockDummyRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Dummy_GetAsync_WithGivenId_ShouldReturnDummyDto()
    {
        //Arrange
        var mockDummy = new Domain.Entities.Dummy
        {
            Id = 1,
            Name = "Test"
        };
        var mockDummyDto = new DummyDto
        {
            Id = 1,
            Name = "Test"
        };
        _mockDummyRepository.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync(mockDummy);
        _mockMapper.Setup(m => m.Map<DummyDto>(It.IsAny<Domain.Entities.Dummy>())).Returns(mockDummyDto);

        //Act
        var result = await _dummyHandler.Handle(new GetDummyQuery(1), default);

        //Assert
        Assert.Equal(result, mockDummyDto);
        _mockDummyRepository.VerifyAll();
    }
}