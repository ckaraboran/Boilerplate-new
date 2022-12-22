namespace Boilerplate.Domain.Interfaces;

public interface IEntityBase
{
    long Id { get; set; }

    bool IsDeleted { get; set; }
}