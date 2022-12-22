namespace Boilerplate.Api.Tests.Middlewares;

public class ExceptionMiddlewareTests
{
    private readonly Mock<ILogger<ExceptionMiddleware>> _mockLogger;

    public ExceptionMiddlewareTests()
    {
        _mockLogger = new Mock<ILogger<ExceptionMiddleware>>();
    }

    [Fact]
    public async Task Given_RecordNotFoundException_When_Thrown_Then_ReturnNotFoundHttpCode()
    {
        //Arrange
        var recordNotFoundException = new RecordNotFoundException("test");
        var mockMiddleware = new Mock<RequestDelegate>();
        mockMiddleware.Setup(x => x.Invoke(It.IsAny<HttpContext>())).Throws(recordNotFoundException);

        var httpContext = new DefaultHttpContext();
        var exceptionHandlingMiddleware = new ExceptionMiddleware(mockMiddleware.Object, _mockLogger.Object);

        //Act
        await exceptionHandlingMiddleware.Invoke(httpContext);

        //Assert
        Assert.Equal(HttpStatusCode.NotFound, (HttpStatusCode)httpContext.Response.StatusCode);
    }

    [Fact]
    public async Task Given_RecordCannotBeDeletedException_When_Thrown_Then_ReturnForbiddenHttpCode()
    {
        //Arrange
        var recordCannotBeDeletedException = new RecordCannotBeDeletedException("test");
        var mockMiddleware = new Mock<RequestDelegate>();
        mockMiddleware.Setup(x => x.Invoke(It.IsAny<HttpContext>())).Throws(recordCannotBeDeletedException);

        var httpContext = new DefaultHttpContext();
        var exceptionHandlingMiddleware = new ExceptionMiddleware(mockMiddleware.Object, _mockLogger.Object);

        //Act
        await exceptionHandlingMiddleware.Invoke(httpContext);

        //Assert
        Assert.Equal(HttpStatusCode.Forbidden, (HttpStatusCode)httpContext.Response.StatusCode);
    }

    [Fact]
    public async Task Given_RecordCannotBeChangedException_When_Thrown_Then_ReturnForbiddenHttpCode()
    {
        //Arrange
        var recordCannotBeChangedException = new RecordCannotBeChangedException("test");
        var mockMiddleware = new Mock<RequestDelegate>();
        mockMiddleware.Setup(x => x.Invoke(It.IsAny<HttpContext>())).Throws(recordCannotBeChangedException);

        var httpContext = new DefaultHttpContext();
        var exceptionHandlingMiddleware = new ExceptionMiddleware(mockMiddleware.Object, _mockLogger.Object);

        //Act
        await exceptionHandlingMiddleware.Invoke(httpContext);

        //Assert
        Assert.Equal(HttpStatusCode.Forbidden, (HttpStatusCode)httpContext.Response.StatusCode);
    }

    [Fact]
    public async Task Given_RecordAlreadyExistsException_When_Thrown_Then_ReturnBadRequestHttpCode()
    {
        //Arrange
        var recordAlreadyExistsException = new RecordAlreadyExistsException("test");
        var mockMiddleware = new Mock<RequestDelegate>();
        mockMiddleware.Setup(x => x.Invoke(It.IsAny<HttpContext>())).Throws(recordAlreadyExistsException);

        var httpContext = new DefaultHttpContext();
        var exceptionHandlingMiddleware = new ExceptionMiddleware(mockMiddleware.Object, _mockLogger.Object);

        //Act
        await exceptionHandlingMiddleware.Invoke(httpContext);

        //Assert
        Assert.Equal(HttpStatusCode.Conflict, (HttpStatusCode)httpContext.Response.StatusCode);
    }

    [Fact]
    public async Task Given_UnauthorizedAccessException_When_Thrown_Then_ReturnUnauthorizedHttpCode()
    {
        //Arrange
        var unauthorizedAccessException = new UnauthorizedAccessException("test");
        var mockMiddleware = new Mock<RequestDelegate>();
        mockMiddleware.Setup(x => x.Invoke(It.IsAny<HttpContext>())).Throws(unauthorizedAccessException);

        var httpContext = new DefaultHttpContext();
        var exceptionHandlingMiddleware = new ExceptionMiddleware(mockMiddleware.Object, _mockLogger.Object);

        //Act
        await exceptionHandlingMiddleware.Invoke(httpContext);

        //Assert
        Assert.Equal(HttpStatusCode.Unauthorized, (HttpStatusCode)httpContext.Response.StatusCode);
    }

    [Fact]
    public async Task Given_RandomException_When_Thrown_Then_ReturnInternalServerErrorStatusCode()
    {
        //Arrange
        var mockException = new Exception("test");
        var mockMiddleware = new Mock<RequestDelegate>();
        mockMiddleware.Setup(x => x.Invoke(It.IsAny<HttpContext>())).Throws(mockException);

        var httpContext = new DefaultHttpContext();
        var exceptionHandlingMiddleware = new ExceptionMiddleware(mockMiddleware.Object, _mockLogger.Object);

        //Act
        await exceptionHandlingMiddleware.Invoke(httpContext);

        //Assert
        Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)httpContext.Response.StatusCode);
    }
}