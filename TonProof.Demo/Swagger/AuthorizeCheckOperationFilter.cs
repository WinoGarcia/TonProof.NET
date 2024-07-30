using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TonProof.Demo.Swagger;

public class AuthorizeCheckOperationFilter : IOperationFilter
***REMOVED***
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    ***REMOVED***
        var hasAuthorize = context.MethodInfo.DeclaringType!.GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>().Any() || context.MethodInfo.GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>().Any();

        if (hasAuthorize)
        ***REMOVED***
            operation.Responses.Add("401", new OpenApiResponse ***REMOVED*** Description = "Unauthorized" ***REMOVED***);
            operation.Responses.Add("403", new OpenApiResponse ***REMOVED*** Description = "Forbidden" ***REMOVED***);

            var securityRequirement = new OpenApiSecurityRequirement
            ***REMOVED***
                ***REMOVED***
                    new OpenApiSecurityScheme
                    ***REMOVED***
                        Reference = new OpenApiReference
                        ***REMOVED***
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    ***REMOVED***
                ***REMOVED***
                    Array.Empty<string>()
            ***REMOVED***
        ***REMOVED***;

            operation.Security = new List<OpenApiSecurityRequirement> ***REMOVED*** securityRequirement ***REMOVED***;
    ***REMOVED***
***REMOVED***
***REMOVED***