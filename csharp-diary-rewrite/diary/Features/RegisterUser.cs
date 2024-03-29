﻿using System.ComponentModel.DataAnnotations;
using csharp_diary_rewrite.Model;
using Microsoft.AspNetCore.Identity;

namespace csharp_diary_rewrite.Features;

public record RegisterUserCommand(string Email, string Password);

public static class RegisterUserHandler
{
    public static async Task<IResult> RegisterUser(RegisterUserCommand registerUserCommand, UserManager<DiaryUser> userManager)
    {
        var user = new DiaryUser
        {
            UserName = registerUserCommand.Email,
            Email = registerUserCommand.Email,
        };
        
        var result = await userManager.CreateAsync(user, registerUserCommand.Password);

        if (!result.Succeeded)
        {
            return Results.BadRequest("Could not create user: " + result.Errors.First().Description);
        }
        
        return Results.Ok();
    }
}