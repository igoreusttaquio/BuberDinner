using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.DinnerAggregate.ValueObjects;
using BuberDinner.Domain.HostAggregate.ValueObjects;
using BuberDinner.Domain.MenuAggregate.ValueObjects;
using BuberDinner.Domain.UserAggregate.ValueObjects;

namespace BuberDinner.Domain.HostAggregate;

public sealed class Host : AggregateRoot<HostId>
{
    private Host(
        HostId id,
        string firstName,
        string lastName,
        string profileImage,
        AverageRating averageRating,
        UserId userId,
        DateTime createdDate,
        DateTime updatedDate) : base(id)
    {

        FirstName = firstName;
        LastName = lastName;
        ProfileImage = profileImage;
        AverageRating = averageRating;
        UserId = userId;
        CreatedDateTime = createdDate;
        UpdatedDateTime = updatedDate;
    }

    private List<MenuId> _menuIds = [];
    private List<DinnerId> _dinnerIds = [];
    public string FirstName { get; }
    public string LastName { get; }
    public string ProfileImage { get; }
    public AverageRating AverageRating { get; }
    public UserId UserId { get; }
    public IReadOnlyList<MenuId> MenuIds => _menuIds.AsReadOnly();
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();

    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    public static Host Create(string firstName,
        string lastName,
        string profileImage,
        UserId userId)
    {
        return new Host(
            HostId.CreateUnique(),
            firstName,
            lastName,
            profileImage,
            AverageRating.Create(),
            userId,
            DateTime.UtcNow,
            DateTime.UtcNow
            );
    }

}
