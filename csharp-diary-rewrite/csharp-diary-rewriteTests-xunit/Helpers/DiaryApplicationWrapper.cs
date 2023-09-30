using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using csharp_diary_rewrite.Features;
using csharp_diary_rewrite.Model;
using Newtonsoft.Json;

namespace csharp_diary_rewriteTests_xunit.Helpers;

//TODO TGIS, split this into 3 classes, one for user1 one for user 2 and one unauthenticated6
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
    public UserData UserSavedInDatabase1 { get; } = new UserData("testUserMail1@test.com", "testPw123");
    public UserData UserSavedInDatabase2 { get; } = new UserData("testUserMail2@test.com", "testPw123");

    public DiaryApplicationWrapper()
    {
        var customWebApplicationFactory = new CustomWebApplicationFactory();
        _httpClient = customWebApplicationFactory.CreateClient();
        _diaryDbContext = customWebApplicationFactory.diaryDbContext;

        //seed database with common test data
        RegisterUser(new RegisterUserCommand(UserSavedInDatabase1.Email, UserSavedInDatabase1.Password));
        RegisterUser(new RegisterUserCommand(UserSavedInDatabase2.Email, UserSavedInDatabase2.Password));
    }

    public HttpResponseMessage RegisterUser(RegisterUserCommand registerUserCommand)
    {
        return _httpClient.PostAsJsonAsync("/api/register", registerUserCommand).Result;
    }

    public HttpResponseMessage LoginUser(LoginUserCommand loginUserCommand)
    {
        //TODO TGIS, it may be more performant if I make this async, but while testing there did not seem to be any differencebut this was probably because all those methods were only used once -\_(o.o)_/-
        return _httpClient.PostAsJsonAsync("/api/login", loginUserCommand).Result;
    }

    public string RetrieveJwtFromRegisteredUser1()
    {
        return RetrieveJwtFromRegisteredUser(new LoginUserCommand(UserSavedInDatabase1.Email,
            UserSavedInDatabase1.Password));
    }

    public string RetrieveJwtFromRegisteredUser2()
    {
        return RetrieveJwtFromRegisteredUser(new LoginUserCommand(UserSavedInDatabase2.Email,
            UserSavedInDatabase2.Password));
    }

    public string RetrieveJwtFromRegisteredUser(LoginUserCommand loginUserCommand)
    {
        var response = LoginUser(loginUserCommand);
        return response.Content.ReadAsStringAsync().Result;
    }

    //get dbContext
    public DiaryDbContext GetDbContext()
    {
        return _diaryDbContext;
    }

    public HttpResponseMessage SaveEntry(SaveEntryCommand saveEntryCommand, string jwt = "")
    {
        var json = JsonConvert.SerializeObject(saveEntryCommand);
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/entry");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
        requestMessage.Content = data;

        return _httpClient.SendAsync(requestMessage).Result;
    }

    /**
     * Saves an entry using the testUser1 which was created in the constructor.
     */
    public HttpResponseMessage SaveEntryAsRegisteredUser1(SaveEntryCommand saveEntryCommand)
    {
        var jwt = RetrieveJwtFromRegisteredUser1();
        return SaveEntry(saveEntryCommand, jwt);
    }

    /**
     * Saves an entry using the testUser2 which was created in the constructor.
     */
    public HttpResponseMessage SaveEntryAsRegisteredUser2(SaveEntryCommand saveEntryCommand)
    {
        var jwt = RetrieveJwtFromRegisteredUser2();
        return SaveEntry(saveEntryCommand, jwt);
    }

    public HttpResponseMessage DeleteEntry(int testEntryId, string jwt = "")
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"/api/entry/{testEntryId}");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        return _httpClient.SendAsync(requestMessage).Result;
    }

    /**
     * Deletes an entry using the testUser which was created in the constructor.
     */
    public HttpResponseMessage DeleteEntryAsRegisteredUser(int testEntryId)
    {
        var jwt = RetrieveJwtFromRegisteredUser1();
        return DeleteEntry(testEntryId, jwt);
    }

    public HttpResponseMessage GetEntriesOverview(string jwt = "")
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"/api/entry");
        requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

        return _httpClient.SendAsync(requestMessage).Result;
    }
}