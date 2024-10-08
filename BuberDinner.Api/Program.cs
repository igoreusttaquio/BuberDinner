//using BuberDinner.Api.Filters;
//using BuberDinner.Api.Middleware;
using BuberDinner.Api;
using static BuberDinner.Application.IoC;
using static BuberDinner.Infrastructure.Ioc;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddControllers();
    //builder.Services.AddControllers(options => options.Filters
    //    .Add<ErrorHadlingFilterAttribute>());
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddPresentation();
    builder.Services.AddRouting(opts =>
    {
        opts.LowercaseUrls = true;
        opts.LowercaseQueryStrings = true;
    });
}

{
    var app = builder.Build();
    //app.UseMiddleware<ErrorHandlingMiddleware>();
    // Configure the HTTP request pipeline.
    // global exception handler
    app.UseExceptionHandler("/error");
    // minimal api aproach - same implementation of Errors Controller
    //app.Map("/error", (HttpContext httpContext) =>
    //{
    //    Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

    //    return Results.Problem(title: exception?.Message);
    //});

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }



    app.UseHttpsRedirection();
    app.UseAuthentication();// middleware
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
