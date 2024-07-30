using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using TonProof;
using TonProof.Demo.Swagger;
using TonProof.Demo.Types;
using TonLibDotNet;
using TonLibDotNet.Types;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.Configure<SettingOptions>(
    configuration.GetSection(SettingOptions.Tokens));

builder.Services.Configure<TonOptions>(o =>
{
    o.UseMainnet = false;
    o.LogTextLimit = 0; // Set to 0 to see full requests/responses
    o.VerbosityLevel = 0;
    o.Options.KeystoreType = new KeyStoreTypeDirectory("D:/Temp/keys");
});

builder.Services.Configure<TonProofOptions>(o =>
{
    o.ValidAuthTime = 24 * 60 * 60 * 30;
    o.AllowedDomains = ["winogarcia.github.io"];
});

builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

// Add services to the container.
builder.Services.AddSingleton<ITonClient, TonClient>();
builder.Services.AddSingleton<ITonProofService, TonProofService>();

var settingOptions = configuration.GetSection(SettingOptions.Tokens).Get<SettingOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = settingOptions.Jwt.Issuer,
            ValidAudience = settingOptions.Jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settingOptions.Jwt.SecretKey))
        };
    });

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/swagger/auth/swagger.json", "auth");
        s.SwaggerEndpoint("/swagger/account/swagger.json", "account");
        s.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseCors(
    b => b
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());

app.Run();