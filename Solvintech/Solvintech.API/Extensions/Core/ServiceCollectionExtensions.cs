using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Solvintech.API.Database;
using Solvintech.API.Interfaces;
using Solvintech.API.Services;
using Solvintech.API.Сommon;
using System.Text;

namespace Solvintech.API.Extensions.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            services.AddSwagger();
            services.AddDatabase(configuration);
            services.AddIdentity(configuration);
            services.AddLocalServices();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString(Constants.ConnectionString.Name)
                ?? throw new InvalidOperationException(Constants.ConfigurationErrors.ConnectionStringNotFound);

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connection);
            });

            return services;
        }

        private static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[Constants.Configuration.SecretKey]
                           ?? throw new InvalidOperationException(Constants.ConfigurationErrors.SecretKetNotFound))),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });

            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(Constants.Configuration.Bearer, new OpenApiSecurityScheme()
                {
                    Name = Constants.Configuration.Authorization,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = Constants.Configuration.Bearer,
                    BearerFormat = Constants.Configuration.JWT,
                    In = ParameterLocation.Header
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id = Constants.Configuration.Bearer
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }

        private static IServiceCollection AddLocalServices(this IServiceCollection services)
        {
            services.AddSingleton<HttpClient>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
