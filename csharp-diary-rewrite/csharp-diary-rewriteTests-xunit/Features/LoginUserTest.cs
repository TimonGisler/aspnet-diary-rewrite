using System.IdentityModel.Tokens.Jwt;
using csharp_diary_rewrite.Features;
using csharp_diary_rewriteTests_xunit.Helpers;
using Microsoft.IdentityModel.JsonWebTokens;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

[Collection("Database collection")]
public class LoginUserTest
{
    private readonly DiaryApplicationClient _unauthenticatedDiaryApplicationClient;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser1;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser2;
    
    public LoginUserTest(TestApplicationFactory testApplicationFactory)
    {
        _unauthenticatedDiaryApplicationClient = testApplicationFactory.DiaryApplicationClientForUnauthenticatedUser;
        _diaryApplicationClientForUser1 = testApplicationFactory.DiaryApplicationClientForUser1;
        _diaryApplicationClientForUser2 = testApplicationFactory.DiaryApplicationClientForUser2;

    }


    [Fact]
    public void logging_in_with_valid_credentials_returns_jwt()
    {
         var jwt = _diaryApplicationClientForUser1.LoginUser();
        
         //try and create a JwtSecurityToken from the jwt string --> if not possible it is not correct format
         var jwtTokenObject =  new JwtSecurityToken(jwt);
         Assert.NotNull(jwtTokenObject);
    }
}