using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Common.ValueObjects;
using BuberDinner.Domain.Host.ValueObjects;
using BuberDinner.Domain.Menu.ValueObjects;
using BuberDinner.Domain.MenuReview.ValueObjects;

namespace BuberDinner.Domain.MenuReview;

public class MenuReview : AggregateRoot<MenuReviewId>
{
    protected MenuReview(
        MenuReviewId id,
        string comment,
        HostId hostId,
        MenuId menuId,
        GuestId guestId,
        DateTime createdDateTime,
        DateTime updatedDateTime
        ) : base(id)
    {
        Comment = comment;
        HostId = hostId;
        MenuId = menuId;
        GuestId = guestId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public string Comment { get; }
    public HostId HostId { get; }
    public MenuId MenuId { get; }
    public GuestId GuestId { get; }
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    public static MenuReview Create(
        string comment,
        HostId hostId,
        MenuId menuId,
        GuestId guestId)
    {
        return new(
            MenuReviewId.CreateUnique(),
            comment,
            hostId,
            menuId,
            guestId,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }
}
