using KanBanAuthpassthru.Authentication;
using KanBanAuthpassthru.Implementation;
using KanBanAuthpassthru.TypedClients;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Net.Http;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var securityScheme = 
    new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JSON Web Token based security",
    };

var securityReq = 
    new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
            {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
            }
            },
            new string[] {}
        }
    };

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtOptions = 
    new JWTOptions 
    {
        Issuer = "example.com",
        Audience = "example.com",
    };

builder.Services
    .AddAuthentication(o =>
    {
        o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

        o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
        o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {                 
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(jwtOptions.Key)
        };
    });

builder.Services
    .AddSwaggerGen(o =>
    {
        o.SwaggerDoc("v1", new OpenApiInfo { Title = "OpenAPI Title" });
        o.AddSecurityDefinition("Bearer", securityScheme);
        o.AddSecurityRequirement(securityReq);
    });

builder.Services
    .AddAuthorization();

builder.Services
    .AddHttpClient<ApiClient>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();

app.SetupAuthentication(jwtOptions, "/authentication/token");

app.UseGetPassThru(inputRoutePrefix: "api");
app.UsePostPassThru(inputRoutePrefix: "api");

app.Run();