using csharp_diary_rewrite.Features;
using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class RegisterUserTest : IClassFixture<DiaryApplicationWrapperFactory>
{
    private readonly DiaryApplicationClient _unauthenticatedDiaryApplicationClient;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser1;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser2;
    
    private readonly DiaryApplicationClient _diaryApplicationClient;
    
    public RegisterUserTest(DiaryApplicationWrapperFactory diaryApplicationWrapperFactory)
    {
        _unauthenticatedDiaryApplicationClient = diaryApplicationWrapperFactory.DiaryApplicationClientForUnauthenticatedUser;
        _diaryApplicationClientForUser1 = diaryApplicationWrapperFactory.DiaryApplicationClientForUser1;
        _diaryApplicationClientForUser2 = diaryApplicationWrapperFactory.DiaryApplicationClientForUser2;

    }
    
    
    [Fact]
    public void register_user_saves_user_in_database()
    {
        const string mail = "test@test.com";
        const string password = "test123";
        var registerUserCommand = new RegisterUserCommand(mail, password);
        
        var response = _diaryApplicationClient.RegisterUser(registerUserCommand);
        
        response.EnsureSuccessStatusCode(); //test if the status code is a success code (200-299)
        var user = _diaryApplicationClient.GetDbContext().Users.FirstOrDefault(u => u.Email == mail);
        Assert.NotNull(user); //todo tgis, make this test non modifying https://learn.microsoft.com/en-us/ef/core/testing/testing-with-the-database#tests-which-modify-data
    }
}