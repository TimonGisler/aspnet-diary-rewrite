using System.IdentityModel.Tokens.Jwt;
using csharp_diary_rewrite.Features;
using csharp_diary_rewriteTests_xunit.Helpers;
using Microsoft.IdentityModel.JsonWebTokens;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class LoginUserTest : IClassFixture<DiaryApplicationWrapper>
{
    private readonly DiaryApplicationWrapper _diaryApplicationWrapper;

    public LoginUserTest(DiaryApplicationWrapper diaryApplicationWrapper)
    {
        this._diaryApplicationWrapper = diaryApplicationWrapper;
    }


    [Fact]
    public async Task logging_in_with_valid_credentials_returns_jwt()
    {
        string jwt = _diaryApplicationWrapper.RetrieveJwtFromRegisteredUser1();

        //try and create a JwtSecurityToken from the jwt string --> if not possible it is not correct format
        var jwtTokenObject = new JwtSecurityToken(jwt);
        Assert.NotNull(jwtTokenObject);
    }
}