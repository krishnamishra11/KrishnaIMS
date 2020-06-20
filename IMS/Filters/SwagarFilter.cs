using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

using System.Collections.Generic;
using System.Web.Http.Description;

namespace IMS.Filters
{
    public class MyDocumentFilter : IDocumentFilter
    {


        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {

            swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = "IMSKrishna.azurewebsites.net" } };
        }
    }
}
