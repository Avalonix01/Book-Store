using BookStore.API.Extensions;
using BookStore.Application.Extensions;
using BookStore.Infrastructure.Extensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using WatchDog;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPresentation(builder.Configuration);

builder.Host.UseSerilog();
builder.Logging.AddWatchDogLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI(opts =>
{
    opts.UIPath = "/health-ui";
    opts.ApiPath = "/health-api";
});

app.UseWatchDogExceptionLogger();
app.UseWatchDog(opt =>
{
    opt.WatchPageUsername = "admin";
    opt.WatchPagePassword = "admin";
});

app.Run();