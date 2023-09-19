using csharp_diary_rewrite.Features;
using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class RegisterUserTest : IClassFixture<DiaryApplicationWrapper>
{
    private readonly DiaryApplicationWrapper _diaryApplicationWrapper;
    
    public RegisterUserTest(DiaryApplicationWrapper diaryApplicationWrapper)
    {
        this._diaryApplicationWrapper = diaryApplicationWrapper;
    }
    
    
    [Fact]
    public void register_user_saves_user_in_database()
    {
        const string mail = "test@test.com";
        const string password = "test123";
        var registerUserCommand = new RegisterUserCommand(mail, password);
        
        var response = _diaryApplicationWrapper.RegisterUser(registerUserCommand);
        
        response.EnsureSuccessStatusCode(); //test if the status code is a success code (200-299)
        var user = _diaryApplicationWrapper.GetDbContext().Users.FirstOrDefault(u => u.Email == mail);
        Assert.NotNull(user); //todo tgis, make this test non modifying https://learn.microsoft.com/en-us/ef/core/testing/testing-with-the-database#tests-which-modify-data
    }
}