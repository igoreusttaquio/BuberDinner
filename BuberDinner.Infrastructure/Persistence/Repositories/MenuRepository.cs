using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.MenuAggregate;

namespace BuberDinner.Infrastructure.Persistence.Repositories;

public class MenuRepository(BuberDinnerDbContext context) : IMenuRepository
{
    private readonly BuberDinnerDbContext _dbContext = context;
    private static readonly List<Menu> _menus = [];
    public void Add(Menu menu)
    {
        _dbContext.Add(menu);
        _dbContext.SaveChanges();
    }
}
