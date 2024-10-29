using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Common.ValueObjects;

public class Rating : ValueObject
{
    public int Value { get; }
    private Rating(int value)
    {
        Value = value;
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static Rating Create(int value)
    { return new Rating(value); }
}
