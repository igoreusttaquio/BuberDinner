using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.MenuAggregate.ValueObjects;

public class MenuItemId : ValueObject
{
    public Guid Value { get; }
   
    private MenuItemId(Guid value)
    {
        Value = value;
    }

    public static MenuItemId CreateUnique() => new(Guid.NewGuid());
    public static MenuItemId Create(Guid value) => new(value);

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

}
