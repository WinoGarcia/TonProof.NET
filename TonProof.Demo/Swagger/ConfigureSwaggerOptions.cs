using System.Reflection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TonProof.Demo.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
***REMOVED***
    public void Configure(SwaggerGenOptions options)
    ***REMOVED***
        options.SwaggerDoc("auth", new OpenApiInfo ***REMOVED*** Title = "TonProof.Demo Auth", Version = "v1" ***REMOVED***);
        options.SwaggerDoc("account", new OpenApiInfo ***REMOVED*** Title = "TonProof.Demo account", Version = "v1" ***REMOVED***);

        options.OperationFilter<AuthorizeCheckOperationFilter>();
        
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        ***REMOVED***
            Type = SecuritySchemeType.Http,
            In = ParameterLocation.Header,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Name = "Authorization",
            Description = "Provide a valid token"
    ***REMOVED***);

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
    ***REMOVED***);

        var xmlFilename = $"***REMOVED***Assembly.GetExecutingAssembly().GetName().Name***REMOVED***.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
***REMOVED***
***REMOVED***