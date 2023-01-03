namespace Boilerplate.Infrastructure.Tests.Repository;

public class GenericRepositoryDummyTests : IDisposable
{
    private readonly DataContext _dataContext;

    public GenericRepositoryDummyTests()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _dataContext = new DataContext(optionsBuilder.Options);
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
    public async Task Given_Dummy_When_AddedToDb_Then_ReturnsInQuery()
    {
        //Arrange
        var mockDummies = new List<Dummy>
        {
            new() { Id = 1, Name = "TestName1" },
            new() { Id = 2, Name = "TestName2" },
            new() { Id = 3, Name = "TestName3" }
        };

        _dataContext.AddRange(mockDummies);
        await _dataContext.SaveChangesAsync();

        var repository = new GenericRepository<Dummy>(_dataContext);

        //Act
        var dummies = await repository.GetAllAsync(default);

        //Assert
        Assert.Equal(mockDummies.Count, dummies.Count);
    }

    [Fact]
    public async Task Given_Dummy_When_GetAsync_Then_ReturnDummy()
    {
        //Arrange
        var mockDummy = new Dummy { Id = 1, Name = "TestName1" };
        _dataContext.Dummies.Add(mockDummy);
        await _dataContext.SaveChangesAsync();

        var repository = new GenericRepository<Dummy>(_dataContext);

        //Act
        var dummies = await repository.GetAllAsync(default);

        //Assert
        Assert.Equal(mockDummy.Id, dummies[0].Id);
        Assert.Equal(mockDummy.Name, dummies[0].Name);
    }

    [Fact]
    public async Task Given_Dummy_When_GetAsyncWithGivenId_Then_ReturnsDummy()
    {
        //Arrange
        var mockDummy = new Dummy { Id = 1, Name = "TestName1" };
        _dataContext.Dummies.Add(mockDummy);
        await _dataContext.SaveChangesAsync();

        var repository = new GenericRepository<Dummy>(_dataContext);

        //Act
        var dummy = await repository.GetByIdAsync(mockDummy.Id, default);

        //Assert
        Assert.Equal(mockDummy.Id, dummy.Id);
        Assert.Equal(mockDummy.Name, dummy.Name);
    }

    [Fact]
    public async Task Given_Dummy_When_GetAsyncWithGivenExpression_Then_ReturnsDummy()
    {
        //Arrange
        var mockDummy = new Dummy { Id = 1, Name = "TestName1" };
        _dataContext.Dummies.Add(mockDummy);
        await _dataContext.SaveChangesAsync();

        var repository = new GenericRepository<Dummy>(_dataContext);

        //Act
        var dummy = await repository.GetAsync(e => e.Id == 1, default);

        //Assert
        Assert.Equal(mockDummy.Id, dummy.Id);
        Assert.Equal(mockDummy.Name, dummy.Name);
    }

    [Fact]
    public async Task Given_Dummy_When_FindAsync_Then_ReturnsDummy()
    {
        //Arrange
        var mockDummy = new Dummy { Id = 1, Name = "TestName1" };
        _dataContext.Dummies.Add(mockDummy);
        await _dataContext.SaveChangesAsync();

        var repository = new GenericRepository<Dummy>(_dataContext);

        //Act
        var users = await repository.FindAsync(e => e.Id == 1, default);

        //Assert
        Assert.Equal(mockDummy.Id, users[0].Id);
        Assert.Equal(mockDummy.Name, users[0].Name);
    }

    [Fact]
    public async Task Given_Dummy_When_AddAsync_Then_ReturnsDummy()
    {
        //Arrange
        var mockDummy = new Dummy { Id = 1, Name = "TestName1" };
        var repository = new GenericRepository<Dummy>(_dataContext);

        //Act
        var dummy = await repository.AddAsync(mockDummy, default);

        //Assert
        Assert.Equal(mockDummy.Id, dummy.Id);
        Assert.Equal(mockDummy.Name, dummy.Name);
    }

    [Fact]
    public async Task Given_Dummy_When_DeleteAsync_Then_DeletesDummy()
    {
        //Arrange
        var mockDummy = new Dummy { Id = 1, Name = "TestName1" };
        _dataContext.Dummies.Add(mockDummy);
        await _dataContext.SaveChangesAsync();

        var repository = new GenericRepository<Dummy>(_dataContext);

        //Act
        await repository.DeleteAsync(mockDummy, default);
        var dummy = await repository.GetByIdAsync(mockDummy.Id, default);

        //Assert
        Assert.Null(dummy);
    }

    [Fact]
    public async Task Given_Dummy_When_UpdateAsync_Then_ReturnsUpdatedDummy()
    {
        //Arrange
        var mockDummy = new Dummy { Id = 1, Name = "TestName1" };
        _dataContext.Dummies.Add(mockDummy);
        await _dataContext.SaveChangesAsync();
        mockDummy.Name = "TestName2";

        var repository = new GenericRepository<Dummy>(_dataContext);

        //Act
        var dummy = await repository.UpdateAsync(mockDummy, default);

        //Assert
        Assert.Equal(mockDummy.Id, dummy.Id);
        Assert.Equal(mockDummy.Name, dummy.Name);
    }

    [Fact]
    public async Task Given_Dummy_When_DatabaseError_Then_ThrowsDbUpdateException()
    {
        await using var dbConnection = new SqliteConnection("DataSource=:memory:");
        dbConnection.Open();
        var dbContext = CreateDataContext(dbConnection, new MockFailCommandInterceptor());
        var repository = new GenericRepository<Dummy>(dbContext);

        async Task Result()
        {
            await repository.AddAsync(new Dummy
            {
                Name = "TestName1"
            }, default);
        }

        await Assert.ThrowsAsync<DbUpdateException>(Result);
    }

    private static DataContext CreateDataContext(DbConnection connection, params IInterceptor[]? interceptors)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>().UseSqlite(connection);

        if (interceptors != null) optionsBuilder.AddInterceptors(interceptors);

        var dbContext = new DataContext(optionsBuilder.Options);
        dbContext.Database.EnsureCreated();

        return dbContext;
    }
}