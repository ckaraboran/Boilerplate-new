using System.Threading;
using Boilerplate.Api.DTOs.Responses.Dummy;
using Boilerplate.Application.Queries;
using MediatR;

namespace Boilerplate.Api.Tests.Controllers;

public class DummyNewControllerTests
{
    private readonly Mock<ISender> _mockMediator;
    private readonly Mock<IMapper> _mockMapper;
    private readonly DummyNewController _sut;

    public DummyNewControllerTests()
    {
        _mockMediator = new Mock<ISender>();
        _mockMapper = new Mock<IMapper>();
        _sut = new DummyNewController(_mockMediator.Object, _mockMapper.Object);
    }
    
    [Fact]
    public async Task Dummy_GetAsync_ShouldReturnAllDummies()
    {
        //Arrange
        var mockDummyDto = new List<DummyDto>
        {
            new() { Id = 1, Name = "Test" },
            new() { Id = 2, Name = "Test2" }
        };
        var mockGetDummiesResponse = new List<GetDummyResponse>
        {
            new() { Id = 1, Name = "Test" },
            new() { Id = 2, Name = "Test2" }
        };
        _mockMediator.Setup(s => s.Send(It.IsAny<GetAllDummiesQuery>(), It.Is<CancellationToken>(x=>x == default))).ReturnsAsync(mockDummyDto);
        _mockMapper.Setup(m => m.Map<List<GetDummyResponse>>(mockDummyDto)).Returns(mockGetDummiesResponse);

        //Act
        var result = await _sut.GetAsync();

        //Assert
        var resultObject = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(expected: mockGetDummiesResponse, actual: resultObject!.Value);
        _mockMediator.VerifyAll();
    }
    
    [Fact]
    public async Task Dummy_GetAsync_WithGivenId_ShouldReturnAllDummy()
    {
        //Arrange
        var mockDummyDto = new DummyDto
        {
            Id = 1,
            Name = "Test"
        };
        var mockGetDummyResponse = new GetDummyResponse
        {
            Id = 1,
            Name = "Test"
        };
        _mockMediator.Setup(s => s.Send(It.IsAny<GetDummyQuery>(),It.Is<CancellationToken>(x=>x == default))).ReturnsAsync(mockDummyDto);
        _mockMapper.Setup(m => m.Map<GetDummyResponse>(mockDummyDto)).Returns(mockGetDummyResponse);

        //Act
        var result = await _sut.GetAsync(1);

        //Assert
        var resultObject = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(expected: mockGetDummyResponse, actual: resultObject!.Value);
        _mockMediator.VerifyAll();
    }
}