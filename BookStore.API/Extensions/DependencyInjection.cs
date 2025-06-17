using Asp.Versioning;
using Microsoft.OpenApi.Models;
using WatchDog;
using WatchDog.src.Enums;

namespace BookStore.API.Extensions;

public static class DependencyInjection
{
    public static void AddPresentation
        (this IServiceCollection services, IConfiguration config)
    {
        #region Configuring Swagger Versioning
        services.AddApiVersioning(opts =>
            {
                opts.DefaultApiVersion = new ApiVersion(1.0);
                opts.AssumeDefaultVersionWhenUnspecified = true;
                opts.ReportApiVersions = true;
                opts.ApiVersionReader = new HeaderApiVersionReader("api-version");
            })
            .AddMvc()
            .AddApiExplorer(setup =>
            {
                setup.GroupNameFormat = "'v'VVV";
                setup.SubstituteApiVersionInUrl = true;
            });
        #endregion

        #region Swagger Auth Integration
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    []
                }
            });
        });
        #endregion

        #region HealthChecks Injection
        services.AddHealthChecks();
        services.AddHealthChecksUI(opts =>
        {
            opts.AddHealthCheckEndpoint("api", "/health");
            opts.SetEvaluationTimeInSeconds(10);
            opts.SetMinimumSecondsBetweenFailureNotifications(10);
        }).AddInMemoryStorage();
        #endregion

        #region WatchDog Injection
        services.AddWatchDogServices(opts =>
        {
            opts.IsAutoClear = true;
            opts.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Monthly;
        });
        #endregion

        #region Versioning Injection
        services.AddApiVersioning(opts =>
        {
            opts.DefaultApiVersion = new ApiVersion(1.0);
            opts.AssumeDefaultVersionWhenUnspecified = true;
            opts.ReportApiVersions = true;
            opts.ApiVersionReader = new HeaderApiVersionReader("api-version");
        }).AddMvc().AddApiExplorer();
        #endregion
    }
}
