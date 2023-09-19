using System.Net.Http.Json;
using csharp_diary_rewrite.Features;
using csharp_diary_rewrite.Model;

namespace csharp_diary_rewriteTests_xunit.Helpers;

/**
 * Provides a convenient api to interact with my application.
 *
 * Tests can use this class to make the correct api calls etc.
 */
public class DiaryApplicationWrapper
{
    private readonly HttpClient _httpClient;
    private readonly DiaryDbContext _diaryDbContext;


    public DiaryApplicationWrapper()
    {
        var customWebApplicationFactory = new CustomWebApplicationFactory();
        _httpClient = customWebApplicationFactory.CreateClient();
        _diaryDbContext = customWebApplicationFactory.diaryDbContext;
    }

    //register user
    public HttpResponseMessage RegisterUser(RegisterUserCommand registerUserCommand)
    {
        return _httpClient.PostAsJsonAsync("/api/register", registerUserCommand).Result;
    }


    //get dbContext
    public DiaryDbContext GetDbContext()
    {
        return _diaryDbContext;
    }
}