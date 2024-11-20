﻿using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.MenuAggregate.ValueObjects;

namespace BuberDinner.Domain.MenuAggregate.Entities;

public sealed class MenuSection : Entity<MenuSectionId>
{
    private readonly List<MenuItem> _items = [];

    private MenuSection(
        MenuSectionId id,
        string name,
        string description
        ) : base(id)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; }
    public string Description { get; }

    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

    public static MenuSection Create(
        string name,
        string description)
    {
        return new MenuSection(
            MenuSectionId.CreateUnique(),
            name,
            description);
    }
}
