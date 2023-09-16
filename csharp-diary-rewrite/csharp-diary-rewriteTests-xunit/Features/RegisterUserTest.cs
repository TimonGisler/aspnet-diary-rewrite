using csharp_diary_rewrite;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class RegisterUserTest
{
    [Fact]
    public void not_logged_in_user_cannot_save_anything()
    {
        var webFactory = new WebApplicationFactory<Program>();
        var httpClient = webFactory.CreateClient();

        var response = httpClient.GetAsync("/api/hello").Result;

        Assert.Equal("HELLO", response.Content.ReadAsStringAsync().Result);
    }
}