using System.IdentityModel.Tokens.Jwt;
using csharp_diary_rewrite.Features;
using csharp_diary_rewriteTests_xunit.Helpers;
using Microsoft.IdentityModel.JsonWebTokens;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class LoginUserTest : IClassFixture<DiaryApplicationWrapperFactory>
{
    private readonly DiaryApplicationClient _unauthenticatedDiaryApplicationClient;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser1;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser2;
    
    public LoginUserTest(DiaryApplicationWrapperFactory diaryApplicationWrapperFactory)
    {
        _unauthenticatedDiaryApplicationClient = diaryApplicationWrapperFactory.DiaryApplicationClientForUnauthenticatedUser;
        _diaryApplicationClientForUser1 = diaryApplicationWrapperFactory.DiaryApplicationClientForUser1;
        _diaryApplicationClientForUser2 = diaryApplicationWrapperFactory.DiaryApplicationClientForUser2;

    }


    [Fact]
    public void logging_in_with_valid_credentials_returns_jwt()
    {
        //TODO TGIS, what to do here?
        // string jwt = _diaryApplicationClient.RetrieveJwtFromRegisteredUser1();
        //
        // //try and create a JwtSecurityToken from the jwt string --> if not possible it is not correct format
        // var jwtTokenObject = _unauthenticatedDiaryApplicationClient.
        // Assert.NotNull(jwtTokenObject);
    }
}