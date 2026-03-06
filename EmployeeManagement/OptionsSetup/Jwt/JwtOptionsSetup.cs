using EmployeeManagement.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace EmployeeManagement.API.OptionsSetup.Jwt
{
    /// <summary>
    /// The option pattern uses classes to provide strongly typed access to groups of related configurations.
    /// </summary>
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        //The jwt section is located in appsettings
        private const string SectionName = "Jwt";
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            //bind section 'Jwt' to options
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
