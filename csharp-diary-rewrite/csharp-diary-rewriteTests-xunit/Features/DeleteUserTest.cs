using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class DeleteUserTest : IClassFixture<DiaryApplicationWrapper>
{
    
    private readonly DiaryApplicationWrapper _diaryApplicationWrapper;
    
    public DeleteUserTest(DiaryApplicationWrapper diaryApplicationWrapper)
    {
        _diaryApplicationWrapper = diaryApplicationWrapper;
    }
    
    [Fact]
    public void user_can_delete_his_account()
    {
        Assert.Fail("Not Implemented");
    }
    
    [Fact]
    public void user_cannot_delete_other_users_account()
    {
        Assert.Fail("Not Implemented");
    }
    
    [Fact]
    public void user_cannot_access_apis_after_deletion()
    {
        //the jwt should not work anymore after the accounts was deleted
        //(add some validation to check if the user for which this jwt was created still exists)
        //https://stackoverflow.com/questions/61371857/check-if-user-exists-in-asp-net-core-webapi-jwt-authentication
        Assert.Fail("Not Implemented");
    }
}