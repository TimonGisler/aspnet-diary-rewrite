using System.Net.Http.Json;
using csharp_diary_rewrite.Features;
using csharp_diary_rewrite.Model;

namespace csharp_diary_rewriteTests_xunit.Helpers;

public record UserData(string Email, string Password); //user data for testing

/**
 * Provides a convenient api to interact with my application.
 *
 * Tests can use this class to make the correct api calls etc.
 */
public class DiaryApplicationWrapper
{
    private readonly HttpClient _httpClient;
    private readonly DiaryDbContext _diaryDbContext;

    //Data to seed in constructor
    private readonly UserData _userSavedInDatabase = new UserData("testUserMail@test.com", "testPw123");
    public DiaryApplicationWrapper()
    {
        var customWebApplicationFactory = new CustomWebApplicationFactory();
        _httpClient = customWebApplicationFactory.CreateClient();
        _diaryDbContext = customWebApplicationFactory.diaryDbContext;
        
        //seed database with common test data
        RegisterUser(new RegisterUserCommand(_userSavedInDatabase.Email, _userSavedInDatabase.Password));
        
    }

    public UserData UserSavedInDatabase => _userSavedInDatabase;

    public HttpResponseMessage RegisterUser(RegisterUserCommand registerUserCommand)
    {
        return _httpClient.PostAsJsonAsync("/api/register", registerUserCommand).Result;
    }

    public HttpResponseMessage LoginUser(LoginUserCommand loginUserCommand)
    {
        return _httpClient.PostAsJsonAsync("/api/login", loginUserCommand).Result;
    }

    //get dbContext
    public DiaryDbContext GetDbContext()
    {
        return _diaryDbContext;
    }

}