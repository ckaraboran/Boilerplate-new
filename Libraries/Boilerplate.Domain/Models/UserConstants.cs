namespace Boilerplate.Domain.Models;

public static class UserConstants
{
    public static IEnumerable<UserModel> Users => UserCollection;

    private static readonly List<UserModel> UserCollection = new()
    {
        new UserModel { Username = "ckaraboran", Password = "ckaraboran_admin", Role = "Admin" }
    };
    
    
}