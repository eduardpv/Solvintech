using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Solvintech.API.Database;
using Solvintech.API.Interfaces;
using Solvintech.API.Services;
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
            services.AddDatabase(configuration);
            services.AddIdentity(configuration);
            services.AddCustomServices();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            const string connectionStringName = "MsSqlServerConnection";
            const string connectionException = $"Connection string '{connectionStringName}' isn't found.";

            var connection = configuration.GetConnectionString(connectionStringName)
                ?? throw new InvalidOperationException(connectionException);

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
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]
                           ?? throw new InvalidOperationException("SecretKey isn't found"))),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });

            return services;
        }

        private static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddSingleton<HttpClient>();

            return services;
        }
    }
}
