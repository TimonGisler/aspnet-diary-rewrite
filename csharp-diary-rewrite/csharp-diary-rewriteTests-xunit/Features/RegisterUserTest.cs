using System.Net.Http.Json;
using csharp_diary_rewrite;
using csharp_diary_rewrite.Features;
using csharp_diary_rewriteTests_xunit.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class RegisterUserTest : IClassFixture<CustomWebApplicationFactory>
{
    public RegisterUserTest(CustomWebApplicationFactory customWebApplicationFactory)
    {
        this.customWebApplicationFactory = customWebApplicationFactory;
    }
    
    CustomWebApplicationFactory customWebApplicationFactory;
    
    [Fact]
    public void register_user_saves_user_in_database()
    {
        var httpClient = customWebApplicationFactory.CreateClient();
        var mail = "test@test.com";
        var password = "test123";
        var registerUserCommand = new RegisterUserCommand(mail, password);
        
        var response = httpClient.PostAsJsonAsync("/api/register", registerUserCommand).Result;
        
        response.EnsureSuccessStatusCode(); //test if the status code is a success code (200-299)
    }
}