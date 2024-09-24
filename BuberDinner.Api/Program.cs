using BuberDinner.Api.Middleware;
using static BuberDinner.Application.IoC;
using static BuberDinner.Infrastructure.Ioc;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddRouting(opts =>
    {
        opts.LowercaseUrls = true;
        opts.LowercaseQueryStrings = true;
    });
}

{
    var app = builder.Build();
    app.UseMiddleware<ErrorHandlingMiddleware>();
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
