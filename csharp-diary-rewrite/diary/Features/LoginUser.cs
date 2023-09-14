using System.ComponentModel.DataAnnotations;
using csharp_diary_rewrite.Model;
using Microsoft.AspNetCore.Identity;

namespace csharp_diary_rewrite.Features;

public record LoginUserCommand(string Email, string Password);

public static class LoginUserHandler
{
    public static async Task RegisterUser(LoginUserCommand registerUserCommand, UserManager<DiaryUser> userManager)
    {
        
    }
}