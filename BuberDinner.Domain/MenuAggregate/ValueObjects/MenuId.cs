using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.MenuAggregate.ValueObjects;

public class MenuId : ValueObject
{
    public Guid Value { get; }
    private MenuId(Guid value)
    {
        Value = value;
    }

    public static MenuId CreateUnique() => new(Guid.NewGuid());
    public static MenuId Create(Guid id) => new(id);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}