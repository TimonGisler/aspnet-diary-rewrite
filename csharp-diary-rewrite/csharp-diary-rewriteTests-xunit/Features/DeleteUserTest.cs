using csharp_diary_rewriteTests_xunit.Helpers;
using Xunit;

namespace csharp_diary_rewriteTests_xunit.Features;

public class DeleteUserTest : IClassFixture<DiaryApplicationWrapperFactory>
{
    
    private readonly DiaryApplicationClient _unauthenticatedDiaryApplicationClient;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser1;
    private readonly DiaryApplicationClient _diaryApplicationClientForUser2;
    
    private readonly DiaryApplicationClient _diaryApplicationClient;
    
    public DeleteUserTest(DiaryApplicationWrapperFactory diaryApplicationWrapperFactory)
    {
        _unauthenticatedDiaryApplicationClient = diaryApplicationWrapperFactory.DiaryApplicationClientForUnauthenticatedUser;
        _diaryApplicationClientForUser1 = diaryApplicationWrapperFactory.DiaryApplicationClientForUser1;
        _diaryApplicationClientForUser2 = diaryApplicationWrapperFactory.DiaryApplicationClientForUser2;

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