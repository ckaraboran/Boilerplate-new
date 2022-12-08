using System.Diagnostics.CodeAnalysis;

namespace Boilerplate.Domain.Models;

public static class UserConstants
{
    public static IEnumerable<UserModel> Users => UserCollection;

    [SuppressMessage("SonarLint", "S2068", Justification = "Ignored intentionally as a boilerplate app")]
    private static readonly List<UserModel> UserCollection = new()
    {
        new UserModel { Username = "ckaraboran", Password = "ckaraboran", Role = "Admin" }
    };
}