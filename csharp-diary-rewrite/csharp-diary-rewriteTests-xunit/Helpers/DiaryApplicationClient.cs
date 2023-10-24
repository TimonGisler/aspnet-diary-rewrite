using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using csharp_diary_rewrite.Features;
using csharp_diary_rewrite.Model;
using Newtonsoft.Json;

namespace csharp_diary_rewriteTests_xunit.Helpers;

public record UserData(string Email, string Password);

/**
 * Provides a convenient api to interact with my application from the perspective of the user.
 *
 * Tests can use this class to make the correct api calls.
 */
public class DiaryApplicationClient
{
    private readonly HttpClient _httpClient;

    private readonly string? _defaultJwtToUse;
    
    public UserData? UserOfThisClient { get; private set; }


    public DiaryApplicationClient(HttpClient httpClient, UserData? userOfThisClient = null) //TODO TGIS; make all the requests etc non modifying
    {
        _httpClient = httpClient;
        UserOfThisClient = userOfThisClient;
        
        //if user is not null, then register and login this user and use the jwt for all requests
        if (userOfThisClient is null) return;
        RegisterUser(new RegisterUserCommand(userOfThisClient.Email, userOfThisClient.Password));
        _defaultJwtToUse = LoginUser(new LoginUserCommand(userOfThisClient.Email, userOfThisClient.Password));
    }

    public HttpResponseMessage RegisterUser(RegisterUserCommand registerUserCommand)
    {
        return _httpClient.PostAsJsonAsync("/api/register", registerUserCommand).Result;
    }

    public string LoginUser()
    {
        if (UserOfThisClient is null) throw new Exception("UserOfThisClient is null, cannot login");
        return LoginUser(new LoginUserCommand(UserOfThisClient.Email, UserOfThisClient.Password));
    }
    private string LoginUser(LoginUserCommand loginUserCommand)
    {
        //TODO TGIS, it may be more performant if I make this async, but while testing there did not seem to be any difference but this was probably because all those methods were only used once -\_(o.o)_/-
        return _httpClient.PostAsJsonAsync("/api/login", loginUserCommand).Result.Content.ReadAsStringAsync().Result;
    }

    public HttpResponseMessage SaveEntry(SaveEntryCommand saveEntryCommand)
    {
        var requestMessage = createRequestMessage(HttpMethod.Post, "/api/entry", saveEntryCommand);
        return _httpClient.SendAsync(requestMessage).Result;
    }

    public HttpResponseMessage DeleteEntry(int testEntryId)
    {
        var requestMessage = createRequestMessage(HttpMethod.Delete, $"/api/entry/{testEntryId}");
        return _httpClient.SendAsync(requestMessage).Result;
    }

    public HttpResponseMessage GetEntriesOverview()
    {
        var requestMessage = createRequestMessage(HttpMethod.Get, $"/api/entry");
        return _httpClient.SendAsync(requestMessage).Result;
    }
    
    public HttpResponseMessage DeleteUser()
    {
        var requestMessage = createRequestMessage(HttpMethod.Delete, $"/user");
        return _httpClient.SendAsync(requestMessage).Result;
    }


    /**
     * Convenience method to create the requestmessage.
     * Automatically adds the jwt to the request.
     * Automatically serializes the content to json and adds it to the request.
     */
    private HttpRequestMessage createRequestMessage(HttpMethod httpMethod, string url, object? content = null)
    {
        var requestMessage = new HttpRequestMessage(httpMethod, url);

        //add data if it exists
        if (content is not null)
        {
            var json = JsonConvert.SerializeObject(content);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            requestMessage.Content = data;
        }

        //add authentication if jwt exists
        if (_defaultJwtToUse is not null)
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _defaultJwtToUse);
        }

        return requestMessage;
    }
}