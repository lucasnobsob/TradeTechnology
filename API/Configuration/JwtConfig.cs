using System.Text;
using Domain.Ports;
using Infra.Data.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Configuration
{
    public static class JwtConfig
    {
        public static void AddJwtConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJWTService, JWTService>();

            var chave = Encoding.ASCII.GetBytes(configuration.GetSection("JWT:Secret").Value);

            services.AddAuthentication(p =>
            {
                p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(p =>
            {
                p.RequireHttpsMetadata = false;
                p.SaveToken = true;
                p.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chave),
                    ValidateIssuer = true,
                    ValidIssuer = configuration.GetSection("JWT:Issuer").Value,
                    ValidateAudience = true,
                    ValidAudience = configuration.GetSection("JWT:Audience").Value,
                    ValidateLifetime = true
                };
            });
        }

        public static void UseJwtConfig(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
