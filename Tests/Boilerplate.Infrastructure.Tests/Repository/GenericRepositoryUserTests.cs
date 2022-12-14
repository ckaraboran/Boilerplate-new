namespace Boilerplate.Infrastructure.Tests.Repository;

public class GenericRepositoryUserTests : IDisposable
{
    private readonly DataContext _dataContext;

    public GenericRepositoryUserTests()
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
    public async Task Given_User_When_AddedToDb_Then_ReturnedInQuery()
    {
        //Arrange
        var mockUsers = new List<User>
        {
            new()
            {
                Id = 1, Username = "Test Username", Name = "TestName1", Surname = "TestSurname1",
                Password = "TestPassword1"
            },
            new()
            {
                Id = 2, Username = "Test Username2", Name = "TestName2", Surname = "TestSurname2",
                Password = "TestPassword1"
            },
            new()
            {
                Id = 3, Username = "Test Username3", Name = "TestName3", Surname = "TestSurname3",
                Password = "TestPassword1"
            }
        };

        _dataContext.AddRange(mockUsers);
        await _dataContext.SaveChangesAsync();

        var repository = new GenericRepository<User>(_dataContext);

        //Act
        var users = await repository.GetAllAsync(default);

        //Assert
        Assert.Equal(mockUsers.Count, users.Count);
    }

    [Fact]
    public async Task Given_User_When_GetAsync_Then_ReturnUser()
    {
        //Arrange
        var mockUser = new User
        {
            Id = 1, Username = "Test Username", Name = "TestName1", Surname = "TestSurname1", Password = "TestPassword"
        };
        _dataContext.Users.Add(mockUser);
        await _dataContext.SaveChangesAsync();

        var repository = new GenericRepository<User>(_dataContext);

        //Act
        var user = await repository.GetByIdAsync(mockUser.Id, default);

        //Assert
        Assert.Equal(mockUser.Id, user.Id);
        Assert.Equal(mockUser.Name, user.Name);
        Assert.Equal(mockUser.Surname, user.Surname);
        Assert.Equal(mockUser.Username, user.Username);
        Assert.False(user.IsDeleted);
    }

    [Fact]
    public async Task Given_User_When_GetAsync_WithGivenId_Then_ReturnUser()
    {
        //Arrange
        var mockUser = new User
        {
            Id = 1, Username = "Test Username", Name = "TestName1", Surname = "TestSurname1", Password = "TestPassword1"
        };
        _dataContext.Users.Add(mockUser);
        await _dataContext.SaveChangesAsync();

        var repository = new GenericRepository<User>(_dataContext);

        //Act
        var users = await repository.GetAllAsync(default);

        //Assert
        Assert.Equal(mockUser.Id, users[0].Id);
        Assert.Equal(mockUser.Name, users[0].Name);
        Assert.Equal(mockUser.Surname, users[0].Surname);
        Assert.Equal(mockUser.Username, users[0].Username);
        Assert.False(users[0].IsDeleted);
    }

    [Fact]
    public async Task Given_User_When_GetAsync_WithGivenExpression_Then_ReturnUser()
    {
        //Arrange
        var mockUser = new User
        {
            Id = 1, Username = "Test Username", Name = "TestName1", Surname = "TestSurname1", Password = "TestPassword1"
        };
        _dataContext.Users.Add(mockUser);
        await _dataContext.SaveChangesAsync();

        var repository = new GenericRepository<User>(_dataContext);

        //Act
        var user = await repository.GetAsync(e => e.Id == 1, default);

        //Assert
        Assert.Equal(mockUser.Id, user.Id);
        Assert.Equal(1, user.Id);
        Assert.Equal(mockUser.Name, user.Name);
        Assert.Equal(mockUser.Surname, user.Surname);
        Assert.Equal(mockUser.Username, user.Username);
        Assert.False(user.IsDeleted);
    }

    [Fact]
    public async Task Given_User_When_FindAsync_WithGivenExpression_Then_ReturnUser()
    {
        //Arrange
        var mockUser = new User
        {
            Id = 1, Username = "Test Username", Name = "TestName1", Surname = "TestSurname1", Password = "TestPassword1"
        };
        _dataContext.Users.Add(mockUser);
        await _dataContext.SaveChangesAsync();

        var repository = new GenericRepository<User>(_dataContext);

        //Act
        var users = await repository.FindAsync(e => e.Id == 1, default);

        //Assert
        Assert.Equal(mockUser.Id, users[0].Id);
        Assert.Equal(mockUser.Name, users[0].Name);
        Assert.Equal(mockUser.Surname, users[0].Surname);
        Assert.Equal(mockUser.Username, users[0].Username);
        Assert.False(users[0].IsDeleted);
    }

    [Fact]
    public async Task Given_User_When_AddAsync_Then_ReturnUser()
    {
        //Arrange
        var mockUser = new User
        {
            Id = 1, Username = "Test Username", Name = "TestName1", Surname = "TestSurname1", Password = "TestPassword1"
        };
        var repository = new GenericRepository<User>(_dataContext);

        //Act
        var user = await repository.AddAsync(mockUser, default);

        //Assert
        Assert.Equal(mockUser.Id, user.Id);
        Assert.Equal(mockUser.Name, user.Name);
        Assert.Equal(mockUser.Surname, user.Surname);
        Assert.Equal(mockUser.Username, user.Username);
        Assert.False(user.IsDeleted);
    }

    [Fact]
    public async Task Given_User_When_DeleteAsync_Then_DeleteUser()
    {
        //Arrange
        var mockUser = new User
        {
            Id = 1, Username = "Test Username", Name = "TestName1", Surname = "TestSurname1", Password = "TestPassword"
        };
        _dataContext.Users.Add(mockUser);
        await _dataContext.SaveChangesAsync();

        var repository = new GenericRepository<User>(_dataContext);

        //Act
        await repository.DeleteAsync(mockUser, default);
        var user = await repository.GetByIdAsync(mockUser.Id, default);

        //Assert
        Assert.Null(user);
    }

    [Fact]
    public async Task Given_User_When_UpdateAsync_Then_ReturnUpdatedUser()
    {
        //Arrange
        var mockUser = new User
        {
            Id = 1, Username = "Test Username", Name = "TestName1", Surname = "TestSurname1",
            Password = "TestPassword1"
        };
        _dataContext.Users.Add(mockUser);
        await _dataContext.SaveChangesAsync();
        mockUser.Name = "TestName2";

        var repository = new GenericRepository<User>(_dataContext);

        //Act
        var user = await repository.UpdateAsync(mockUser, default);

        //Assert
        Assert.Equal(mockUser.Id, user.Id);
        Assert.Equal(mockUser.Name, user.Name);
        Assert.Equal(mockUser.Surname, user.Surname);
        Assert.Equal(mockUser.Username, user.Username);
        Assert.False(user.IsDeleted);
    }

    [Fact]
    public async Task Given_User_When_DbError_Then_ThrowsDbUpdateException()
    {
        await using var dbConnection = new SqliteConnection("DataSource=:memory:");
        dbConnection.Open();
        var dbContext = CreateDataContext(dbConnection, new MockFailCommandInterceptor());
        var repository = new GenericRepository<User>(dbContext);

        async Task Result()
        {
            await repository.AddAsync(new User
            {
                Id = 1, Username = "Test Username", Name = "TestName1", Surname = "TestSurname1"
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