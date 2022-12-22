using System.Diagnostics.CodeAnalysis;
using Boilerplate.Domain.Enums;
using Boilerplate.Infrastructure.Extensions;
using Boilerplate.Infrastructure.Maps;

namespace Boilerplate.Infrastructure;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Dummy> Dummies { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UsersRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new EntityMapBase<Dummy>());
        modelBuilder.ApplyConfiguration(new EntityMapBase<Role>());
        modelBuilder.ApplyConfiguration(new EntityMapBase<User>());
        modelBuilder.ApplyConfiguration(new EntityMapBase<UserRole>());
        Seed(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    private static void Seed(ModelBuilder modelBuilder)
    {
        AddSeedUserWithPassword(modelBuilder, 1, KnownRoles.Manager.ToString()
            , "AQAAAAEAACcQAAAAEPAP+lL/oxuJsRb0+JNJqx5qJM7ZwJFO07Mozj19QlCpb5mSeDp/VfRbOTy03G2qaQ==");
        AddSeedUserWithPassword(modelBuilder, 2, KnownRoles.User.ToString()
            , "AQAAAAEAACcQAAAAEEYBeXoqyOYv3D0Gnzdp2aa/p4XJEu2Rt9YAtezKBEcF6ECjPv2NoRamELuvdBSvaw==");
    }

    private static void AddSeedUserWithPassword(ModelBuilder modelBuilder, long id, string name, string password)
    {
        var user = new User
        {
            Id = id, Username = name, Name = name,
            Surname = name,
            Password = password
        };
        var role = new Role { Id = id, Name = name };
        modelBuilder.Entity<Role>().HasData(role);
        modelBuilder.Entity<User>().HasData(user);
        modelBuilder.Entity<UserRole>().HasData(new UserRole { Id = id, UserId = user.Id, RoleId = role.Id });
    }

    [ExcludeFromCodeCoverage]
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ChangeTracker.SetAuditProperties();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    [ExcludeFromCodeCoverage]
    public override int SaveChanges()
    {
        ChangeTracker.SetAuditProperties();
        return base.SaveChanges();
    }

    [ExcludeFromCodeCoverage]
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        ChangeTracker.SetAuditProperties();
        return base.SaveChangesAsync(cancellationToken);
    }

    [ExcludeFromCodeCoverage]
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        ChangeTracker.SetAuditProperties();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}