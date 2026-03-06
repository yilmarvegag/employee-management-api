using EmployeeManagement.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EmployeeManagement.API.OptionsSetup.Jwt
{
    /// <summary>
    /// Configures and post-processes JWT bearer authentication options using application-specific JWT settings.
    /// </summary>
    /// <remarks>This class is typically used to apply custom JWT configuration, such as issuer, audience,
    /// signing key, and token validation parameters, to the <see cref="JwtBearerOptions"/> after initial setup. It is
    /// intended for use with ASP.NET Core authentication infrastructure and is registered as an <see
    /// cref="IPostConfigureOptions{JwtBearerOptions}"/> implementation. Thread safety is ensured by the ASP.NET Core
    /// options pattern. Use this class when you need to centralize and enforce JWT validation settings across your
    /// application.</remarks>
    public class JwtBearerOptionsSetup : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly JwtOptions _jwtOptions;

        public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public void PostConfigure(string? name, JwtBearerOptions options)
        {
            //Identify who is considered a valid issuer for the token.
            options.TokenValidationParameters.ValidateIssuer = true;
            options.TokenValidationParameters.ValidIssuer = _jwtOptions.Issuer;

            //Must contain the intended audience(s) for which the token is intended,
            //and match the one indicated when the token was generated.
            options.TokenValidationParameters.ValidateAudience = true;
            options.TokenValidationParameters.ValidAudience = _jwtOptions.Audience;
            /*options.TokenValidationParameters.ValidAudiences = new List<string>() { _jwtOptions.Audience, "" };*/

            //Enter the secret key that was used to digitally sign the token.
            options.TokenValidationParameters.ValidateIssuerSigningKey = true;
            options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            Console.WriteLine($"[JwtBearerOptionsSetup] SecretKey: {_jwtOptions.SecretKey}");
            options.IncludeErrorDetails = true;

            //Validate token lifetime.
            options.TokenValidationParameters.ValidateLifetime = true;

            //There must not be any deviation of the clock from the token lifetime.
            options.TokenValidationParameters.ClockSkew = TimeSpan.Zero;

            //Events to handle authentication failures and token validation.
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<JwtBearerOptionsSetup>>();
                    logger.LogWarning("Token inválido: {Error}", context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    // Additional custom validation can be performed here if needed.
                    return Task.CompletedTask;
                }
            };

        }
    }
}
