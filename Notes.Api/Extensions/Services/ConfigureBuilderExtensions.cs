using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Notes.Bll.Interfaces;
using Notes.Common.DTOs;
using System.Security.Claims;

namespace Notes.Api.Extensions.Services
{
    public class ConfigureBuilderExtensions
    {
        public static JwtBearerEvents ConfigureJwtBearerEvents(IServiceProvider serviceProvider)
        {
            return new JwtBearerEvents
            {
                OnTokenValidated = async context =>
                {
                    try
                    {
                        var claims = context.Principal?.Claims;
                        if (!claims?.Any() ?? true)
                        {
                            context.Fail("Missing NameIdentifier claim");
                            return;
                        }

                        var userStringId = claims!.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

                        using var scope = serviceProvider.CreateScope();
                        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                        //using var postgreDbContext = context.HttpContext.RequestServices.GetRequiredService<IDbContextFactory<NotesDbContext>>().CreateDbContext();

                        if (Guid.TryParse(userStringId, out var userId) && !(await userService.ExistUserAsync(userId)))
                        {
                            var userFullName = claims!.FirstOrDefault(f => f.Type == JwtRegisteredClaimNames.Name)?.Value ?? string.Empty;
                            var userEmail = claims!.FirstOrDefault(f => f.Type == ClaimTypes.Email)?.Value ?? string.Empty;
                            var userLogin = claims!.FirstOrDefault(f => f.Type == JwtRegisteredClaimNames.PreferredUsername)?.Value ?? string.Empty;

                            await userService.AddUserAsync(new UserDto
                            {
                                Id = userId,
                                Email = userEmail,
                                IsDelete = false,
                                FullName = userFullName,
                                Username = userLogin
                            });
                        }
                        return;

                    }
                    catch (Exception ex)
                    {
                        context.Fail("Authentication failed.  " + ex.Message);
                    }
                }
            };
        }
    }
}
