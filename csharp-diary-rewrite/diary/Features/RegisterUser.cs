using System.ComponentModel.DataAnnotations;
using csharp_diary_rewrite.Model;
using Microsoft.AspNetCore.Identity;

namespace csharp_diary_rewrite.Features;

public record RegisterUserCommand(string Email, string Password); //TODO TGIS, can I make use of this?

public static class RegisterUserHandler
{
    public static async Task RegisterUser(RegisterUserCommand registerUserCommand, UserManager<DiaryUser> userManager)
    {
        Console.WriteLine("EXECUTED");
        var user = new DiaryUser
        {
            UserName = registerUserCommand.Email,
            Email = registerUserCommand.Email,
        };
        
        var result = await userManager.CreateAsync(user, registerUserCommand.Password);
        Console.WriteLine(result.Succeeded);
    }
}