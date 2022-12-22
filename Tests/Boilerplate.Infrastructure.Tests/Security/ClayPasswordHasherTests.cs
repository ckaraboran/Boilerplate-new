using Boilerplate.Infrastructure.Security;

namespace Boilerplate.Infrastructure.Tests.Security;

public class ClayPasswordHasherTests
{
    [Fact]
    public void Given_ExistingDbValues_When_VerifyPasswordHash_Then_ReturnsTrue()
    {
        var hashedPassword = "AQAAAAEAACcQAAAAEKEuZwcbIWaBgJFY56Jxj6bUIoSX6hQBcwU3mYLCRi/fymMd2eUSauIZCBs4RIDpeQ==";
        var password = "Test user";

        var user = new User
        {
            Id = 1,
            Username = password,
            IsDeleted = false,
            Name = password,
            Surname = password,
            Password = string.Empty
        };
        Assert.True(ClayPasswordHasher.IsSameWithHashedPassword(user, hashedPassword, password));
    }

    [Fact]
    public void Given_User_When_Hashed_Then_VerifyValue()
    {
        var password = "Test user";

        var user = new User
        {
            Id = 1,
            Username = password,
            IsDeleted = false,
            Name = password,
            Surname = password,
            Password = string.Empty
        };
        var hashedPassword = ClayPasswordHasher.HashPassword(user, password);
        Assert.True(ClayPasswordHasher.IsSameWithHashedPassword(user, hashedPassword, password));
    }
}