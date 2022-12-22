using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Boilerplate.Infrastructure.Extensions;

public static class ChangeTrackerExtensions
{
    public static void SetAuditProperties(this ChangeTracker changeTracker)
    {
        changeTracker.DetectChanges();
        var entities =
            changeTracker
                .Entries()
                .Where(t => t.Entity is IEntityBase && t.State == EntityState.Deleted);

        foreach (var entry in entities)
        {
            var entity = (IEntityBase)entry.Entity;
            entity.IsDeleted = true;
            entry.State = EntityState.Modified;
        }
    }
}