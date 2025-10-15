using ECommerce.BuildingBlocks.Shared.Kernel.Auth.Options;
using ECommerce.CatalogService.API.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Space.DependencyInjection;

namespace ECommerce.CatalogService.API;

public static class ServiceInstaller
{
    private const string SectionName = "PostgreSQL";

    public static IServiceCollection InstallServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        #region OpenApi
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes = new Dictionary<string, OpenApiSecurityScheme>
                {
                    ["Bearer"] = new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Description = "JWT Authorization header using the Bearer scheme."
                    }
                };

                document.SecurityRequirements = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
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
                            new string[] {}
                        }
                    }
                };

                return Task.CompletedTask;
            });
        });
        #endregion

        #region DB
        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.UseNpgsql(configuration.GetConnectionString(SectionName));
        });
        #endregion


        #region Interfaces
        #endregion

        #region Exceptions
        #endregion

        #region Space
        services.AddSpace(configuration =>
        {
            configuration.ServiceLifetime = ServiceLifetime.Scoped;
        });
        #endregion

        #region Auth
        services.AddHttpContextAccessor();

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.Configure<JwtOptions>(configuration.GetSection("JwtSettings"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer();

        services.AddAuthorization();
        #endregion

        return services;
    }
}
