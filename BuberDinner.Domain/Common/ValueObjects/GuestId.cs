using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Common.ValueObjects;

public class GuestId : ValueObject
{
    public Guid Value { get; }
    private GuestId(Guid value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static GuestId CreateUnique()
    { return new GuestId(Guid.NewGuid()); }
}
