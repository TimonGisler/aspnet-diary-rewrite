using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using csharp_diary_rewrite.Model;
using csharp_diary_rewrite.Settings;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace csharp_diary_rewrite.Features;

public record LoginUserCommand(string Email, string Password);

public static class LoginUserHandler
{
    public static async Task<IResult> LoginUser(LoginUserCommand registerUserCommand, UserManager<DiaryUser> userManager)
    {
        var authenticatedUser = await FindUserWithCorrectMailAndPassword(registerUserCommand, userManager);
        return authenticatedUser != null ? Results.Ok(GenerateToken(authenticatedUser.Id, authenticatedUser.Email!)) : Results.BadRequest("Username or password is incorrect.");
    }

    private static string GenerateToken(string userId, string userEmail)
    {
        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.Email, userEmail),
        };
        
        var key = DiaryAppSettings.GetJwtKey();
        var signingAlgo = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            DiaryAppSettings.GetIssuer(),
            DiaryAppSettings.GetAudience(),
            claims,
            expires: DateTime.Now.AddDays(60),
            signingCredentials: signingAlgo);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /**
     * Finds and returns the User with a matchin email and password.
     * Otherwise returns null.
     */
    private static async Task<DiaryUser?> FindUserWithCorrectMailAndPassword(LoginUserCommand registerUserCommand, UserManager<DiaryUser> userManager)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(registerUserCommand.Email);
            var userExistsAndPasswordIsCorrect = user != null && await userManager.CheckPasswordAsync(user, registerUserCommand.Password);

            return userExistsAndPasswordIsCorrect ? user : null;
        }
        catch (Exception e)
        {
            return null;
        }

    }
}