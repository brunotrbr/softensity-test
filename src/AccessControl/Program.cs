using AccessControl.Application.UseCases.v1;
using AccessControl.Domain.Interfaces.v1.Repository;
using AccessControl.Domain.Interfaces.v1.UseCases;
using AccessControl.ExceptionHandlers;
using AccessControl.Filters;
using AccessControl.Infrastructure;
using AccessControl.Infrastructure.Repository.v1;
using System.Text.Json;
using System.Text.Json.Serialization;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSerilog();

    // Adding context
    builder.Services.AddDbContext<DBContext>();

    // Adding exception handling
    builder.Services.AddExceptionHandler<GeneralExceptionHandler>();
    builder.Services.AddProblemDetails();

    builder.Services
        .AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));


    // Adding swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(config => 
    {
        config.SchemaFilter<EnumSchemaFilter>();
        config.SwaggerDoc("v1", new() { Title = "Access Control API", Version = "v1" });
    });

    // Adding Use Cases
    builder.Services.AddScoped<ICardsUseCase, CardsUseCase>();
    builder.Services.AddScoped<IDoorsUseCase, DoorsUseCase>();

    // Adding Repositories
    builder.Services.AddScoped<ICardsRepository, CardsRepository>();
    builder.Services.AddScoped<IDoorsRepository, DoorsRepository>();

    var app = builder.Build();

    app.UseExceptionHandler();

    // Enabling swagger only for development
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
        });
    }

    app.UseRouting();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}