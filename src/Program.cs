using AccessControl;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AccessControlService>();

builder.Services.AddControllers();

// Adding swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config => 
{
    config.SwaggerDoc("v1", new() { Title = "Access Control API", Version = "v1" });
});


var app = builder.Build();

// Enabling swagger only for development
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
    });
}

app.UseRouting();

app.MapControllers();

app.Run();
