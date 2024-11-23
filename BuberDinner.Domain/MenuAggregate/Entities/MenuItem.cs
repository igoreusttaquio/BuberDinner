using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.MenuAggregate.ValueObjects;

namespace BuberDinner.Domain.MenuAggregate.Entities;

public class MenuItem : Entity<MenuItemId>
{

    public string Name { get; private set; }
    public string Description { get; private set; }
    public MenuId MenuId { get; private set; }

    private MenuItem(MenuItemId id, string name, string description, MenuId menuId) : base(id)
    {
        Name = name;
        Description = description;
        MenuId = menuId;
    }

    public static MenuItem Create(
        string name,
        string description,
        MenuId menuId)
    {
        return new(
            MenuItemId.CreateUnique(),
            name,
            description,
            menuId);
    }
}
