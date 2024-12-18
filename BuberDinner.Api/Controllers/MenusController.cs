﻿using BuberDinner.Application.Menus.Commands.CreateMenu;
using BuberDinner.Contracts.Menus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers;

[Route("hosts/{hostId}/menus")]
[AllowAnonymous]
public class MenusController(ISender sender) : ApiController
{
    private readonly ISender _sender = sender;
    [HttpPost]
    public async Task<IActionResult> CreateMenu(CreateMenuRequest request, string hostId)
    {

        var menuSections = request.Sections.ConvertAll(
            x => new MenuSectionCommand(x.Name, x.Description,
            x.Items.ConvertAll(x => new MenuItemCommand(x.Name, x.Description))));
        var command = new CreateMenuCommand(request.Name, request.Description, menuSections, hostId);

        var createdMenuResult = await _sender.Send(command);

        // return createdMenuResult.Match(menu => Ok(menu), errors => Problem([.. errors]));

        return createdMenuResult.Match(Ok, errors => Problem([.. errors]));
    }

}
