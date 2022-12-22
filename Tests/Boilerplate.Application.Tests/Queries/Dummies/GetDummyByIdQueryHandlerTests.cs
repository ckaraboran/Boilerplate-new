using System;
using Boilerplate.Application.Mappings;
using Boilerplate.Application.Queries.Dummies;
using Boilerplate.Domain.Entities;
using Boilerplate.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Application.Tests.Queries.Dummies;

public class GetDummyByIdQueryHandlerTests : IDisposable
{
    private readonly DataContext _dataContext;
    private readonly GetDummyByIdQueryHandler _dummyHandler;

    public GetDummyByIdQueryHandlerTests()
    {
        var dbOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        _dataContext = new DataContext(dbOptions);
        var myProfile = new AutoMapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
        var mapper = new Mapper(configuration);
        _dummyHandler = new GetDummyByIdQueryHandler(_dataContext, mapper);
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
    public async Task Given_DummyGet_When_WithGivenId_Then_ReturnDummyDto()
    {
        //Arrange

        var mockDummies = new List<Dummy>
        {
            new() { Id = new Random().Next(), Name = "Dummy1" },
            new() { Id = new Random().Next(), Name = "Dummy2" },
            new() { Id = new Random().Next(), Name = "Dummy3" }
        };
        var newMockDummy = new Dummy
        {
            Id = new Random().Next(),
            Name = "Dummy4"
        };
        mockDummies.Add(newMockDummy);

        await _dataContext.AddRangeAsync(mockDummies);
        await _dataContext.SaveChangesAsync();

        //Act
        var result = await _dummyHandler.Handle(new GetDummyByIdQuery(newMockDummy.Id), default);

        //Assert
        Assert.Equal(result.Id, newMockDummy.Id);
        Assert.Equal(result.Name, newMockDummy.Name);
    }
}