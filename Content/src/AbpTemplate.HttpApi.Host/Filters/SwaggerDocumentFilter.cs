using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace AbpTemplate.Filters
{
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var tags = new List<OpenApiTag>
            {
                new OpenApiTag {
                    Name = "Authentication",
                    Description = "Authentication",
                    ExternalDocs = new OpenApiExternalDocs { Description = "Authentication" }
                },
                new OpenApiTag {
                    Name = "Localization",
                    Description = "Localization",
                    ExternalDocs = new OpenApiExternalDocs { Description = "Localization" }
                }
            };

            swaggerDoc.Tags = tags.OrderBy(x => x.Name).ToList();

            var apis = context.ApiDescriptions.Where(x => x.RelativePath.Contains("abp"));
            if (apis.Any())
            {
                foreach (var item in apis)
                {
                    swaggerDoc.Paths.Remove("/" + item.RelativePath);
                }
            }
        }
    }
}