using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API_Versioning.DependencyInjection
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions swagger)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                swagger.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"Api Versioning - v{description.ApiVersion}" + (description.IsDeprecated ? " - ⚠️ DEPRECATED." : null),
                    Version = description.ApiVersion.ToString(),
                    Description = $"DOT NET Core 8.0 Web API -  v{description.ApiVersion}"
                });
            }
        }
    }
}
