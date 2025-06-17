using System.Text;
using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using BookStore.Application.Services.AuthServices;
using BookStore.Application.Validators.Author;
using BookStore.Application.Validators.Book;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace BookStore.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication
        (this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IBookService, BookService>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddSingleton<IJwtService, JwtService>();

        #region Auth injection
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
            };
        });
        #endregion

        #region Logging injection
        Log.Logger = new LoggerConfiguration()
          .Enrich.FromLogContext()
          .WriteTo.Console()
          .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
          .CreateLogger();

        services.AddSingleton(Log.Logger);
        #endregion

        #region FluentValidation injection
        services.AddFluentValidationAutoValidation();

        services.AddValidatorsFromAssemblyContaining<AuthorCreateDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<AuthorUpdateDtoValidator>();

        services.AddValidatorsFromAssemblyContaining<BookCreateDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<BookUpdateDtoValidator>();
        #endregion

        return services;
    }
}
