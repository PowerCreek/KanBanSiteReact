using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace KanBanAuthpassthru.Authentication
{
    public static class ImplementationExt
    {
        public static WebApplication SetupAuthentication(this WebApplication app, JWTOptions options, string endpoint)
        {
            app.MapPost(endpoint,
                [AllowAnonymous] async (AccessClass access) =>
                {
                    var result = await Task.FromResult(access);

                    if (result is not { }) return Results.BadRequest(403);

                        var issuer = options.Issuer;
                        var audience = options.Audience;
                        var securityKey = new SymmetricSecurityKey(options.Key);
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            issuer: issuer,
                            audience: audience,
                            signingCredentials: credentials);

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var stringToken = tokenHandler.WriteToken(token);
                        
                        return Results.Ok(stringToken);
                });

            return app;
        }
    }

    public class JWTOptions 
    { 
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public byte[] Key => Encoding.UTF8.GetBytes("keykeykeykeykeykeykeykeykeykeykey");
    }

    public class AccessClass
    {
        public string Data { get; set; }
    }
}
