using BuberDinner.Domain.HostAggregate.ValueObjects;
using BuberDinner.Domain.MenuAggregate;
using BuberDinner.Domain.MenuAggregate.Entities;
using BuberDinner.Domain.MenuAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuberDinner.Infrastructure.Persistence.Configurations;

public class MenuConfigurations : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        ConfigureMenusTable(builder);
        ConfigureMenusSectionTable(builder);
        ConfigureMenusDinnerIdsTable(builder);
        ConfigureMenusReviewIdsTable(builder);
    }

    private static void ConfigureMenusReviewIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.MenuReviewIds, reviewsBuilder =>
        {
            reviewsBuilder.ToTable("MenuReviewIds");

            reviewsBuilder.WithOwner().HasForeignKey("MenuId");
            reviewsBuilder.HasKey("Id");// shadow property - for each MenuId value object
            reviewsBuilder.Property(d => d.Value)
                .HasColumnName("ReviewId")
                .ValueGeneratedNever();
        });

        builder.Metadata
            .FindNavigation(nameof(Menu.MenuReviewIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureMenusDinnerIdsTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.DinnerIds, dinnerBuilder =>
        {
            dinnerBuilder.ToTable("MenuDinnerIds");

            dinnerBuilder.WithOwner().HasForeignKey("MenuId");
            dinnerBuilder.HasKey("Id");// shadow property - for each MenuId value object
            dinnerBuilder.Property(d => d.Value)
                .HasColumnName("DinnerId")
                .ValueGeneratedNever();
        });

        builder.Metadata
            .FindNavigation(nameof(Menu.DinnerIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureMenusItemTable(OwnedNavigationBuilder<MenuSection, MenuItem> menuItemBuilder)
    {
        menuItemBuilder.ToTable("MenuItems");

        menuItemBuilder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
            id => id.Value,
            value => MenuItemId.Create(value))
            .HasColumnName("MenuItemId");

        menuItemBuilder.HasKey(nameof(MenuItem.Id), "MenuSectionId", "MenuId");// shadow properties
        //menuItemBuilder.HasKey("MenuItemId", "MenuSectionId", "MenuId");// shadow properties

        // This ensures MenuItem has foreign keys MenuSectionId and MenuId
        menuItemBuilder.WithOwner().HasForeignKey("MenuSectionId", "MenuId");

        menuItemBuilder.Property(i => i.Name)
            .HasMaxLength(100);
        menuItemBuilder.Property(i => i.Description)
            .HasMaxLength(100);
    }

    private static void ConfigureMenusSectionTable(EntityTypeBuilder<Menu> builder)
    {
        builder.OwnsMany(m => m.Sections, sb =>
        {
            // sb stands for Menu Section Builder
            sb.ToTable("MenuSections");


            // Link MenuSection to Menu via MenuId
            sb.WithOwner().HasForeignKey("MenuId");

            // composite key
            // sb.HasKey(s => new[] { s.Id, s.MenuId }); // MenuId is shadowing property and not accessible this way
            sb.HasKey("Id", "MenuId");// composite key - Define composite key for MenuSection

            sb.Property(s => s.Id)
                .HasColumnName("MenuSectionId")
                .ValueGeneratedNever()
                .HasConversion(
                    ms => ms.Value,
                    value => MenuSectionId.Create(value));


            sb.Property(s => s.Name)
                 .HasMaxLength(100);

            sb.Property(s => s.Description)
                 .HasMaxLength(100);

            sb.OwnsMany(s => s.Items, ib =>
            {
                // ib stands for Item Builder
                // Define MenuItem configuration within MenuSection
                ConfigureMenusItemTable(ib);
            });

            // FindNavigation are not available in nested builders e.g OwnedNavigationBuilder
            sb.Navigation(s => s.Items).Metadata.SetField("_items");
            sb.Navigation(s => s.Items)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });
    }

    private static void ConfigureMenusTable(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable("Menus");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => MenuId.Create(value));

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(100);

        // Table splitting
        // Configure the AverageRating as a part of the Menu table (table splitting)
        builder.OwnsOne(m => m.AverageRating, avgBuilder =>
        {
            avgBuilder.Property(avg => avg.Value)
                .HasColumnName("AverageRatingValue");
            avgBuilder.Property(avg => avg.NumRatings)
                .HasColumnName("AverageRatingNumRatings");

            avgBuilder.WithOwner(); // Ensure it is linked to the owner
        });

        builder.Property(m => m.HostId)
        .HasConversion(
            id => id.Value,
            value => HostId.Create(value));


        builder.Metadata
            .FindNavigation(nameof(Menu.Sections))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
