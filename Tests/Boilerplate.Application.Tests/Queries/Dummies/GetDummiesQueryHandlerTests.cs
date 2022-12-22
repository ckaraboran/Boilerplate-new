using System;
using Boilerplate.Application.Mappings;
using Boilerplate.Application.Queries.Dummies;
using Boilerplate.Domain.Entities;
using Boilerplate.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Application.Tests.Queries.Dummies;

public class GetDummiesQueryHandlerTests : IDisposable
{
    private readonly DataContext _dataContext;
    private readonly GetDummiesQueryHandler _dummyHandler;

    public GetDummiesQueryHandlerTests()
    {
        var dbOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dataContext = new DataContext(dbOptions);
        var myProfile = new AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        var mapper = new Mapper(configuration);
        _dummyHandler = new GetDummiesQueryHandler(_dataContext, mapper);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool _)
    {
        _dataContext.Database.EnsureDeleted();
    }

    [Fact]
    public async Task Given_Dummy_When_GetAsync_Then_ReturnDummiesDto()
    {
        //Arrange
        var mockDummies = new List<Dummy>
        {
            new() { Id = 1, Name = "Test" },
            new() { Id = 2, Name = "Test2" }
        };
        await _dataContext.AddRangeAsync(mockDummies);
        await _dataContext.SaveChangesAsync();
        //Act
        var result = await _dummyHandler.Handle(new GetDummiesQuery(), default);

        //Assert
        Assert.Equal(result[0].Id, mockDummies[0].Id);
        Assert.Equal(result[0].Name, mockDummies[0].Name);
        Assert.Equal(result[1].Id, mockDummies[1].Id);
        Assert.Equal(result[1].Name, mockDummies[1].Name);
    }
}