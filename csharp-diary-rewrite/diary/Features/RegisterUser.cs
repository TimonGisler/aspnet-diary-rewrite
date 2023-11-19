using System.ComponentModel.DataAnnotations;
using csharp_diary_rewrite.Model;
using Microsoft.AspNetCore.Identity;

namespace csharp_diary_rewrite.Features;

public record RegisterUserCommand(string Email, string Password);

public static class RegisterUserHandler
{
    public static async Task RegisterUser(RegisterUserCommand registerUserCommand, UserManager<DiaryUser> userManager)
    {
        var user = new DiaryUser
        {
            UserName = registerUserCommand.Email,
            Email = registerUserCommand.Email,
        };
        
        var result = await userManager.CreateAsync(user, registerUserCommand.Password);

    }
}