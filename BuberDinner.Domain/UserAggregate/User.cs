using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.UserAggregate.ValueObjects;

namespace BuberDinner.Domain.UserAggregate;

public class User : AggregateRoot<UserId>
{
    protected User(
        UserId id,
        string firstName,
        string lastName,
        string email,
        string password,
        DateTime createdDatetime,
        DateTime updatedDatetime) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        CreatedDateTime = createdDatetime;
        UpdatedDateTime = updatedDatetime;
    }

    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public string Email { get; } = null!;
    public string Password { get; } = null!;
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string password)
    {
        return new(
            UserId.CreateUnique(),
            firstName,
            lastName,
            email,
            password,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
