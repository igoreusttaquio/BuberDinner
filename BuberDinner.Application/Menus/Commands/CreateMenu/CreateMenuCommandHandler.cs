using BuberDinner.Domain.HostAggregate.ValueObjects;
using BuberDinner.Domain.MenuAggregate;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        Task.Delay(500, cancellationToken).Wait();

        // create Menu itself
        Menu menu = Menu.Create(request.Name, request.Description, HostId.CreateUnique());

        // persist Menu

        // returns Menu

        return menu;
    }
}
